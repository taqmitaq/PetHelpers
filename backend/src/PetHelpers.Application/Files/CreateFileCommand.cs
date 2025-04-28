using PetHelpers.Application.Abstractions;

namespace PetHelpers.Application.Files;

public record CreateFileCommand(Stream Content, string FileName) : ICommand;