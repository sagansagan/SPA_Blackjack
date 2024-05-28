# Blackjack Game Application

This project is a web application built using the React and ASP.NET core template. The application utilizes Entity Framework for data management and ASP.NET Identity for authentication and authorization.

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
- [Project Structure](#project-structure)
- [Features](#features)
- [Technologies Used](#technologies-used)

## Installation

Make sure you have the following:

    - .NET SDK installed
    - Node.js and npm installed

## Configuration

1. Update the connection string in appsettings.json to point to your database. The default configuration uses SQL Server.
2. Apply migrations to create the database schema.

That is all.

## Project Structure

SPAgame.Client/: Contains the React frontend application.
SPAgame.Server/: Contains the ASP.NET Core backend application.
Controllers/: ASP.NET controllers for handling API requests.
Data/: Contains Entity Framework DbContext and migrations.
Models/: Entity classes and data models.

## Features 

Blackjack Game: Play a full-featured game of Blackjack.
Highscore List: Displays top players based on wins and games played. 
Authentication and Authorization: Uses ASP.NET Identity for user management.
Entity Framework: For database operations.
React Frontend: Modern single-page application (SPA) using React.
Protected Routes: Certain routes are accessible only to authenticated users.

## Technologies Used

.NET 8: Backend framework.
ASP.NET Core: Web framework for building web applications.
Entity Framework Core: ORM for database management.
ASP.NET Identity: Authentication and authorization.
React: Frontend framework for building user interfaces.
SQL Server: Default database (can be changed in configuration).
