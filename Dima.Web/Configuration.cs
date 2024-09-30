using MudBlazor;

namespace Dima.Web;

public static class Configuration
{
    public const string HttpClienteName = "dima";
    public static string BackendUrl { get; set; } = "http://localhost:5133";

    public static MudTheme Theme =
        new()
        {
            Typography = new() { Default = new Default { FontFamily = ["Raleway", "sans-serif"] } },
            PaletteLight = new()
            {
                Primary = new("#1efa2d"),
                Secondary = Colors.LightGreen.Darken3,
                Background = Colors.Gray.Lighten4,
                AppbarBackground = new("#1efa2d"),
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                PrimaryContrastText = Colors.Shades.Black,
                DrawerText = Colors.Shades.Black,
                DrawerBackground = Colors.LightGreen.Lighten4
            },
            PaletteDark = new()
            {
                Primary = Colors.LightGreen.Accent3,
                Secondary = Colors.LightGreen.Darken3,
                AppbarBackground = Colors.LightGreen.Accent3,
                AppbarText = Colors.Shades.Black,
            }
        };
}
