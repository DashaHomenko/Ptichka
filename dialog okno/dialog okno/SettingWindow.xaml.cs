using System.Windows;
using System.Windows.Media;

namespace ParabolaTrajectory
{
    public partial class SettingsWindow : Window
    {
        public DisplaySettings Settings { get; private set; }

        public SettingsWindow(DisplaySettings currentSettings)
        {
            InitializeComponent();
            Settings = currentSettings;

            // Инициализация элементов управления текущими настройками
            cmbColors.SelectedIndex = GetColorIndex(Settings.TrajectoryColor);
            sliderThickness.Value = Settings.TrajectoryThickness;
            chkShowMarkers.IsChecked = Settings.ShowMarkers;
            chkShowAxes.IsChecked = Settings.ShowAxes;
        }

        private int GetColorIndex(Color color)
        {
            if (color == Colors.Blue) return 0;
            if (color == Colors.Red) return 1;
            if (color == Colors.Green) return 2;
            return 3; // Black
        }

        private Color GetSelectedColor()
        {
            switch (cmbColors.SelectedIndex)
            {
                case 0: return Colors.Blue;
                case 1: return Colors.Red;
                case 2: return Colors.Green;
                default: return Colors.Black;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Settings.TrajectoryColor = GetSelectedColor();
            Settings.TrajectoryThickness = sliderThickness.Value;
            Settings.ShowMarkers = chkShowMarkers.IsChecked ?? true;
            Settings.ShowAxes = chkShowAxes.IsChecked ?? true;

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
