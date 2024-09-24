using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkoutApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataToExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "exercises",
                columns: new[] { "Id", "Category", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Strength", "An exercise that targets the chest, shoulders, and triceps.", "Push-up" },
                    { 2, "Strength", "A lower body exercise that targets the thighs and glutes.", "Squat" },
                    { 3, "Strength", "An upper body exercise that works the back and biceps.", "Pull-up" },
                    { 4, "Core", "A core exercise that targets the abdominals and lower back.", "Plank" },
                    { 5, "Strength", "A lower body exercise that works the legs and glutes.", "Lunge" },
                    { 6, "Strength", "An exercise that focuses on the biceps using weights.", "Bicep Curl" },
                    { 7, "Strength", "A strength exercise that targets the entire body, especially the back and legs.", "Deadlift" },
                    { 8, "Strength", "A chest exercise performed with a barbell or dumbbells.", "Bench Press" },
                    { 9, "Core", "An abdominal exercise that targets the upper abs.", "Crunch" },
                    { 10, "Cardio", "A full-body exercise that combines a squat, push-up, and jump.", "Burpee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "exercises",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
