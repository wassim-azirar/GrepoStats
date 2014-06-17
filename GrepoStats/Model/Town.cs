using GalaSoft.MvvmLight;

namespace GrepoStats.Model
{
    public class Town : ViewModelBase
    {
        // $id, $player_id, $name, $island_x, $island_y, $number_on_island, $points

        private int _id;
        private int _islandX;
        private int _islandY;
        private string _name;
        private int _numberOnIsland;
        private int? _playerId;
        private int _points;

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

        public int? PlayerId
        {
            get { return _playerId; }
            set
            {
                if (value == _playerId)
                {
                    return;
                }

                RaisePropertyChanging(() => PlayerId);
                _playerId = value;
                RaisePropertyChanged(() => PlayerId);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                {
                    return;
                }

                RaisePropertyChanging(() => Name);
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int IslandX
        {
            get { return _islandX; }
            set
            {
                if (value == _islandX)
                {
                    return;
                }

                RaisePropertyChanging(() => IslandX);
                _islandX = value;
                RaisePropertyChanged(() => IslandX);
            }
        }

        public int IslandY
        {
            get { return _islandY; }
            set
            {
                if (value == _islandY)
                {
                    return;
                }

                RaisePropertyChanging(() => IslandY);
                _islandY = value;
                RaisePropertyChanged(() => IslandY);
            }
        }

        public int NumberOnIsland
        {
            get { return _numberOnIsland; }
            set
            {
                if (value == _numberOnIsland)
                {
                    return;
                }

                RaisePropertyChanging(() => NumberOnIsland);
                _numberOnIsland = value;
                RaisePropertyChanged(() => NumberOnIsland);
            }
        }

        public int Points
        {
            get { return _points; }
            set
            {
                if (value == _points)
                {
                    return;
                }

                RaisePropertyChanging(() => Points);
                _points = value;
                RaisePropertyChanged(() => Points);
            }
        }
    }
}