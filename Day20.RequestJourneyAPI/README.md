###gs

### Day20.RequestJourneyAPI (.net core + minimal middleware )

This is a very small project for a a low-energy day.
The goal is not to build a big system..
The goal is to build a learning chain alive..

### Objective..
---

This project shows how one request travels through:

- Middleware
- Service 
- Endpoint
- Response

### Plan

- custom middleware
- One service
- One endpont


### Learning Ajenda
---

*** Understand basic middleware flow
*** Practice UseMiddleware syntax
*** Practice di
*** Keep simple program.cs


### Core Flow
---

Browser Button => Get/journey 

=> Middleware executes 

=> ServiceExecutes => Response returns


------

syntax for the day
===

```
app.UseMiddleware<RequestJourneyMiddleware>();

```

What is middleware?

Middleware is a component that sits in the request pipeline 

and can inspect, modify, log, allow, block, or transform

request and responses before they reach the endpoint.

===

```
public async Task InvokeAsync(
	HttpContext context,
	RequestDelegate next)
{
	await next(context);
}
//HttpContext = the request and response

//RequestDelegate next = the next stop in the pipeline

//await next(context) = continue the journey

```






time allowance

60 minutes

vision:

##gs## 
Even on difficult day, one small completed step protects the learning flame .
##gs##



### oneday###fool###Make###this###happen



Gratitude:
---------

My deep gratitude towards the wonderful mentors especially the coding teachers at 
<a href="https://the-tech-academy.com">Tech&hearts;academy</a>
who illuminated the path of C sharp and DOTNET world..
anyone interested to learn ..Learn from Tech-academy you meet with best explanations.. 
and to the World around me who relentlessly teach me roughest lessons only to build me stronger..

 with love &infin;
 the fool.. &hearts;




