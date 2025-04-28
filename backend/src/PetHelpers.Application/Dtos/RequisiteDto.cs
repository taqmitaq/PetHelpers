namespace PetHelpers.Application.Dtos;

public record RequisiteDto(string Title, string Description);

// TODO: добавить атрибут [JsonConstructor] к конструкторам рекордов, которые используем типа фото, реквизитов, адреса и т. п.