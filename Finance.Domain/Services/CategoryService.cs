using Finance.Domain.Interfaces.ICategory;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Entities.Entities;

namespace Finance.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategory _iCategory;

    public CategoryService(ICategory iCategory)
    {
        _iCategory = iCategory;
    }

    public async Task AddCategory(Category category)
    {
        var valid = category.ValidatePropertyString(category.Name, "Name");
        if(valid) 
            await _iCategory.Add(category);
    }

    public async Task UpdateCategory(Category category)
    {
        var valid = category.ValidatePropertyString(category.Name, "Name");
        if (valid)
            await _iCategory.Update(category);
    }
}
