# 🎨 Visual Quick Reference

## 🗺️ File Organization Map

```
📁 rfq.api/
│
├── 📄 Program.cs ⭐ START HERE!
│   └── Configures: Database, Services, Middleware, Swagger
│
├── 📁 Controllers/ 🚪 LAYER 1: API Endpoints
│   └── RFQController.cs
│       ├── [HttpGet] GetAllRfqs()
│       ├── [HttpGet("{id}")] GetRfqById(int id)
│       ├── [HttpPost] CreateRfq(RfqDto dto)
│       ├── [HttpPut("{id}")] UpdateRfq(int id, RfqDto dto)
│       └── [HttpDelete("{id}")] DeleteRfq(int id)
│
├── 📁 Services/ 🧠 LAYER 2: Business Logic
│   ├── Interfaces/
│   │   └── IRfqPortalRfqService.cs (Contract)
│   └── RfqPortalRfqService.cs (Implementation)
│       ├── GetAllAsync() → Calls Repository
│       ├── GetByIdAsync(id) → Calls Repository
│       ├── CreateAsync(dto) → Business Rules + Repository
│       ├── UpdateAsync(dto) → Business Rules + Repository
│       └── DeleteAsync(id) → Business Rules + Repository
│
├── 📁 Repositories/ 📦 LAYER 3: Database Access
│   ├── Interfaces/
│   │   └── IRfqPortalRfqRepository.cs (Contract)
│   └── RfqPortalRfqRepository.cs (Implementation)
│       ├── GetAllAsync() → SELECT * FROM rfqs
│       ├── GetByIdAsync(id) → SELECT WHERE id = @id
│       ├── AddAsync(entity) → INSERT INTO rfqs
│       ├── UpdateAsync(entity) → UPDATE rfqs SET...
│       └── DeleteAsync(id) → DELETE FROM rfqs WHERE...
│
├── 📁 Entities/ 📋 Database Models
│   └── RfqPortalRfq.cs
│       └── Properties = Database Columns
│           ├── RfqId (PK)
│           ├── RfqNumber
│           ├── Title
│           ├── Description
│           └── ... (17 properties total)
│
├── 📁 DTOs/ 📮 API Data Shapes
│   ├── RfqPortalRfqDto.cs (For API responses)
│   └── ApiResponse.cs (Wrapper for all responses)
│       ├── IsSuccess (bool)
│       ├── Message (string)
│       └── Data (T)
│
├── 📁 Data/ 🗄️ Database Configuration
│   ├── ApplicationDbContext.cs (Main DB Context)
│   │   ├── DbSet<RfqPortalRfq> RfqPortalRfqs
│   │   └── OnModelCreating() → Loads configurations
│   │
│   └── 📁 Configurations/ (Entity configurations)
│       ├── RfqPortalRfqConfiguration.cs
│       ├── _ConfigurationTemplate.cs ⭐ COPY THIS!
│       ├── README.md
│       └── QUICKSTART.md
│
├── 📁 Middleware/ 🛡️ Request Interceptors
│   └── ExceptionHandlingMiddleware.cs
│       └── Catches all errors → Returns friendly response
│
├── 📁 Constants/ 📝 Fixed Values
│   └── MessageConstants.cs
│       └── Success/Error messages
│
└── 📄 Configuration Files
	├── appsettings.json (Connection strings, settings)
	├── launchSettings.json (Port, browser settings)
	└── rfq.api.csproj (NuGet packages)
```

---

## 🔄 Request Flow Visualization

### GET Request Flow
```
┌─────────────────────────────────────────────────────────────┐
│ 1. USER ACTION                                              │
│    User clicks "Get All RFQs" in frontend                   │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ HTTP Request
┌─────────────────────────────────────────────────────────────┐
│ 2. MIDDLEWARE (ExceptionHandlingMiddleware)                 │
│    ✓ Wraps request in try-catch                             │
│    ✓ Logs request                                           │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓
┌─────────────────────────────────────────────────────────────┐
│ 3. CONTROLLER (RFQController.cs)                            │
│    📍 GetAllRfqs() method                                    │
│                                                              │
│    var result = await _rfqService.GetAllAsync();            │
│    return Ok(result);                                        │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ Calls Service
┌─────────────────────────────────────────────────────────────┐
│ 4. SERVICE (RfqPortalRfqService.cs)                         │
│    📍 GetAllAsync() method                                   │
│                                                              │
│    var rfqs = await _repository.GetAllAsync();              │
│    return MapToDtos(rfqs);                                   │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ Calls Repository
┌─────────────────────────────────────────────────────────────┐
│ 5. REPOSITORY (RfqPortalRfqRepository.cs)                   │
│    📍 GetAllAsync() method                                   │
│                                                              │
│    return await _context.RfqPortalRfqs.ToListAsync();       │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ Entity Framework Query
┌─────────────────────────────────────────────────────────────┐
│ 6. EF CORE (ApplicationDbContext)                           │
│    Translates LINQ to SQL                                    │
│                                                              │
│    SELECT * FROM [b31testingUser].[rfq_portal_rfqs]        │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ SQL Query
┌─────────────────────────────────────────────────────────────┐
│ 7. SQL SERVER                                               │
│    Executes query and returns rows                          │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ Returns rows as entities
┌─────────────────────────────────────────────────────────────┐
│ 8. REPOSITORY                                                │
│    List<RfqPortalRfq> entities                              │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ Returns entities
┌─────────────────────────────────────────────────────────────┐
│ 9. SERVICE                                                   │
│    Convert: Entity → DTO                                     │
│    List<RfqPortalRfqDto> dtos                               │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ Returns DTOs
┌─────────────────────────────────────────────────────────────┐
│ 10. CONTROLLER                                              │
│     Wraps in ApiResponse                                     │
│     return Ok(new ApiResponse { Data = dtos })              │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ↓ HTTP Response (JSON)
┌─────────────────────────────────────────────────────────────┐
│ 11. USER                                                     │
│     Receives JSON response:                                  │
│     {                                                        │
│       "isSuccess": true,                                     │
│       "message": "RFQs retrieved successfully",             │
│       "data": [...]                                          │
│     }                                                        │
└─────────────────────────────────────────────────────────────┘
```

---

## 📊 Layer Responsibilities Chart

| Layer | Files | Job | Can Call | Cannot Call |
|-------|-------|-----|----------|-------------|
| **Controller** | `*Controller.cs` | Handle HTTP | Service | Repository, DbContext |
| **Service** | `*Service.cs` | Business Logic | Repository, Other Services | DbContext |
| **Repository** | `*Repository.cs` | Database Queries | DbContext | Nothing below |
| **Entity** | `*.cs` in Entities/ | Data Model | Nothing | Nothing |
| **DTO** | `*Dto.cs` | API Shape | Nothing | Nothing |
| **DbContext** | `ApplicationDbContext.cs` | DB Connection | EF Core | Nothing |
| **Configuration** | `*Configuration.cs` | Table Mapping | EF Core | Nothing |
| **Middleware** | `*Middleware.cs` | Intercept Requests | Services (if needed) | Repository |

---

## 🎯 When to Add Code Where?

### ❓ I Need To...

#### 📥 Get Data from API
```
ADD: Controller method
CALL: Service.GetXAsync()
RETURN: ApiResponse<DTO>
```

#### 💾 Save Data to Database
```
ADD: Repository.AddAsync(entity)
CALL: _context.EntitySet.Add(entity)
SAVE: await _context.SaveChangesAsync()
```

#### ✅ Validate Business Rule
```
ADD: Service method
CHECK: if (condition) throw exception
EXAMPLE: if (age < 18) throw new Exception("Too young")
```

#### 🔍 Search/Filter Data
```
REPOSITORY: Build LINQ query
	var query = _context.Entities.Where(x => x.Property == value);
SERVICE: Call repository, map to DTO
CONTROLLER: Return result
```

#### 🆕 Add New Field
```
1. Add property to Entity
2. Update Configuration (Data/Configurations/)
3. Create migration: Add-Migration AddNewField
4. Update database: Update-Database
5. Add property to DTO
6. Update mapping (Entity ↔ DTO)
```

#### 📊 Add New Table
```
1. Create Entity class (Entities/)
2. Copy _ConfigurationTemplate.cs
3. Rename to {Entity}Configuration.cs
4. Configure all properties
5. Add DbSet to ApplicationDbContext
6. Create migration: Add-Migration Add{Entity}Table
7. Update database: Update-Database
```

---

## 🧩 Code Pattern Templates

### Controller Method Template
```csharp
/// <summary>
/// Description of what this endpoint does
/// </summary>
[Http{Method}]
public async Task<IActionResult> MethodName(parameters)
{
	try
	{
		// 1. Validate input
		if (invalid)
			return BadRequest(...);

		// 2. Call service
		var result = await _service.MethodAsync(parameters);

		// 3. Return response
		return Ok(new ApiResponse<T>
		{
			IsSuccess = true,
			Message = "Success message",
			Data = result
		});
	}
	catch (Exception ex)
	{
		return StatusCode(500, new ApiResponse<object>
		{
			IsSuccess = false,
			Message = "Error message"
		});
	}
}
```

### Service Method Template
```csharp
public async Task<ResultDto> MethodAsync(parameters)
{
	// 1. Validate business rules
	if (invalid)
		throw new Exception("Business rule violation");

	// 2. Call repository
	var entity = await _repository.MethodAsync(parameters);

	// 3. Apply business logic
	// ... calculations, transformations ...

	// 4. Convert to DTO
	return MapToDto(entity);
}
```

### Repository Method Template
```csharp
public async Task<Entity> MethodAsync(parameters)
{
	// Simple LINQ query
	return await _context.EntitySet
		.Where(e => e.Property == value)
		.FirstOrDefaultAsync();
}
```

---

## 🔑 HTTP Method Reference

| Method | Purpose | Example | Expected Status Code |
|--------|---------|---------|---------------------|
| `GET` | Retrieve data | Get all RFQs | 200 OK, 404 Not Found |
| `POST` | Create new | Create RFQ | 201 Created, 400 Bad Request |
| `PUT` | Update entire | Update RFQ | 200 OK, 404 Not Found |
| `PATCH` | Update partial | Update status only | 200 OK, 404 Not Found |
| `DELETE` | Remove | Delete RFQ | 200 OK, 404 Not Found |

---

## 📦 Common LINQ Patterns

### Get All
```csharp
await _context.Entities.ToListAsync()
```

### Get By ID
```csharp
await _context.Entities.FirstOrDefaultAsync(e => e.Id == id)
```

### Filter
```csharp
await _context.Entities
	.Where(e => e.Property == value)
	.ToListAsync()
```

### Search (Contains)
```csharp
await _context.Entities
	.Where(e => e.Name.Contains(searchTerm))
	.ToListAsync()
```

### Sort
```csharp
await _context.Entities
	.OrderBy(e => e.Name)              // A-Z
	.OrderByDescending(e => e.Date)    // Newest first
	.ToListAsync()
```

### Pagination
```csharp
await _context.Entities
	.Skip((page - 1) * pageSize)
	.Take(pageSize)
	.ToListAsync()
```

### Count
```csharp
await _context.Entities.CountAsync()
```

### Check Exists
```csharp
await _context.Entities.AnyAsync(e => e.Id == id)
```

---

## 🎨 Naming Conventions

### Files
- Controllers: `{Entity}Controller.cs` (e.g., `RFQController.cs`)
- Services: `{Entity}Service.cs` (e.g., `RfqPortalRfqService.cs`)
- Repositories: `{Entity}Repository.cs`
- Entities: `{TableName}.cs` (e.g., `RfqPortalRfq.cs`)
- DTOs: `{Entity}Dto.cs`
- Interfaces: `I{ClassName}.cs` (e.g., `IRfqPortalRfqService.cs`)

### Methods
- Get single: `GetByIdAsync(int id)`
- Get all: `GetAllAsync()`
- Create: `CreateAsync(TDto dto)` or `AddAsync(TEntity entity)`
- Update: `UpdateAsync(TDto dto)`
- Delete: `DeleteAsync(int id)`
- Search: `SearchAsync(criteria)`

### Variables
- Entities: `rfq`, `rfqEntity`, `entity`
- DTOs: `rfqDto`, `dto`
- Lists: `rfqs`, `rfqList`
- Services: `_rfqService`, `_service`
- Repositories: `_rfqRepository`, `_repository`

---

## ✅ Checklist: Adding New Feature

- [ ] Write down what you want to do
- [ ] Decide which table(s) you need
- [ ] Create/Update Entity (if needed)
- [ ] Create/Update DTO (if needed)
- [ ] Add Repository method + interface
- [ ] Implement Repository method
- [ ] Add Service method + interface
- [ ] Implement Service method (with validation)
- [ ] Add Controller endpoint
- [ ] Test in Swagger
- [ ] Test edge cases (empty, null, invalid data)
- [ ] Check error handling

---

## 🚨 Red Flags (Things to Avoid)

❌ Controller directly accessing `_context`
❌ Service directly accessing `_context`
❌ Business logic in Controller
❌ SQL queries in Repository (no LINQ)
❌ Returning Entity from Controller (return DTO!)
❌ Missing `async`/`await`
❌ Forgetting to add method to Interface
❌ No error handling
❌ Not validating input

---

*Print this page and keep it handy while coding!*
