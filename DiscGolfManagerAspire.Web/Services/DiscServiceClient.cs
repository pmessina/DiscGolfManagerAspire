using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DiscGolfManagerAspire.Web.Services
{
    public class DiscServiceClient
    {
        HttpClient _httpClient;
        NavigationManager manager;

        public DiscServiceClient(HttpClient httpClient, NavigationManager manager)
        {
            _httpClient = httpClient;
            this.manager = manager;
        }

        public async Task<IResult> CreateDisc(Disc disc)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/discs/create", disc);

            if (result.IsSuccessStatusCode)
            {
                //manager.NavigateTo("/alldiscs");
            }
            return await Task.FromResult(Results.Created("CreateDisc", disc));
            //return Results<Disc>.CreatedAtRoute("CreateDisc", new { id = disc.Id }, disc);
            //return await result.Content.ReadFromJsonAsync<Disc?>();

        }

        public async Task DeleteDiscById(int id)
        {
            await _httpClient.DeleteAsync($"/api/discs/{id}");
        }

        public async Task<Disc?> GetDisc(int id)
        {
            var result = await _httpClient.GetAsync($"api/discs/{id}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Disc>();
            }

            return null;
        }

        public async Task UpdateDisc(Disc disc)
        {
            await _httpClient.PutAsJsonAsync($"/api/discs/{disc.Id}", disc);
        }

        public async Task<List<Disc>?> GetAllDiscs()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Disc>>("/api/discs");

            return result;
        }

        //May break this off into separate methods
        //Depending if I go with separate EditForms
        public async Task<List<Disc>?> GetDiscsbyDiscType(DiscType discType, int numDiscs)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Disc>>($"api/discs/{discType}/{numDiscs}") ?? new List<Disc>();

            return result;
        }

        public async Task<List<Disc>?> GetPutters(int numDiscs)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Disc>>($"api/discs/putters/{numDiscs}");

            return result;
        }

        public List<Disc> SortDiscs(List<Disc> discs)
        {
            return discs.OrderByDescending(c => c.DiscType).ToList();
        }
    }

    public record Disc : IParameterPolicy
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Plastic { get; set; }

        public double Speed { get; set; }

        public int Glide { get; set; }

        public double Turn { get; set; }

        public double Fade { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public DiscType DiscType { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public int TimesUsed { get; set; }

        public static Disc Create(string name, string plastic, double speed, int glide, double turn, double fade)
        {
            return new Disc
            {
                Name = name,
                Plastic = plastic,
                Speed = speed,
                Glide = glide,
                Turn = turn,
                Fade = fade,
                Manufacturer = Manufacturer.Innova,
                DiscType = DiscType.Putter
            };
        }


    }

    public enum Manufacturer
    {
        Innova = 0,
        Discmania = 1,
        Discraft = 2,
        Prodigy = 3,
        DynamicDiscs = 4,
        Latitude64 = 5,
        WestsideDiscs = 6,
        MVP = 7,
        Axiom = 8,
        Streamline = 9,
        Kastaplast = 10
    }

    public enum DiscType
    {
        Putter = 0,
        MidRange = 1,
        FairwayDriver = 2,
        DistanceDriver = 3
    }
}
