
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
        Black
    }

    public enum Culture
    {
        American,
        European,
        Asian
    }

    public enum Nation
    {
        USA,
        Canada,
        Germany,
        France,
        // ...
    }

    // TODO Culture -> has % of skin types
    // TODO Nationality -> has culture assigned. Skin type % per nationality, defaults to assigned culture if not set
    // TODO loading screens
    // TODO first names and surnames for each nationality
    // TODO hair color, facial hair color

}
