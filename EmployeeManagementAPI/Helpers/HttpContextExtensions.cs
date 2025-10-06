using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Helpers
{
        public static class HttpContextExtensions
        {
            public static async Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext,
                IQueryable<T> queryable, int recordsPerPage)
            {
                double count = await queryable.CountAsync();
                double pagesQuantity = Math.Ceiling(count / recordsPerPage);
                httpContext.Response.Headers.Add("pagesQuantity", pagesQuantity.ToString());
            }
        }
}
