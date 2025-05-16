# Pet Management Application with AI Integration

## Overview

This is a comprehensive pet management application with integrated AI capabilities, designed to help users manage their pets, appointments, and access AI-powered assistance. The application features both a user portal and an admin dashboard.

## Features

### User Features

- **User Authentication**: Secure login and registration system
- **Pet Management**: Add, update, and delete pet profiles
- **AI Chatbot**: General AI assistance for pet-related questions
- **AI Pet Bio Generator**: Creates customized pet biographies based on user input
- **Appointment Scheduler**: Manage pet appointments and track completed appointments
- **FAQ Access**: View frequently asked questions and answers
- **Resources**: Access to organization documents and resources

### Admin Features

- **User Management**: Add, update, and delete user accounts
- **FAQ Management**: Manage FAQ entries
- **Administrative Controls**: Advanced system management

## Technologies Used

### Frontend

- Blazor WebAssembly
- Bootstrap
- JavaScript/jQuery
- FullCalendar.js

### Backend

- ASP.NET Core 8.0
- Entity Framework Core
- SQLite Database
- RESTful API architecture

### External Services

- OpenAI API for AI chat functionality
- Email notifications using SMTP

## Installation & Setup

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or Visual Studio Code
- Git

### Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/Cesar6060/AA-CHATBOT.git
   cd AA-CHATBOT
   ```

2. **Set up environment variables**

   - Create a `.env` file in the `AAU BE` directory with your OpenAI API key:
     ```
     OPENAI__APIKEY=your_openai_api_key
     ```

3. **Restore dependencies**

   ```bash
   dotnet restore
   ```

4. **Run the backend**

   ```bash
   cd "AAU BE"
   dotnet run
   ```

5. **Run the frontend**

   ```bash
   cd ../AAU/AAU
   dotnet run
   ```

6. Access the application at:
   - Frontend: http://localhost:5180
   - Backend API: http://localhost:5052

## Project Structure

### Backend (`AAU BE`)

- **Controllers/**: API endpoints for authentication, pets, appointments, FAQ, and AI
- **Database/**: Database configuration, queries and SQLite integration
- **Models/**: Data models for application entities
- **Services/**: Business logic services

### Frontend (`AAU/AAU`)

- **Components/**: Blazor components for UI
- **Models/**: Client-side data models
- **Services/**: Client-side services for API communication
- **wwwroot/**: Static assets and client-side JavaScript

## API Endpoints

### Authentication

- POST `/user/login`: User login
- POST `/user/register`: User registration

### Pet Management

- GET `/pet/{id}`: Get all pets for a user
- POST `/pet`: Create a new pet
- PATCH `/pet/{id}`: Update a pet
- DELETE `/pet/delete/{id}`: Delete a pet

### Appointments

- GET `/appointments/{id}`: Get all appointments for a user
- POST `/appointments`: Create a new appointment
- PATCH `/appointments/{id}`: Update an appointment
- DELETE `/appointments/delete/{id}`: Delete an appointment

### AI Integration

- POST `/AI`: Send a message to the AI chatbot
- POST `/AI/generatebio`: Generate a pet biography

### FAQ

- GET `/faq/all`: Get all FAQs
- POST `/faq/add`: Add a new FAQ
- PATCH `/faq/update`: Update a FAQ
- DELETE `/faq/delete`: Delete a FAQ

## Security Considerations

- API keys and sensitive information are stored in environment variables
- Authentication is required for accessing protected endpoints
- Data validation is implemented for all user inputs

## License

This project is proprietary software.

## Contact

For any inquiries about this application, please contact the development team.
