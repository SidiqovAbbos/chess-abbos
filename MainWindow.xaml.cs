using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Border[,] matrix = null;
        public MainWindow()
        {
            InitializeComponent();
            Initial();
            Reset();
        }

        private void Initial()
        {
            matrix = new Border[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Border border = new Border();
                    string name = $"border{i}{j}";
                    border.Name = name;
                    border.Tag = $"{i}|{j}";
                    border.Margin = new Thickness(1);
                    border.CornerRadius = new CornerRadius(5);
                    border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
                    grid.Children.Add(border);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    matrix[i, j] = border;
                }
            }
        }


        void Reset()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                        matrix[i, j].Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                    else
                        matrix[i, j].Background = new SolidColorBrush(Color.FromRgb(15, 15, 15));
                    matrix[i, j].Opacity = 1;
                }
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Reset();
            var border = (Border)sender;
            border.Background = Brushes.Yellow;
            border.Opacity = .5;
            var stuff = border.Tag.ToString().Split('|');
            int x1 = int.Parse(stuff[0]);
            int y1 = int.Parse(stuff[1]);
            if (shah.IsChecked == true)
                DisplayTable(Shah, x1, y1);

            if (ferz.IsChecked == true)
                DisplayTable(Ferz, x1, y1);

            if (ladya.IsChecked == true)
                DisplayTable(Ladya, x1, y1);

            if (slon.IsChecked == true)
                DisplayTable(Slon, x1, y1);

            if (kon.IsChecked == true)
                DisplayTable(Konya, x1, y1);
        }

        void DisplayTable(Func<int, int, int, int, bool> func, int x1, int y1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == x1 && j == y1)
                        continue;
                    if (func(x1, y1, i, j))
                    {
                        matrix[i, j].Background = Brushes.Green;
                        matrix[i, j].Opacity = .5;
                    }
                }
            }
        }

        bool Ladya(int x1, int y1, int x2, int y2) =>
            x1 == x2 || y1 == y2;

        bool Konya(int x1, int y1, int x2, int y2) =>
           (Math.Abs(x1 - x2) == 2 && Math.Abs(y1 - y2) == 1) || (Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 2);

        bool Slon(int x1, int y1, int x2, int y2) =>
            (x1 - y1) == (x2 - y2) || (x1 + y1) == (x2 + y2);

        bool Ferz(int x1, int y1, int x2, int y2) =>
            (x1 - y1 == x2 - y2) || (x1 + y1) == (x2 + y2) || (x1 == x2 || y1 == y2);

        bool Shah(int x1, int y1, int x2, int y2) =>
            (Math.Abs(x1 - x2) == 1 || Math.Abs(x1 - x2) == 0) &&
            (Math.Abs(y1 - y2) == 1 || Math.Abs(y1 - y2) == 0);

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (matrix != null)
                Reset();
        }


    }
}
