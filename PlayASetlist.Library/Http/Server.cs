using PlayASetlist.Library.Votes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace PlayASetlist.Library.Http
{
    public static class Server
    {
        public static string Response = string.Empty;
        private static readonly Json Json = new Json();
        private static HttpListener HttpListenerServer = new HttpListener();
        private static Thread ServerListener;
        private static int CurrentPort = 50300;

        public static void ParseResponse(string title, int mode, string textTimeLeft = "", string textOption = "")
        {
            var data = new Data
            {
                Title = title
            };

            if (mode.Equals(0) && Vote.CurrentRandomSongs.Count > 0)
            {
                var options = new List<Option>();

                for (int i = 1; i < 6; i++)
                {
                    var vote = 0;
                    if (Vote.CurrentVotes.ContainsKey(i))
                        vote = Vote.CurrentVotes[i];

                    options.Add(new Option { Name = $"{i} - {Vote.CurrentRandomSongs[i - 1].Property}", Votes = vote });
                }

                options.Add(new Option { Name = $"{6} - {textOption}", Votes = Vote.CurrentVotes.ContainsKey(6) ? Vote.CurrentVotes[6] : 0 });

                data.TimeLeft = $"{textTimeLeft} {Vote.TimeLeftToVote}";

                data.Options = options;
            }
            else if (mode.Equals(1))
            {
                var selection = new List<Selected>();

                for (int i = 1; i < Vote.CurrentExportSongs.Count + 1; i++)
                {
                    selection.Add(new Selected { Name = $"{i}) {Vote.CurrentExportSongs[i - 1].Property}" });
                }

                data.Selected = selection;
            }

            Response = string.IsNullOrEmpty(title) ? "" : Json.Serialize(data);
        }

        public static void Start(int port)
        {
            CurrentPort = port;
            ServerListener = new Thread(CreateListener);
            ServerListener.Start();
        }

        public static void Stop()
        {
            if (HttpListenerServer?.IsListening == true)
            {
                HttpListenerServer.Stop();
                ServerListener.Abort();
            }
        }

        private static void CreateListener()
        {
            HttpListenerServer = new HttpListener();
            HttpListenerServer.Prefixes.Add($"http://localhost:{CurrentPort}/playasetlist/"); // Cambia al puerto 8080
            HttpListenerServer.Start();

            while (HttpListenerServer.IsListening)
            {
                try
                {
                    HttpListenerContext context = HttpListenerServer.GetContext();

                    byte[] buffer = Encoding.UTF8.GetBytes(Response);

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); // Permitir solicitudes desde cualquier origen
                    context.Response.ContentType = "application/json";
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}