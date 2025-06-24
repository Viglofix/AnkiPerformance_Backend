using AnkiCommunicationService.Dtos;

namespace AnkiCommunicationService.Services.Contracts;

public interface IAnkiPageAuthentication
{
    Task LoginAsync(string email, string password);
} 