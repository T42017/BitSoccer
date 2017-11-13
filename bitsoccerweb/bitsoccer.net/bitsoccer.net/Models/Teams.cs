using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bitsoccer.net.Models
{
    public class Teams
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TeamsId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool Private { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
    }
}