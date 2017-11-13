using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitsoccer.net.Models
{
    public class PostedCheckBoxes
    {
        //this array will be used to POST values from the form to the controller
        public string[] CheckBoxIds { get; set; }
    }
}