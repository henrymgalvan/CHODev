using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using cho500.Entity;

namespace cho500.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("cho505", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Person> Patient { get; set; }

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Physician> Physicians { get; set; }
        public DbSet<BloodType> BloodType { get; set; }


        public DbSet<ChildHealthRecord> ChildHealthRecord { get; set; }
        public DbSet<ChildBirthFollowUpVisit> ChildBirthFollowUpVisits { get; set; }
        public DbSet<HouseHoldClassificationPerVisit> Classifications { get; set; }
        public DbSet<HouseholdProfile> HouseholdProfiles { get; set; }
        public DbSet<ChildImmunizationRecord> ChildImmunizationRecords { get; set; }

        public System.Data.Entity.DbSet<cho500.Entity.HouseholdMember> HouseholdMembers { get; set; }
    }
}