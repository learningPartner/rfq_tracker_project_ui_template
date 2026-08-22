# 🎯 Beginner Practice Tasks

## 📚 Learning by Doing

This file contains **10 practical exercises** to help you master the architecture.

**Difficulty Levels:**
- 🟢 Easy (Beginner)
- 🟡 Medium (Intermediate)
- 🔴 Hard (Advanced)

---

## 🟢 Task 1: Get RFQ by Status (Easy)

### Goal
Create an endpoint to filter RFQs by status.

### Requirements
- Endpoint: `GET /api/rfq/by-status?status=Draft`
- Return all RFQs with matching status
- Case-insensitive comparison

### Files to Modify
1. `IRfqPortalRfqRepository.cs`
2. `RfqPortalRfqRepository.cs`
3. `IRfqPortalRfqService.cs`
4. `RfqPortalRfqService.cs`
5. `RFQController.cs`

### Expected Response
```json
{
  "isSuccess": true,
  "message": "Found 5 RFQs with status 'Draft'",
  "data": [
	{
	  "rfqId": 1,
	  "rfqStatus": "Draft",
	  ...
	}
  ]
}
```

### Hints
- Similar to SearchByTitle
- Use `.Where(r => r.RfqStatus.ToLower() == status.ToLower())`
- Validate that status is not empty

---

## 🟢 Task 2: Count RFQs (Easy)

### Goal
Create an endpoint to get the total count of RFQs.

### Requirements
- Endpoint: `GET /api/rfq/count`
- Return total number of RFQs in database

### Expected Response
```json
{
  "isSuccess": true,
  "message": "Total RFQs in system",
  "data": 42
}
```

### Hints
- Repository: `return await _context.RfqPortalRfqs.CountAsync();`
- Service: Just pass through the count
- Controller: Return count as data

---

## 🟢 Task 3: Get Latest RFQs (Easy)

### Goal
Get the 5 most recently created RFQs.

### Requirements
- Endpoint: `GET /api/rfq/latest`
- Return 5 most recent RFQs
- Ordered by CreatedAt descending

### Expected Response
```json
{
  "isSuccess": true,
  "message": "Latest 5 RFQs retrieved",
  "data": [...]
}
```

### Hints
- Use `.OrderByDescending(r => r.CreatedAt)`
- Use `.Take(5)`
- Example: `.OrderByDescending(...).Take(5).ToListAsync()`

---

## 🟡 Task 4: Update RFQ Status (Medium)

### Goal
Create an endpoint to update only the status of an RFQ.

### Requirements
- Endpoint: `PATCH /api/rfq/{id}/status`
- Body: `{ "status": "Published" }`
- Update only the status field
- Validate status is one of: Draft, Published, Closed, Awarded, Cancelled

### Expected Response
```json
{
  "isSuccess": true,
  "message": "RFQ status updated to 'Published'",
  "data": {
	"rfqId": 1,
	"rfqStatus": "Published",
	...
  }
}
```

### Files to Modify
1. Create DTO: `UpdateRfqStatusDto.cs`
2. Repository: Add `UpdateAsync` method
3. Service: Add validation logic
4. Controller: Add PATCH endpoint

### Validation Rules (in Service)
```csharp
private bool IsValidStatus(string status)
{
	var validStatuses = new[] { "Draft", "Published", "Closed", "Awarded", "Cancelled" };
	return validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase);
}
```

---

## 🟡 Task 5: Search with Pagination (Medium)

### Goal
Add pagination to the search functionality.

### Requirements
- Endpoint: `GET /api/rfq/search?title=project&page=1&pageSize=10`
- Return paginated results
- Default: page=1, pageSize=10
- Include total count in response

### Expected Response
```json
{
  "isSuccess": true,
  "message": "Page 1 of 3 (total: 25 items)",
  "data": {
	"items": [...],
	"currentPage": 1,
	"pageSize": 10,
	"totalPages": 3,
	"totalCount": 25
  }
}
```

### Create New DTO
```csharp
// DTOs/PaginatedResult.cs
public class PaginatedResult<T>
{
	public List<T> Items { get; set; }
	public int CurrentPage { get; set; }
	public int PageSize { get; set; }
	public int TotalPages { get; set; }
	public int TotalCount { get; set; }
}
```

### Hints
```csharp
// In Repository
var totalCount = await query.CountAsync();
var items = await query
	.Skip((page - 1) * pageSize)
	.Take(pageSize)
	.ToListAsync();
```

---

## 🟡 Task 6: Get RFQs by Date Range (Medium)

### Goal
Filter RFQs created within a date range.

### Requirements
- Endpoint: `GET /api/rfq/by-date-range?startDate=2024-01-01&endDate=2024-12-31`
- Both dates are optional
- If startDate is null, show all up to endDate
- If endDate is null, show all from startDate

### Expected Response
```json
{
  "isSuccess": true,
  "message": "Found 15 RFQs between 01/01/2024 and 12/31/2024",
  "data": [...]
}
```

### Hints
```csharp
// In Repository
var query = _context.RfqPortalRfqs.AsQueryable();

if (startDate.HasValue)
	query = query.Where(r => r.CreatedAt >= startDate.Value);

if (endDate.HasValue)
	query = query.Where(r => r.CreatedAt <= endDate.Value);

return await query.ToListAsync();
```

---

## 🔴 Task 7: Bulk Status Update (Hard)

### Goal
Update status for multiple RFQs at once.

### Requirements
- Endpoint: `PATCH /api/rfq/bulk-update-status`
- Body: `{ "rfqIds": [1, 2, 3], "newStatus": "Closed" }`
- Update all specified RFQs
- Return count of updated items

### Expected Response
```json
{
  "isSuccess": true,
  "message": "3 RFQs updated to status 'Closed'",
  "data": {
	"updatedCount": 3,
	"failedIds": []
  }
}
```

### Create DTOs
```csharp
// DTOs/BulkUpdateStatusDto.cs
public class BulkUpdateStatusDto
{
	public List<int> RfqIds { get; set; }
	public string NewStatus { get; set; }
}

// DTOs/BulkUpdateResultDto.cs
public class BulkUpdateResultDto
{
	public int UpdatedCount { get; set; }
	public List<int> FailedIds { get; set; }
}
```

---

## 🔴 Task 8: Advanced Search with Multiple Filters (Hard)

### Goal
Create a comprehensive search with multiple optional filters.

### Requirements
- Endpoint: `GET /api/rfq/advanced-search`
- Filters: title, category, industry, status, minDate, maxDate
- All filters are optional
- Combine multiple filters with AND logic

### Query Parameters
```
?title=project
&category=Electronics
&industry=Manufacturing
&status=Published
&minDate=2024-01-01
&maxDate=2024-12-31
```

### Expected Response
```json
{
  "isSuccess": true,
  "message": "Found 8 RFQs matching criteria",
  "data": [...]
}
```

### Create Search DTO
```csharp
// DTOs/RfqSearchCriteriaDto.cs
public class RfqSearchCriteriaDto
{
	public string? Title { get; set; }
	public string? Category { get; set; }
	public string? Industry { get; set; }
	public string? Status { get; set; }
	public DateTime? MinDate { get; set; }
	public DateTime? MaxDate { get; set; }
}
```

### Hints
```csharp
// In Repository
var query = _context.RfqPortalRfqs.AsQueryable();

if (!string.IsNullOrEmpty(criteria.Title))
	query = query.Where(r => r.Title.Contains(criteria.Title));

if (!string.IsNullOrEmpty(criteria.Category))
	query = query.Where(r => r.Category == criteria.Category);

// ... add more filters

return await query.ToListAsync();
```

---

## 🔴 Task 9: Get RFQ Statistics (Hard)

### Goal
Get statistical summary of RFQs.

### Requirements
- Endpoint: `GET /api/rfq/statistics`
- Return counts by status
- Return counts by category
- Return average RFQs per month

### Expected Response
```json
{
  "isSuccess": true,
  "message": "RFQ statistics retrieved",
  "data": {
	"totalRfqs": 100,
	"byStatus": {
	  "Draft": 45,
	  "Published": 30,
	  "Closed": 20,
	  "Awarded": 5
	},
	"byCategory": {
	  "Electronics": 40,
	  "Mechanical": 35,
	  "Software": 25
	},
	"averagePerMonth": 8.3
  }
}
```

### Create DTO
```csharp
// DTOs/RfqStatisticsDto.cs
public class RfqStatisticsDto
{
	public int TotalRfqs { get; set; }
	public Dictionary<string, int> ByStatus { get; set; }
	public Dictionary<string, int> ByCategory { get; set; }
	public double AveragePerMonth { get; set; }
}
```

### Hints
```csharp
// In Repository or Service
var byStatus = await _context.RfqPortalRfqs
	.GroupBy(r => r.RfqStatus)
	.Select(g => new { Status = g.Key, Count = g.Count() })
	.ToDictionaryAsync(x => x.Status, x => x.Count);
```

---

## 🔴 Task 10: Delete RFQ with Validation (Hard)

### Goal
Implement soft delete with business rule validation.

### Requirements
- Endpoint: `DELETE /api/rfq/{id}`
- Can only delete RFQs with status "Draft"
- Return error if trying to delete Published/Closed/Awarded RFQs
- Implement soft delete (don't actually remove from database)

### Expected Response (Success)
```json
{
  "isSuccess": true,
  "message": "RFQ deleted successfully",
  "data": null
}
```

### Expected Response (Error)
```json
{
  "isSuccess": false,
  "message": "Cannot delete RFQ with status 'Published'. Only Draft RFQs can be deleted.",
  "data": null
}
```

### Soft Delete Implementation

**Step 1:** Add property to Entity
```csharp
// Entities/RfqPortalRfq.cs
public bool IsDeleted { get; set; } = false;
public DateTime? DeletedAt { get; set; }
```

**Step 2:** Update database configuration
```csharp
// Data/Configurations/RfqPortalRfqConfiguration.cs
entity.Property(e => e.IsDeleted)
	.HasColumnName("is_deleted")
	.HasDefaultValue(false);

entity.Property(e => e.DeletedAt)
	.HasColumnName("deleted_at")
	.HasColumnType("datetime2(7)");

// Add query filter to exclude deleted items
entity.HasQueryFilter(e => !e.IsDeleted);
```

**Step 3:** Add migration
```bash
Add-Migration AddSoftDeleteToRfq
Update-Database
```

---

## ✅ Completion Checklist

Mark items as you complete them:

### Easy Tasks
- [ ] Task 1: Get RFQ by Status
- [ ] Task 2: Count RFQs
- [ ] Task 3: Get Latest RFQs

### Medium Tasks
- [ ] Task 4: Update RFQ Status
- [ ] Task 5: Search with Pagination
- [ ] Task 6: Get RFQs by Date Range

### Hard Tasks
- [ ] Task 7: Bulk Status Update
- [ ] Task 8: Advanced Search
- [ ] Task 9: Get Statistics
- [ ] Task 10: Soft Delete

---

## 🎯 Learning Objectives

After completing these tasks, you will understand:

✅ How to add new endpoints following the architecture
✅ Repository pattern and LINQ queries
✅ Service layer business logic and validation
✅ Controller HTTP methods (GET, POST, PATCH, DELETE)
✅ DTOs for input and output
✅ Error handling and validation
✅ Pagination and filtering
✅ Aggregation and statistics
✅ Soft delete pattern

---

## 📖 Solution Files

After attempting each task:
1. Test your solution in Swagger
2. Verify the response format
3. Check for edge cases
4. Compare with existing code patterns

---

## 💡 Tips for Success

1. **Start Small**: Begin with easy tasks
2. **Read Existing Code**: Look at similar features
3. **Test Frequently**: Use Swagger after each step
4. **Follow Patterns**: Maintain consistency with existing code
5. **Ask Questions**: If stuck, review ARCHITECTURE.md and BEGINNER_WORKFLOW.md

---

## ⚠️ Common Pitfalls

1. ❌ Forgetting to add method to interface
2. ❌ Skipping validation in Service layer
3. ❌ Not handling null cases
4. ❌ Returning entities instead of DTOs
5. ❌ Missing async/await keywords
6. ❌ Not testing edge cases

---

## 🚀 Bonus Challenges (After completing all tasks)

1. Add unit tests for Service layer
2. Add input validation attributes to DTOs
3. Implement caching for frequently accessed data
4. Add sorting options to list endpoints
5. Create a dashboard endpoint combining multiple statistics

---

*Remember: Learning takes time. Complete tasks at your own pace and don't hesitate to refer back to the architecture documentation!*
