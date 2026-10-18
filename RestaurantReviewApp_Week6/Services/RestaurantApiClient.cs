using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RestaurantReviewApp.Models;

namespace RestaurantReviewApp.Services;

// Uses IHttpClientFactory, following the Week 7 web-services lecture.
public class RestaurantApiClient
{
    private readonly HttpClient _http;

    public RestaurantApiClient(IHttpClientFactory clientFactory)
    {
        _http = clientFactory.CreateClient("RestaurantApi");
    }

    public async Task<List<Restaurant>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<Restaurant>>("api/restaurants") ?? new List<Restaurant>();

    public Task<Restaurant?> GetByIdAsync(int id) =>
        _http.GetFromJsonAsync<Restaurant>($"api/restaurants/{id}");

    public async Task<Restaurant?> AddAsync(Restaurant restaurant)
    {
        using var response = await _http.PostAsJsonAsync("api/restaurants", restaurant);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Restaurant>();
    }

    public async Task<bool> UpdateAsync(Restaurant restaurant)
    {
        using var response = await _http.PutAsJsonAsync($"api/restaurants/{restaurant.Id}", restaurant);
        if (response.StatusCode == HttpStatusCode.NotFound) return false;
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var response = await _http.DeleteAsync($"api/restaurants/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound) return false;
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<Restaurant?> AddReviewAsync(int id, string text)
    {
        using var response = await _http.PostAsJsonAsync($"api/restaurants/{id}/reviews", new ReviewRequest(text));
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Restaurant>();
    }

    private sealed record ReviewRequest(string Text);
}
