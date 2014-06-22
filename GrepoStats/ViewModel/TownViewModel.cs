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
    public class TownViewModel : ViewModelBase
    {
        #region Constantes

        private const string REMOTE_FILE_PATH = @"http://fr74.grepolis.com/data/towns.txt.gz";
        private const string LOCAL_FILE_PATH = @"C:\Grepolis\towns.txt.gz";
        private const string EXTRACTED_FILE_NAME = @"C:\Grepolis\towns.txt";

        #endregion

        #region Ctor

        public TownViewModel()
        {
            if (IsInDesignMode)
            {
                TownsList = new ObservableCollection<Town>
                {
                    new Town
                    {
                        Id = 1,
                        Name = "Paris",
                        Points = 11009,
                        IslandX = 119,
                        IslandY = 390,
                        NumberOnIsland = 0,
                        PlayerId = 3
                    },

                    new Town
                    {
                        Id = 2,
                        Name = "Stronghold",
                        Points = 3377,
                        IslandX = 399,
                        IslandY = 401,
                        NumberOnIsland = 3,
                        PlayerId = 1
                    },

                    new Town
                    {
                        Id = 3,
                        Name = "SimCity",
                        Points = 3771,
                        IslandX = 100,
                        IslandY = 240,
                        NumberOnIsland = 0,
                        PlayerId = 2
                    }
                };
            }
            else
            {
                TownsList = new ObservableCollection<Town>();
                DownloadFileAsync(REMOTE_FILE_PATH);    
            }
        }

        #endregion

        #region Properties

        private int _progressPercentage;
        private ObservableCollection<Town> _townsList;

        public ObservableCollection<Town> TownsList
        {
            get { return _townsList; }
            set
            {
                if (_townsList == value)
                    return;

                RaisePropertyChanging(() => TownsList);
                _townsList = value;
                RaisePropertyChanged(() => TownsList);
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

            UnzipFile(LOCAL_FILE_PATH);
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

                    TownsList.Add(new Town
                    {
                        // $id, $player_id, $name, $island_x, $island_y, $number_on_island, $points

                        Id = Convert.ToInt32(parts[0]),
                        PlayerId = !string.IsNullOrEmpty(parts[1]) ? Convert.ToInt32(parts[1]) : (int?) null,
                        Name = Convert.ToString(parts[2]),
                        IslandX = Convert.ToInt32(parts[3]),
                        IslandY = Convert.ToInt32(parts[4]),
                        NumberOnIsland = Convert.ToInt32(parts[5]),
                        Points = Convert.ToInt32(parts[6]),
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