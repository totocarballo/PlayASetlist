using PlayASetlist.Library.Votes;
using System;
using System.Diagnostics;
using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Models;

namespace PlayASetlist.ChatTwitch
{
    public class ChatApi
    {
        private ClientOptions TwitchClientOptions;
        private ConnectionCredentials TwitchCredentials;
        private WebSocketClient TwitchWSClient;
        private TwitchClient TwitchClient;

        public bool IsConnected = false;

        public bool CheckConnection()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!IsConnected)
            {
                if (stopwatch.ElapsedMilliseconds > 10000)
                {
                    stopwatch.Stop();
                    return false;
                }

                Thread.Sleep(500);
            }

            stopwatch.Stop();
            return true;
        }

            public bool Start(string username, string oauth, string channel)
        {
            try
            {
                TwitchCredentials = new ConnectionCredentials(username, oauth);

                TwitchClientOptions = new ClientOptions
                {
                    MessagesAllowedInPeriod = 750,
                    ThrottlingPeriod = TimeSpan.FromSeconds(30)
                };

                TwitchWSClient = new WebSocketClient(TwitchClientOptions);

                TwitchClient = new TwitchClient(TwitchWSClient);
                TwitchClient.Initialize(TwitchCredentials, channel);
            }
            catch
            {
                return false;
            }

            TwitchClient.OnMessageReceived += Client_OnMessageReceived;
            TwitchClient.OnConnected += Client_OnConnected;
            TwitchClient.OnDisconnected += TwitchClient_OnDisconnected;

            TwitchClient.Connect();

            return true;
        }

        public void Stop()
        {
            TwitchClient.Disconnect();
        }

        public void Send(string message)
        {
            TwitchClient.SendMessage(TwitchClient.JoinedChannels[0], message);
        }

        private void TwitchClient_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            IsConnected = false;
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            IsConnected = true;
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (!Vote.Voting)
                return;

            var user = e.ChatMessage.Username;

            var msg = e.ChatMessage.Message;

            string[] options = { "#1", "#2", "#3", "#4", "#5", "#6" };

            int index = Array.FindIndex(options, option => msg.Equals(option)) + 1;

            if (!index.Equals(-1))
            {
                if (Vote.CurrentUsersVoted.Contains(user))
                {
                    return;
                }

                Vote.CurrentUsersVoted.Add(user);
                Vote.Add(index);
            }
        }
    }
}