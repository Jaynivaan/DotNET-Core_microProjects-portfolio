##gs

#Day 05 - Logging System

---

### 0bjective
 
 The gowl of this project is to implement a structured logging system in ASP.NET Core 
 and understand how application track events, errors , and other relevant information for debugging and monitoring purposes.

 Additonally, this project introduces secure logging practices to prevent misuse and protect system integrity.

 ---

 ### Planned features

 - Log api requests
 - Log responses
 - Log errors 
 - Centralized logging system.
 - Structured logging format 

 -Return safe responses 

 ---

 ### My Learning Intentions

 *** Understand how logging works in ASP.NET Core or in any backend systems.
 *** Learn the importance of structured logs.
 *** Learn how logs help debugging and monitoring.
 *** Learn safe logging practices (avoid data leaks, prevent log injection, etc.)
 *** Build awareness of AI-based exploitation techniques that target logging systems and how to mitigate them.

 ---

 ### Thought Chain

 +logging is not just debugging -> its system visibility and monitoring tool.
 +logs can expose sensitive data -> must be careful about what we log.
 +AI systems can expoit Patterns in logs -> logs must be designed to defend against this.
 +focus on structure over volume 
 +avoid logging secrets (tokens, passwords)
 +build mindset: system is always under observation.

 ---

 ### Implementation Plan

 1. Create ASP.NET Core Web API project.
 2. Configure logging (built-in logging providers, Serilog)
 3. Log incoming requests
 4. Log responses (safe data only format)
 5. Log exceptions globally.
 6. Add middleware for centralized logging.
 7. Test logging behavior.

 ---

 ### Tech stack

 - C#
 - ASP.NET Core
 - Serilog (for structured logging)
 
 ---

 ### How to run

 1. Clone the repository.

 2. Navigate to the project directory.
	```cd Day05-LoggingSystem
	```
3. Build the project.
	```dotnet build
	```
4. Run the project.
	```dotnet run
	```
5. Use tools like Postman or curl to send requests to the API and observe the logs in the console or log files.

the curl commands to test the api endpoints are not added yet, but you can easily create them based on the API routes defined in the project.

Note this project is a learning exercise and may not include all best practices for production logging. Always consider security and performance implications when implementing logging in real applications.
and this readme is the step one and the time constraint for building the project is 90 minutes.and time starts fromthe time i commit the readme file.