using Finance.Domain.Interfaces.ICategory;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategory _ICategory;
    private readonly ICategoryService _ICategoryService;

    public CategoryController(ICategory ICategory, ICategoryService ICategoryService)
    {
        _ICategory = ICategory;
        _ICategoryService = ICategoryService;
    }

    [HttpGet("/api/ListUserCategories")]
    [Produces("application/json")]
    public async Task<object> ListUserCategories(string userEmail)
    {
        return _ICategory.ListUserCategories(userEmail);
    }

    [HttpPost("/api/AddCategory")]
    [Produces("application/json")]
    public async Task<object> AddCategory(Category category)
    {
        await _ICategoryService.AddCategory(category);

        return category;
    }

    [HttpPut("/api/UpdateCategory")]
    [Produces("application/json")]
    public async Task<object> UpdateCategory(Category category)
    {
        await _ICategoryService.UpdateCategory(category);

        return category;
    }

    [HttpGet("/api/GetCategory")]
    [Produces("application/json")]
    public async Task<object> GetCategory(int id)
    {
        return await _ICategory.GetEntityById(id);
    }

    [HttpDelete("/api/DeleteCategory")]
    [Produces("application/json")]
    public async Task<object> DeleteCategory(int id)
    {
        try
        {
            var category = await _ICategory.GetEntityById(id);

            await _ICategory.Delete(category);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
