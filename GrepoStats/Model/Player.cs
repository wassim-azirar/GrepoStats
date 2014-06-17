using GalaSoft.MvvmLight;

namespace GrepoStats.Model
{
    public class Player : ViewModelBase
    {
        //$id, $name, $alliance_id, $points, $rank, $towns

        private int? _allianceId;
        private int _id;
        private string _name;
        private int _points;
        private int _rank;
        private int _towns;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;

                RaisePropertyChanging(() => Id);
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;

                RaisePropertyChanging(() => Name);
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int? AllianceId
        {
            get { return _allianceId; }
            set
            {
                if (_allianceId == value)
                    return;

                RaisePropertyChanging(() => AllianceId);
                _allianceId = value;
                RaisePropertyChanged(() => AllianceId);
            }
        }

        public int Points
        {
            get { return _points; }
            set
            {
                if (_points == value)
                    return;

                RaisePropertyChanging(() => Points);
                _points = value;
                RaisePropertyChanged(() => Points);
            }
        }

        public int Rank
        {
            get { return _rank; }
            set
            {
                if (_rank == value)
                    return;

                RaisePropertyChanging(() => Rank);
                _rank = value;
                RaisePropertyChanged(() => Rank);
            }
        }

        public int Towns
        {
            get { return _towns; }
            set
            {
                if (_towns == value)
                    return;

                RaisePropertyChanging(() => Towns);
                _towns = value;
                RaisePropertyChanged(() => Towns);
            }
        }
    }
}