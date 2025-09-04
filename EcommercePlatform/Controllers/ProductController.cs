using Ecommerce.Entities.DTO.Product;
using Ecommerce.Entities.Shared.Bases;
using Ecommerce.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ResponseHandler _responseHandler;


    public ProductController(
        IProductService productService,
        ResponseHandler responseHandler)
    {
        _productService = productService;
        _responseHandler = responseHandler;

    }

    [HttpPost]
    public async Task<ActionResult<Response<ReadProductDto>>> Create([FromForm] CreateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage);
            return _responseHandler.BadRequest<ReadProductDto>(string.Join("; ", errors));
        }

        var result = await _productService.CreateProductAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Response<ReadProductDto>>> Update(Guid id, [FromForm] UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage);
            return _responseHandler.BadRequest<ReadProductDto>(string.Join("; ", errors));
        }
        

        var result = await _productService.UpdateProductAsync(id , dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Response<bool>>> Delete(Guid id)
    {
       
        var result = await _productService.DeleteProductAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<ReadProductDto>>> GetById(Guid id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ReadProductDto>>>> GetAll()
    {
        var result = await _productService.GetAllProductsAsync();
        return StatusCode((int)result.StatusCode, result);
    }
}

