using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private NumericUpDown angleInput;
        private NumericUpDown velocityInput;
        private NumericUpDown gravityInput;
        private NumericUpDown heightInput;
        private Button calculateButton;
        private TextBox resultBox;

        public Form1()
        {
            InitializeCustomComponents(); // Наш метод инициализации
            this.Text = "Калькулятор параметров полета";
            this.ClientSize = new Size(400, 350);
        }

        private void InitializeCustomComponents()
        {
            // Инициализация элементов управления
            angleInput = new NumericUpDown();
            velocityInput = new NumericUpDown();
            gravityInput = new NumericUpDown();
            heightInput = new NumericUpDown();
            calculateButton = new Button();
            resultBox = new TextBox();

            // Настройка NumericUpDown для угла
            angleInput.Location = new Point(150, 18);
            angleInput.Size = new Size(80, 27);
            angleInput.Minimum = 1;
            angleInput.Maximum = 89;
            angleInput.Value = 45;

            // Настройка NumericUpDown для скорости
            velocityInput.Location = new Point(150, 58);
            velocityInput.Size = new Size(80, 27);
            velocityInput.Minimum = 1;
            velocityInput.Maximum = 200;
            velocityInput.Value = 20;

            // Настройка NumericUpDown для гравитации
            gravityInput.Location = new Point(150, 98);
            gravityInput.Size = new Size(80, 27);
            gravityInput.Minimum = 1;
            gravityInput.Maximum = 50;
            gravityInput.DecimalPlaces = 1;
            gravityInput.Value = 9.8m;

            // Настройка NumericUpDown для высоты
            heightInput.Location = new Point(150, 138);
            heightInput.Size = new Size(80, 27);
            heightInput.Minimum = 0;
            heightInput.Maximum = 1000;

            // Настройка кнопки
            calculateButton.Text = "Рассчитать параметры";
            calculateButton.Location = new Point(20, 180);
            calculateButton.Size = new Size(180, 35);
            calculateButton.Click += CalculateParameters;

            // Настройка текстового поля для результатов
            resultBox.Multiline = true;
            resultBox.Location = new Point(20, 230);
            resultBox.Size = new Size(350, 80);
            resultBox.ReadOnly = true;
            resultBox.Font = new Font("Arial", 10);

            // Добавление элементов на форму
            this.Controls.Add(new Label { Text = "Угол броска (°):", Location = new Point(20, 20), AutoSize = true });
            this.Controls.Add(angleInput);
            this.Controls.Add(new Label { Text = "Начальная скорость (м/с):", Location = new Point(20, 60), AutoSize = true });
            this.Controls.Add(velocityInput);
            this.Controls.Add(new Label { Text = "Ускорение (м/с²):", Location = new Point(20, 100), AutoSize = true });
            this.Controls.Add(gravityInput);
            this.Controls.Add(new Label { Text = "Начальная высота (м):", Location = new Point(20, 140), AutoSize = true });
            this.Controls.Add(heightInput);
            this.Controls.Add(calculateButton);
            this.Controls.Add(resultBox);
        }

        private void CalculateParameters(object sender, EventArgs e)
        {
            try
            {
                double angle = (double)angleInput.Value;
                double velocity = (double)velocityInput.Value;
                double gravity = (double)gravityInput.Value;
                double initialHeight = (double)heightInput.Value;

                double flightTime = (velocity * Math.Sin(angle * Math.PI / 180) +
                                   Math.Sqrt(Math.Pow(velocity * Math.Sin(angle * Math.PI / 180), 2) + 2 * gravity * initialHeight)) / gravity;
                double maxHeight = initialHeight +
Math.Pow(velocity * Math.Sin(angle * Math.PI / 180), 2) / (2 * gravity);
                double distance = velocity * Math.Cos(angle * Math.PI / 180) * flightTime;

                resultBox.Text = $"Результаты расчета:\n\n" +
                                $"Дальность полета: {distance:F2} м\n" +
                                $"Максимальная высота: {maxHeight:F2} м\n" +
                                $"Время полета: {flightTime:F2} с";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка расчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}