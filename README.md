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
<ul>
 <li>SPAgame.Client/: Contains the React frontend application.</li>
 <li>SPAgame.Server/: Contains the ASP.NET Core backend application.</li>
 <li>Controllers/: ASP.NET controllers for handling API requests.</li>
 <li>Data/: Contains Entity Framework DbContext and migrations.</li>
 <li>Models/: Entity classes and data models.</li>
</ul>

## Features 
<ul>
    <li>Blackjack Game: Play a full-featured game of Blackjack.</li>
    <li>Highscore List: Displays top players based on wins and games played. </li>
    <li>Authentication and Authorization: Uses ASP.NET Identity for user management.</li>
    <li>Entity Framework: For database operations.</li>
    <li>React Frontend: Modern single-page application (SPA) using React.</li>
    <li>Protected Routes: Certain routes are accessible only to authenticated users.</li>
</ul>

## Technologies Used
<ul>
    <li>.NET 8: Backend framework.</li>
    <li>ASP.NET Core: Web framework for building web applications.</li>
    <li>Entity Framework Core: ORM for database management.</li>
    <li>ASP.NET Identity: Authentication and authorization.</li>
    <li>React: Frontend framework for building user interfaces.</li>
    <li>SQL Server: Default database (can be changed in configuration).</li>
</ul>
