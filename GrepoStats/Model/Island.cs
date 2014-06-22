using GalaSoft.MvvmLight;

namespace GrepoStats.Model
{
    public class Island : ViewModelBase
    {
        // $id, $x, $y, $island_type, $available_towns

        private int _id;
        private int _x;
        private int _y;
        private string _islandType;
        private int _availableTowns;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id)
                {
                    return;
                }

                RaisePropertyChanging(() => Id);
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                if (value == _x)
                {
                    return;
                }

                RaisePropertyChanging(() => X);
                _x = value;
                RaisePropertyChanged(() => X);
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value == _y)
                {
                    return;
                }

                RaisePropertyChanging(() => Y);
                _y = value;
                RaisePropertyChanged(() => Y);
            }
        }

        public string IslandType
        {
            get { return _islandType; }
            set
            {
                if (value == _islandType)
                {
                    return;
                }

                RaisePropertyChanging(() => IslandType);
                _islandType = value;
                RaisePropertyChanged(() => IslandType);
            }
        }

        public int AvailableTowns
        {
            get { return _availableTowns; }
            set
            {
                if (value == _availableTowns)
                {
                    return;
                }

                RaisePropertyChanging(() => AvailableTowns);
                _availableTowns = value;
                RaisePropertyChanged(() => AvailableTowns);
            }
        }
    }
}
