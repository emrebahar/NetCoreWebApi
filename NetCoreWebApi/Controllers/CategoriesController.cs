using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.DAL.Context;
using NetCoreWebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using var context = new NetCoreWebApiContext();
            return Ok(context.Categories.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using var context = new NetCoreWebApiContext();
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            using var context = new NetCoreWebApiContext();
            //var updatedCategory = context.Categories.Find(category.Id);
            var updatedCategory = context.Find<Category>(category.Id);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            updatedCategory.Name = category.Name;
            context.Update(updatedCategory);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new NetCoreWebApiContext();
            var deletedCategory = context.Categories.Find(id);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            context.Remove(deletedCategory);
            context.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            using var context = new NetCoreWebApiContext();
            context.Categories.Add(category);
            context.SaveChanges();
            return Created("", category);
        }

        [HttpGet("{id}/Blogs")]
        public IActionResult GetWithBlogsById(int id)
        {
            using var context = new NetCoreWebApiContext();
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryWithBlogs = context.Categories.Where(I => I.Id == id).Include(I => I.Blogs).ToList();
            return Ok(categoryWithBlogs);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            if (file.ContentType == "image/jpeg")
            {
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents/" + newFileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return Created("", file);
            }
            return BadRequest();
            
        }
    }
}
