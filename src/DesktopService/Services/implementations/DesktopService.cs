using DesktopService.Dtos;
using DesktopService.Services.contracts;

using AutoMapper;
using DesktopService.Data;
using DesktopService.Entity;

namespace DesktopService.Services.implementations;

public class DesktopService : IDesktopService
{
    // Constant Data
    private const string ExceptionMessage = "Sentence is too long or null. Maximum length is 1000 characters.";

    // Inject Reusable Services
    private readonly DesktopServiceContext _context;
    private readonly IMapper _mapper;
    private readonly IDeepSeekTranslate _translate;
    private readonly ISignalRConnection _signalRConnection;
    public DesktopService(DesktopServiceContext context, IMapper mapper, IDeepSeekTranslate translate, ISignalRConnection signalRConnection)
    {
        _context = context;
        _mapper = mapper;
        _translate = translate;
        _signalRConnection = signalRConnection;
    }

    /// <summary>
    ///  Method that is responsible for handling external clients requests
    /// </summary>
    /// <param name="foreignSentenceDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ForeignSentenceDto> Add(ForeignSentenceDto foreignSentenceDto)
    {
        var entity = _mapper.Map<ForeignSentence>(foreignSentenceDto);

        if (entity.Sentence is null) { throw new Exception(ExceptionMessage); }

        // Database Related Operations
        await SaveTrimedMessageToDb(entity);

        // Translate given sentence using AI algorithsm
        entity.TranslatedSentence = await _translate.TranslateAnkiSentence(entity.Sentence!);
        foreignSentenceDto = _mapper.Map<ForeignSentenceDto>(entity);

        // Starts SignalR Connecition
        await _signalRConnection.StartAndDisposeConnection(foreignSentenceDto);

        return foreignSentenceDto;
    }

    /// <summary>
    /// Method that is responsible for database related operations for the ForeignSentence entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private async Task SaveTrimedMessageToDb(ForeignSentence entity)
    {
        entity.Sentence = entity.Sentence!.Trim();
        _context.ForeignSentences.Add(entity);
        await _context.SaveChangesAsync();
    }
}
