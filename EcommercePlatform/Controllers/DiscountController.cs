using Ecommerce.DataAccess.Services.Discount;
using Ecommerce.Entities.DTO.Discount;
using Ecommerce.Entities.Shared.Bases;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
            private readonly IDiscountService _service;
            private readonly ResponseHandler _responseHandler;
            private readonly IValidator<CreateDiscountRequest> _createValidator;
            private readonly IValidator<UpdateDiscountRequest> _updateValidator;
            private readonly IValidator<GetDiscountsQuery> _getDiscountsValidator;

            public DiscountController(
                IDiscountService service,
                ResponseHandler responseHandler,
                IValidator<CreateDiscountRequest> createValidator,
                IValidator<UpdateDiscountRequest> updateValidator,
                IValidator<GetDiscountsQuery> getDiscountsValidator)
            {
                _service = service;
                _responseHandler = responseHandler;
                _createValidator = createValidator;
                _updateValidator = updateValidator;
                _getDiscountsValidator = getDiscountsValidator;
            }

            [HttpGet("all-discounts")]
            public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = false)
            {
                var result = await _service.GetAllAsync(onlyActive);
                return StatusCode((int)result.StatusCode, result);
            }

            [HttpGet("discounts/paginated")]
            public async Task<IActionResult> GetDiscounts([FromQuery] GetDiscountsQuery query)
            {
                var validationResult = await _getDiscountsValidator.ValidateAsync(query);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                     _responseHandler.BadRequest<object>(errors));
                }

                var response = await _service.GetPaginatedAsync(query);
                return StatusCode((int)response.StatusCode, response);
            }

            [HttpGet("{id:guid}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var result = await _service.GetByIdAsync(id);
                return StatusCode((int)result.StatusCode, result);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateDiscountRequest request)
            {
                ValidationResult validation = await _createValidator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    string errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                    var bad = _responseHandler.BadRequest<object>(errors);
                    return StatusCode((int)bad.StatusCode, bad);
                }

                var result = await _service.CreateAsync(request);
                return StatusCode((int)result.StatusCode, result);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDiscountRequest request)
            {
                ValidationResult validation = await _updateValidator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    string errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                    var bad = _responseHandler.BadRequest<object>(errors);
                    return StatusCode((int)bad.StatusCode, bad);
                }

                var result = await _service.UpdateAsync(id, request);
                return StatusCode((int)result.StatusCode, result);
            }

            [HttpPut("{id}/deactivate")]
            public async Task<IActionResult> Deactivate(Guid id)
            {
                var result = await _service.DeactivateAsync(id);
                return StatusCode((int)result.StatusCode, result);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var response = await _service.DeleteAsync(id);
                return StatusCode((int)response.StatusCode, response);
            }

    }
}
