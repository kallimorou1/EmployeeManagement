using EmployeeManagement.Shared.Models;
using System.Runtime.InteropServices;

namespace EmployeeManagement.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate <T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.QuantityPerPage)
                .Take(pagination.QuantityPerPage);
        }
        }
}
