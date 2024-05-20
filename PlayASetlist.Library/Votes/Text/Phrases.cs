namespace PlayASetlist.Library.Votes.Text
{
    public class Phrases
    {
        public string LastOption { get; set; } = "More...";
        public string MessageErrorChatReader { get; set; } = "Can't initialize the chat reader.\n\nMake sure that you have installed Microsoft Visual C++ Redistributable before running the program.";
        public string MessageErrorNoSongs { get; set; } = "Can't retrieve the songs directory.\nPlease ensure that the game has loaded the songs before continuing.";
        public string MessageErrorSettingsLoaded { get; set; } = "Unable to load #VAL songs.\n\nDo you want to select a new settings file?";
        public string MessageErrorSongsFilter { get; set; } = "Unable to find songs with \"#VAL\" filter.\n\nThe filter was disabled.";
        public string MessageErrorTwitchConnection { get; set; } = "Can't establish a connection with Twitch.\n\nPlease check your internet connection and ensure the entered data is correct.";
        public string MessageErrorTwitchUser { get; set; } = "Unable to find the username.\n\nVerify the username and try again.";
        public string MessageErrorYargActual { get; set; } = "YARG currently only supports overwriting the 'Favorite' playlist and does not support setting custom speed per song.\n\nPlease make a backup to preserve your list of favorite songs.\n\nThe game MUST BE CLOSED before continuing.\nYou can reopen it after the playlist is exported.";
        public string MessageErrorYoutubeUser { get; set; } = "Unable to find a livestream in the channel.\n\nVerify the URL and the livestream must be in public.";
        public string MessageToolTip { get; set; } = "When the voting has finished, the setlist is automaticly exported to the game to play.\nName format: PlayASetlist_MM-dd-yy_HH-mm";
        public string SelectingRandomSongs { get; set; } = "Selecting random songs...";
        public string StartVoting { get; set; } = "Starting the voting!";
        public string StartVotingTwitch { get; set; } = "New votation started! Send: #1, #2, #3, #4, #5 or #6 to vote your option! There's #NUM seconds to vote!";
        public string StartVotingViewer { get; set; } = "STARTING THE VOTING!";
        public string ToolTipFilterInstrument { get; set; } = "Try to select songs that has the instrument selected.";
        public string ToolTipSecondsBetweenVote { get; set; } = "Amount of seconds to wait between rounds.";
        public string ToolTipSecondsVote { get; set; } = "Amonut of seconds to vote per round.";
        public string ToolTipSongsPerSetlist { get; set; } = "Amount of rounds and songs that will be selected.";
        public string ToolTipStart { get; set; } = "Hold 'Shift' when pressing Start to load a new game settings.";
        public string ToolTipTwitchChannel { get; set; } = "Twitch channel from which messages will be read and send.";
        public string ToolTipTwitchChat { get; set; } = "Insert the name of the Twitch channel.";
        public string ToolTipTwitchOAuth { get; set; } = "From the same username, generate and insert the Twitch Messaging Interface password.";
        public string ToolTipTwitchToggle { get; set; } = "Insert the corresponding data to use it.";
        public string ToolTipTwitchUsername { get; set; } = "This account will be use as a Bot to read and send message to the Twitch Channel";
        public string ToolTipYoutubeChat { get; set; } = "Insert the URL of the YouTube Channel. Livestream must be public.";
        public string VoteEnded { get; set; } = "The voting has ended! Setlist was exported!";
        public string VoteEndedTwitch { get; set; } = "The voting has ended! Thanks for participating!";
        public string VoteEndedViewer { get; set; } = "VOTED LIST:";
        public string VoteNewRound { get; set; } = "Preparing next round...";
        public string VoteNewRoundViewer { get; set; } = "PREPARING NEXT ROUND...";
        public string Voting { get; set; } = "Time left to vote:";
        public string VotingViewer { get; set; } = "Time left:";
        public string VotingViewerTitle { get; set; } = "VOTING:";
    }
}