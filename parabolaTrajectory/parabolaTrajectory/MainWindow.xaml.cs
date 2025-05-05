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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtVelocity.Text, out double v0) ||
                !double.TryParse(txtAngle.Text, out double angle))
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.");
                return;
            }

            // Переводим угол в радианы
            double angleRad = angle * Math.PI / 180;

            // Рассчитываем параметры движения
            double flightTime = 2 * v0 * Math.Sin(angleRad) / g;
            double maxDistance = v0 * v0 * Math.Sin(2 * angleRad) / g;
            double maxHeight = Math.Pow(v0 * Math.Sin(angleRad), 2) / (2 * g);

            // Выводим результаты
            lblTime.Text = $"{flightTime:F2} с";
            lblDistance.Text = $"{maxDistance:F2} м";
            lblHeight.Text = $"{maxHeight:F2} м";

            // Рисуем траекторию
            DrawTrajectory(v0, angleRad, maxDistance, maxHeight);
        }

        private void DrawTrajectory(double v0, double angleRad, double maxDistance, double maxHeight)
        {
            trajectoryCanvas.Children.Clear();

            // Масштабируем для отображения в Canvas
            double canvasWidth = trajectoryCanvas.ActualWidth;
            double canvasHeight = trajectoryCanvas.ActualHeight;

            double scaleX = canvasWidth / maxDistance;
            double scaleY = canvasHeight / (maxHeight * 1.2); // Добавляем небольшой отступ сверху

            // Выбираем минимальный масштаб, чтобы траектория полностью помещалась
            double scale = Math.Min(scaleX, scaleY);

            // Рассчитываем точки траектории
            double timeStep = 0.05;
            PointCollection points = new PointCollection();

            for (double t = 0; t <= 2 * v0 * Math.Sin(angleRad) / g; t += timeStep)
            {
                double x = v0 * Math.Cos(angleRad) * t;
                double y = v0 * Math.Sin(angleRad) * t - g * t * t / 2;

                points.Add(new Point(x * scale, canvasHeight - y * scale));
            }

            // Рисуем траекторию
            Polyline trajectory = new Polyline
            {
                Points = points,
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            trajectoryCanvas.Children.Add(trajectory);

            // Добавляем маркеры
            AddMarker(0, 0, "Старт", scale, canvasHeight);
            AddMarker(maxDistance, 0, "Финиш", scale, canvasHeight);
            AddMarker(maxDistance / 2, maxHeight, "Макс. высота", scale, canvasHeight);

            // Добавляем оси
            DrawAxes(canvasWidth, canvasHeight);
        }

        private void AddMarker(double x, double y, string text, double scale, double canvasHeight)
        {
            Ellipse marker = new Ellipse
            {
                Width = 6,
                Height = 6,
                Fill = Brushes.Red,
                Stroke = Brushes.Black
            };

            Canvas.SetLeft(marker, x * scale - 3);
            Canvas.SetTop(marker, canvasHeight - y * scale - 3);
            trajectoryCanvas.Children.Add(marker);

            TextBlock label = new TextBlock
            {
                Text = text,
                FontSize = 10
            };

            Canvas.SetLeft(label, x * scale + 5);
            Canvas.SetTop(label, canvasHeight - y * scale - 15);
            trajectoryCanvas.Children.Add(label);
        }

        private void DrawAxes(double canvasWidth, double canvasHeight)
        {
            // Ось X
            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = canvasHeight,
                X2 = canvasWidth,
                Y2 = canvasHeight,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            // Ось Y
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

            // Подписи осей
            TextBlock xLabel = new TextBlock { Text = "Расстояние (м)", Margin = new Thickness(canvasWidth - 80, canvasHeight - 20, 0, 0) };
            TextBlock yLabel = new TextBlock { Text = "Высота (м)", Margin = new Thickness(10, 10, 0, 0) };

            trajectoryCanvas.Children.Add(xLabel);
            trajectoryCanvas.Children.Add(yLabel);
        }
    }
}
