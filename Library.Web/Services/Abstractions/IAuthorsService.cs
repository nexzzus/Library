using Library.Share.Core;
using Library.Share.DTOs;

namespace Library.Web.Services.Abstractions;

public interface IAuthorsService
{
    public Task<Response<List<AuthorDTO>>> GetAllAuthorsAsync();
    public Task<Response<AuthorDTO>> CreateAuthorAsync(AuthorDTO dto);
    public Task<Response<AuthorDTO>> EditAuthorAsync(AuthorDTO dto);
    public Task<Response<object>> DeleteAuthorAsync(Guid id);
    public Task<Response<AuthorDTO>> GetAuthorAsync(Guid id);
}