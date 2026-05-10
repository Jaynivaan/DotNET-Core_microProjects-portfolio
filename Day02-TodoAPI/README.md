# Day 02 - ToDo API (.NET Core)

---------

###  Objectives

The goal of this project is to build a ToDo API using ASP.NET Core and understand CRUD operations and Data handling.

--------

### Planned Features

--Perform basic CRUD Operations
*Create ToDo
*Get all ToDos
*Get ToDo by Id
*Update ToDo
*Delete ToDo
*Mark ToDo as Completed

--Use HTTP Endpoints.

--Return structured Response to user .


---------

### My Learning Intentions

*** Understand How real API's manage data.
*** Learn Http Methods ( GET, POST, PUT, DELETE )
*** work with in-memory data storage.
*** Improve structured thinking in the backend design.

------

### Thought chain 

+understand  how CRUD work in backend
+focus on data structure (List<T>)
+learn request body handling
+focus only  on correct flow.
+no db for now keep this simple to my current standard..lol
+i should complete this in one  hour from the time i finish this readme.md
+this should be making my understanding on data flow through apis even better.. hopefully.

---

### Implementation Plan

1. create a new project ( we done this already as of now using "dotnet new webapi -n {projectname} " command)
2. define the Todo model
3. create in memory List to store ToDos
4. Implement CRUD EndPoints
5. Return structured Json Responses
6. Test using browser or Postman service. 



-----
### Tech stack

- C#
- ASP.NET Core Web API
- Visual Studio
- .NET

-----

How to Run

1. Clone the Repo

2. Change directory to the Project folder
  
  ```bash

	cd Day02-TodoAPI

   ```
3. Run the app through terminal command 
  
  ```
  dotnet run 
  
  ```

4. EndPoints

create
```
POST/todo

```

Get all Todos
```
GET/todos

```

Get ToDo by Id
 
```

GET/todo/{id}

```
Delete Todo

```
DELETE/todo/{id}

```

Gratitute
==========
My deep gratitude towards the wonderful mentors and teachers at <a href="https://the-tech-academy.com">Tech-Academy</a>who iluminated the path of C sharp and DOTNET world..