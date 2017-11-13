using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using bitsoccer.net.HelpClasses;
using bitsoccer.net.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace bitsoccer.net.Controllers
{
    public class GetStartedController : Controller
    {
        // GET: GetStarted
        public ActionResult GetStarted()
        {
            ViewBag.Title = "Get Started";
            
            return View("~/Views/GetStarted/GetStarted.cshtml", GetCheckBoxInitialModel());
        }

        /// <summary>
        /// for get all CheckBox
        /// </summary>
        [HttpGet]
        public ActionResult CheckboxIndex()
        {
            return View(GetCheckBoxInitialModel());
        }

        /// <summary>
        /// for post user selected CheckBox
        /// </summary>
        [HttpPost]
        public ActionResult CheckboxIndex(PostedCheckBoxes postedCheckBoxes)
        {
            return View(GetCheckBoxModel(postedCheckBoxes));
        }

        /// <summary>
        /// for setup view model, after post the user selected CheckBox data
        /// </summary>
        private CheckBoxViewModel GetCheckBoxModel(PostedCheckBoxes postedCheckBoxes)
        {
            // setup properties
            var model = new CheckBoxViewModel();
            var selectedCheckBox = new List<CheckBox>();
            var postedCheckBoxIds = new string[0];
            if (postedCheckBoxes == null) postedCheckBoxes = new PostedCheckBoxes();

            // if a view model array of posted CheckBox ids exists
            // and is not empty,save selected ids
            if (postedCheckBoxes.CheckBoxIds != null && postedCheckBoxes.CheckBoxIds.Any())
            {
                postedCheckBoxIds = postedCheckBoxes.CheckBoxIds;
            }

            // if there are any selected ids saved, create a list of CheckBox
            if (postedCheckBoxIds.Any())
            {
                selectedCheckBox = ListCheckboxes()
                 .Where(x => postedCheckBoxIds.Any(s => x.Id.ToString().Equals(s)))
                 .ToList();
            }

            //setup a view model
            model.AvailableCheckBoxes = ListCheckboxes();
            model.SelectedCheckBoxes = selectedCheckBox;
            model.PostedCheckBoxes = postedCheckBoxes;

            // Generates xml
            SaveMatch(postedCheckBoxes.CheckBoxIds);

            return model;
        }

        /// <summary>
        /// for setup initial view model for all CheckBox
        /// </summary>
        private CheckBoxViewModel GetCheckBoxInitialModel()
        {
            //setup properties
            var model = new CheckBoxViewModel();
            var selectedCheckBox = new List<CheckBox>();

            //setup a view model
            model.AvailableCheckBoxes = ListCheckboxes();
            model.SelectedCheckBoxes = selectedCheckBox;
            model.PostedCheckBoxes = new PostedCheckBoxes();
            return model;
        }

        private IEnumerable<CheckBox> ListCheckboxes()
        {
            var list = new List<CheckBox>();


            foreach (var applicationUser in UserManager.Users)
            {
                foreach (var applicationUserTeam in applicationUser.Teams)
                {
                    list.Add(new CheckBox() { Name = applicationUserTeam.Name, Path = applicationUserTeam.FilePath });

                }
            }
            return list;
        }

        public ActionResult Upload()
        {
            return GetStarted();
        }

        public ActionResult SaveMatch(string[] paths)
        {
            if (paths != null && paths.Length == 2)
            {
                var task = UserManager.FindByIdAsync(User.Identity.GetUserId());
                var user = task.Result;

                var targetPath = Server.MapPath("~/App_data/matches/" + user.UserName + "/");

                Directory.CreateDirectory(targetPath);

                var filePath = Path.Combine(targetPath, "match.xml");

                Player.PlayMatch(paths[0], paths[1], filePath);

                ViewBag.Path = filePath;
            }
            else
            {
                ViewBag.Message = "Team selection failed!";
            }

            return GetStarted();
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)    // validate CSRF token
            {
                //return View("~/Views/GetStarted/GetStarted.cshtml");
                return GetStarted();
            }
            
            if (file != null && file.ContentLength > 0)
                try
                {
                    if (file.FileName.EndsWith(".dll"))
                    {
                        var task = UserManager.FindByIdAsync(User.Identity.GetUserId());
                        var user = task.Result;

                        var fileName = Path.GetFileName(file.FileName);

                        var targetPath = Server.MapPath("~/App_data/uploads/" + user.UserName + "/");

                        Directory.CreateDirectory(targetPath);  // create directory if not exists, otherwise ignore.

                        var path = Path.Combine(targetPath, fileName);
                        var name = fileName.TrimEnd(".dll".ToCharArray());

                        if (System.IO.File.Exists(path) || user.Teams.Any(t => t.Name == name))
                            throw new Exception("Team already exists!");
                        else
                        {
                            user.Teams.Add(new Teams
                            {
                                Name = name,
                                FilePath = path,
                                Private = false
                            });

                            await UserManager.UpdateAsync(user);
                            
                            file.SaveAs(path);

                            ViewBag.Message = "File (" + fileName + ") has been uploaded!";
                        }
                    }
                    else
                        throw new Exception("File is not a .DLL!");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR: " + ex.Message.ToString();
                }
            else
                ViewBag.Message = "You have not specified a file.";
            
            return GetStarted();
            //return PartialView("~/Views/GetStarted/GetStarted.cshtml");
        }
    }
}