using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace PlayASetlist.ChatReader
{
    internal static class Parse
    {
        public static bool ChannelExist(string channel)
        {
            string url = $"https://nightdev.com/api/1/kapchat/channels/{channel}/bootstrap";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static string GetVideoId(string channelUrl)
        {
            string channel = channelUrl;
            string pattern = channel.Contains("channel") ? @"(?:\/channel\/)([^\/]+)(?:\/.*)?$" : @"(?:youtube\.com\/)([^\/]+)";

            var match = Regex.Match(channel, pattern);

            if (!match.Success)
            {
                return null; // Channel URL isn't in the correct format
            }

            if (channel.StartsWith("http://"))
            {
                channel = channel.Replace("http://", "https://");
            }
            else if (!channel.StartsWith("https://"))
            {
                channel = "https://" + channel;
            }

            if (channel.EndsWith("/"))
            {
                channel = channel.Substring(0, channel.Length - 1);
            }
            else if (channel.EndsWith("/featured"))
            {
                channel = channel.Substring(0, channel.Length - 9);
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string htmlContent = webClient.DownloadString($"{channel}/live");
                    string videoId = ParseVideoId(htmlContent);

                    if (string.IsNullOrEmpty(videoId))
                    {
                        return null; // The channel isn't livestreaming right now.
                    }

                    return videoId;
                }
            }
            catch (Exception)
            {
                return null; // Error: {ex}
            }
        }
        private static string ParseVideoId(string htmlContent)
        {
            int indexVideoIdStart = htmlContent.IndexOf("videoId\":\"") + 10;
            int indexVideoIdEnd = htmlContent.IndexOf("\"", indexVideoIdStart);

            var videoId = htmlContent.Substring(indexVideoIdStart, indexVideoIdEnd - indexVideoIdStart);

            if (!htmlContent.Contains($"{videoId}/hqdefault_live.jpg"))
            {
                return null;
            }

            return videoId;
        }
    }
}