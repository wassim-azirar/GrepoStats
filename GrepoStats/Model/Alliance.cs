using GalaSoft.MvvmLight;

namespace GrepoStats.Model
{
    public class Alliance : ViewModelBase
    {
        // $id, $name, $points, $villages, $members, $rank

        private int _id;
        private int _members;
        private string _name;
        private int _points;
        private int _rank;
        private int _villages;

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

        public int Villages
        {
            get { return _villages; }
            set
            {
                if (value == _villages)
                {
                    return;
                }

                RaisePropertyChanging(() => Villages);
                _villages = value;
                RaisePropertyChanged(() => Villages);
            }
        }

        public int Members
        {
            get { return _members; }
            set
            {
                if (value == _members)
                {
                    return;
                }

                RaisePropertyChanging(() => Members);
                _members = value;
                RaisePropertyChanged(() => Members);
            }
        }

        public int Rank
        {
            get { return _rank; }
            set
            {
                if (value == _rank)
                {
                    return;
                }

                RaisePropertyChanging(() => Rank);
                _rank = value;
                RaisePropertyChanged(() => Rank);
            }
        }
    }
}