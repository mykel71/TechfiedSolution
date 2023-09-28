using System.Collections.Generic;
using System.Threading.Tasks;
using TechfiedSolution.Entities;

namespace TechfiedSolution.Data;

public interface IDataFunctions
{
    Task UpdateUserCategoryEntityAsync(List<UserCategory> userCategoryItemsToDelete, List<UserCategory> userCategoryItemsToAdd);
}
