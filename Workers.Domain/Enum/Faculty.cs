using System.ComponentModel.DataAnnotations;

namespace Workers.Domain.Enum;

public enum Faculty
{
    [Display(Name = "Факультет биологии и экологии")]
    BiologyAndEcology = 0,
    [Display(Name = "Экономический факультет")]
    Economics = 1,
    [Display(Name = "Факультет социально-политических наук")]
    SocialAndPoliticalSciences = 2,
    [Display(Name = "Исторический факультет")]
    History = 3,
    [Display(Name = "Юридический факультет")]
    Law = 4,
    [Display(Name = "Математический факультет")]
    Mathematics = 5,
    [Display(Name = "Физический факультет")]
    Physics = 6,
    [Display(Name = "Факультет психологии")]
    Psychology = 7,
    [Display(Name = "Факультет информатики и вычислительной техники")]
    InformationAndComputerScience = 8,
    [Display(Name = "Факультет филологии и коммуникации")]
    PhilologyAndCommunication = 9,
    [Display(Name = "Институт иностранных языков")]
    InstituteOfForeignLanguagaes = 10,
    [Display(Name = "Университетский колледж")]
    InstituteForDigitalAndInstructionalSystemsDesign = 11,
}