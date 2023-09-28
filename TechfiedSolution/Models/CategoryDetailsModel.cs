using System.Collections.Generic;
using TechfiedSolution.Entities;

namespace TechfiedSolution.Models;

public class CategoryDetailsModel
{
    public IEnumerable<GroupedCategoryItemsByCategoryModel> GroupedCategoryItemsByCategoryModels { get; set; }
    public IEnumerable<Category> Categories { get; set; }

}
