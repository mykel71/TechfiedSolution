using System.Collections.Generic;
using TechfiedSolution.Entities;

namespace TechfiedSolution.Models;

public class CategoriesToUserModel
{
    public string UserId { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Category> CategoriesSelected { get; set; }

}
