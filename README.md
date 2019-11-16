# Alten Challenge project by Nader

## Table of Contents
1. [About the Project](#about-the-project)
1. [Getting Started](#getting-started)

# About the Project
Angular Application for showing an overview of all vehicles together with their status with the ability to filter to only show vehicles for a specific customer or that have a specific status.

# Getting Started
•	Prerequisites:

  -	Visual Studio 2017 or 2019
  
  -	.NET Core 2.2
  
  -	Nodejs 12.13
  
  -	npm 6.12 (Angular CLI)
  
  -	Docker 19.03.4
  
  -	Docker Compose 1.24.1
  
•	Steps:

  -	New email has been created for testing challenge components like Azure, email credentials are sent via email
  
    -	Email have access to GitHub repository and can push changes
    
    -	Email can login to azure project via GitHub login
    
    -	SonarCloud access to check code analysis reports
    
  -	Clone solution from GitHub repository
  
  -	Open solution with visual studio (minimum version 2017)
  
  -	Build solution 
  
  -	Open command window and redirect to solution path
  
  -	Enter “docker-compose up” command to build all containers
  
  -	After successful operation of all containers, system should run as follow:
  
    -	Angular app : http://localhost:7070/
    
    -	API Gateway : http://localhost:9090/api/{controller}/{action}
    
      -	Ex. http://localhost:9090/api/customer/getcustomers
      
    -	Vehicle service : http://localhost:6060/ 
    
    -	If containers are up not in background mode, ping service logs will be shown in every ping with vehicle Id
    
  -	Make any change in code and push changes to trigger Continues integration
  
  -	Check Azure Build Pipeline to check the new build triggered by github to start code verification and tests
  
  -	After successful build, go to SonarCloud project to check code analysis report generated by azure build
  

