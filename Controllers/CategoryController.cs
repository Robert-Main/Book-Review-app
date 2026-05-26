using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReview.Dtos;
using BookReview.interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryInterface _categoryRepository;
        public CategoryController(ICategoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(new
            {
                success = true,
                message = "Categories retrieved successfully",
                data = new {
                    categories = categories.Select(c => new CategoryDtos
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound(new
                {
                    success = false,
                    message = "Category not found"
                });
            return Ok(new
            {
                success = true,
                message = "Category retrieved successfully",
                data = new {
                    category = new CategoryDtos
                    {
                        Id = category.Id,
                        Name = category.Name
                    }
                }
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });
            var result = await _categoryRepository.CreateCategoryAsync(category);
            if (!result)
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while creating the category"
                });
            return Ok(new
            {
                success = true,
                message = "Category created successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid model state"
                });
            category.Id = id;
            var result = await _categoryRepository.UpdateCategoryAsync(category);
            if (!result)
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while updating the category"
                });
            return Ok(new
            {
                success = true,
                message = "Category updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategoryAsync(id);
            if (!result)
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while deleting the category"
                });
            return Ok(new
            {
                success = true,
                message = "Category deleted successfully"
            });
        }
    }
}