using PetHelpers.Application.Volunteers.AddPetPhotos;

namespace PetHelpers.API.Processors;

public class FormFileProcessor
{
    private readonly List<CreateFileCommand> _fileDtos = [];

    public List<CreateFileCommand> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new CreateFileCommand(stream, file.FileName);
            _fileDtos.Add(fileDto);
        }

        return _fileDtos;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileDtos)
        {
            await file.Content.DisposeAsync();
        }
    }
}