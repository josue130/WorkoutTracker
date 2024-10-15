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
- **POST** `auth/login`
  - **Description**: Authenticates a user  and returns a JWT token.
  - **Request Body**:
    ```json
    {
      "userName": "user@example.com",
      "password": "pas5word-*123"
    }
    ```
  - **Response**: JWT token on success.
    
- **POST** `auth/register`
  - **Description**: Registers a new user in the system.
  - **Request Body**:
    ```json
    {
      "userName": "string",
      "email": "string",
      "fullName": "string",
      "password": "string"
    }
    ```
### Exercise
- **GET** `exercises`
    -  **Description** : Retrieve all exercises.
    -  **Authentication**: Required.
    
  
### Workout
- **GET** `/workouts/{workout_id}`
    - **Description** : Retrieve general information about a workout..
    -  **Authentication**: Required.
  
- **GET** `/workouts`
    -  **Description** : Retrieve all workouts **that are already scheduled** .
    -  **Authentication**: Require.
      
- **POST** `/workouts`
  - **Description**: Create a new workout.
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "name": "string",
      "description": "string"
    }
    ```
    
- **PUT** `/workouts`
  - **Description**: Update a workout.
  - **Authentication**: Required. 
  - **Request Body**:
    ```json
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "description": "string"
    }
    ```
  
- **DELETE** `/workouts/{workout_id}`
  - **Description**: Delete a workout.
  - **Authentication**: Required. 

### Scheduled Workout
- **GET** `/workout-schedules`
  - **Description**: Retrieve a list of workouts scheduled for the future.
  - **Authentication**: Required. 
  
- **POST** `/workout-schedules`
  - **Description**: Schedule a workout.
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "scheduledDate": "2024-10-15T02:25:00.954Z",
      "workoutId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
    ```
  
- **PUT** `/schedule-workouts`
  - **Description**: Update a scheduled workout..
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "scheduledDate": "2024-10-15T02:25:00.954Z",
      "workoutId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
    ```
  
- **DELETE** `/workout-schedules/{schedule_workout_id}`
  - **Description**: Delete a scheduled workout.
  - **Authentication**: Required. 
  
### Workout comments
- **GET** `/workout-comments/{workout_id}`
  - **Description**: Retrieve all comments from a workout.
  - **Authentication**: Required. 
  
- **POST** `/workout-comments`
  - **Description**: Add a comment to a workout.
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "workoutId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "comment": "string"
    }
    ```
  
- **PUT** `/WorkoutComments`
  - **Description**: Update a comment. 
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "workoutId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "comment": "string"
    }
    ```
    
- **DELETE** `/workout-comments/{workout_comment_id}`
  - **Description**: Delete a comment.
  - **Authentication**: Required.
  
### Workout exercise
- **GET** `/workout-exercises/{workout_id}`
  - **Description**: Retrieve all exercises from a workout.
  - **Authentication**: Required. 
  
- **POST** `/workout-exercises`
  - **Description**: Add an exercise to a workout.
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "sets": 5,
      "repetitions": 5,
      "weight": 6,
      "exerciseId": 7,
      "workoutId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
    ```
  
- **PUT** `/workout-exercises`
  - **Description**: Update an exercise from a workout. 
  - **Authentication**: Required.
  - **Request Body**:
    ```json
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "sets": 5,
      "repetitions": 5,
      "weight": 6,
      "exerciseId": 7,
      "workoutId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
    ```
  
- **DELETE** `/workout-exercises/{workou_exercise_id}`
  - **Description**: Delete an exercise from a workout. 
  - **Authentication**: Required.
    
### Report
- **GET** `/reports`
  - **Description**: Retrieve a report of the recent workouts.
  - **Authentication**: Required.
    
## Useful Links
- [Project detail](https://roadmap.sh/projects/fitness-workout-tracker)

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
