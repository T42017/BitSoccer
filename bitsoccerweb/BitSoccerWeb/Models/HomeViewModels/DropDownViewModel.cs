using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BitSoccerWeb.Models.HomeViewModels
{
    public class DropDownViewModel
    {
        public List<SelectListItem> Teams { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "T1", Text = "TeamOne" },
            new SelectListItem { Value = "T2", Text = "TeamTwo" },
            new SelectListItem { Value = "TPro", Text = "ProTeam" },
        };

    }
}
