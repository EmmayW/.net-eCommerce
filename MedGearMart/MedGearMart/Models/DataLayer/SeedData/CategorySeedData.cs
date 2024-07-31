using MedGearMart.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MedGearMart.Models.DataLayer.SeedData
{
    public class CategorySeedData
    {

        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Medical Devices",
                    CategoryDescription = "Equipment used for medical purposes."
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Diagnostics",
                    CategoryDescription = "Tools and devices for medical diagnostics."
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Surgical Instruments",
                    CategoryDescription = "Instruments used in surgical procedures."
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Patient Care",
                    CategoryDescription = "Products related to patient care and comfort."
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Personal Protective Equipment",
                    CategoryDescription = "Protective gear for medical professionals."
                }
            );


        }

    }
}
