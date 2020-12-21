using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        enum FIGURES { LINE = 0, BOX, RIGHT_HORSE, LEFT_HORSE, RIGHT_SNAKE, LEFT_SNAKE, PENNIS };
        enum MOVE { LEFT, RIGHT };
        Random random = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        public static List<Point> filledCubes = new List<Point>();
        Figure figure;
        int score;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            score = 0;
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_tick;
            figure = new Box(new Point(160, 0));
            for (int i = 0; i < 4; i++)
            {
                GameField.Children.Add(figure.rectangles[i].rect);
            }
            timer.Start();
        }

        public void Timer_tick(object sender, EventArgs e)
        {
            if (figure.moveDown() == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dispatcher.Invoke(() => figure.rectangles[i].rect.Fill = Brushes.Green);
                }
                refreshField();
            }
        }

        private void refreshField()
        {
            for (int i = 0; i < 4; i++)
            {
                filledCubes.Add(new Point(figure.FigureCoordinates[i].X, figure.FigureCoordinates[i].Y));
            }

            //checking
            for (int i = 0; i < 16; i++)
            {
                if (CheckRow(i * 40))
                {
                    deleteRow(i);
                }
            }

            //add new figure
            figure = createFigure((FIGURES)random.Next(0,6), new Point(160, 0));
            for (int i = 0; i < 4; i++)
            {
                GameField.Children.Add(figure.rectangles[i].rect);
            }

            if (checkLost())
            {
                try
                {
                    MessageBox.Show("You are looser.\nYour scored " + score, "Important message");
                    Application.Current.Shutdown();
                }
                catch (Exception)
                {

                }
            }

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    {
                        bool movedDown = figure.moveDown();
                        while (movedDown)
                        {
                            movedDown = figure.moveDown();
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            Dispatcher.Invoke(() => figure.rectangles[i].rect.Fill = Brushes.Green);
                        }
                        refreshField();
                        break;
                    }
                case Key.Left:
                    {
                        figure.moveLeft();
                        break;
                    }
                case Key.Right:
                    {
                        figure.moveRight();
                        break;
                    }
                case Key.R:
                    {
                        figure.rotate();
                        break;
                    }
            }
        }

        private bool CheckRow(int y)
        {
            for (int k = 0; k < 12; k++)
            {
                if (!filledCubes.Exists(p => p.X == k * 40 && p.Y == y))
                {
                    return false;
                }
            }
            return true;
        }

        delegate void deleg(Rectangle r);

        private void deleteRow(int n)
        {
            var children = GameField.Children.OfType<UIElement>().ToList();
            for (int i = 0; i < 12; i++)
            {
                foreach (UIElement child in children)
                {
                    if (Canvas.GetLeft(child) == i * 40 && Canvas.GetTop(child) == n * 40)
                    {
                        GameField.Children.Remove(child);
                        filledCubes.Remove(new Point(i * 40, n * 40));
                        score += 1;
                    }
                }

            }
            children = GameField.Children.OfType<UIElement>().ToList();
            foreach (UIElement child in children)
            {
                if (Canvas.GetTop(child) < n * 40)
                    Canvas.SetTop(child, Canvas.GetTop(child) + 40);
            }
            for (int i = 0; i < filledCubes.Count; i++)
            {
                if (filledCubes[i].Y < n * 40)
                    filledCubes[i] = new Point(filledCubes[i].X, filledCubes[i].Y + 40);
            }
        }

        bool checkLost()
        {
            foreach (Point p in filledCubes)
            {
                if (p.Y == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private Figure createFigure(FIGURES figure, Point coordinates)
        { 
            switch (figure){
                case FIGURES.BOX:
                {
                    return new Box(coordinates);
                }
                case FIGURES.LEFT_HORSE:
                {
                    return new LeftHorse(coordinates);
                }
                case FIGURES.RIGHT_HORSE:
                {
                    return new RightHorse(coordinates);
                }
                case FIGURES.LEFT_SNAKE:
                {
                    return new LeftSnake(coordinates);
                }
                case FIGURES.RIGHT_SNAKE:
                {
                    return new RightSnake(coordinates);
                }
                case FIGURES.LINE:
                {
                    return new Line(coordinates);
                }
                case FIGURES.PENNIS:
                {
                    return new Pennis(coordinates);
                }
            }
            return new Box(coordinates);
        }

    }
}