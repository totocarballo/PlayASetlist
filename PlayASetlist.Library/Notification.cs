using System;
using System.Media;

namespace PlayASetlist.Library
{
    public static class Notification
    {
        public static void Sound()
        {
            try
            {
                SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}