using PetHelpers.Application.Dtos;
using PetHelpers.Application.Files.Delete;

namespace PetHelpers.API.Controllers.Files.Requests;

public record DeleteFileRequest(FileDto Dto)
{
    public DeleteFileCommand ToCommand() => new(Dto);
}