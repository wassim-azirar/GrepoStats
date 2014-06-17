using GalaSoft.MvvmLight.Ioc;
using GrepoStats.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace GrepoStats
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PlayerViewModel>();
            SimpleIoc.Default.Register<AllianceViewModel>();
            SimpleIoc.Default.Register<TownViewModel>();
            SimpleIoc.Default.Register<IslandViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public PlayerViewModel PlayerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<PlayerViewModel>(); }
        }

        public AllianceViewModel AllianceViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AllianceViewModel>(); }
        }

        public TownViewModel TownViewModel
        {
            get { return ServiceLocator.Current.GetInstance<TownViewModel>(); }
        }

        public IslandViewModel IslandViewModel
        {
            get { return ServiceLocator.Current.GetInstance<IslandViewModel>(); }
        }

        public static void Cleanup()
        {
        }
    }
}