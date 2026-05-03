
//gs//

# Day 03 - Authentication System (.NetCore)

---
###Objective
- Create a simple authentication system using .NetCore.

---

### Planned Features
--User Registration
--User Login
--Basic credential validation
--Token-Generation (simple JWT or similar)
--Protected Endpoints (for authenticated users only)

--use Http Endpoints

--return structured responses

---

### My learning Intentions

***- Understand how authentication works in .NetCore.
***- Learn how back end validates user credentials.
***-Learn token based authentication (JWT or similar).
***-Understand Protected routes.

---

### Thought chain

+ understand how login system works internally
+ focus on identity validation
+ learn how tokens are used for authentication
+ keep logic simple and clear
+ focus on correct flow over complex features
+ this should help me understand the real world authentication systems better
+ +i should complete this in one  hour from the time i finish comiting this readme.md to github.


---

###Implementation Plan
1. Set up a new .NetCore Web API project.
2 define user model( username, password ).
3. Store user data in memory (List<User>).
4. Implement registration endpoint to add new users.
5. Implement login endpoint 
6. Generate simple token (JWT) on successful login.
7. Create protected endpoint that requires valid token for access.
8. Validate token Before allowing access to protected endpoint.
9. Testing.


---

### Tech Stack
- .NetCore Web API
- C#
- Visual Studio 

---
### How to Run

1. Clone the repository..

2. Navigate to  project directory.
 
 ```
 cd AuthSystem
 ```

 3. Build the project using Visual Studio or command line.
```
 dotnet build
```
4. Run the application.
```
 dotnet run
```		

use terminal or postman to test the endpoints.





