using System.Text.RegularExpressions;

namespace PlayASetlist.Library.Songs
{
    public static class FormatFile
    {
        public static int InstrumentFilter { get; set; }

        public static FormatSong Chart(string file)
        {
            string[] lines = Files.ReadAllLines(file);

            if (lines.Length == 0)
            {
                return null;
            }

            var song = ParseChart(lines);
            return FilterAndReturnSong(song);
        }

        public static FormatSong Ini(string file)
        {
            string[] lines = Files.ReadAllLines(file);

            if (lines.Length == 0)
            {
                return null;
            }

            var song = ParseIni(lines);
            return FilterAndReturnSong(song);
        }

        private static string CapitalizeFirstLetter(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        private static FormatSong FilterAndReturnSong(FormatSong song)
        {
            if (InstrumentFilter <= 0)
            {
                return song;
            }

            var propertiesToCheck = new string[] { "Diffguitar", "Diffbass", "Diffdrums", "Diffdrumsreal", "Diffkeys", "Diffguitarghl", "Diffbassghl" };

            var propertyName = propertiesToCheck[InstrumentFilter - 1];
            var propertyValue = typeof(FormatSong).GetProperty(propertyName)?.GetValue(song)?.ToString();

            if (string.IsNullOrWhiteSpace(propertyValue) || (int.TryParse(propertyValue, out int value) && value < 1))
            {
                return null;
            }

            return song;
        }

        private static FormatSong ParseChart(string[] lines)
        {
            var song = new FormatSong();

            foreach (string line in lines)
            {
                ParseChartLine(line, song);
            }

            return song;
        }

        private static void ParseChartLine(string line, FormatSong song)
        {
            var matchSettings = Regex.Match(line.Trim(), @"^(Name|Artist|Charter|Album|Year|Genre)\s*=\s*""(.*)""$");
            var matchDifficulty = Regex.Match(line.Trim(), "\\[(Expert|Easy|Medium|Hard)?(Single|DoubleBass|Drums|Keyboard|GHLGuitar|GHLBass)\\]");

            if (matchSettings.Success)
            {
                var propertyName = matchSettings.Groups[1].Value;
                var propertyValue = matchSettings.Groups[2].Value;
                typeof(FormatSong).GetProperty(propertyName)?.SetValue(song, propertyValue);
            }

            if (matchDifficulty.Success)
            {
                var propertyName = matchDifficulty.Groups[2].Value;
                if (propertyName.Contains("Single"))
                {
                    typeof(FormatSong).GetProperty("Diffguitar")?.SetValue(song, "1");
                }
                else if (propertyName.Contains("DoubleBass"))
                {
                    typeof(FormatSong).GetProperty("Diffbass")?.SetValue(song, "1");
                }
                else if (propertyName.Contains("Drums"))
                {
                    typeof(FormatSong).GetProperty("Diffdrums")?.SetValue(song, "1");
                }
                else if (propertyName.Contains("Keyboard"))
                {
                    typeof(FormatSong).GetProperty("Diffkeys")?.SetValue(song, "1");
                }
                else if (propertyName.Contains("GHLGuitar"))
                {
                    typeof(FormatSong).GetProperty("Diffguitarghl")?.SetValue(song, "1");
                }
                else if (propertyName.Contains("GHLBass"))
                {
                    typeof(FormatSong).GetProperty("Diffbassghl")?.SetValue(song, "1");
                }
            }
        }

        private static FormatSong ParseIni(string[] lines)
        {
            var song = new FormatSong();

            foreach (string line in lines)
            {
                ParseIniLine(line, song);
            }

            return song;
        }

        private static void ParseIniLine(string line, FormatSong song)
        {
            var matchSettings = Regex.Match(line.Trim(), @"^(name|artist|charter|album|year|genre)\s*=\s*(.*)$");
            var matchDifficulty = Regex.Match(line.Trim(), @"^(diff_guitar|diff_bass|diff_drums|diff_keys|diff_guitarghl|diff_bassghl|diff_drums_real|modchart)\s*=\s*(.*)$");

            if (matchSettings.Success)
            {
                var propertyName = CapitalizeFirstLetter(matchSettings.Groups[1].Value.Replace("_", ""));
                var propertyValue = matchSettings.Groups[2].Value.Trim();
                typeof(FormatSong).GetProperty(propertyName)?.SetValue(song, propertyValue);
            }
            else if (matchDifficulty.Success)
            {
                var propertyName = CapitalizeFirstLetter(matchDifficulty.Groups[1].Value.Replace("_", "").ToLower());
                var propertyValue = matchDifficulty.Groups[2].Value;
                typeof(FormatSong).GetProperty(propertyName)?.SetValue(song, propertyValue);
            }
        }
    }
}