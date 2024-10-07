using Discs.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DiscGolfManagerAspire.DiscsApiService
{
    public static class DiscsApi
    {
        public static RouteGroupBuilder MapDiscsApi(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("api/");

            group.MapGet("discs", async (DiscGolfDBContext context) =>
            {
                return await context.Discs.ToListAsync();
            });

            group.MapPost("discs/create", async (DiscGolfDBContext context, Disc disc) =>
            {
                context.Discs.Add(disc);
                await context.SaveChangesAsync();

                return Results.Ok();

            });

            group.MapPut("discs/edit/{id}", async (int id, Disc disc, DiscGolfDBContext context) =>
            {
                if (id != disc.Id) return Results.BadRequest();

                if (!context.Discs.Any(e=>e.Id == id))
                {
                    return Results.NotFound();
                }
                context.Entry(disc).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Results.Ok();

            });

            return group;
        }

    }
}
