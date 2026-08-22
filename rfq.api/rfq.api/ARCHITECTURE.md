# 🏗️ Project Architecture - Beginner's Guide

## 📚 Table of Contents
1. [What is Architecture?](#what-is-architecture)
2. [Project Structure Overview](#project-structure-overview)
3. [How Everything Works Together](#how-everything-works-together)
4. [Understanding Each Layer](#understanding-each-layer)
5. [Request Flow Example](#request-flow-example)
6. [Quick Reference](#quick-reference)

---

## 🤔 What is Architecture?

Think of architecture like building a house:
- **Foundation** = Database (where data is stored)
- **Rooms** = Layers (each has a specific purpose)
- **Plumbing** = How data flows between layers
- **Front Door** = Controllers (where requests come in)

**Good architecture:**
- ✅ Easy to understand
- ✅ Easy to change
- ✅ Easy to test
- ✅ Organized and clean

---

## 📁 Project Structure Overview

```
rfq.api/
│
├── 📂 Controllers/          ← 🚪 Front Door (HTTP requests come here)
│   └── RFQController.cs     
│
├── 📂 Services/             ← 🧠 Brain (Business logic & rules)
│   ├── Interfaces/
│   │   └── IRfqPortalRfqService.cs
│   └── RfqPortalRfqService.cs
│
├── 📂 Repositories/         ← 📦 Storage Manager (Database operations)
│   ├── Interfaces/
│   │   └── IRfqPortalRfqRepository.cs
│   └── RfqPortalRfqRepository.cs
│
├── 📂 Entities/             ← 📋 Data Models (Database tables)
│   └── RfqPortalRfq.cs
│
├── 📂 DTOs/                 ← 📮 Envelopes (Data transfer objects)
│   ├── RfqPortalRfqDto.cs
│   └── ApiResponse.cs
│
├── 📂 Data/                 ← 🗄️ Database Connection
│   ├── ApplicationDbContext.cs
│   └── Configurations/       (Database table settings)
│       ├── RfqPortalRfqConfiguration.cs
│       └── _ConfigurationTemplate.cs
│
├── 📂 Middleware/           ← 🛡️ Guards (Error handling, security)
│   └── ExceptionHandlingMiddleware.cs
│
├── 📂 Constants/            ← 📝 Fixed Values (Messages, codes)
│   └── MessageConstants.cs
│
└── Program.cs               ← ⚙️ Engine Start (App configuration)
```

---

## 🔄 How Everything Works Together

### The 3-Layer Architecture (Clean Architecture)

```
┌─────────────────────────────────────────────────┐
│  USER (Browser/Mobile App/Postman)              │
└──────────┬──────────────────────────────────────┘
		   │ HTTP Request (JSON)
		   ▼
┌─────────────────────────────────────────────────┐
│  LAYER 1: PRESENTATION (Controllers)            │
│  📍 Role: Receive requests, send responses       │
│  📍 Example: RFQController.cs                    │
│                                                   │
│  ✅ Validates input                              │
│  ✅ Calls Service layer                          │
│  ✅ Returns JSON response                        │
└──────────┬──────────────────────────────────────┘
		   │ Calls Service
		   ▼
┌─────────────────────────────────────────────────┐
│  LAYER 2: BUSINESS LOGIC (Services)             │
│  📍 Role: Business rules & logic                 │
│  📍 Example: RfqPortalRfqService.cs              │
│                                                   │
│  ✅ Applies business rules                       │
│  ✅ Coordinates operations                       │
│  ✅ Calls Repository layer                       │
└──────────┬──────────────────────────────────────┘
		   │ Calls Repository
		   ▼
┌─────────────────────────────────────────────────┐
│  LAYER 3: DATA ACCESS (Repositories)            │
│  📍 Role: Talk to database                       │
│  📍 Example: RfqPortalRfqRepository.cs           │
│                                                   │
│  ✅ CRUD operations (Create, Read, Update, Delete)│
│  ✅ Database queries                             │
│  ✅ No business logic here!                      │
└──────────┬──────────────────────────────────────┘
		   │ Uses Entity Framework
		   ▼
┌─────────────────────────────────────────────────┐
│  DATABASE (SQL Server)                          │
│  📍 Role: Store data permanently                 │
│  📍 Example: rfq_portal_rfqs table               │
└─────────────────────────────────────────────────┘
```

---

## 📚 Understanding Each Layer

### 🚪 1. Controllers (The Front Door)

**What it does:** Receives HTTP requests from users

**Real-world analogy:** Like a receptionist at a hotel
- Greets guests (receives requests)
- Directs them to the right department (calls services)
- Gives them their room keys (sends responses)

**Example:**
```csharp
// RFQController.cs
[HttpGet]
public async Task<IActionResult> GetAllRfqs()
{
	// 1. Call the service
	var rfqs = await _rfqService.GetAllAsync();

	// 2. Return the response
	return Ok(rfqs);
}
```

**Rules:**
- ❌ NO database code here
- ❌ NO business logic here
- ✅ ONLY receive requests and return responses

---

### 🧠 2. Services (The Brain)

**What it does:** Contains business logic and rules

**Real-world analogy:** Like a manager at a hotel
- Makes decisions (business logic)
- Coordinates departments (calls repositories)
- Enforces rules (validation, calculations)

**Example:**
```csharp
// RfqPortalRfqService.cs
public async Task<RfqPortalRfqDto> CreateAsync(RfqPortalRfqDto dto)
{
	// Business logic: Generate unique RFQ number
	dto.RfqNumber = GenerateUniqueRfqNumber();

	// Business logic: Set default status
	dto.RfqStatus = "Draft";

	// Call repository to save
	var entity = MapToEntity(dto);
	var created = await _repository.AddAsync(entity);

	return MapToDto(created);
}
```

**Rules:**
- ✅ Business logic goes here
- ✅ Validation and calculations
- ✅ Coordinate multiple repository calls
- ❌ NO direct database queries

---

### 📦 3. Repositories (The Storage Manager)

**What it does:** Talks directly to the database

**Real-world analogy:** Like a warehouse worker
- Stores items (INSERT)
- Retrieves items (SELECT)
- Updates items (UPDATE)
- Removes items (DELETE)

**Example:**
```csharp
// RfqPortalRfqRepository.cs
public async Task<RfqPortalRfq> GetByIdAsync(int id)
{
	// Simple database query - no business logic!
	return await _context.RfqPortalRfqs
		.FirstOrDefaultAsync(x => x.RfqId == id);
}
```

**Rules:**
- ✅ Database operations ONLY
- ❌ NO business logic here
- ❌ NO calculations or validations

---

### 📋 4. Entities (The Database Models)

**What it does:** Represents database tables

**Real-world analogy:** Like a blueprint of a form
- Each property = a field on the form
- Each entity = a table in the database

**Example:**
```csharp
// RfqPortalRfq.cs (Entity)
public class RfqPortalRfq
{
	public int RfqId { get; set; }           // Primary Key
	public string RfqNumber { get; set; }     // RFQ-001
	public string Title { get; set; }         // Project Title
	public DateTime CreatedAt { get; set; }   // Timestamp
}
```

**Rules:**
- ✅ Just properties (data)
- ❌ NO methods or logic
- ❌ NO validation rules

---

### 📮 5. DTOs (Data Transfer Objects)

**What it does:** Shapes data for sending/receiving

**Real-world analogy:** Like an envelope
- Contains only what needs to be sent
- Hides sensitive information
- Makes data easy to read

**Example:**
```csharp
// RfqPortalRfqDto.cs
public class RfqPortalRfqDto
{
	public int RfqId { get; set; }
	public string RfqNumber { get; set; }
	public string Title { get; set; }
	// No sensitive data like passwords!
}
```

**Why use DTOs?**
- 🔒 Security: Hide sensitive fields
- 📦 Performance: Send only needed data
- 🔧 Flexibility: Different views of same data

---

### 🗄️ 6. Data / DbContext (Database Connection)

**What it does:** Manages database connection and configuration

**Real-world analogy:** Like a database administrator
- Sets up tables
- Manages relationships
- Configures database settings

**Example:**
```csharp
// ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
	public DbSet<RfqPortalRfq> RfqPortalRfqs { get; set; }

	// Configuration happens in separate files
	// See: Data/Configurations/RfqPortalRfqConfiguration.cs
}
```

---

### 🛡️ 7. Middleware (Guards/Interceptors)

**What it does:** Handles cross-cutting concerns

**Real-world analogy:** Like security guards at different checkpoints
- Check IDs (authentication)
- Check permissions (authorization)
- Catch problems (error handling)

**Example:**
```csharp
// ExceptionHandlingMiddleware.cs
public async Task InvokeAsync(HttpContext context)
{
	try
	{
		await _next(context); // Continue to next middleware
	}
	catch (Exception ex)
	{
		// Handle error and return friendly message
		await HandleExceptionAsync(context, ex);
	}
}
```

---

## 🎯 Request Flow Example

### Scenario: User wants to get all RFQs

```
Step 1: User makes request
┌─────────────────────────┐
│ GET /api/rfq            │ ← User sends HTTP request
└───────────┬─────────────┘
			│
Step 2: Goes through middleware
┌───────────▼─────────────┐
│ Middleware              │
│ - Checks for errors     │
│ - Logs request          │
└───────────┬─────────────┘
			│
Step 3: Reaches Controller
┌───────────▼─────────────┐
│ RFQController.cs        │
│ GetAllRfqs()            │
│ - Receives request      │
└───────────┬─────────────┘
			│ Calls Service
Step 4: Service processes
┌───────────▼─────────────┐
│ RfqPortalRfqService.cs  │
│ GetAllAsync()           │
│ - Business logic        │
└───────────┬─────────────┘
			│ Calls Repository
Step 5: Repository queries database
┌───────────▼─────────────┐
│ RfqPortalRfqRepository  │
│ GetAllAsync()           │
│ - SELECT * FROM rfqs    │
└───────────┬─────────────┘
			│ Queries database
Step 6: Database returns data
┌───────────▼─────────────┐
│ SQL Server              │
│ Returns rows            │
└───────────┬─────────────┘
			│ Returns entities
Step 7: Repository returns to Service
┌───────────▼─────────────┐
│ RfqPortalRfqRepository  │
│ Returns List<RfqEntity> │
└───────────┬─────────────┘
			│ Returns data
Step 8: Service processes & returns
┌───────────▼─────────────┐
│ RfqPortalRfqService.cs  │
│ Converts to DTOs        │
│ Returns List<RfqDto>    │
└───────────┬─────────────┘
			│ Returns DTOs
Step 9: Controller returns response
┌───────────▼─────────────┐
│ RFQController.cs        │
│ return Ok(rfqs)         │
└───────────┬─────────────┘
			│ HTTP Response (JSON)
Step 10: User receives data
┌───────────▼─────────────┐
│ [{                      │
│   "rfqId": 1,           │
│   "title": "Project"    │
│ }]                      │
└─────────────────────────┘
```

---

## 🎓 Learning Path for Beginners

### Phase 1: Understanding the Basics (Week 1)
1. ✅ Read this ARCHITECTURE.md
2. ✅ Study one simple feature (e.g., GetById)
3. ✅ Trace request from Controller → Service → Repository → Database
4. ✅ Read BEGINNER_WORKFLOW.md

### Phase 2: Working with Code (Week 2-3)
1. ✅ Modify existing feature (change a property)
2. ✅ Add simple validation in Service
3. ✅ Test with Swagger
4. ✅ Read BEGINNER_TASKS.md

### Phase 3: Creating Features (Week 4+)
1. ✅ Add new endpoint (following existing pattern)
2. ✅ Add new table (using Configuration template)
3. ✅ Add relationships between tables
4. ✅ Read ADVANCED_CONCEPTS.md

---

## 🚀 Quick Reference

### Where to Put Code?

| I want to... | Put code in... | Example |
|-------------|----------------|---------|
| Handle HTTP request | Controller | `[HttpGet]` method |
| Calculate something | Service | Price calculation |
| Validate business rule | Service | Check age > 18 |
| Save to database | Repository | `AddAsync()` |
| Query database | Repository | `GetByIdAsync()` |
| Define table structure | Entity | Properties |
| Configure database mapping | Configuration | `.HasMaxLength()` |
| Format API response | DTO | Public properties |
| Handle errors | Middleware | Try-catch |

### Common Mistakes to Avoid

❌ **WRONG:**
```csharp
// Controller with database code
[HttpGet]
public IActionResult GetRfq(int id)
{
	var rfq = _context.RfqPortalRfqs.Find(id); // ❌ NO!
	return Ok(rfq);
}
```

✅ **CORRECT:**
```csharp
// Controller calls Service
[HttpGet]
public async Task<IActionResult> GetRfq(int id)
{
	var rfq = await _rfqService.GetByIdAsync(id); // ✅ YES!
	return Ok(rfq);
}
```

---

## 📖 Next Steps

1. **Read next:** `BEGINNER_WORKFLOW.md` - Step-by-step how to add a new feature
2. **Read later:** `BEGINNER_TASKS.md` - Practice exercises
3. **Reference:** `Data/Configurations/README.md` - Database configuration
4. **Reference:** `Data/Configurations/QUICKSTART.md` - Adding new tables

---

## ❓ Need Help?

**Common Questions:**
- Q: Where do I add validation? → A: Service layer
- Q: Where do I write SQL queries? → A: Repository (but use LINQ, not raw SQL)
- Q: Can I call Repository from Controller? → A: NO! Always go through Service
- Q: Why use DTOs instead of Entities? → A: Security, performance, flexibility

**Architecture Rules to Remember:**
1. 📊 Controller → Service → Repository → Database (One direction only!)
2. 🚫 Never skip layers (Controller should NOT call Repository directly)
3. 🎯 Each layer has ONE job (Single Responsibility)
4. 🔄 Data flows up and down, but layers don't skip

---

*This guide is designed for beginners. Take your time understanding each concept before moving forward.*
