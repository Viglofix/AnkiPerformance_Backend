using System.ComponentModel.DataAnnotations;

namespace DesktopService.Dtos;

public class ForeignSentenceDto
{
    [Required]
    public string? Sentence { get; set; }
    public string? TranslatedSentence { get; set; }
}
