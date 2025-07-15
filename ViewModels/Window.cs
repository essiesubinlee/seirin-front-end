using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using seirin1.Data;

namespace seirin1.ViewModels
{
    public class ChartWindow : INotifyPropertyChanged
    {
        private ObservableCollection<PowerPoints> _data;

        public ObservableCollection<PowerPoints> Data
        {
            get => _data;
            set
            {
                _data = value;
                _data.CollectionChanged += (s, e) => DataUpdated?.Invoke();
                OnPropertyChanged();
            }
        }

        public string ChartType { get; set; }
        public string Title { get; set; }
        public Point Position { get; set; } = new Point(0, 0);
        public Size Size { get; set; } = new Size(400, 300);
        public event Action DataUpdated;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}