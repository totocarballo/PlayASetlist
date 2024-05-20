using PlayASetlist.Library.Votes;
using PlayASetlist.Library.Votes.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlayASetlist.Library.Songs.Setlist
{
    public static class Generate
    {
        private static Options CurrentOptions;

        public static void GetRangeSongs(Dictionary<string, string> songsFile, Options options)
        {
            CurrentOptions = options;

            var randomSongs = new List<FormatSong>();
            var random = new Random();
            var lockObject = new object();
            var attempts = 0;
            var currentSongs = new Dictionary<string, string>(songsFile);

            while (randomSongs.Count < 5)
            {
                Parallel.ForEach(currentSongs, (entry, state) =>
                {
                    if (randomSongs.Count >= 5)
                    {
                        state.Break();
                        return;
                    }

                    var path = entry.Key;
                    var file = entry.Value;

                    FormatSong parse = null;

                    if (file.Contains("chart"))
                    {
                        parse = FormatFile.Chart($"{path}\\{file}");
                    }
                    else if (file.Contains(".ini"))
                    {
                        parse = FormatFile.Ini($"{path}\\{file}");
                    }

                    if (parse != null && !string.IsNullOrEmpty(parse.Name) && !string.IsNullOrEmpty(parse.Artist))
                    {
                        string pathFile;
                        if (file.Contains(".ini"))
                        {
                            if (Files.Exists($"{path}\\notes.chart"))
                            {
                                pathFile = $"{path}\\notes.chart";
                            }
                            else if (Files.Exists($"{path}\\notes.mid"))
                            {
                                pathFile = $"{path}\\notes.mid";
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            pathFile = $"{path}\\notes.chart";
                        }

                        parse.Path = pathFile;

                        lock (lockObject)
                        {
                            if (randomSongs.Count < 5 && !randomSongs.Contains(parse))
                            {
                                randomSongs.Add(parse);
                            }
                        }
                    }
                    else if (parse == null)
                    {
                        songsFile.Remove(entry.Key); // Delete path from settings cache
                    }
                    else
                    {
                        attempts++;
                        if (attempts > 100)
                        {
                            FormatFile.InstrumentFilter = 0;
                        }
                    }
                });
            }

            Vote.CurrentRandomSongs = SetProperty(randomSongs);
        }

        private static List<FormatSong> SetProperty(List<FormatSong> songs)
        {
            var rand = new Random();
            var songProperties = new string[] { "Name", "Artist", "Album", "Year", "Genre", "Charter", "Modchart" };

            var speedUp = FormatFile.InstrumentFilter == 3 || FormatFile.InstrumentFilter == 4 ? "110" : "125";

            foreach (var song in songs)
            {
                var num = 0;

                while (true)
                {
                    if (rand.Next(20) == 0) // 5% 125% song
                    {
                        song.Property = $"\"{song.Name}\" {speedUp}% - \"{song.Artist}\"";
                        break;
                    }

                    string randomProperty;
                    string propertyValue;

                    do
                    {
                        randomProperty = songProperties[rand.Next(songProperties.Length)];
                        propertyValue = typeof(FormatSong).GetProperty(randomProperty)?.GetValue(song).ToString();
                    }
                    while
                    (
                        propertyValue == null || (string.IsNullOrEmpty(propertyValue) && string.IsNullOrWhiteSpace(propertyValue))
                    );

                    var property = string.Empty;

                    switch (randomProperty)
                    {
                        case "Name":
                            property = CurrentOptions.Name.Replace("#NAME", song.Name).Replace("#ARTIST", song.Artist);
                            break;

                        case "Artist":
                            property = CurrentOptions.Artist.Replace("#VAL", propertyValue);
                            break;

                        case "Album":
                            property = CurrentOptions.Album.Replace("#VAL", propertyValue);
                            break;

                        case "Genre":
                            property = CurrentOptions.Genre.Replace("#VAL", propertyValue);
                            break;

                        case "Charter":
                            property = CurrentOptions.Charter.Replace("#VAL", propertyValue);
                            break;

                        case "Year":
                            property = CurrentOptions.Year.Replace("#VAL", Regex.Match(propertyValue, @"\d+").Value);
                            break;

                        case "Modchart":
                            property = CurrentOptions.Modchart;
                            break;
                    }

                    property = Regex.Replace(property, "<[^>]+>", ""); // Remove HTML

                    var alreadyAdded = songs.Any(s => s.Property == property);

                    if (!alreadyAdded || num > 10)
                    {
                        song.Property = property;
                        break;
                    }

                    num++;
                }
            }

            return songs;
        }
    }
}