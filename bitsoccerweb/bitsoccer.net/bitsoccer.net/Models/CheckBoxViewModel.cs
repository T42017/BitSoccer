using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitsoccer.net.Models
{
    public class CheckBoxViewModel
    {
        public IEnumerable<CheckBox> AvailableCheckBoxes { get; set; }
        public IEnumerable<CheckBox> SelectedCheckBoxes { get; set; }
        public PostedCheckBoxes PostedCheckBoxes { get; set; }
    }
}