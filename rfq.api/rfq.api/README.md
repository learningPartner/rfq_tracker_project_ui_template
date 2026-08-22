# 📖 Getting Started Guide for Beginners

Welcome to the RFQ API project! This guide will help you understand and work with the codebase.

---

## 📚 Documentation Road Map

**Read documentation in this order:**

### 1️⃣ Start Here (You are here!)
- **README.md** - This file

### 2️⃣ Understand Architecture (30 minutes)
- **ARCHITECTURE.md** - How the project is organized
  - What each folder does
  - How layers work together
  - Request flow diagrams

### 3️⃣ Learn the Workflow (45 minutes)
- **BEGINNER_WORKFLOW.md** - Step-by-step guide
  - How to add new features
  - Complete code examples
  - Testing your changes

### 4️⃣ Practice (Ongoing)
- **BEGINNER_TASKS.md** - 10 practice exercises
  - Easy, Medium, and Hard tasks
  - Build real features
  - Learn by doing

### 5️⃣ Database Configuration
- **Data/Configurations/README.md** - EF Core patterns
- **Data/Configurations/QUICKSTART.md** - Adding tables
- **Data/Configurations/_ConfigurationTemplate.cs** - Template

---

## 🚀 Quick Start (5 Minutes)

### Step 1: Clone and Open Project
```bash
# Already done if you're reading this!
```

### Step 2: Update Connection String
```json
// appsettings.json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;..."
  }
}
```

### Step 3: Run Database Migration
```bash
# Package Manager Console
Update-Database
```

### Step 4: Run Project
```bash
Press F5 in Visual Studio
```

### Step 5: Test API
- Browser should open to: `https://localhost:7238/swagger`
- Try the **GET /api/rfq** endpoint
- Click "Try it out" → "Execute"

✅ **You're ready!**

---

## 🏗️ Project Structure (Simple View)

```
rfq.api/
│
├── 🎯 Program.cs           START HERE - App configuration
│
├── 📂 Controllers/         HTTP Endpoints (API)
├── 📂 Services/            Business Logic
├── 📂 Repositories/        Database Access
├── 📂 Entities/            Database Tables
├── 📂 DTOs/                API Input/Output
├── 📂 Data/                Database Configuration
│
└── 📖 Documentation Files  (You are reading one!)
```

---

## 🎓 Learning Path

### Week 1: Understanding
- [ ] Read ARCHITECTURE.md
- [ ] Trace one request through all layers
- [ ] Understand Controller → Service → Repository → Database
- [ ] Test existing endpoints in Swagger

### Week 2: Exploring
- [ ] Read BEGINNER_WORKFLOW.md
- [ ] Modify an existing endpoint (change response message)
- [ ] Add simple validation in Service
- [ ] Study DTOs and how they differ from Entities

### Week 3: Creating
- [ ] Complete Easy tasks (1-3) from BEGINNER_TASKS.md
- [ ] Add your first new endpoint
- [ ] Test with different inputs
- [ ] Handle error cases

### Week 4: Mastering
- [ ] Complete Medium tasks (4-6)
- [ ] Add a new table and entity
- [ ] Create full CRUD operations
- [ ] Understand relationships between tables

---

## 🔑 Key Concepts for Beginners

### 1. Layered Architecture
```
Controller → Service → Repository → Database
```
- Each layer has ONE job
- Don't skip layers
- Data flows up and down

### 2. Separation of Concerns
- **Controller**: HTTP only
- **Service**: Business rules
- **Repository**: Database only

### 3. Dependency Injection
```csharp
// This is automatic - don't worry about it yet!
public RFQController(IRfqPortalRfqService service)
{
	_service = service; // Injected by .NET
}
```

### 4. Async/Await
```csharp
// Always use async for database operations
public async Task<List<Rfq>> GetAllAsync()
{
	return await _repository.GetAllAsync();
}
```

### 5. Entity vs DTO
- **Entity**: Matches database exactly
- **DTO**: What API sends/receives (can be different)

---

## 🛠️ Tools You'll Use

### Visual Studio
- **F5**: Run with debugging
- **Ctrl+F5**: Run without debugging
- **Ctrl+.**: Quick actions (important!)
- **F12**: Go to definition

### Package Manager Console
```bash
# View menu → Other Windows → Package Manager Console
Add-Migration MigrationName    # Create migration
Update-Database                # Apply migration
```

### Swagger UI
- Automatically opens when you run project
- Interactive API documentation
- Test endpoints without code

---

## 📝 Common Tasks

### Adding a New Endpoint
1. Add method to Repository + Interface
2. Add method to Service + Interface
3. Add method to Controller
4. Test in Swagger

### Adding a New Table
1. Create Entity class
2. Create Configuration class (copy template)
3. Add DbSet to ApplicationDbContext
4. Create migration
5. Update database

### Modifying Existing Feature
1. Find the Controller method
2. Trace to Service method
3. Trace to Repository method
4. Make changes in appropriate layer
5. Test changes

---

## 🎯 Your First Task

**Goal:** Understand how data flows

1. **Run the project** (F5)
2. **Open Swagger** (should open automatically)
3. **Find GET /api/rfq** endpoint
4. **Click "Try it out"** → **"Execute"**
5. **See the response**

Now trace the code:
1. Open `RFQController.cs` → Find `GetAllRfqs()` method
2. See it calls `_rfqService.GetAllAsync()`
3. Open `RfqPortalRfqService.cs` → Find `GetAllAsync()` method
4. See it calls `_repository.GetAllAsync()`
5. Open `RfqPortalRfqRepository.cs` → Find `GetAllAsync()` method
6. See it queries the database

**Congratulations!** You traced your first request! 🎉

---

## ❓ FAQ for Beginners

### Q: Where do I write database queries?
**A:** In Repository classes only. Use LINQ, not raw SQL.

### Q: Where do I add validation rules?
**A:** In Service classes. Example: age > 18, email format, etc.

### Q: Can Controller talk to Repository directly?
**A:** NO! Always go through Service layer.

### Q: What's the difference between `async` and `sync` methods?
**A:** `async` doesn't block the thread while waiting for database/network. Always use `async` for I/O operations.

### Q: When do I use Entity vs DTO?
**A:** Entity is for database. DTO is for API. Convert between them in Service layer.

### Q: How do I add a new property to an entity?
**A:** 
1. Add property to Entity class
2. Update Configuration class
3. Create migration: `Add-Migration AddNewProperty`
4. Update database: `Update-Database`

### Q: What if I break something?
**A:** Don't worry! Use Git to revert changes. That's what version control is for.

---

## 🚨 Common Beginner Mistakes

### 1. Forgetting Interface
```csharp
// Added method to RfqPortalRfqService
// ❌ FORGOT to add to IRfqPortalRfqService
// Result: Compile error
```

### 2. Missing async/await
```csharp
// ❌ WRONG
public Task<List<Rfq>> GetAllAsync()
{
	return _repository.GetAllAsync(); // Missing await
}

// ✅ CORRECT
public async Task<List<Rfq>> GetAllAsync()
{
	return await _repository.GetAllAsync();
}
```

### 3. Returning Entity instead of DTO
```csharp
// ❌ WRONG
public async Task<RfqPortalRfq> GetByIdAsync(int id)
{
	return await _repository.GetByIdAsync(id); // Entity!
}

// ✅ CORRECT
public async Task<RfqPortalRfqDto> GetByIdAsync(int id)
{
	var entity = await _repository.GetByIdAsync(id);
	return MapToDto(entity); // DTO!
}
```

### 4. Business Logic in Wrong Layer
```csharp
// ❌ WRONG - Business logic in Controller
[HttpPost]
public async Task<IActionResult> Create(RfqDto dto)
{
	dto.RfqNumber = GenerateNumber(); // ❌ NO!
	await _service.CreateAsync(dto);
}

// ✅ CORRECT - Business logic in Service
public async Task<RfqDto> CreateAsync(RfqDto dto)
{
	dto.RfqNumber = GenerateNumber(); // ✅ YES!
	// ... rest of code
}
```

---

## 📞 Getting Help

### When Stuck:
1. **Read error message carefully** - It usually tells you what's wrong
2. **Check documentation** - ARCHITECTURE.md, BEGINNER_WORKFLOW.md
3. **Look at existing code** - Find similar feature and copy pattern
4. **Google the error** - Someone has probably solved it
5. **Ask for help** - Include error message and what you tried

### Good Resources:
- Microsoft Docs: https://docs.microsoft.com/aspnet/core
- Entity Framework Core: https://docs.microsoft.com/ef/core
- C# Reference: https://docs.microsoft.com/dotnet/csharp

---

## 🎯 Success Metrics

You're making progress when you can:

**Week 1:**
- [ ] Explain what each layer does
- [ ] Trace a request through all layers
- [ ] Test endpoints in Swagger

**Week 2:**
- [ ] Modify existing endpoint
- [ ] Add simple validation
- [ ] Understand DTOs vs Entities

**Week 3:**
- [ ] Add new endpoint from scratch
- [ ] Create your own DTO
- [ ] Handle errors properly

**Week 4:**
- [ ] Add new entity and table
- [ ] Create full CRUD operations
- [ ] Understand relationships

---

## 🎓 Next Steps

### Now:
1. ✅ Read ARCHITECTURE.md (30 min)
2. ✅ Run project and test in Swagger (10 min)
3. ✅ Trace one request through code (15 min)

### Today:
1. ✅ Read BEGINNER_WORKFLOW.md (45 min)
2. ✅ Complete "Your First Task" above (30 min)
3. ✅ Try modifying a response message (15 min)

### This Week:
1. ✅ Complete Easy tasks from BEGINNER_TASKS.md
2. ✅ Add your first endpoint
3. ✅ Study Data/Configurations/README.md

### This Month:
1. ✅ Complete all Medium tasks
2. ✅ Add a new table
3. ✅ Attempt Hard tasks

---

## 💡 Tips for Success

1. **Take Your Time** - Don't rush. Understanding is more important than speed.
2. **Type Code** - Don't just copy-paste. Type it to learn.
3. **Break When Frustrated** - Taking a break often helps solve problems.
4. **Experiment** - Try things! You can always undo changes.
5. **Ask Questions** - No question is too basic.
6. **Keep Learning** - Everyone was a beginner once.

---

## 🎉 You've Got This!

Learning .NET and Web APIs takes time, but you're already on the right path by reading this documentation. Follow the learning path, complete the practice tasks, and you'll be building APIs confidently in no time!

**Start with:** ARCHITECTURE.md → BEGINNER_WORKFLOW.md → BEGINNER_TASKS.md

Good luck! 🚀

---

*This project follows Clean Architecture principles and industry best practices. It's designed as a learning resource for .NET beginners.*
