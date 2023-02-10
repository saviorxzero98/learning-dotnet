using System;
using System.Configuration;
using System.IO;
using Topshelf.Logging;

namespace CS_Topshelf
{
    public class BackendService
    {
        private static readonly LogWriter _log = HostLogger.Get<BackendService>();
        private FileSystemWatcher _watcher;
        public bool OnStart()
        {
            _watcher = new FileSystemWatcher(@"WorkingFiles", "*.json");
            _watcher.Changed += OnFileChange;
            _watcher.IncludeSubdirectories = false;
            _watcher.EnableRaisingEvents = true;
            return true;
        }
        private void OnFileChange(object sender, FileSystemEventArgs e)
        {
            _log.InfoFormat($"{e.FullPath} Changed");
            var config = ConfigurationManager.AppSettings["App Setting"].ToString();
            Console.WriteLine(config);
        }
        public bool OnStop()
        {
            _watcher.Dispose();
            return true;
        }
    }
}
