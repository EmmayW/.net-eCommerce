
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MedGearMart.Models.DomainModel;

namespace MedGearMart.Models.DataLayer
{
    public class MedGearMartDbContext : IdentityDbContext<AppUser>
    {
        public MedGearMartDbContext(DbContextOptions<MedGearMartDbContext> options) : base(options)
        {


        }
    }
}
