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
 <li><strong>SPAgame.Client/:</strong> Contains the React frontend application.</li>
 <li><strong>SPAgame.Server/:</strong> Contains the ASP.NET Core backend application.</li>
 <li><strong>Controllers/:</strong> ASP.NET controllers for handling API requests.</li>
 <li><strong>Data/:</strong> Contains Entity Framework DbContext and migrations.</li>
 <li><strong>Models/:</strong> Entity classes and data models.</li>
</ul>

## Features 
<ul>
    <li><strong>Blackjack Game:</strong> Play a full-featured game of Blackjack.</li>
    <li><strong>Highscore List:</strong> Displays top players based on wins and games played. </li>
    <li><strong>Authentication and Authorization:</strong> Uses ASP.NET Identity for user management.</li>
    <li><strong>Entity Framework:</strong> For database operations.</li>
    <li><strong>React Frontend:</strong> Modern single-page application (SPA) using React.</li>
    <li><strong>Protected Routes:</strong> Certain routes are accessible only to authenticated users.</li>
</ul>

## Technologies Used
<ul>
    <li><strong>.NET 8:</strong> Backend framework.</li>
    <li><strong>ASP.NET Core:</strong> Web framework for building web applications.</li>
    <li><strong>Entity Framework Core:</strong> ORM for database management.</li>
    <li><strong>ASP.NET Identity:</strong> Authentication and authorization.</li>
    <li><strong>React:</strong> Frontend framework for building user interfaces.</li>
    <li><strong>SQL Server:</strong> Default database (can be changed in configuration).</li>
</ul>
