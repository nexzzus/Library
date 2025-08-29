using Library.Share.Core;
using Library.Share.DTOs;
using Library.Web.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class AuthorsController : Controller
{
    private readonly IAuthorsService  _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        Response<List<AuthorDTO>> authors = await _authorsService.GetAllAuthorsAsync();
        return View(authors.Result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }
        Response<AuthorDTO> response = await _authorsService.CreateAuthorAsync(dto);
        if (!response.Succeded)
        {
            ModelState.AddModelError(null, response.Errors.FirstOrDefault());
            return View(dto);
        }
        return RedirectToAction(nameof(Index));
    }
}