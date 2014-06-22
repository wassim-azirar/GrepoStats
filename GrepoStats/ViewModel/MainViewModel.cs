using GalaSoft.MvvmLight;

namespace GrepoStats.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Ctor

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
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