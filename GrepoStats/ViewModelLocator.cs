using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GrepoStats.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace GrepoStats
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<AllianceViewModel>();
            SimpleIoc.Default.Register<IslandViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PlayerViewModel>();            
            SimpleIoc.Default.Register<TownViewModel>();            
        }

        #endregion

        #region Instances

        public AllianceViewModel AllianceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AllianceViewModel>(CurrentKey); }
        }
        
        public IslandViewModel IslandViewModel
        {
            get { return ServiceLocator.Current.GetInstance<IslandViewModel>(CurrentKey); }
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(CurrentKey); }
        }

        public PlayerViewModel PlayerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PlayerViewModel>(CurrentKey); }
        }

        public TownViewModel TownViewModel
        {
            get { return ServiceLocator.Current.GetInstance<TownViewModel>(CurrentKey); }
        }

        #endregion

        #region CleanUp

        private static string _currentKey = System.Guid.NewGuid().ToString();
        public static string CurrentKey
        {
            get { return _currentKey; }
            private set { _currentKey = value; }
        }

        public static void Cleanup()
        {
            var viewModelLocator = (ViewModelLocator)Application.Current.Resources["Locator"];
            
            viewModelLocator.AllianceViewModel.Cleanup();
            viewModelLocator.IslandViewModel.Cleanup();
            viewModelLocator.MainViewModel.Cleanup();
            viewModelLocator.PlayerViewModel.Cleanup();
            viewModelLocator.TownViewModel.Cleanup();

            SimpleIoc.Default.Unregister<AllianceViewModel>(CurrentKey);
            SimpleIoc.Default.Unregister<IslandViewModel>(CurrentKey);
            SimpleIoc.Default.Unregister<MainViewModel>(CurrentKey);
            SimpleIoc.Default.Unregister<PlayerViewModel>(CurrentKey);
            SimpleIoc.Default.Unregister<TownViewModel>(CurrentKey);

            Messenger.Reset();

            CurrentKey = System.Guid.NewGuid().ToString();
        }

        #endregion
    }
}