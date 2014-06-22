using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using GalaSoft.MvvmLight;
using GrepoStats.Helper;
using GrepoStats.Model;
using GrepoStats.Utils;

namespace GrepoStats.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        #region Constantes

        private const string REMOTE_FILE_PATH = @"http://fr74.grepolis.com/data/players.txt.gz";
        private const string LOCAL_FILE_PATH = @"C:\Grepolis\players.txt.gz";
        private const string EXTRACTED_FILE_NAME = @"C:\Grepolis\players.txt";

        #endregion

        #region Ctor

        public PlayerViewModel()
        {
            if (IsInDesignMode)
            {
                PlayersList = DefaultDataLoader.GetPlayersDefaultData().ToObservableCollection();
            }
            else
            {
                PlayersList = new ObservableCollection<Player>();
                DownloadFileAsync(REMOTE_FILE_PATH);
            }
        }

        #endregion

        #region Properties

        private ObservableCollection<Player> _playersList;

        private int _progressPercentage;

        public ObservableCollection<Player> PlayersList
        {
            get { return _playersList; }
            set
            {
                if (_playersList == value)
                    return;

                RaisePropertyChanging(() => PlayersList);
                _playersList = value;
                RaisePropertyChanged(() => PlayersList);
            }
        }

        public int ProgressPercentage
        {
            get { return _progressPercentage; }
            set
            {
                if (_progressPercentage == value)
                    return;

                RaisePropertyChanging(() => ProgressPercentage);
                _progressPercentage = value;
                RaisePropertyChanged(() => ProgressPercentage);
            }
        }

        #endregion

        #region Methods

        private void DownloadFileAsync(string file)
        {
            var client = new WebClient
                {
                    Credentials = CredentialCache.DefaultCredentials,
                    Proxy = {Credentials = CredentialCache.DefaultCredentials}
                };

            client.DownloadProgressChanged += ClientOnDownloadProgressChanged;
            client.DownloadFileCompleted += ClientOnDownloadFileCompleted;
            client.DownloadFileAsync(new Uri(file), LOCAL_FILE_PATH);
        }

        private void ClientOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs eventArgs)
        {
            ProgressPercentage = eventArgs.ProgressPercentage;
        }

        private void ClientOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
        {
            Debug.WriteLine("Downloading file completed");

            if (FileHelper.IsFileOld(LOCAL_FILE_PATH, EXTRACTED_FILE_NAME))
            {
                UnzipFile(LOCAL_FILE_PATH);
            }
            else
            {
                // TODO : Notify the View that the file is up to date
            }
        }

        private void UnzipFile(string file)
        {
            using (var fileInputStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var gzipStream = new GZipStream(fileInputStream, CompressionMode.Decompress))
                {
                    using (var fileOutpuStream = new FileStream(EXTRACTED_FILE_NAME, FileMode.Create, FileAccess.Write))
                    {
                        var tempBytes = new byte[4096];
                        int i;
                        while ((i = gzipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                        {
                            fileOutpuStream.Write(tempBytes, 0, i);
                        }
                    }
                }
            }

            Debug.WriteLine("Unzipping file completed");

            ReadFile(EXTRACTED_FILE_NAME);
        }

        private void ReadFile(string file)
        {
            // 1- Reading each line into a string
            using (StreamReader streamReader = File.OpenText(file))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    PlayersList.Add(new Player
                        {
                            //$id, $name, $alliance_id, $points, $rank, $towns

                            Id = Convert.ToInt32(parts[0]),
                            Name = Convert.ToString(parts[1]),
                            AllianceId = !string.IsNullOrEmpty(parts[2]) ? Convert.ToInt32(parts[2]) : (int?) null,
                            Points = Convert.ToInt32(parts[3]),
                            Rank = Convert.ToInt32(parts[4]),
                            Towns = Convert.ToInt32(parts[5])
                        });
                }
            }

            // 2- Reading each line into a string using a BufferedReader
            // http://blogs.davelozinski.com/curiousconsultant/csharp-net-fastest-way-to-read-text-files
            /*
            using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Do work here
                }
            }*/

            Debug.WriteLine("Reading file completed");
        }

        #endregion

        #region CleanUp

        /// <summary>
        ///     Unregisters this instance from the Messenger class.
        ///     To cleanup additional resources, override this method, clean up and then call base.Cleanup()
        /// </summary>
        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }
}