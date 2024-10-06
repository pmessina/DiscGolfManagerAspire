using Discs.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DiscGolfManagerAspire.ApiService
{
    public static class DiscsApi
    {
        public static RouteGroupBuilder MapDiscsApi(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("");

            group.MapGet("api/discs", async (DiscGolfDBContext context) =>
            {
                return await context.Discs.ToListAsync();

            });

            return group;
        }
    }
}
