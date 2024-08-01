using MedGearMart.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MedGearMart.Models.DataLayer.SeedData
{
    public class GearSeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gear>().HasData(
             // Medical Devices
                new Gear { GearId = 1, GearName = "Heart Monitor", Description = "Portable heart monitoring device.", Price = 250.00m, Stock = 10, ImageUrl = "heart_monitor.jpg", CategoryId = 1 },
                new Gear { GearId = 2, GearName = "Blood Pressure Cuff", Description = "Digital blood pressure cuff.", Price = 50.00m, Stock = 20, ImageUrl = "blood_pressure_cuff.jpg", CategoryId = 1 },
                new Gear { GearId = 3, GearName = "Thermometer", Description = "Infrared forehead thermometer.", Price = 20.00m, Stock = 50, ImageUrl = "thermometer.jpg", CategoryId = 1 },

                // Diagnostics
                new Gear { GearId = 4, GearName = "X-ray Machine", Description = "Digital X-ray imaging system.", Price = 5000.00m, Stock = 0, ImageUrl = "xray_machine.jpg", CategoryId = 2 },
                new Gear { GearId = 5, GearName = "Ultrasound Device", Description = "Portable ultrasound scanner.", Price = 3000.00m, Stock = 8, ImageUrl = "ultrasound_device.jpg", CategoryId = 2 },
                new Gear { GearId = 6, GearName = "Blood Test Kit", Description = "Comprehensive blood test kit.", Price = 100.00m, Stock = 100, ImageUrl = "blood_test_kit.jpg", CategoryId = 2 },

                // Surgical Instruments
                new Gear { GearId = 7, GearName = "Scalpel", Description = "High-precision surgical scalpel.", Price = 15.00m, Stock = 200, ImageUrl = "scalpel.jpg", CategoryId = 3 },
                new Gear { GearId = 8, GearName = "Forceps", Description = "Stainless steel surgical forceps.", Price = 25.00m, Stock = 150, ImageUrl = "forceps.jpg", CategoryId = 3 },
                new Gear { GearId = 9, GearName = "Surgical Scissors", Description = "Sharp surgical scissors.", Price = 30.00m, Stock = 100, ImageUrl = "surgical_scissors.jpg", CategoryId = 3 },

                // Patient Care
                new Gear { GearId = 10, GearName = "Wheelchair", Description = "Comfortable and durable wheelchair.", Price = 200.00m, Stock = 15, ImageUrl = "wheelchair.jpg", CategoryId = 4 },
                new Gear { GearId = 11, GearName = "Hospital Bed", Description = "Adjustable hospital bed with mattress.", Price = 1500.00m, Stock = 10, ImageUrl = "hospital_bed.jpg", CategoryId = 4 },
                new Gear { GearId = 12, GearName = "Oxygen Tank", Description = "Portable oxygen tank with mask.", Price = 300.00m, Stock = 30, ImageUrl = "oxygen_tank.jpg", CategoryId = 4 },

                // Personal Protective Equipment
                new Gear { GearId = 13, GearName = "Face Mask", Description = "N95 respiratory face mask.", Price = 5.00m, Stock = 500, ImageUrl = "face_mask.jpg", CategoryId = 5 },
                new Gear { GearId = 14, GearName = "Gloves", Description = "Disposable nitrile gloves.", Price = 10.00m, Stock = 1000, ImageUrl = "gloves.jpg", CategoryId = 5 },
                new Gear { GearId = 15, GearName = "Protective Goggles", Description = "Anti-fog protective goggles.", Price = 20.00m, Stock = 300, ImageUrl = "protective_goggles.jpg", CategoryId = 5 }
                );

        }
    }
}
