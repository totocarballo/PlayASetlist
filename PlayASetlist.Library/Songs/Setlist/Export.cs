using PlayASetlist.Library.Http;
using PlayASetlist.Library.Votes;
using PlayASetlist.Library.Votes.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayASetlist.Library.Songs.Setlist
{
    public static class Export
    {
        public static void CurrentVoting(bool isFinish, Phrases phrases = null)
        {
            var outputLines = new List<string>();

            if (isFinish)
            {
                Files.WriteAllLines("Voting.txt", outputLines);
                Server.ParseResponse("", 2);
                return;
            }

            outputLines.Add($"{phrases.VotingViewerTitle}\n");

            for (var v1 = 1; v1 <= 6; v1++)
            {
                var currentVotes = Vote.CurrentVotes.ContainsKey(v1) ? Vote.CurrentVotes[v1] : 0;
                if (v1 <= 5)
                    outputLines.Add($"#{v1} - {Vote.CurrentRandomSongs[v1 - 1].Property} ({currentVotes} Votes)");
                else
                    outputLines.Add($"#{v1} - {phrases.LastOption} ({currentVotes} Votes)\n");
            }

            outputLines.Add($"{phrases.VotingViewer} {Vote.TimeLeftToVote}");

            if (Vote.CurrentExportSongs.Count > 0)
            {
                outputLines.Add("");
                for (var v2 = 1; v2 < Vote.CurrentExportSongs.Count + 1; v2++)
                {
                    outputLines.Add($"{v2}) {Vote.CurrentExportSongs[v2 - 1].Property}");
                }
            }

            Files.WriteAllLines("Voting.txt", outputLines);
        }

        public static void Playlist(string path, List<FormatSong> songs) // YARG
        {
            if (!Files.DirectoryExist(path))
            {
                Files.CreateDirectory(path);
            }

            var playlist = new Playlist
            {
                Name = $"Play A Setlist ({DateTime.Now:MM/dd/yy HH:mm:ss})",
                Author = "Play a Setlist",
                Id = Guid.NewGuid()
            };

            foreach (var song in songs)
            {
                playlist.SongHashes.Add(Files.HashSHA1(song.Path));
            }

            var json = new Json();
            var outputJson = json.SerializePlaylist(playlist);

            //The only file YARG reads for now
            string filePath = $"{path}favorites.json";

            Files.WriteAllText(filePath, outputJson);
        }

        public static void Setlist(string path, List<FormatSong> songs) // CH
        {
            if (!Files.DirectoryExist(path))
            {
                Files.CreateDirectory(path);
            }

            string filePath = $"{path}PlayASetlist ({DateTime.Now:MM-dd HH-mm-ss}).setlist";

            byte[] header = new byte[] { 0xEA, 0xEC, 0x33, 0x01 };

            using (var fs = Files.FileStreamCreate(filePath))
            {
                fs.Write(header, 0, header.Length);

                byte[] totalSongsBytes = BitConverter.GetBytes(songs.Count);
                fs.Write(totalSongsBytes, 0, totalSongsBytes.Length);

                foreach (var song in songs)
                {
                    fs.WriteByte(0x20);

                    byte[] hashBytes = Encoding.UTF8.GetBytes(Files.HashMD5(song.Path));

                    fs.Write(hashBytes, 0, hashBytes.Length);
                    fs.WriteByte(song.Property.Contains("125% - ") ? (byte)0x7D : (byte)0x64);
                    fs.WriteByte(0x00);
                }
            }
        }

        public static void VotedList(Phrases phrases)
        {
            var outputLines = new List<string>
            {
                $"{phrases.VoteEndedViewer}\n"
            };

            for (var v1 = 1; v1 <= Vote.CurrentExportSongs.Count; v1++)
            {
                outputLines.Add($"{v1}) {Vote.CurrentExportSongs[v1 - 1].Property}");
            }

            Files.WriteAllLines("Voting.txt", outputLines);
            Server.ParseResponse(phrases.VoteEndedViewer, 1);
        }
    }
}