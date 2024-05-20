using PlayASetlist.Library.Http;
using PlayASetlist.Library.Songs;
using PlayASetlist.Library.Songs.Setlist;
using PlayASetlist.Library.Votes.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PlayASetlist.Library.Votes
{
    public static class Vote
    {
        public static List<FormatSong> CurrentExportSongs = new List<FormatSong>();
        public static List<FormatSong> CurrentRandomSongs = new List<FormatSong>();
        public static VotesTimer Timer;

        private static Phrases _phrases;

        public static event EventHandler CleanVotes;

        public static event EventHandler ExportSetlist;

        public static event EventHandler<List<string>> RandomSongsSelected;

        public static event EventHandler<string> SongSelectedToList;

        public static event EventHandler StartNewRound;

        public static event EventHandler<int> TimerElapsed;

        public static event EventHandler<Tuple<int, int>> Voted;

        public static List<string> CurrentUsersVoted { get; set; } = new List<string>();
        public static Dictionary<int, int> CurrentVotes { get; set; } = new Dictionary<int, int>();
        public static bool NotificationSound { get; set; } = true;
        public static int SongsPerSetlist { get; set; }
        public static int TimeLeftToVote { get; set; }
        public static int TimeToVote { get; set; }
        public static int TimeToWait { get; set; }
        public static bool Voting { get; set; } = false;
        private static string LastOption { get; set; } = "More...";
        public static void Add(int num)
        {
            if (!Voting)
            {
                return;
            }

            int vote;
            if (CurrentVotes.ContainsKey(num))
            {
                vote = CurrentVotes[num];
                vote++;
                CurrentVotes[num] = vote;
            }
            else
            {
                CurrentVotes[num] = 1;
                vote = 1;
            }

            Voted?.Invoke(null, Tuple.Create(num, vote));
        }

        public static void NewRound(Dictionary<string, string> songsFilePaths, Options options)
        {
            CurrentVotes.Clear();
            CurrentUsersVoted.Clear();
            Generate.GetRangeSongs(songsFilePaths, options);
        }

        public static void Start(string textLastOption, Phrases phrases)
        {
            Timer = new VotesTimer(TimerCallback, 1000);
            Timer.Start();
            LastOption = textLastOption;
            _phrases = phrases;
        }

        public static void Stop()
        {
            Timer?.Stop();
            Timer?.Dispose();
        }

        public static void TimerCallback()
        {
            if (!Voting)
            {
                Voting = true;
                TimeLeftToVote = TimeToVote / 1000;

                if (NotificationSound)
                {
                    Notification.Sound();
                }

                var listRandomSongs = new List<string>();

                for (int j = 0; j < 5; j++)
                {
                    listRandomSongs.Add(CurrentRandomSongs[j].Property);
                }

                listRandomSongs.Add(LastOption);

                RandomSongsSelected?.Invoke(null, listRandomSongs);

                return;
            }

            if (TimeLeftToVote.Equals(0))
            {
                Voting = false;

                CleanVotes?.Invoke(null, null);

                var topVotedIndex = CurrentVotes.FirstOrDefault().Key;

                if (CurrentVotes.Count > 0)
                {
                    foreach (var vote in CurrentVotes)
                    {
                        if (vote.Value > CurrentVotes[topVotedIndex])
                        {
                            topVotedIndex = vote.Key;
                        }
                    }
                }
                else
                {
                    var r = new Random();
                    topVotedIndex = r.Next(1, 5);
                }

                if (!topVotedIndex.Equals(6))
                {
                    var property = CurrentRandomSongs[topVotedIndex - 1].Property;
                    SongSelectedToList?.Invoke(null, property);
                    CurrentExportSongs.Add(CurrentRandomSongs[topVotedIndex - 1]);
                }

                if (CurrentExportSongs.Count == SongsPerSetlist)
                {
                    Stop();
                    ExportSetlist?.Invoke(null, null);

                    return;
                }

                Server.ParseResponse(_phrases.VoteNewRoundViewer, 2);
                StartNewRound?.Invoke(null, null);

                Timer.Pause();
                Thread.Sleep(TimeToWait);
                Timer.Resume();
            }

            Export.CurrentVoting(false, _phrases);

            Server.ParseResponse(_phrases.VotingViewerTitle, 0, _phrases.VotingViewer, _phrases.LastOption);

            TimerElapsed?.Invoke(null, TimeLeftToVote);

            TimeLeftToVote--;
        }
    }
}