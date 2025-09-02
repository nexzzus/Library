using AspNetCoreHero.ToastNotification.Abstractions;
using Library.Share.Core;
using Library.Share.DTOs;
using Library.Web.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class AuthorsController : Controller
{
    private readonly IAuthorsService _authorsService;
    private readonly INotyfService _notyf;

    public AuthorsController(IAuthorsService authorsService, INotyfService notyf)
    {
        _authorsService = authorsService;
        _notyf = notyf;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        Response<List<AuthorDTO>> authors = await _authorsService.GetAllAuthorsAsync();
        if (!authors.Succeded)
        {
            _notyf.Error(authors.Message);
        }

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
            _notyf.Error("Debe ajustar los datos del formulario");
            return View(dto);
        }

        Response<AuthorDTO> response = await _authorsService.CreateAuthorAsync(dto);
        if (!response.Succeded)
        {
//          ModelState.AddModelError(null, response.Errors.FirstOrDefault());
            _notyf.Error(response.Message);
            return View(dto);
        }

        _notyf.Success("Autor creado correctamente");
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] Guid id)
    {
        Response<AuthorDTO> response = await _authorsService.GetAuthorAsync(id);
        if (!response.Succeded)
        {
            _notyf.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }
        
        return View(response.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AuthorDTO dto)
    {
        if (!ModelState.IsValid)
        {
            _notyf.Error("Debe ajustar los datos del formulario");
            return View(dto);
        }

        Response<AuthorDTO> response = await _authorsService.EditAuthorAsync(dto);
        if (!response.Succeded)
        {
            // ModelState.AddModelError(null, response.Errors.FirstOrDefault());
            _notyf.Error(response.Message);
            return View(dto);
        }

        _notyf.Success(response.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Response<object> response = await _authorsService.DeleteAuthorAsync(id);
        if (!response.Succeded)
        {
            _notyf.Error(response.Message);
        }
        else
        {
            _notyf.Success(response.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}