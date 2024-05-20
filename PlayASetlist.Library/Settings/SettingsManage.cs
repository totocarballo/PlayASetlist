using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlayASetlist.Library.Settings
{
    public class SettingsManage
    {
        public string SettingsFile { get; } = "settings.json";

        public (bool, bool, int, SettingsCurrent) Load()
        {
            if (!Files.Exists(SettingsFile))
            {
                // New settings
                return (false, false, 0, null);
            }

            var jsonInput = Files.ReadAllText(SettingsFile);

            var json = new Json();
            var settings = json.DeserealizeSettings(jsonInput);

            int failedSongs = 0;

            void checkDirectoryExistence(KeyValuePair<string, string> song)
            {
                if (!Files.DirectoryExist(song.Key))
                {
                    settings.SongsFilePathCache.Remove(song.Key);
                    Interlocked.Increment(ref failedSongs); // Incrementar de forma segura la variable failedSongs
                }
            }

            Parallel.ForEach(settings.SongsFilePathCache, checkDirectoryExistence);

            if (failedSongs > 0)
            {
                // Ask new settings
                return (false, true, failedSongs, settings);
            }

            // Loaded
            return (true, false, 0, settings);
        }

        public void Reset(SettingsCurrent settings)
        {
            settings.SongsFilePathCache = null;
            settings.TwitchUsername = null;
            settings.TwitchOAuth = null;
            settings.TwitchChannel = null;
            settings.YoutubeChannel = null;
            settings.SongsPerList = 2;
            settings.SecondsToVote = 10;
            settings.SecondsBetweenVote = 1;
            settings.IndexFilterInstrument = 0;
            settings.PathGameFolder = null;
            settings.Colored = true;
            settings.NotificationSound = true;
            settings.ServerPort = 50300;
        }
    }
}