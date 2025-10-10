using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext,
            IQueryable<T> queryable, int recordsPerPage)
        {
            double count = await queryable.CountAsync();
            double pagesQuantity = Math.Ceiling(count / recordsPerPage);

            httpContext.Response.Headers["pagesQuantity"] = pagesQuantity.ToString();

            httpContext.Response.Headers["X-Total-Count"] = count.ToString();

        }
    }
}
