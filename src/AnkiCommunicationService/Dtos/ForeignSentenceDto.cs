using System.ComponentModel.DataAnnotations;

namespace AnkiCommunicationService.Dtos;

/// <summary>
///  DTO for the sentence and the translated counterpart
/// </summary>
public class ForeignSentenceDto
{
    [Required]
    public string? Sentence { get; set; }

    public string? TranslatedSentence { get; set; }
}
