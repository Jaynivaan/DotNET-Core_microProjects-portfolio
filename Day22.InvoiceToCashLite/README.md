###gs

### Day22. InvoiceToCashLite (.Net Core + Blazor )

### Objectives:

The goal of this is to craft the foundation of a small invoice to cash platform
inspired by the modern Accounts Recievable systems.

App is designed to evolve feature by feature following vertical slice architecture..


today focussing o nly on
- invoice foundation
- Open api Contract generation
- Blazor texting surface
- Solid principles


-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

### the whole app vision

InvoiceToCashLite is a tiny invoice-to-cash learning System.

The larger app will grow feature by feature:

Invoice Created => Payment Submitted => Payment Applied

=> invoice status updated

=>Balance Recalculated => Reconciliation Generated => Blazor dashboard

The goal is to understand real-world 
payment and accounts receivable style workflows
in a small , achievable way.

-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

feature 1:

Invoice foundation

This feature allows the system to :

- create an invoice
- validate invoice rules
- store invoice in memory
- list invoices
- get invoices by id
- display invoices data through Blazor.
- openapi documentation.

### learning intentions

---
= understand teh invoice to cash workenergy flow basics.
= Learn business rule separation.
= blazor minimal test surface.
= practice service layer design
= strictly SOLID based building
= build interview relavant  business software thinking..


---
============================

### syntax of the day:

---------
```
public enum InvoiceStatus
{
	Open,
	PartiallyPaid,
	Paid,
	Cancelled
}
```

enum gives fixed named possible states of existence for a given invoice object.

invoice should move through clearly defines states instead of random strings.


modern methodologies used:

- minimal api
- service layer
- Dtos
- Feature by feature roll out..



Time allowance:

180 minutes


vision
----

One feature at a time, business system become maintainable when features take its 
genuine time to evolve independently . when the contracts remain discoverable
and workflows remain understandable..###gs



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


