using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ApiRpgEtec
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiMaps();

            // --- CÓDIGO PARA LER O JSON EMBUTIDO CORRIGIDO ---
            var assembly = Assembly.GetExecutingAssembly();

            // Ajustado para o namespace real do seu projeto: ApiRpgEtec
            using var stream = assembly.GetManifestResourceStream("ApiRpgEtec.appsettings.local.json");

            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream) // Agora o VS vai reconhecer este método!
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }
            // -------------------------------------------------

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
