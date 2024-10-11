using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workout.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "workoutPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workoutPlans_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scheduleWorkouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheduleWorkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_scheduleWorkouts_workoutPlans_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "workoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workoutComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workoutComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workoutComments_workoutPlans_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "workoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workoutExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workoutExercises_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workoutExercises_workoutPlans_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "workoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_scheduleWorkouts_WorkoutId",
                table: "scheduleWorkouts",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_workoutComments_WorkoutId",
                table: "workoutComments",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_workoutExercises_ExerciseId",
                table: "workoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_workoutExercises_WorkoutId",
                table: "workoutExercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_workoutPlans_UserId",
                table: "workoutPlans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scheduleWorkouts");

            migrationBuilder.DropTable(
                name: "workoutComments");

            migrationBuilder.DropTable(
                name: "workoutExercises");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "workoutPlans");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
