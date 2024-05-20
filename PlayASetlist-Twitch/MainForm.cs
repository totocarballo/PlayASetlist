using PlayASetlist.ChatTwitch;
using PlayASetlist.Library;
using PlayASetlist.Library.Http;
using PlayASetlist.Library.Settings;
using PlayASetlist.Library.Songs;
using PlayASetlist.Library.Songs.Setlist;
using PlayASetlist.Library.Votes;
using PlayASetlist.Library.Votes.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PlayASetlist_Twitch
{
    public partial class MainForm : Form
    {
        private readonly string Title = "Play A Setlist";
        private readonly ChatApi TwitchRead = new ChatApi();
        private Options Options = new Options();
        private Phrases Phrases = new Phrases();
        private SettingsCurrent SettingsCurrent = new SettingsCurrent();
        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.icon;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Vote.Stop();
            Vote.CurrentExportSongs.Clear();
            Vote.CurrentRandomSongs.Clear();

            GbxSettings.Enabled = true;
            BtnStart.Enabled = true;
            LstVoted.Items.Clear();
            TipMessage.SetToolTip(LblTimeLeft, "");
            LblTimeLeft.Text = "";

            Clean();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (SettingsCurrent.SongsFilePathCache == null || SettingsCurrent.SongsFilePathCache.Count == 0 || ModifierKeys == Keys.Shift)
            {
                if (!SettingsNew())
                {
                    if (SettingsCurrent.SongsFilePathCache?.Count > 0)
                    {
                        SettingsDone();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    SettingsDone();
                }
            }

            BtnClear.PerformClick();

            LblTimeLeft.Text = Phrases.StartVoting;
            Server.ParseResponse(Phrases.StartVotingViewer, 2);

            if (TwitchRead.IsConnected)
            {
                TwitchRead.Send(Phrases.StartVotingTwitch.Replace("#NUM", NumSecondsToVote.Value.ToString()));
            }

            BtnStart.Enabled = false;
            GbxSettings.Enabled = false;

            Vote.Voting = false;
            SettingsCurrent.SongsPerList = (int)NumSongsPerSetlist.Value;

            LblTimeLeft.Text = Phrases.SelectingRandomSongs;

            Vote.TimeToVote = (int)NumSecondsToVote.Value * 1000;
            Vote.TimeToWait = (int)NumWait.Value * 1000;
            Vote.SongsPerSetlist = (int)NumSongsPerSetlist.Value;

            NewRound();

            Vote.Start(Phrases.LastOption, Phrases);
        }

        private void BtnVote_Click(object sender, EventArgs e)
        {
            if (!Vote.Voting)
            {
                return;
            }

            var buttonPressed = sender as Button;
            int buttonNum = int.Parse(buttonPressed.Name.Substring(7));

            ButtonsEnabled(false);

            Vote.Add(buttonNum);
        }

        private void ButtonsEnabled(bool status)
        {
            for (var num = 1; num < 7; num++)
            {
                var controls = Controls.Find($"BtnVote{num}", true);
                var button = (Button)controls[0];

                button.Enabled = status;
            }
        }

        private void CbxFilterInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormatFile.InstrumentFilter = CbxFilterInstrument.SelectedIndex;
        }

        private void ChkTwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTwitch.Checked)
            {
                if (string.IsNullOrEmpty(TxtTwitchUsername.Text) ||
                string.IsNullOrEmpty(TxtTwitchOAuth.Text) ||
                string.IsNullOrEmpty(TxtTwitchChannel.Text))
                {
                    ChkTwitch.Checked = false;
                    return;
                }

                var currentTitle = Text;

                Text = $"{Title} - Connecting to Twitch chat...";

                var start = TwitchRead.Start(TxtTwitchUsername.Text, TxtTwitchOAuth.Text, TxtTwitchChannel.Text);

                var connected = TwitchRead.CheckConnection();

                if (!start || !connected)
                {
                    MessageBox.Show(this, Phrases.MessageErrorTwitchConnection, "Play A Setlist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ChkTwitch.Checked = false;
                    TwitchRead.Stop();
                }

                Text = currentTitle;
            }
            else
            {
                if (TwitchRead.IsConnected)
                {
                    TwitchRead.Stop();
                }
            }

            TxtTwitchUsername.Enabled = !ChkTwitch.Checked;
            TxtTwitchOAuth.Enabled = !ChkTwitch.Checked;
            TxtTwitchChannel.Enabled = !ChkTwitch.Checked;
        }

        private void Clean()
        {
            Export.CurrentVoting(true);

            for (int i = 1; i < 7; i++)
            {
                VotesToLabel(i);
                PropertyToTextBox(i);
            }

            ButtonsEnabled(true);
        }

        private void ColorControl()
        {
            var listColors = new Color[] { Color.Lime, Color.FromArgb(255, 50, 50), Color.Yellow, Color.DeepSkyBlue, Color.Orange, Color.MediumPurple };
            for (var num = 1; num < 7; num++)
            {
                var buttons = Controls.Find($"BtnVote{num}", true);
                var textboxes = Controls.Find($"TxtProperty{num}", true);
                var labels = Controls.Find($"LblVote{num}", true);

                var button = (Button)buttons[0];
                var textbox = (TextBox)textboxes[0];
                var label = (Label)labels[0];

                button.BackColor = listColors[num - 1];
                textbox.BackColor = listColors[num - 1];
                label.BackColor = listColors[num - 1];
            }
        }

        private void GbxSettings_Leave(object sender, EventArgs e)
        {
            if (SettingsCurrent.SongsFilePathCache?.Count > 0)
            {
                SettingsSave();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SettingsCurrent.SongsFilePathCache?.Count > 0)
            {
                SettingsSave();
            }

            Server.Stop();
            Export.CurrentVoting(true);

            if (TwitchRead.IsConnected)
            {
                TwitchRead.Stop();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show();

            Invalidate();

            SettingsStart();

            Server.Start(SettingsCurrent.ServerPort);

            Vote.Voted += Vote_VotesUpdate;
            Vote.CleanVotes += Vote_CleanVotes;
            Vote.ExportSetlist += Vote_ExportSetlist;
            Vote.RandomSongsSelected += Vote_RandomSongsSelected;
            Vote.SongSelectedToList += Vote_SongSelectedToList;
            Vote.StartNewRound += Vote_StartNewRound;
            Vote.TimerElapsed += Vote_TimerElapsed;
        }

        private void NewRound()
        {
            var actualInstrumentIndex = CbxFilterInstrument.SelectedIndex;

            Vote.NewRound(SettingsCurrent.SongsFilePathCache, Options);

            if (actualInstrumentIndex != FormatFile.InstrumentFilter)
            {
                MessageBox.Show(this, Phrases.MessageErrorSongsFilter.Replace("#VAL", CbxFilterInstrument.Items[actualInstrumentIndex].ToString()), "Play A Setlist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CbxFilterInstrument.SelectedIndex = 0;
            }
        }

        private void PropertyToTextBox(int num, string property = "")
        {
            var controls = Controls.Find($"TxtProperty{num}", true);
            var textRandom = (TextBox)controls[0];

            BeginInvoke((MethodInvoker)delegate
            {
                textRandom.Text = property;
                TipMessage.SetToolTip(textRandom, property);
            });
        }

        private void SettingsDone()
        {
            if (SettingsCurrent.PathGameSettings?.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                MessageBox.Show(this, Phrases.MessageErrorYargActual, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (SettingsCurrent.SongsFilePathCache?.Count > 0)
            {
                Text = $"{Title} - {SettingsCurrent.SongsFilePathCache?.Count} Songs";
            }

            Export.CurrentVoting(true);

            CbxFilterInstrument.SelectedIndex = CbxFilterInstrument.SelectedIndex == -1 ? 0 : CbxFilterInstrument.SelectedIndex;
        }

        private bool SettingsNew()
        {
            Text = $"{Title} - Loading songs...";

            var inputs = SettingsPath.Get(); // Search for game default settings location

            var dialog = new OpenFileDialog
            {
                Title = "Select the settings file from the game",
                Filter = "Clone Hero, ScoreSpy or YARG|settings.ini;settings.json",
                FileName = inputs.Item2,
                InitialDirectory = inputs.Item1,
                AutoUpgradeEnabled = true,
                ValidateNames = true,
                CheckFileExists = true,
                CheckPathExists = true,
                SupportMultiDottedExtensions = false
            };

            if (dialog.ShowDialog(this).Equals(DialogResult.OK))
            {
                SettingsCurrent.PathGameFolder = dialog.InitialDirectory;
                SettingsCurrent.PathGameSettings = dialog.FileName;
                SettingsPath.Set(SettingsCurrent);

                if (SettingsCurrent.SongsFilePathCache?.Count == 0 || SettingsCurrent.SongsFilePathCache == null)
                {
                    MessageBox.Show(this, Phrases.MessageErrorNoSongs, "Play A Setlist", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Text = Title;
                    return false;
                }

                return true;
            }

            Text = Title;

            return false;
        }

        private void SettingsSave()
        {
            var settings = new SettingsCurrent
            {
                TwitchUsername = TxtTwitchUsername.Text,
                TwitchOAuth = TxtTwitchOAuth.Text,
                TwitchChannel = TxtTwitchChannel.Text,
                IndexFilterInstrument = CbxFilterInstrument.SelectedIndex,
                SecondsBetweenVote = (int)NumWait.Value,
                SecondsToVote = (int)NumSecondsToVote.Value,
                SongsPerList = (int)NumSongsPerSetlist.Value,
                SongsFilePathCache = SettingsCurrent.SongsFilePathCache,
                PathGameFolder = SettingsCurrent.PathGameFolder,
                PathGameSettings = SettingsCurrent.PathGameSettings,
                Colored = SettingsCurrent.Colored,
                NotificationSound = SettingsCurrent.NotificationSound,
                ServerPort = SettingsCurrent.ServerPort,
            };

            if (!settings.Equals(SettingsCurrent))
            {
                var json = new Json();

                Files.WriteAllText("settings.json", json.SerializeSettings(settings));
            }
        }

        private void SetPhrases()
        {
            TipMessage.SetToolTip(CbxFilterInstrument, Phrases.ToolTipFilterInstrument);
            TipMessage.SetToolTip(LblFilterInstrument, Phrases.ToolTipFilterInstrument);
            TipMessage.SetToolTip(NumWait, Phrases.ToolTipSecondsBetweenVote);
            TipMessage.SetToolTip(LblWait, Phrases.ToolTipSecondsBetweenVote);
            TipMessage.SetToolTip(NumSecondsToVote, Phrases.ToolTipSecondsVote);
            TipMessage.SetToolTip(LblSecondsToVote, Phrases.ToolTipSecondsVote);
            TipMessage.SetToolTip(NumSongsPerSetlist, Phrases.ToolTipSongsPerSetlist);
            TipMessage.SetToolTip(LblSongsPerSetlist, Phrases.ToolTipSongsPerSetlist);
            TipMessage.SetToolTip(BtnStart, Phrases.ToolTipStart);
            TipMessage.SetToolTip(ChkTwitch, Phrases.ToolTipTwitchChannel);
            TipMessage.SetToolTip(ChkTwitch, Phrases.ToolTipTwitchUsername);
            TipMessage.SetToolTip(LblTwitchChannel, Phrases.ToolTipTwitchChannel);
            TipMessage.SetToolTip(TxtTwitchChannel, Phrases.ToolTipTwitchChannel);
            TipMessage.SetToolTip(LblTwitchOAuth, Phrases.ToolTipTwitchOAuth);
            TipMessage.SetToolTip(TxtTwitchOAuth, Phrases.ToolTipTwitchOAuth);
            TipMessage.SetToolTip(LblTwitchUsername, Phrases.ToolTipTwitchToggle);
            TipMessage.SetToolTip(TxtTwitchUsername, Phrases.ToolTipTwitchToggle);
        }

        private void SettingsStart()
        {
            var json = new Json();

            if (Files.Exists("dialogs.json"))
            {
                (Options, Phrases) = json.DeserealizeDialogs(Files.ReadAllText("dialogs.json"));
            }
            else
            {
                Files.WriteAllText("dialogs.json", json.SerializeText(Options, Phrases));
            }

            SetPhrases();

            var settingsManage = new SettingsManage();

            var load = settingsManage.Load();

            if (load == (false, false, 0, null)) // If no previous settings saved
            {
                settingsManage.Reset(SettingsCurrent);

                SettingsNew();
            }
            else if (load.Item3 > 0) // If error finding previous setting
            {
                var scanResult = MessageBox.Show(this, Phrases.MessageErrorSettingsLoaded.Replace("VAL", load.Item3.ToString()), "Play A Setlist", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (scanResult == DialogResult.Yes && !SettingsNew())
                {
                    return;
                }
            }

            if (load.Item4 != null)
            {
                SettingsCurrent = load.Item4;
            }

            if (SettingsCurrent.Colored)
            {
                ColorControl();
            }

            TxtTwitchUsername.Text = SettingsCurrent.TwitchUsername;
            TxtTwitchOAuth.Text = SettingsCurrent.TwitchOAuth;
            TxtTwitchChannel.Text = SettingsCurrent.TwitchChannel;
            CbxFilterInstrument.SelectedIndex = SettingsCurrent.IndexFilterInstrument;
            NumWait.Value = SettingsCurrent.SecondsBetweenVote;
            NumSecondsToVote.Value = SettingsCurrent.SecondsToVote;
            NumSongsPerSetlist.Value = SettingsCurrent.SongsPerList;
            Vote.NotificationSound = SettingsCurrent.NotificationSound;

            SettingsDone();
        }
        private void Vote_CleanVotes(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                Clean();
            });
        }

        private void Vote_ExportSetlist(object sender, EventArgs e)
        {
            if (Files.Exists($"{SettingsCurrent.PathGameFolder}\\settings.json"))
            {
                Export.Playlist($"{SettingsCurrent.PathGameFolder}\\playlists\\", Vote.CurrentExportSongs);
            }
            else
            {
                Export.Setlist($"{SettingsCurrent.PathGameFolder}\\Setlists\\", Vote.CurrentExportSongs);
            }

            Export.VotedList(Phrases);

            BeginInvoke((MethodInvoker)delegate
            {
                LblTimeLeft.Text = Phrases.VoteEnded;
                TipMessage.SetToolTip(LblTimeLeft, Phrases.MessageToolTip);
                BtnStart.Enabled = true;
                GbxSettings.Enabled = true;

                if (TwitchRead.IsConnected)
                {
                    TwitchRead.Send(Phrases.VoteEndedTwitch);
                }
            });
        }

        private void Vote_RandomSongsSelected(object sender, List<string> e)
        {
            for (int j = 1; j < 7; j++)
            {
                PropertyToTextBox(j, e[j - 1]);
            }
        }

        private void Vote_SongSelectedToList(object sender, string e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                LstVoted.Items.Add(e);
            });
        }

        private void Vote_StartNewRound(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                LblTimeLeft.Text = Phrases.VoteNewRound;
                NewRound();
            });
        }

        private void Vote_TimerElapsed(object sender, int e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                if (e < 0)
                {
                    return;
                }

                LblTimeLeft.Text = $"{Phrases.Voting} {e}";
            });
        }

        private void Vote_VotesUpdate(object sender, Tuple<int, int> e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                VotesToLabel(e.Item1, e.Item2);
            });
        }

        private void VotesToLabel(int num, int vote = 0)
        {
            var controls = Controls.Find($"LblVote{num}", true);
            var labelVote = (Label)controls[0];

            labelVote.Text = vote.ToString();
        }
    }
}