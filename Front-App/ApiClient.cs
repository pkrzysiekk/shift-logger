using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Front_App;

public class ShiftApiClient : IShiftClient
{
    private readonly HttpClient _httpClient;

    public ShiftApiClient()
    {
        var handler = new HttpClientHandler
        {
            // AKCEPTUJ każdy certyfikat SSL — tylko na potrzeby testów!
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:7279")
        };
    }

    public async Task<List<Shift>> GetShiftsAsync()
    {
        var response = await _httpClient.GetAsync("/Shift");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Shift>>();
    }

    public async Task CreateShiftAsync(Shift shift)
    {
        var response = await _httpClient.PostAsJsonAsync("api/shift", shift);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateShiftAsync(Shift shift)
    {
        var response = await _httpClient.PutAsJsonAsync("api/shift", shift);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteShiftAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/shift?id={id}");
        response.EnsureSuccessStatusCode();
    }
}