using DiscGolfManagerAspire.Utils;
using Discs.Models;
//using DiscGolfManagerAspire.Web.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
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

            group.MapGet("discs/{id}", (DiscGolfDBContext context, int id) =>
            {
                Disc disc = context.Discs.Where(c => c.Id == id).FirstOrDefault() ?? new Disc();
                return disc;
            });

            group.MapGet("discs/{discType}/{numDiscs}", (DiscGolfDBContext context, DiscType discType, int numDiscs) =>
            {
                List<Disc> discs = context.Discs.Where(c => c.DiscType == discType).ToList();
                discs = RandUtils.GetRandomDiscs(discs, discType, numDiscs);
                return discs;
            }
            );

            group.MapPost("discs/create", async (DiscGolfDBContext context, Disc disc) =>
            {
                context.Discs.Add(disc);
                await context.SaveChangesAsync();

                return Results.Ok();

            });

            group.MapPut("discs/edit/{id}", async (int id, Disc disc, DiscGolfDBContext context) =>
            {
                if (id != disc.Id) return Results.BadRequest();

                if (!context.Discs.Any(e => e.Id == id))
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
