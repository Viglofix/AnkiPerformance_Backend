using System.ComponentModel.DataAnnotations;

namespace DesktopService.Entity;

public class ForeignSentence
{
    public long Id { get; set; }
    public string? Sentence { get; set; }
    public string? TranslatedSentence { get; set; }
}
