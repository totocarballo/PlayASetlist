using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayASetlist.Library.Settings
{
    public static class SettingsPath
    {
        private static List<string> ListSettingsFolders;

        public static (string, string) Get()
        {
            var localLow = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
            var dirYarg = $"{localLow}\\YARC\\YARG\\release";
            const string dirSs = "C:\\Program Files\\ScoreSpy Launcher\\GameData\\100\\";
            var dirCh = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Clone Hero\\";
            var dirDefault = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (Files.DirectoryExist(dirYarg))
            {
                return (dirYarg, "settings.json");
            }
            else if (Files.DirectoryExist(dirSs))
            {
                return (dirSs, "settings.ini");
            }
            else if (Files.DirectoryExist(dirCh))
            {
                return (dirCh, "settings.ini");
            }
            else
            {
                return (dirDefault, "settings.ini");
            }
        }

        public static void Set(SettingsCurrent settings)
        {
            ListSettingsFolders = new List<string>();

            if (Files.GetExtension(settings.PathGameSettings).IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var json = new Json();
                ListSettingsFolders.AddRange(json.ReturnSongFolder(settings.PathGameSettings));
            }
            else if (Files.GetExtension(settings.PathGameSettings).IndexOf("ini", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                foreach (string line in Files.ReadAllLines(settings.PathGameSettings))
                {
                    if (line.Trim().StartsWith("path"))
                    {
                        string path = line.Split('=')[1].Trim();
                        ListSettingsFolders.Add(path);
                    }
                }
            }

            if (!ListSettingsFolders.Count.Equals(0))
            {
                GetSongsFilesFromPaths(settings);
            }
        }

        private static void GetSongsFilesFromPaths(SettingsCurrent settings)
        {
            var songsFilePath = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(ListSettingsFolders, (folder) =>
            {
                var chartFiles = Files.GetFilesFromDirectory(folder, "notes.chart");
                var midFiles = Files.GetFilesFromDirectory(folder, "notes.mid");

                if (chartFiles == null && midFiles == null)
                {
                    return;
                }

                foreach (var file in chartFiles.Concat(midFiles).AsParallel().Select(f => f.ToLower()).Distinct())
                {
                    var basePath = Files.GetDirectory(file);

                    songsFilePath.TryAdd(basePath, Files.GetExtension(file) == ".chart" ? "notes.chart" : "song.ini");
                }
            });

            if (songsFilePath.Count == 0)
            {
                return;
            }

            var fileBadSongs = Files.PathCombine(settings.PathGameFolder, "badsongs.txt");

            if (!Files.Exists(fileBadSongs))
            {
                settings.SongsFilePathCache = new Dictionary<string, string>(songsFilePath);
                return;
            }

            var badSongFolders = new List<string>();

            badSongFolders = Files.ReadLines(fileBadSongs)
                                  .SkipWhile(line => line.Length < 3 || !(line[1] == ':' && line[2] == '\\'))
                                  .Where(line => line.Length >= 3 && line[1] == ':' && line[2] == '\\')
                                  .Select(line => Files.GetDirectory(line).ToLower())
                                  .ToList();

            foreach (var badFolder in badSongFolders)
            {
                songsFilePath.TryRemove(badFolder, out _);
            }

            settings.SongsFilePathCache = new Dictionary<string, string>(songsFilePath);
        }
    }
}