using Clinic.Common.Authentication;
using Clinic.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Context
{
    public class MeetingsContext : IdentityDbContext<ApplicationUser>
    {

        public MeetingsContext(DbContextOptions<MeetingsContext> context)
           : base(context)
        {
        }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<ConversationRef> Conv { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }




    }
}
