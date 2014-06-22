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
    public class AllianceViewModel : ViewModelBase
    {
        #region Constantes

        private const string REMOTE_FILE_PATH = @"http://fr74.grepolis.com/data/alliances.txt.gz";
        private const string LOCAL_FILE_PATH = @"C:\Grepolis\alliances.txt.gz";
        private const string EXTRACTED_FILE_NAME = @"C:\Grepolis\alliances.txt";

        #endregion

        #region Ctor

        public AllianceViewModel()
        {
            if (IsInDesignMode)
            {
                AlliancesList = new ObservableCollection<Alliance>
                {
                    new Alliance
                    {
                        Id = 1,
                        Name = "La gloire rouge",
                        Points = 39040,
                        Rank = 39,
                        Members = 35,
                        Villages = 198
                    },

                    new Alliance
                    {
                        Id = 2,
                        Name = "The red dark magic",
                        Points = 109390,
                        Rank = 16,
                        Members = 109,
                        Villages = 37
                    },

                    new Alliance
                    {
                        Id = 3,
                        Name = "Arriba abajo para siempre",
                        Points = 3090290,
                        Rank = 1,
                        Members = 1233,
                        Villages = 11
                    }
                };
            }
            else
            {
                AlliancesList = new ObservableCollection<Alliance>();
                DownloadFileAsync(REMOTE_FILE_PATH);
            }
        }

        #endregion

        #region Properties

        private ObservableCollection<Alliance> _alliancesList;

        private int _progressPercentage;

        public ObservableCollection<Alliance> AlliancesList
        {
            get { return _alliancesList; }
            set
            {
                if (_alliancesList == value)
                    return;

                RaisePropertyChanging(() => AlliancesList);
                _alliancesList = value;
                RaisePropertyChanged(() => AlliancesList);
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

                    AlliancesList.Add(new Alliance
                        {
                            // $id, $name, $points, $villages, $members, $rank

                            Id = Convert.ToInt32(parts[0]),
                            Name = Convert.ToString(parts[1]),
                            Points = Convert.ToInt32(parts[2]),
                            Villages = Convert.ToInt32(parts[3]),
                            Members = Convert.ToInt32(parts[4]),
                            Rank = Convert.ToInt32(parts[5])
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