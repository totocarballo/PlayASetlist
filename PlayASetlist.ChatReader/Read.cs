using CefSharp;
using CefSharp.OffScreen;
using PlayASetlist.Library.Votes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using System.Timers;

namespace PlayASetlist.ChatReader
{
    public class Read
    {
        public bool IsConnected;
        private readonly HashSet<string> MessagesCache = new HashSet<string>();
        private readonly int TimerInterval = 250;
        private ChromiumWebBrowser Browser;
        private bool IsTwitch;
        private Timer TimerRead;
        private string Channel;
        public void AddVote(string user, string message)
        {
            if (!Vote.Voting || Vote.CurrentUsersVoted.Contains(user))
            {
                return;
            }

            string[] options = { "#1", "#2", "#3", "#4", "#5", "#6" };

            int index = Array.FindIndex(options, option => message.Equals(option)) + 1;

            Console.WriteLine($"{user}: {message}");

            if (!index.Equals(-1))
            {
                Vote.CurrentUsersVoted.Add(user);
                Vote.Add(index);
            }
        }

        public void ClearCache()
        {
            MessagesCache.Clear();
        }

        public bool SetUser(bool isTwitch, string channel)
        {
            IsTwitch = isTwitch;

            if (isTwitch)
            {
                if (Parse.ChannelExist(channel))
                {
                    Channel = $"https://nightdev.com/hosted/obschat/?theme=undefined&channel={channel}";
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var chatId = Parse.GetVideoId(channel); //yt;

                if (!string.IsNullOrEmpty(chatId))
                {
                    Channel = $"https://www.youtube.com/live_chat?is_popout=1&v={chatId}";
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Start()
        {
            try
            {
                Browser = new ChromiumWebBrowser(Channel);

                Browser.LoadingStateChanged += ChromeBrowser_LoadingStateChanged;
                Browser.Size = new Size(320, 240);
                Browser.LoadUrl(Channel);
            }
            catch
            {
                return false;
            }

            TimerRead = new Timer { Interval = TimerInterval };

            if (IsTwitch)
            {
                TimerRead.Elapsed += Timer_Twitch;
            }
            else
            {
                TimerRead.Elapsed += Timer_Youtube;
            }

            TimerRead.Enabled = true;

            return true;
        }

        public void Stop()
        {
            try
            {
                IsConnected = false;
                Browser.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ChromeBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                Browser.LoadingStateChanged -= ChromeBrowser_LoadingStateChanged;
                IsConnected = true;
            }
        }

        private void ParseMessage(string response)
        {
            const string patron = @"\[(.*?)\]\s(.*?):\s(.*)";

            var list = response.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var item in list)
            {
                var match = Regex.Match(item, patron);
                if (match.Success)
                {
                    string idMessage = match.Groups[1].Value;
                    string user = match.Groups[2].Value;
                    string message = match.Groups[3].Value;

                    if (user.Equals("Chat") || string.IsNullOrEmpty(message))
                    {
                        continue;
                    }

                    if (!MessagesCache.Contains(idMessage))
                    {
                        //Console.WriteLine(match.Value);
                        MessagesCache.Add(idMessage);
                        AddVote(user, message);
                    }
                }
            }
        }

        private async void Timer_Twitch(object sender, ElapsedEventArgs e)
        {
            if (!IsConnected)
            {
                return;
            }

            var frame = Browser.GetMainFrame();
            var response = await frame.EvaluateScriptAsync(@"
                (function() {
                    var elements = document.querySelectorAll('.chat_line');
                    var result = [];
                    elements.forEach(function(element) {
                        var timestamp = '[' + element.getAttribute('data-timestamp') + ']';
                        var nick = element.querySelector('.nick').textContent.trim();
                        var message = element.querySelector('.message').textContent.trim();
                        result.push(timestamp + ' ' + nick + ': ' + message);
                    });
                    return result.join('\n');
                })();
            ");

            if (response.Success && !string.IsNullOrWhiteSpace(response.Result.ToString()))
            {
                ParseMessage(response.Result.ToString());
            }
        }

        private async void Timer_Youtube(object sender, ElapsedEventArgs e)
        {
            if (!IsConnected)
            {
                return;
            }

            var frame = Browser.GetMainFrame();
            var response = await frame.EvaluateScriptAsync(@"
                (function() {
                    var items = document.querySelectorAll('yt-live-chat-text-message-renderer');
                    var result = [];
                    items.forEach(function(item) {
                        var timeStamp = item.querySelector('span#timestamp');
                        var message = item.querySelector('span#message');
                        var authorName = item.querySelector('span#author-name');
                        if (timeStamp && message && authorName) {
                            var content = '[' + item.getAttribute('id') + '] ' + authorName.textContent.trim() + ': ' + message.textContent.trim();
                            result.push(content);
                        }
                    });
                    return result.join('\n');
                })();
            ");

            if (response.Success && !string.IsNullOrWhiteSpace(response.Result.ToString()))
            {
                ParseMessage(response.Result.ToString());
            }
        }
    }
}