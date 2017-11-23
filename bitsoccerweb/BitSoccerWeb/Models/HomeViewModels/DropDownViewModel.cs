using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BitSoccerWeb.Models.HomeViewModels
{
    public class DropDownViewModel
    {
        public string TeamCode { get; set; }
        public SelectList TeamList { get; set; }
    }
}
