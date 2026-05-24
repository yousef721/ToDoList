namespace TodoList.MVC.Controllers;

[Authorize]
public class TodoController : Controller
{
    private readonly ITodoService _todoService;
    private readonly IMapper _mapper;

    public TodoController(ITodoService todoService, IMapper mapper)
    {
        _todoService = todoService;
        _mapper = mapper;
    }

    private string UserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new Exception("User not authenticated");

    public async Task<IActionResult> Index(TodoQueryParameters parameters, string? filter)
    {
        parameters.UserId = UserId;

        var paged = await _todoService.GetPagedAsync(parameters, filter);

        var vm = new TodoListViewModel
        {
            PagedResult = new PagedResult<TodoListItemViewModel>
            {
                Items = _mapper.Map<IEnumerable<TodoListItemViewModel>>(paged.Items),
                TotalCount = paged.TotalCount,
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize
            },

            SearchTerm = parameters.SearchTerm,
            Filter = filter,

            TotalAll = await _todoService.CountByFilterAsync(UserId, null),
            TotalCompleted = await _todoService.CountByFilterAsync(UserId, "completed"),
            TotalPending = await _todoService.CountByFilterAsync(UserId, "pending")
        };

        return View(vm);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTodoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please fix validation errors.";
            return View(vm);
        }

        try
        {
            var dto = _mapper.Map<CreateTodoDto>(vm);
            dto.UserId = UserId;

            await _todoService.CreateAsync(dto);

            TempData["Success"] = "Todo created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            TempData["Error"] = "Failed to create todo.";

            return View(vm);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _todoService.GetByIdAsync(id, UserId);

        if (dto == null)
        {
            TempData["Error"] = "Todo not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(_mapper.Map<EditTodoViewModel>(dto));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditTodoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please fix validation errors.";
            return View(vm);
        }

        try
        {
            var dto = _mapper.Map<UpdateTodoDto>(vm);
            dto.UserId = UserId;

            await _todoService.UpdateAsync(dto);

            TempData["Success"] = "Todo updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            TempData["Error"] = "Failed to update todo.";

            return View(vm);
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var dto = await _todoService.GetByIdAsync(id, UserId);

        if (dto == null)
        {
            TempData["Error"] = "Todo not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(_mapper.Map<TodoListItemViewModel>(dto));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _todoService.DeleteAsync(id, UserId);

            TempData["Success"] = "Todo deleted successfully.";
        }
        catch
        {
            TempData["Error"] = "Failed to delete todo.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleCompleted(int id)
    {
        try
        {
            await _todoService.ToggleCompletedAsync(id, UserId);

            TempData["Success"] = "Todo status updated.";
        }
        catch
        {
            TempData["Error"] = "Failed to update todo status.";
        }

        return RedirectToAction(nameof(Index));
    }
}