using System;
using System.Windows;

namespace FlightCalculator
{
    public partial class MainWindow : Window
    {
        private const double Gravity = 9.81; // ускорение свободного падения (м/с²)

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем входные данные
                double initialVelocity = double.Parse(InitialVelocityTextBox.Text);
                double launchAngleDegrees = double.Parse(LaunchAngleTextBox.Text);

                // Преобразуем угол в радианы
                double launchAngleRadians = launchAngleDegrees * Math.PI / 180;

                // Рассчитываем параметры полета
                double timeOfFlight = CalculateTimeOfFlight(initialVelocity, launchAngleRadians);
                double range = CalculateRange(initialVelocity, launchAngleRadians, timeOfFlight);
                double maxHeight = CalculateMaxHeight(initialVelocity, launchAngleRadians);

                // Отображаем результаты
                TimeOfFlightText.Text = $"Время полета: {timeOfFlight:F2} секунд";
                RangeText.Text = $"Дальность полета: {range:F2} метров";
                MaxHeightText.Text = $"Максимальная высота: {maxHeight:F2} метров";
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateTimeOfFlight(double v0, double angle)
        {
            // Время полета = (2 * v0 * sin(angle)) / g
            return (2 * v0 * Math.Sin(angle)) / Gravity;
        }

        private double CalculateRange(double v0, double angle, double timeOfFlight)
        {
            // Дальность полета = v0 * cos(angle) * время полета
            return v0 * Math.Cos(angle) * timeOfFlight;
        }

        private double CalculateMaxHeight(double v0, double angle)
        {
            // Максимальная высота = (v0² * sin²(angle)) / (2 * g)
            return (Math.Pow(v0, 2) * Math.Pow(Math.Sin(angle), 2)) / (2 * Gravity);
        }
    }
}
