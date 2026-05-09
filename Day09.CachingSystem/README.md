//gs
### Day09 - Caching System API

### Objectives

Build a small ASP.NET Core WebAPI  that demonstrates caching with clean architecture thinking.

This  Project focuses on :
- cache hit
- cache miss
- TTL
- stale data awareness
- dependency flow
- clean boundaries

### Time Constraint 
Target: 90 min.

Small project strong learning finished outcome.

### interview wisdom

Junior answer: caching makes things faster.

Stronger answer: caching improves performance, but it can create stale data issues. so we implement TTL,Invalidation, and selective caching.


### Architectural flow

HTTP Request

-> API
-> Application Use Case
-> Infrastructure cache
-> DTO Response

###planned Endpoint

```
GET/cache-demo/{key}
```
### expected response

{
	"key" : " wisdom ",
	"value" : "Generated Value",
	"source" : "Cache Miss",
	"cachedAt" : "2026-05-09T10:00:00Z",
	"ttlSeconds" : 30

}

###Modern dotnet Tools

- ASP.NET WebApi
- Minimal Api
- IMemoryCache
- Dependency Injection
- DTO Responses
- async-ready structure

### Security Thinking

+ Do not cache Security data
+ Validate cache keys
+ keep TTL short and sweet
+ Avoid exposing internal errors
+ Do not return domain entities directly.

### Learning Outcome 

= by the end, i should explain:
== what cache hit means
== what cache miss means
== why stale data happens
== how ttl reduces stale data risk
== why infrastructure should be isolated..



+++ Future (may be day 10 and day 11 i will still work with cache world)

+= Hybrid Cache
+= Redis
+= Output Caching
+= BenchmarkDotnet
+= Rate limiting..


