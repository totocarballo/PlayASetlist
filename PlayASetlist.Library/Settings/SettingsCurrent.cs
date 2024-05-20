using System.Collections.Generic;

namespace PlayASetlist.Library.Settings
{
    public class SettingsCurrent
    {
        public bool Colored { get; set; }
        public int IndexFilterInstrument { get; set; }
        public bool NotificationSound { get; set; }
        public string PathGameFolder { get; set; }
        public string PathGameSettings { get; set; }
        public int SecondsBetweenVote { get; set; }
        public int SecondsToVote { get; set; }
        public int ServerPort { get; set; }
        public Dictionary<string, string> SongsFilePathCache { get; set; }
        public int SongsPerList { get; set; }
        public string TwitchChannel { get; set; }
        public string TwitchOAuth { get; set; }
        public string TwitchUsername { get; set; }
        public string YoutubeChannel { get; set; }
    }
}