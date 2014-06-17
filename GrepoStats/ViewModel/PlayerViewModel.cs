using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using GalaSoft.MvvmLight;
using GrepoStats.Model;

namespace GrepoStats.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        #region Constantes

        private const string RemoteFilePath = @"http://fr74.grepolis.com/data/players.txt.gz";
        private const string LocalFilePath = @"C:\Grepolis\players.txt.gz";
        private const string ExtractedFileName = @"C:\Grepolis\players.txt";

        #endregion

        #region Ctor

        public PlayerViewModel()
        {
            if (IsInDesignMode)
            {
                PlayersList = new ObservableCollection<Player>
                {
                    new Player
                    {
                        Id = 1,
                        AllianceId = 25,
                        Name = "Stéphane",
                        Points = 3900,
                        Rank = 34,
                        Towns = 3
                    },

                    new Player
                    {
                        Id = 2,
                        AllianceId = 31,
                        Name = "Jean Jack",
                        Points = 1890,
                        Rank = 47,
                        Towns = 1
                    },

                    new Player
                    {
                        Id = 3,
                        AllianceId = 10,
                        Name = "Pierre-Marie",
                        Points = 12001,
                        Rank = 2,
                        Towns = 10
                    },
                };
            }
            else
            {
                PlayersList = new ObservableCollection<Player>();
                DownloadFileAsync(RemoteFilePath);
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
            client.DownloadFileAsync(new Uri(file), LocalFilePath);
        }

        private void ClientOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs eventArgs)
        {
            ProgressPercentage = eventArgs.ProgressPercentage;
        }

        private void ClientOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
        {
            Debug.WriteLine("Downloading file completed");

            UnzipFile(LocalFilePath);
        }

        private void UnzipFile(string file)
        {
            using (var fileInputStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var gzipStream = new GZipStream(fileInputStream, CompressionMode.Decompress))
                {
                    using (var fileOutpuStream = new FileStream(ExtractedFileName, FileMode.Create, FileAccess.Write))
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

            ReadFile(ExtractedFileName);
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
    }
}