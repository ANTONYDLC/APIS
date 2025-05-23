public class ApiService
{
    private readonly HttpClient _http;
    public ApiService(HttpClient http) => _http = http;

    public async Task<List<Usuario>> GetUsuariosAsync()
    {
        var response = await _http.GetFromJsonAsync<List<Usuario>>("https://localhost:5001/api/usuarios");
        return response ?? new List<Usuario>();
    }

    public async Task<PaisInfo?> GetPaisInfoAsync(string nombrePais)
    {
        try
        {
            var datos = await _http.GetFromJsonAsync<List<dynamic>>($"https://restcountries.com/v3.1/name/{nombrePais}");
            if (datos == null || datos.Count == 0) return null;

            var pais = datos[0];
            return new PaisInfo
            {
                NombreOficial = pais.name.official,
                BanderaUrl = pais.flags.png,
                Region = pais.region
            };
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> ValidarPaisAsync(string nombrePais)
    {
        try
        {
            var response = await _http.GetAsync($"https://restcountries.com/v3.1/name/{nombrePais}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
