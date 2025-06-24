using AnkiCommunicationService.Dtos;

namespace AnkiCommunicationService.Services.Contracts;

public interface IAnkiPageOperations
{
    Task<string> GetSentenceAsync(ForeignSentenceDto foreignSentence);
}
