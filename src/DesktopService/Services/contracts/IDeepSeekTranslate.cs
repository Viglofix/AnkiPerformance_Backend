namespace DesktopService.Services.contracts;

public interface IDeepSeekTranslate
{
    Task<string> TranslateAnkiSentence(string sentenceToTranslate);
}
