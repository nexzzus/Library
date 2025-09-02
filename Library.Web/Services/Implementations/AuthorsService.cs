using Library.Share.Core;
using Library.Share.DTOs;
using Library.Web.Data;
using Library.Web.Entities;
using Library.Web.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Services.Implementations;

public class AuthorsService : IAuthorsService
{
    private readonly DataContext _context;

    public AuthorsService(DataContext context)
    {
        _context = context;
    }


    public async Task<Response<List<AuthorDTO>>> GetAllAuthorsAsync()
    {
        try
        {
            List<Author> authors = await _context.Authors.ToListAsync();
            List<AuthorDTO> list = authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
            }).ToList();
            return new Response<List<AuthorDTO>>
            {
                Succeded = true,
                Result = list
            };
        }
        catch (Exception e)
        {
            return new Response<List<AuthorDTO>>
            {
                Succeded = false,
                Message = e.Message
            };
        }
    }

    public async Task<Response<AuthorDTO>> CreateAuthorAsync(AuthorDTO dto)
    {
        try
        {
            Author author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
           await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            dto.Id = author.Id;

            return new Response<AuthorDTO>
            {
                Succeded = true,
                Message = "Autor creado con éxito",
                Result = dto
            };
        }
        catch (Exception e)
        {
            return new Response<AuthorDTO>()
            {
                Succeded = false,
                Message = e.Message
            };
        }
    }

    public async Task<Response<AuthorDTO>> EditAuthorAsync(AuthorDTO dto)
    {
        try
        {
            Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (author is null)
            {
                return new Response<AuthorDTO>()
                {
                    Succeded = false,
                    Message = $"El autor con id '{dto.Id}' no existe."
                };
            }
            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            
            return new Response<AuthorDTO>()
            {
                Succeded = true,
                Message = "Autor actualizado con éxito",
                Result = dto
            };
        }
        catch (Exception e)
        {
            return new Response<AuthorDTO>()
            {
                Succeded = false,
                Message = e.Message
            };
        }
    }

    public async Task<Response<object>> DeleteAuthorAsync(Guid id)
    {
        try
        {
            Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author is null)
            {
                return new Response<object>()
                {
                    Succeded = false,
                    Message = $"El autor con id '{id}' no existe."
                };
            }
            
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return new Response<object>
            {
                Succeded = true,
                Message = "Autor eliminado con éxito"
            };
        }
        catch (Exception e)
        {
            return new Response<object>()
            {
                Succeded = false,
                Message = e.Message
            };
        }
    }
    
    public async Task<Response<AuthorDTO>> GetAuthorAsync(Guid id)
    {
        try
        {
            Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author is null)
            {
                return new Response<AuthorDTO>()
                {
                    Succeded = false,
                    Message = $"El autor con id '{id}' no existe."
                };
            }

            AuthorDTO dto = new AuthorDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
            
            return new Response<AuthorDTO>()
            {
                Succeded = true,
                Message = "Autor obtenido con éxito",
                Result = dto
            };
        }
        catch (Exception e)
        {
            return new Response<AuthorDTO>()
            {
                Succeded = false,
                Message = e.Message
            };
        }
    }
}