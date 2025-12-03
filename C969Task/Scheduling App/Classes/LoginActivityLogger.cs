using System;
using System.IO;

namespace Scheduling_App
{
    public static class LoginActivityLogger
    {
        private static readonly string fileName = "Login_History.txt";

        public static void logActivity(User user)
        {
            string line = $"{DateTime.UtcNow:u} - {user.UserName}";
            File.AppendAllText(fileName, line + Environment.NewLine);
        }
    }
}
