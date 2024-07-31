using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedGearMart.Migrations
{
    public partial class addseeddatacg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Equipment used for medical purposes.", "Medical Devices" },
                    { 2, "Tools and devices for medical diagnostics.", "Diagnostics" },
                    { 3, "Instruments used in surgical procedures.", "Surgical Instruments" },
                    { 4, "Products related to patient care and comfort.", "Patient Care" },
                    { 5, "Protective gear for medical professionals.", "Personal Protective Equipment" }
                });

            migrationBuilder.InsertData(
                table: "Gears",
                columns: new[] { "GearId", "CategoryId", "Description", "GearName", "ImageUrl", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Portable heart monitoring device.", "Heart Monitor", "heart_monitor.jpg", 250.00m, 10 },
                    { 2, 1, "Digital blood pressure cuff.", "Blood Pressure Cuff", "blood_pressure_cuff.jpg", 50.00m, 20 },
                    { 3, 1, "Infrared forehead thermometer.", "Thermometer", "thermometer.jpg", 20.00m, 50 },
                    { 4, 2, "Digital X-ray imaging system.", "X-ray Machine", "xray_machine.jpg", 5000.00m, 5 },
                    { 5, 2, "Portable ultrasound scanner.", "Ultrasound Device", "ultrasound_device.jpg", 3000.00m, 8 },
                    { 6, 2, "Comprehensive blood test kit.", "Blood Test Kit", "blood_test_kit.jpg", 100.00m, 100 },
                    { 7, 3, "High-precision surgical scalpel.", "Scalpel", "scalpel.jpg", 15.00m, 200 },
                    { 8, 3, "Stainless steel surgical forceps.", "Forceps", "forceps.jpg", 25.00m, 150 },
                    { 9, 3, "Sharp surgical scissors.", "Surgical Scissors", "surgical_scissors.jpg", 30.00m, 100 },
                    { 10, 4, "Comfortable and durable wheelchair.", "Wheelchair", "wheelchair.jpg", 200.00m, 15 },
                    { 11, 4, "Adjustable hospital bed with mattress.", "Hospital Bed", "hospital_bed.jpg", 1500.00m, 10 },
                    { 12, 4, "Portable oxygen tank with mask.", "Oxygen Tank", "oxygen_tank.jpg", 300.00m, 30 },
                    { 13, 5, "N95 respiratory face mask.", "Face Mask", "face_mask.jpg", 5.00m, 500 },
                    { 14, 5, "Disposable nitrile gloves.", "Gloves", "gloves.jpg", 10.00m, 1000 },
                    { 15, 5, "Anti-fog protective goggles.", "Protective Goggles", "protective_goggles.jpg", 20.00m, 300 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Gears",
                keyColumn: "GearId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);
        }
    }
}
