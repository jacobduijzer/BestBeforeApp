using System;
using System.IO;
using BestBeforeApp.Helpers;

namespace BestBeforeApp.Droid
{
    public class DatabaseFileHelper : IDatabaseFileHelper
    {
        public string GetLocalFilePath(string filename) =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
    }
}
