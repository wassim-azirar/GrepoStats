using GalaSoft.MvvmLight;

namespace GrepoStats.ViewModel
{
    public class IslandViewModel : ViewModelBase
    {
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
