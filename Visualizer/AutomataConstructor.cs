using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для AutomataConstructor.xaml
    /// </summary>
    public partial class AutomataConstructor : Page, INotifyPropertyChanged
    {
        public AutomataConstructor()
        {
            DataContext = this;
            InitializeComponent();
        }

        private bool _isInfinite;
        public bool IsInfinite
        {
            get => _isInfinite;
            set
            {
                if (_isInfinite!= value)
                {
                    _isInfinite = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
