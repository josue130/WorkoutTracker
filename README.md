# Workout API Documentation

## Project Description
This project involves creating a backend system for a workout tracker application where users can sign up, log in, create workout plans, and track their progress. The system will feature JWT authentication, CRUD operations for workouts, and generate reports on past workouts.

## Prerequisites
- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQL Server

## How to Run
1. Clone the repository.
2. Set up the connection string in `appsettings.json`.
3. Run the project.

## Usage

### Auth
- **POST**  `/login`
  Log in a user and return a JWT.
- **POST** `/register`
  Register a new user.
### Exercise
- **GET** `/exercises`
  Retrieve all exercises.
- **GET** `/exercises/{exercise_id}`
  Retrieves a specific  exercise.
### Workout
- **GET** `/workouts/{workout_id}`
  Retrieve a workout.
- **GET** `/workouts`
  List all workout plans for the current user.
- **GET** `/workouts/report`
  Generate reports on past workouts.
- **POST** `/workouts`
  Create a new workout.
- **PUT** `/workouts`
  Update a workout.
- **DELETE** `/workouts/{workout_id}`
  Delete a workout.
### Scheduled Workout
- **GET** `/schedule-workouts`
  List future scheduled workouts.
- **GET** `/schedule-workouts/{schedule_workout_id}`
  Retrieve a scheduled workout.
- **POST** `/schedule-workouts`
  Schedule a workout.
- **PUT** `/schedule-workouts`
  Update a scheduled workout.
- **DELETE** `/schedule-workouts/{schedule_workout_id}`
  Delete a scheduled workout.
### Workout comments
- **GET** `/workout-comments/{workout_id}`
  Retrieve all comments from a workout.
- **POST** `/workout-comments`
  Add a comment to a workout.
- **PUT** `/WorkoutComments`
  Update a comment. 
- **DELETE** `/workout-comments/{workout_comment_id}`
  Delete a comment.
### Workout exercise
- **GET** `/workout-exercises/{workout_id}`
  Retrieve all exercises from a workout.
- **POST** `/workout-exercises`
  Add an exercise to a workout.
- **PUT** `/workout-exercises`
  Update an exercise from a workout. 
- **DELETE** `/workout-exercises/{workou_exercise_id}`
  Delete an exercise from a workout. 
## Useful Links
- [Project detail](https://roadmap.sh/projects/fitness-workout-tracker)
