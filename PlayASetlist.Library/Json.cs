using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayASetlist.Library.Http;
using PlayASetlist.Library.Settings;
using PlayASetlist.Library.Votes.Text;
using System;
using System.Collections.Generic;

namespace PlayASetlist.Library
{
    public class Json
    {
        public SettingsCurrent DeserealizeSettings(string json)
        {
            return JsonConvert.DeserializeObject<SettingsCurrent>(json);
        }

        public (Options, Phrases) DeserealizeDialogs(string json)
        {
            JObject combinedJson = JsonConvert.DeserializeObject<JObject>(json);

            JObject optionsJson = combinedJson["Options"] as JObject;
            Options options = optionsJson.ToObject<Options>();

            JObject phrasesJson = combinedJson["Phrases"] as JObject;
            Phrases phrases = phrasesJson.ToObject<Phrases>();

            return (options, phrases);
        }

        public List<string> ReturnSongFolder(string json)
        {
            var list = new List<string>();

            string jsonContent = Files.ReadAllText(json);

            JObject jsonObject = JObject.Parse(jsonContent);

            JArray songFolders = (JArray)jsonObject["SongFolders"];

            if (songFolders != null)
            {
                foreach (var folder in songFolders)
                {
                    list.Add(folder.ToString());
                }
            }

            return list;
        }

        public string Serialize(Data data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public string SerializePlaylist(Playlist playlist)
        {
            return JsonConvert.SerializeObject(playlist, Formatting.Indented);
        }

        public string SerializeSettings(SettingsCurrent settings)
        {
            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }

        public string SerializeText(Options options, Phrases phrases)
        {
            JObject combinedJson = new JObject
            {
                ["Options"] = JObject.FromObject(options),
                ["Phrases"] = JObject.FromObject(phrases)
            };

            return JsonConvert.SerializeObject(combinedJson, Formatting.Indented);
        }
    }

    public class Playlist
    {
        public Playlist()
        {
            SongHashes = new List<string>();
        }

        public string Author { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> SongHashes { get; set; }
    }
}