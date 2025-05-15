using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ParabolaTrajectory
{
    public partial class MainWindow : Window
    {
        private const double g = 9.8; // Ускорение свободного падения (м/с²)
        private DisplaySettings displaySettings = new DisplaySettings();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtVelocity.Text, out double v0) || v0 <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную начальную скорость (положительное число).",
                    "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtAngle.Text, out double angle) || angle <= 0 || angle >= 90)
            {
                MessageBox.Show("Пожалуйста, введите корректный угол броска (от 0 до 90 градусов).",
                    "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double angleRad = angle * Math.PI / 180;
            double flightTime = 2 * v0 * Math.Sin(angleRad) / g;
            double maxDistance = v0 * v0 * Math.Sin(2 * angleRad) / g;
            double maxHeight = Math.Pow(v0 * Math.Sin(angleRad), 2) / (2 * g);

            lblTime.Content = $"{flightTime:F2} с";
            lblDistance.Content = $"{maxDistance:F2} м";
            lblHeight.Content = $"{maxHeight:F2} м";

            DrawTrajectory(v0, angleRad, maxDistance, maxHeight);
        }

        private void DrawTrajectory(double v0, double angleRad, double maxDistance, double maxHeight)
        {
            if (trajectoryCanvas.ActualWidth <= 0 || trajectoryCanvas.ActualHeight <= 0)
                return;

            trajectoryCanvas.Children.Clear();

            double canvasWidth = trajectoryCanvas.ActualWidth;
            double canvasHeight = trajectoryCanvas.ActualHeight;

            double scaleX = maxDistance > 0 ? canvasWidth / maxDistance : 0;
            double scaleY = maxHeight > 0 ? canvasHeight / (maxHeight * 1.2) : 0;
            double scale = Math.Min(scaleX, scaleY);

            PointCollection points = new PointCollection();
            double totalTime = 2 * v0 * Math.Sin(angleRad) / g;
            for (double t = 0; t <= totalTime; t += 0.05)
            {
                double x = v0 * Math.Cos(angleRad) * t;
                double y = v0 * Math.Sin(angleRad) * t - g * t * t / 2;
                points.Add(new Point(x * scale, canvasHeight - y * scale));
            }

            Polyline trajectory = new Polyline
            {
                Points = points,
                Stroke = new SolidColorBrush(displaySettings.TrajectoryColor),
                StrokeThickness = displaySettings.TrajectoryThickness,
                StrokeLineJoin = PenLineJoin.Round
            };

            trajectoryCanvas.Children.Add(trajectory);

            if (displaySettings.ShowMarkers)
            {
                AddMarker(0, 0, "Старт", scale, canvasHeight);
                AddMarker(maxDistance, 0, "Финиш", scale, canvasHeight);
                AddMarker(maxDistance / 2, maxHeight, $"Макс. высота\n{maxHeight:F1} м", scale, canvasHeight);
            }

            if (displaySettings.ShowAxes)
            {
                DrawAxes(canvasWidth, canvasHeight, maxDistance, maxHeight, scale);
            }
        }

        private void AddMarker(double x, double y, string text, double scale, double canvasHeight)
        {
            Ellipse marker = new Ellipse
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(marker, x * scale - 4);
            Canvas.SetTop(marker, canvasHeight - y * scale - 4);
            trajectoryCanvas.Children.Add(marker);

            TextBlock label = new TextBlock
            {
                Text = text,
                FontSize = 11,
                TextWrapping = TextWrapping.Wrap,
                Background = Brushes.White,
                Padding = new Thickness(2)
            };

            Canvas.SetLeft(label, x * scale + 10);
            Canvas.SetTop(label, canvasHeight - y * scale - 15);
            trajectoryCanvas.Children.Add(label);
        }

        private void DrawAxes(double canvasWidth, double canvasHeight, double maxDistance, double maxHeight, double scale)
        {
            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = canvasHeight,
                X2 = canvasWidth,
                Y2 = canvasHeight,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Line yAxis = new Line
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = canvasHeight,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            trajectoryCanvas.Children.Add(xAxis);
            trajectoryCanvas.Children.Add(yAxis);

            TextBlock xLabel = new TextBlock
            {
                Text = $"Расстояние, м (макс. {maxDistance:F1})",
                Margin = new Thickness(canvasWidth - 150, canvasHeight - 25, 0, 0)
            };

            TextBlock yLabel = new TextBlock
            {
                Text = $"Высота, м (макс. {maxHeight:F1})",
                Margin = new Thickness(10, 5, 0, 0),
                RenderTransform = new RotateTransform(-90)
            };

            trajectoryCanvas.Children.Add(xLabel);
            trajectoryCanvas.Children.Add(yLabel);
        }

        private void NewCalculation_Click(object sender, RoutedEventArgs e)
        {
            txtVelocity.Clear();
            txtAngle.Clear();
            lblTime.Content = "-";
            lblDistance.Content = "-";
            lblHeight.Content = "-";
            trajectoryCanvas.Children.Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DisplaySettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsDialog = new SettingsWindow(displaySettings);
            if (settingsDialog.ShowDialog() == true)
            {
                displaySettings = settingsDialog.Settings;
                if (!string.IsNullOrEmpty(txtVelocity.Text) && !string.IsNullOrEmpty(txtAngle.Text))
                    BtnCalculate_Click(null, null);
            }
        }

        private void UnitsSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал выбора единиц измерения в разработке",
                          "Информация",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Параболическая траектория v1.0\n© 2023",
                          "О программе",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://ru.wikipedia.org/wiki/Движение_тела,_брошенного_под_углом_к_горизонту",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть справку: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class DisplaySettings
    {
        public Color TrajectoryColor { get; set; } = Colors.Blue;
        public double TrajectoryThickness { get; set; } = 2;
        public bool ShowMarkers { get; set; } = true;
        public bool ShowAxes { get; set; } = true;
    }
}