using System;
using Serilog;

namespace AffinityEx.Launcher {

    public abstract class LauncherBase {

        public static void Launch(string appName, ushort appVersion = 2) {
            InitLogger();

            Log.Information("Starting chainloading for Affinity {AppName}", appName);
            try {
                ChainloaderStage1.Run(appName, appVersion);
            } catch (Exception ex) {
                Log.Fatal(ex, "Chainload failure");
            }
        }

        public static void InitLogger() {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Verbose()
                .WriteTo.Debug()
#else
                .MinimumLevel.Information()
#endif
                .WriteTo.File("AffinityEx.log")
                .CreateLogger();
        }

    }

}
