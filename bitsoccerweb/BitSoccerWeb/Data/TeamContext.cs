using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitSoccerWeb.Models.HomeViewModels;
using Microsoft.EntityFrameworkCore;

namespace BitSoccerWeb.Data
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options)
        {
            
        }
        public DbSet<DropDownViewModel> dropDown { get; set; }
    }
}
