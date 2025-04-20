using ApplicationLayer.Common;
using ApplicationLayer.Dtos.ProductDTOS;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        
        [HttpGet("get-all-products")]
        public async Task<ActionResult<OperationResult<IEnumerable<ProductDto>>>> GetAllProducts()
        {
            var result = await _productRepo.GetAllAsync();

            if (!result.IsSuccess)
                return NotFound(OperationResult<IEnumerable<ProductDto>>.Failure(result.ErrorMessage!));

            var dtoList = _mapper.Map<IEnumerable<ProductDto>>(result.Data);
            return Ok(OperationResult<IEnumerable<ProductDto>>.Success(dtoList));
        }

        
        [HttpGet("get-product-with-id")]
        public async Task<ActionResult<OperationResult<ProductDto>>> GetProductWithId(int id)
        {
            var result = await _productRepo.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(OperationResult<ProductDto>.Failure(result.ErrorMessage!));

            var dto = _mapper.Map<ProductDto>(result.Data);
            return Ok(OperationResult<ProductDto>.Success(dto));
        }

        
        [HttpPost("create-product")]
        public async Task<ActionResult<OperationResult<ProductDto>>> CreateProduct([FromBody] CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var result = await _productRepo.AddAsync(entity);

            if (!result.IsSuccess)
                return BadRequest(OperationResult<ProductDto>.Failure(result.ErrorMessage!));

            var createdDto = _mapper.Map<ProductDto>(result.Data);
            return Ok(OperationResult<ProductDto>.Success(createdDto));
        }

        
        [HttpPut("update-product-{id}")]
        public async Task<ActionResult<OperationResult<bool>>> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            var existingResult = await _productRepo.GetByIdAsync(id);
            if (!existingResult.IsSuccess)
                return NotFound(OperationResult<bool>.Failure("Product not found"));

            var product = existingResult.Data!;
            product.Name = dto.Name;
            product.Price = dto.Price;

            var result = await _productRepo.UpdateAsync(product);

            return result.IsSuccess
                ? Ok(OperationResult<bool>.Success(true))
                : BadRequest(OperationResult<bool>.Failure(result.ErrorMessage!));
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResult<bool>>> Delete(int id)
        {
            var result = await _productRepo.DeleteAsync(id);

            return result.IsSuccess
                ? Ok(OperationResult<bool>.Success(true))
                : NotFound(OperationResult<bool>.Failure(result.ErrorMessage!));
        }
    }
}
