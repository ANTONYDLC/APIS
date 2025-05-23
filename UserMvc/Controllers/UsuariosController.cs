public class UsuariosController : Controller
{
    private readonly ApiService _api;

    public UsuariosController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = await _api.GetUsuariosAsync();
        var usuariosConPais = new List<(Usuario usuario, PaisInfo? paisInfo)>();

        foreach (var u in usuarios)
        {
            var pais = await _api.GetPaisInfoAsync(u.Pais);
            usuariosConPais.Add((u, pais));
        }

        return View(usuariosConPais);
    }

    public IActionResult Crear() => View();

    [HttpPost]
    public async Task<IActionResult> Crear(Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);

        bool paisValido = await _api.ValidarPaisAsync(usuario.Pais);
        if (!paisValido)
        {
            ModelState.AddModelError("Pais", "País no válido.");
            return View(usuario);
        }

        await _api._http.PostAsJsonAsync("https://localhost:5001/api/usuarios", usuario);
        return RedirectToAction("Index");
    }
}
