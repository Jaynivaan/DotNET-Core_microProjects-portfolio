###gs

### Day15.HealthCheckObservabilityAPI (.net Core + Health Checks)

This app is like a heartbeat monitor for software.
it answers:

1. is app alive?

2. is it responding?

3. how long has it been running?

4. what env is it running in ?

### Objectives:

The goal of this project is to build a small production-style API  that can 
report whether the system is alive, healthy, and observable.

This project focuses on : 

Health Checks
Observability Thinking
System metadata
Uptime Tracking
Thin APi
DI
Options Pattern 
Structured responses
Service Layer
Production ready backend thinking

---

### planned features:

- built - in health check endpoint
- system status endpoint
- System Metadata Endpoint
- Uptime Calculation
- Structured response format
- Config driven system info
- Clean Service Separation
- Production readiness thinking

===

### My learing Intentions

*** Understand how real Systems check Health
*** Learn ASP.NET Core Health Checks
*** Learn Observability basics
*** Learn Uptime computations
*** Practice clean service structure
*** Practice Metadata response design
*** Learn production ready backend thinking

---

$$$ Modern Engineering styles i am adapting on this project are 
\ Thin api 
/ Built in health checks
\ Di
/ options
\ metadata
/ service dir
\ clean


$$$

---

planned endpoints

```
http://localhost:port/health
```

```
http://localhost:port/healthz
```

```
http://localhost:port/metadata
```

---

Time planned :

180 minutes

---

### syntax i am keenly focussed to memorize

builder.Services.AddHealthChecks();

app.MapHealthEndpoints();


###
future vision

The fool will one day see that happen .

##gs##
--

### Gratitude ###

My deep gratitude towards the wonderful mentors especially the coding teachers at  <a href="https://the-tech-academy.com">Tech-Academy</a>who illuminated the path of C sharp and DOTNET world..
anyone interested to learn ..Learn from Tech-academy you meet with best explanations.. 
and to the World around me who relentlessly teach me roughest lessons only to build me stronger..
 with love &infin;
 the fool.. &hearts;



###gs

