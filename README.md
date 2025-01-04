
# BrainZap: Interactive Quiz Application

Sharpen your knowledge, challenge your friends, and climb the leaderboard with **💡BrainZap** – an engaging platform that combines fun, competition, and learning.

![Welcome Page Screenshot](./readme-img/welcome-page.png)

## Overview

BrainZap is a full-stack web application designed to provide an interactive quiz experience for users. Users can take quizzes on various topics, compete with others on leaderboards, and learn in an enjoyable manner. Admins have advanced features to manage users and quizzes efficiently.

## Features

### User Features
- **Interactive Quizzes:** Participate in quizzes across multiple categories, such as Science, History, Geography, and Technology.
- **Leaderboards:** Compete with others and track your scores on the leaderboard.
- **Personalized Quiz Results:** Review detailed feedback and correct answers for completed quizzes.
- **Authentication & Authorization:** Secure login and role-based access using JSON Web Tokens (JWT).

### Admin Features
- **User Management:** Add, edit, or delete users in the system.
- **Quiz Management:** Create, modify, and remove quizzes, including their questions and answers.
- **Authorization:** Admin-only access to user and quiz management functionalities.

### Additional Features
- **Responsive UI:** A modern and responsive interface built with Angular Material and Tailwind CSS.
- **Swagger API Documentation:** Comprehensive API documentation for developers.
- **Secure Operations:** Features like secure password hashing with BCrypt and proper data validations.

## Technologies Used

### Frontend
- **Angular 18:** For building the dynamic and reactive user interface.
- **Angular Material, Bootstrap 5 & Tailwind CSS:** For elegant and responsive designs.
- **RxJS:** Facilitates reactive programming and observable-based state management.
- **JWT Decode:** Decodes and manages JSON Web Tokens on the client side.
- **RxJS:** Facilitates reactive programming and observable-based state management.
- **JWT Decode:** Decodes and manages JSON Web Tokens on the client side.

### Backend
- **.NET 8 Web API:** High-performance API for quiz and user management.
- **Entity Framework Core:** Object-relational mapping for SQL Server database operations.
- **SQL Server:** Robust database system to manage data.
- **BCrypt:** Ensures secure password hashing.
- **Newtonsoft.Json:** Handles JSON serialization and deserialization.
- **Swashbuckle (Swagger):** Provides interactive API documentation for testing and exploration.
- **Serilog:** Enables advanced logging to the console and files for better debugging and monitoring.

### Additional Tools
- **Swagger Documentation:** A developer-friendly interface to test and explore the API (as shown below).

![Swagger API Screenshot1](./readme-img/swagger-quiz.png)
![Swagger API Screenshot2](./readme-img/swagger-user.png)

## Getting Started

### Prerequisites
- **Node.js** for running the Angular frontend.
- **.NET SDK** for the backend API.
- **SQL Server** for the database.

### Installation
1. Clone the repository:
   
   git clone https://github.com/your-repository.git

2. Navigate to the backend folder and restore dependencies:
   
   cd Backend
   dotnet restore
   
3. Navigate to the frontend folder and install dependencies:
 
   cd Frontend
   npm install
   
4. Configure the database connection string in `appsettings.json`.

### Running the Application
1. Start the backend server:
   
   dotnet run
   
2. Start the Angular frontend:
   
   ng serve
   
3. Access the application in your browser at `http://localhost:4200`.

## Screenshots

### Landing Page
![Welcome Page Screenshot](./readme-img/welcome-page.png)

### Home Page
![Home Page Screenshot](./readme-img/home-page.png)

### Quiz
![Quiz Screenshot1](./readme-img/confirm-dialog.png)
![Quiz Screenshot2](./readme-img/food-quiz.png)
![Quiz Screenshot3](./readme-img/submit-quiz.png)
![Quiz Screenshot4](./readme-img/quiz-results.png)
![Quiz Screenshot5](./readme-img/quiz-results2.png)

### Leaderboard
![Leaderboard Screenshot](./readme-img/leaderboards.png)

### User Management (Admin) with confirm dialog messages
![User Management Screenshot1](./readme-img/user-management.png)
![User Management Screenshot2](./readme-img/add-new-user.png)
![User Management Screenshot3](./readme-img/edit-user.png)
![User Management Screenshot3](./readme-img/user-update-success.png)
![User Management Screenshot4](./readme-img/delete-user.png)
![User Management Screenshot4](./readme-img/delete-user-success.png)

### Quiz Management (Admin) with confirm dialog messages
![Quiz Management Screenshot1](./readme-img/quiz-management.png)
![Quiz Management Screenshot1](./readme-img/add-quiz.png)
![Quiz Management Screenshot1](./readme-img/edit-quiz.png)
![Quiz Management Screenshot1](./readme-img/delete-quiz.png)

### Serilog Backend Logging
![Serilog Screenshot](./readme-img/serilog.png)

