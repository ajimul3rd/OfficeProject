using System.Net.Http.Headers;
using Blazored.LocalStorage;
using System.Net.Http;
using OfficeProject.Models.Entities;
using System.Text.Json;
using OfficeProject.Authentication;
using OfficeProject.Models.DTO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;

public class ApiService
{
    private readonly HttpClient http;
    private readonly ILocalStorageService localStorage;

    public ApiService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        http = httpClient;
        this.localStorage = localStorage;
    }
    // 🛡️ Add Authorization Header with JWT
    private async Task AddAuthHeaderAsync()
    {
        var token = await localStorage.GetItemAsync<string>("authToken");
        //Console.WriteLine($"Token being added: {token}"); // Add this line for debug
        if (!string.IsNullOrWhiteSpace(token))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    // LOGIN USER
    public async Task<AuthResponse?> LoginAsync(LoginModel model)
    {
        var response = await http.PostAsJsonAsync("api/auth/login", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }

    // REGISTER USER
    public async Task<HttpResponseMessage> RegisterUserAsync(RegisterModel model)
    {
        return await http.PostAsJsonAsync("api/auth/register", model);
    }

    // UPDATE USER
    public async Task<HttpResponseMessage> UpdateUserDTOAsync(UserDTO user,int id)
    {
        await AddAuthHeaderAsync();
        return   await http.PutAsJsonAsync($"api/user/{id}", user);
        
    }

    // REFRESH TOKEN


    // LOGOUT (requires auth header)


    // ✅ GET ALL USERS (Protected)

    public async Task<List<UserDTO>?> GetAllUsersDTOAsync()
    {
        var token = await localStorage.GetItemAsync<string>("authToken");
        var request = new HttpRequestMessage(HttpMethod.Get, "api/user");
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
    // ✅ GET USER BY ID
    public async Task<UserDTO?> GetUserByIdAsync(int id)
    {
        await AddAuthHeaderAsync();
        return await http.GetFromJsonAsync<UserDTO>($"api/user/{id}");
    }

    // ✅ GET USER BY USERNAME
    public async Task<UserDTO?> GetUserByUsernameAsync(string username)
    {
        await AddAuthHeaderAsync();
        return await http.GetFromJsonAsync<UserDTO>($"api/user/by-username/{username}");
    }

    // ✅ ADD USER
    public async Task<HttpResponseMessage> AddUserAsync(Users user)
    {
        await AddAuthHeaderAsync();
        return await http.PostAsJsonAsync("api/user", user);
    }

    // ✅ UPDATE USER


    // ✅ DELETE USER
    public async Task<HttpResponseMessage> DeleteUserAsync(int id)
    {
        await AddAuthHeaderAsync();
        return await http.DeleteAsync($"api/user/{id}");
    }

    // ✅ UPDATE REFRESH TOKEN
    public async Task<HttpResponseMessage> UpdateRefreshTokenAsync(int userId, string refreshToken)
    {
        await AddAuthHeaderAsync();
        return await http.PostAsJsonAsync("api/user/update-refresh-token", new { userId, refreshToken });
    }

    // ✅ REVOKE REFRESH TOKEN
    public async Task<HttpResponseMessage> RevokeRefreshTokenAsync(string username)
    {
        await AddAuthHeaderAsync();
        return await http.PostAsJsonAsync("api/user/revoke-refresh-token", username);
    }

    // ✅ GET CURRENT LOGGED USER
    public async Task<UserDTO?> GetCurrentLoggedUserAsync()
    {
        await AddAuthHeaderAsync();

        var response = await http.GetAsync("api/user/current");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
    // ✅ GET CURRENT LOGGED USER
    public async Task<List<UserDTO>> GetPreeAssignUsersAsync()
    {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync("api/user/pree-assign-user");

        if (response.IsSuccessStatusCode)
        {

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
                        return JsonSerializer.Deserialize<List<UserDTO>>(content, options) ?? new List<UserDTO>();
        }
        else
        {
            // Handle non-success codes gracefully
            return new List<UserDTO>();
        }
    }


    //#################################################### UserTask########################################################

    // ✅ GET USER TASKS (current authenticated user)
    public async Task<List<UserTaskMaster>> GetUserTasksMasterAsync()
    {
        await AddAuthHeaderAsync();

        var response = await http.GetAsync("api/UserTaskMaster");

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserTaskMaster>>(content, options) ?? new List<UserTaskMaster>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error fetching tasks: {response.StatusCode} - {error}");
        }
    }

    // ✅ ADD OR UPDATE USER TASK
    public async Task<HttpResponseMessage> AddOrUpdateUserTasksMasterAsync(UserTaskMaster task)
    {
        await AddAuthHeaderAsync();

        return await http.PostAsJsonAsync("api/UserTaskMaster", task);
    }
    // ✅ DELETE USER TASK
    public async Task<HttpResponseMessage> DeleteUserTaskMasterAsync(int taskId)
    {
        await AddAuthHeaderAsync();
        return await http.DeleteAsync($"api/UserTaskMaster/{taskId}");
    }






    //############################################### Client Services ##################################################

    public async Task<HttpResponseMessage> AddClientAsync(ClientsDTO clientDto)
    {
        await AddAuthHeaderAsync();
        return await http.PostAsJsonAsync("api/clients", clientDto);
    }



    // ✅ GET ALL CLIENTS WITH THEIR PROJECTS FOR THE LOGGED-IN USER
    public async Task<List<ClientsDTO>?> GetClientListAsync()
    {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync("api/clients");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<ClientsDTO>>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
    // ✅ Get Client list with
    public async Task<List<ClientsDTO>?> GetClientListdAsync()
    {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync("api/clients");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<ClientsDTO>>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }



    //############################################### Project Services ##################################################
    // ✅ ADD PROJECT
    public async Task<ProjectsDTO> AddProjectAsync(ProjectsDTO projectDto)
    {
        await AddAuthHeaderAsync();
        var response = await http.PostAsJsonAsync("api/projects/add", projectDto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProjectsDTO>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }

    }

    public async Task<ProjectsDTO> SaveOrUpdateProjects(ProjectsDTO projectDto)
    {
        await AddAuthHeaderAsync();
        var response = await http.PostAsJsonAsync("api/projects/save-or-update", projectDto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProjectsDTO>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }

    }
    

   public async Task<List<ProjectsDTO>> GetAllProjectAsync()
        {
        try
        {
            await AddAuthHeaderAsync();

            // Make sure this matches your actual API endpoint
            var response = await http.GetAsync("api/projects");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Deserialize<List<ProjectsDTO>>(content, options) ?? new List<ProjectsDTO>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
        }

     public async Task<List<ProjectsDTO>> GetProjectPerUserAsync()
    {
        try
        {
            await AddAuthHeaderAsync();

            // Make sure this matches your actual API endpoint
            var response = await http.GetAsync("api/projects/user");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Deserialize<List<ProjectsDTO>>(content, options) ?? new List<ProjectsDTO>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
    }
   


    // ✅ Get Project By Id
    public async Task<ProjectsDTO?> GetProjectById(int projectId)
    {
        await AddAuthHeaderAsync();

        var response = await http.GetAsync($"api/projects/{projectId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProjectsDTO>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
    public async Task<List<ProjectsDTO>> GetUserWorksAsync(int projectId)
    {
        try
        {
            await AddAuthHeaderAsync();

            var response = await http.GetAsync($"api/projects/user-works/{projectId}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Deserialize<List<ProjectsDTO>>(content, options) ?? new List<ProjectsDTO>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
    }




    ////############################################### MarketingPhase Services ##################################################

    //public async Task<MarketingPhaseDTO?> AddOrUpdateMaketingPhaseAsync(MarketingPhaseDTO marketingPhase)
    //{
    //    await AddAuthHeaderAsync(); // Optional if your endpoint requires JWT auth

    //    var response = await http.PostAsJsonAsync($"api/marketing/add-or-update", marketingPhase);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        return await response.Content.ReadFromJsonAsync<MarketingPhaseDTO>();
    //    }
    //    else
    //    {
    //        var error = await response.Content.ReadAsStringAsync();
    //        throw new Exception($"Error {response.StatusCode}: {error}");
    //    }


    //}

    //############################################### Work Task  Services ##################################################

    public async Task<HttpResponseMessage> SaveOrUpdateProjectsAsync(WorkTaskDetailsDto dto)
    {
        await AddAuthHeaderAsync();
        return await http.PostAsJsonAsync("api/WorkTask/save-or-update", dto);
    }

    public async Task<WorkTaskDetailsDto> GetWorkTaskDetailsById(int workTaskId)
    {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync($"api/WorkTask/{workTaskId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<WorkTaskDetailsDto>();
    }

    public async Task<List<WorkTaskDetailsDto>> GetWorkingRecordPerUserAsync()
    {
        try
        {
            await AddAuthHeaderAsync();

            // Make sure this matches your actual API endpoint
            var response = await http.GetAsync("api/WorkTask");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Deserialize<List<WorkTaskDetailsDto>>(content, options) ?? new List<WorkTaskDetailsDto>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
    }


    //############################################### Products Services ##################################################
    // ✅ ADD PRODUCTS 
    public async Task<HttpResponseMessage> AddOrUpdateProductAsync(ProductsDTO productsDTO)
    {
        await AddAuthHeaderAsync();
        return await http.PostAsJsonAsync("api/Products", productsDTO);
    }
    public async Task<List<ProductsDTO>?> GetAllProductsDTOAsync()   {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync("api/Products");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<ProductsDTO>>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
    public async Task<ProductsDTO>? GetProductDTOByIdAsync(int id)
    {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync($"api/Products/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProductsDTO>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
    public async Task<ProductsDTO>? UpdateProductDTOAsync(int id)
    {
        await AddAuthHeaderAsync();
        var response = await http.GetAsync($"api/Products/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProductsDTO>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error {response.StatusCode}: {error}");
        }
    }
   


}