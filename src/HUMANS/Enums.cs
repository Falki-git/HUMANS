
namespace Humans
{
    /// <summary>
    /// Color Palette from https://huebliss.com/skin-color-code/
    /// </summary>
    public enum SkinType
    {
        Real,
        Fair,
        Dark,
        Caucasian,
        Indian,
        BeautifulTouch,
        Ebony,
        NormalAndTan,
        Natural,
        Asian,
        Black,
        Kerbal
    }

    public enum HairColorType
    {
        Human,
        Kerbal
    }

    public enum CultureName
    {
        American,
        European,
        Asian
    }

    public enum NationName
    {
        USA,
        Canada,
        Germany,
        France,
        // ...
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    // TODO Culture -> has % of skin types
    // TODO Nationality -> has culture assigned. Skin type % per nationality, defaults to assigned culture if not set
    // TODO loading screens
    // TODO first names and surnames for each nationality
    // TODO hair color, facial hair color

    // TODO TeamColor1, TeamColor2 ?

}
