# 🚀 Beginner Workflow - Step by Step

## 📋 How to Add a New Feature from Scratch

This guide shows **exactly** how to add a new API endpoint to your project.

---

## 🎯 Example Task: Add "Search RFQs by Title"

We want to create an endpoint: `GET /api/rfq/search?title=project`

---

## Step-by-Step Process

### 🗂️ Step 1: Plan Your Feature (5 minutes)

**Ask Yourself:**
1. What data do I need? (Input: title as string)
2. What should I return? (Output: List of RFQs)
3. What business rules? (Case-insensitive search)
4. Which table? (rfq_portal_rfqs)

**Write it down:**
```
Feature: Search RFQs by Title
Input: title (string)
Output: List of RfqPortalRfqDto
Logic: Find all RFQs where title contains the search term
```

---

### 🗄️ Step 2: Add Repository Method (Start from bottom - Database layer)

**File:** `Repositories/RfqPortalRfqRepository.cs`

```csharp
// Add this method to the repository
public async Task<List<RfqPortalRfq>> SearchByTitleAsync(string title)
{
	// Database query: Find RFQs where title contains search term
	return await _context.RfqPortalRfqs
		.Where(rfq => rfq.Title.Contains(title))
		.OrderByDescending(rfq => rfq.CreatedAt)
		.ToListAsync();
}
```

**File:** `Repositories/Interfaces/IRfqPortalRfqRepository.cs`

```csharp
// Add this to the interface
Task<List<RfqPortalRfq>> SearchByTitleAsync(string title);
```

**🎓 What's happening:**
- `.Where()` filters the data (like WHERE in SQL)
- `.Contains()` does partial match (like LIKE '%title%' in SQL)
- `.OrderByDescending()` sorts newest first
- `.ToListAsync()` executes query and returns list

---

### 🧠 Step 3: Add Service Method (Business logic layer)

**File:** `Services/RfqPortalRfqService.cs`

```csharp
// Add this method to the service
public async Task<List<RfqPortalRfqDto>> SearchByTitleAsync(string title)
{
	// Business rule: Don't search if title is empty
	if (string.IsNullOrWhiteSpace(title))
	{
		return new List<RfqPortalRfqDto>(); // Return empty list
	}

	// Business rule: Trim whitespace and convert to lowercase for better search
	title = title.Trim().ToLower();

	// Call repository
	var rfqs = await _repository.SearchByTitleAsync(title);

	// Convert entities to DTOs
	return rfqs.Select(rfq => new RfqPortalRfqDto
	{
		RfqId = rfq.RfqId,
		RfqNumber = rfq.RfqNumber,
		ClientOrganizationId = rfq.ClientOrganizationId,
		Title = rfq.Title,
		Description = rfq.Description,
		Industry = rfq.Industry,
		Category = rfq.Category,
		ManufacturingProcess = rfq.ManufacturingProcess,
		Material = rfq.Material,
		LocationCity = rfq.LocationCity,
		LocationState = rfq.LocationState,
		ResponseDeadline = rfq.ResponseDeadline,
		RfqStatus = rfq.RfqStatus,
		PublishedDate = rfq.PublishedDate,
		CreatedByUserId = rfq.CreatedByUserId,
		AwardedQuoteId = rfq.AwardedQuoteId,
		CreatedAt = rfq.CreatedAt,
		UpdatedAt = rfq.UpdatedAt
	}).ToList();
}
```

**File:** `Services/Interfaces/IRfqPortalRfqService.cs`

```csharp
// Add this to the interface
Task<List<RfqPortalRfqDto>> SearchByTitleAsync(string title);
```

**🎓 What's happening:**
- Validates input (empty check)
- Normalizes data (trim, lowercase)
- Calls repository
- Converts Entity → DTO

---

### 🚪 Step 4: Add Controller Endpoint (HTTP request handler)

**File:** `Controllers/RFQController.cs`

```csharp
/// <summary>
/// Search RFQs by title
/// </summary>
/// <param name="title">Search term for RFQ title</param>
/// <returns>List of matching RFQs</returns>
[HttpGet("search")]
public async Task<IActionResult> SearchByTitle([FromQuery] string title)
{
	try
	{
		// Input validation
		if (string.IsNullOrWhiteSpace(title))
		{
			return BadRequest(new ApiResponse<object>
			{
				IsSuccess = false,
				Message = "Search term is required",
				Data = null
			});
		}

		// Call service
		var rfqs = await _rfqService.SearchByTitleAsync(title);

		// Return response
		return Ok(new ApiResponse<List<RfqPortalRfqDto>>
		{
			IsSuccess = true,
			Message = $"Found {rfqs.Count} RFQs matching '{title}'",
			Data = rfqs
		});
	}
	catch (Exception ex)
	{
		return StatusCode(500, new ApiResponse<object>
		{
			IsSuccess = false,
			Message = "An error occurred while searching RFQs",
			Data = null
		});
	}
}
```

**🎓 What's happening:**
- `[HttpGet("search")]` creates endpoint: `/api/rfq/search`
- `[FromQuery]` gets parameter from URL: `?title=project`
- Validates input
- Calls service
- Returns formatted JSON response

---

### ✅ Step 5: Test Your Feature

#### Option 1: Using Swagger UI (Easiest)
1. Run the project (F5)
2. Browser opens to `/swagger`
3. Find `GET /api/rfq/search`
4. Click "Try it out"
5. Enter title: `project`
6. Click "Execute"
7. See results!

#### Option 2: Using Postman
```
GET http://localhost:7238/api/rfq/search?title=project
```

#### Expected Response:
```json
{
  "isSuccess": true,
  "message": "Found 3 RFQs matching 'project'",
  "data": [
	{
	  "rfqId": 1,
	  "rfqNumber": "RFQ-001",
	  "title": "New Project Requirements",
	  "description": "...",
	  ...
	},
	...
  ]
}
```

---

## 📊 Complete Code Flow Diagram

```
User Request
	│
	↓
GET /api/rfq/search?title=project
	│
	↓
┌─────────────────────────────────────┐
│ RFQController.cs                    │
│ SearchByTitle(string title)         │
│                                     │
│ 1. Validate input                   │
│ 2. Call service                     │
│ 3. Return response                  │
└──────────────┬──────────────────────┘
			   │ _rfqService.SearchByTitleAsync(title)
			   ↓
┌─────────────────────────────────────┐
│ RfqPortalRfqService.cs              │
│ SearchByTitleAsync(string title)    │
│                                     │
│ 1. Validate & normalize             │
│ 2. Call repository                  │
│ 3. Convert Entity → DTO             │
└──────────────┬──────────────────────┘
			   │ _repository.SearchByTitleAsync(title)
			   ↓
┌─────────────────────────────────────┐
│ RfqPortalRfqRepository.cs           │
│ SearchByTitleAsync(string title)    │
│                                     │
│ 1. Build LINQ query                 │
│ 2. Execute query                    │
│ 3. Return entities                  │
└──────────────┬──────────────────────┘
			   │ SELECT * FROM rfq_portal_rfqs WHERE...
			   ↓
┌─────────────────────────────────────┐
│ SQL Server Database                 │
│                                     │
│ Returns matching rows               │
└─────────────────────────────────────┘
```

---

## 🎯 Quick Reference: The Order

**Always follow this order when adding a feature:**

1. ✅ **Repository** (Database operations) - BOTTOM LAYER
2. ✅ **Repository Interface** (Contract)
3. ✅ **Service** (Business logic) - MIDDLE LAYER
4. ✅ **Service Interface** (Contract)
5. ✅ **Controller** (HTTP endpoint) - TOP LAYER
6. ✅ **Test** (Verify it works)

**Why this order?**
- Build from foundation up (database → API)
- Each layer depends on the one below
- Test after each layer if you want

---

## 🔧 Common Variations

### Variation 1: Search by Multiple Fields

**Repository:**
```csharp
public async Task<List<RfqPortalRfq>> SearchAsync(string title, string category)
{
	var query = _context.RfqPortalRfqs.AsQueryable();

	if (!string.IsNullOrEmpty(title))
		query = query.Where(r => r.Title.Contains(title));

	if (!string.IsNullOrEmpty(category))
		query = query.Where(r => r.Category == category);

	return await query.ToListAsync();
}
```

**Controller:**
```csharp
[HttpGet("search")]
public async Task<IActionResult> Search(
	[FromQuery] string? title, 
	[FromQuery] string? category)
{
	var rfqs = await _rfqService.SearchAsync(title, category);
	return Ok(rfqs);
}
```

### Variation 2: Get Single Item by ID

**Repository:**
```csharp
public async Task<RfqPortalRfq?> GetByIdAsync(int id)
{
	return await _context.RfqPortalRfqs
		.FirstOrDefaultAsync(r => r.RfqId == id);
}
```

**Service:**
```csharp
public async Task<RfqPortalRfqDto?> GetByIdAsync(int id)
{
	var rfq = await _repository.GetByIdAsync(id);

	if (rfq == null)
		return null;

	return MapToDto(rfq);
}
```

**Controller:**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
	var rfq = await _rfqService.GetByIdAsync(id);

	if (rfq == null)
		return NotFound(new ApiResponse<object>
		{
			IsSuccess = false,
			Message = $"RFQ with ID {id} not found"
		});

	return Ok(new ApiResponse<RfqPortalRfqDto>
	{
		IsSuccess = true,
		Message = "RFQ retrieved successfully",
		Data = rfq
	});
}
```

### Variation 3: Create New Item

**Repository:**
```csharp
public async Task<RfqPortalRfq> AddAsync(RfqPortalRfq rfq)
{
	_context.RfqPortalRfqs.Add(rfq);
	await _context.SaveChangesAsync();
	return rfq;
}
```

**Service:**
```csharp
public async Task<RfqPortalRfqDto> CreateAsync(RfqPortalRfqDto dto)
{
	// Business logic: Generate RFQ number
	dto.RfqNumber = $"RFQ-{DateTime.UtcNow.Ticks}";
	dto.RfqStatus = "Draft";
	dto.CreatedAt = DateTime.UtcNow;

	var entity = MapToEntity(dto);
	var created = await _repository.AddAsync(entity);

	return MapToDto(created);
}
```

**Controller:**
```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] RfqPortalRfqDto dto)
{
	if (!ModelState.IsValid)
		return BadRequest(ModelState);

	var created = await _rfqService.CreateAsync(dto);

	return CreatedAtAction(
		nameof(GetById), 
		new { id = created.RfqId }, 
		new ApiResponse<RfqPortalRfqDto>
		{
			IsSuccess = true,
			Message = "RFQ created successfully",
			Data = created
		});
}
```

---

## ⚠️ Common Mistakes

### ❌ Mistake 1: Skipping Layers
```csharp
// WRONG: Controller calling Repository directly
[HttpGet]
public async Task<IActionResult> GetAll()
{
	var rfqs = await _repository.GetAllAsync(); // ❌ NO!
	return Ok(rfqs);
}
```

✅ **CORRECT:**
```csharp
[HttpGet]
public async Task<IActionResult> GetAll()
{
	var rfqs = await _rfqService.GetAllAsync(); // ✅ YES!
	return Ok(rfqs);
}
```

### ❌ Mistake 2: Business Logic in Controller
```csharp
// WRONG: Calculation in Controller
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
	var rfq = await _rfqService.GetByIdAsync(id);
	rfq.DiscountedPrice = rfq.Price * 0.9; // ❌ NO!
	return Ok(rfq);
}
```

✅ **CORRECT:** Put logic in Service
```csharp
// Service
public async Task<RfqPortalRfqDto> GetByIdAsync(int id)
{
	var rfq = await _repository.GetByIdAsync(id);
	var dto = MapToDto(rfq);
	dto.DiscountedPrice = CalculateDiscount(dto.Price); // ✅ YES!
	return dto;
}
```

### ❌ Mistake 3: Database Queries in Service
```csharp
// WRONG: LINQ query in Service
public async Task<List<RfqPortalRfqDto>> GetAllAsync()
{
	var rfqs = await _context.RfqPortalRfqs.ToListAsync(); // ❌ NO!
	return MapToDtos(rfqs);
}
```

✅ **CORRECT:** Queries in Repository
```csharp
// Service calls Repository
public async Task<List<RfqPortalRfqDto>> GetAllAsync()
{
	var rfqs = await _repository.GetAllAsync(); // ✅ YES!
	return MapToDtos(rfqs);
}
```

---

## 🎓 Practice Exercise

**Task:** Add an endpoint to get RFQs by status

Requirements:
- Endpoint: `GET /api/rfq/by-status?status=Draft`
- Filter RFQs by status (case-insensitive)
- Return list of RFQs

**Checklist:**
- [ ] Add method to IRfqPortalRfqRepository
- [ ] Implement method in RfqPortalRfqRepository
- [ ] Add method to IRfqPortalRfqService
- [ ] Implement method in RfqPortalRfqService
- [ ] Add endpoint in RFQController
- [ ] Test in Swagger

**Solution:** Follow the SearchByTitle example above, but replace `Title` with `RfqStatus`

---

## 📖 Next Steps

1. ✅ Practice: Add 2-3 simple features following this guide
2. ✅ Read: `BEGINNER_TASKS.md` for more exercises
3. ✅ Study: Existing code in RFQController.cs
4. ✅ Experiment: Try different query types in Repository

---

*Remember: Start from Repository (bottom) and work your way up to Controller (top). Build the foundation first!*
