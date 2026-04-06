namespace CycleMoney.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringExpensesController : ControllerBase
    {
        private readonly IRecurringExpenseService _service;

        public RecurringExpensesController(IRecurringExpenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RecurringExpenseDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, RecurringExpenseDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok($"Expense {id} deleted");
        }
    }
}
