using System;
using System.IO;
using BestBeforeApp.Helpers;

namespace BestBeforeApp.iOS
{
    public class DatabaseFileHelper : IDatabaseFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            return Path.Combine(libraryPath, filename);
        }
    }
}
