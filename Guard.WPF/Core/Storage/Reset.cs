﻿using System.IO;
using Guard.WPF.Core.Icons;
using Guard.WPF.Core.Security;
using Guard.Core;

namespace Guard.WPF.Core.Storage
{
    internal class Reset
    {
        internal static void DeleteEverything()
        {
            string path = InstallationContext.GetAppDataFolderPath();
            Database.Deinit();
            _ = WindowsHello.Unregister();
            Auth.DeleteWindowsHelloProtectedKey();
            IconManager.RemoveAllCustomIcons();

            string[] files = ["auth-keys", "settings", "TokenDatabase.db", "TokenDatabase-log.db"];

            foreach (string file in files)
            {
                string filePath = Path.Combine(path, file);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
