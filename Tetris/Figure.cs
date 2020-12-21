using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Tetris
{
    public struct Cube{
        public Rectangle rect;
        public int top;
        public int left;
    } 

    abstract class Figure
    {
        public int step = 40;
        private Point[] figureCoordinates = new Point[4];
        public  Point[] FigureCoordinates { get { return figureCoordinates; } set { figureCoordinates = value; } }
        public Cube [] rectangles = new Cube[4];

        public Figure()
        {
            for(int i = 0; i < 4; i++)
            {
                rectangles[i].rect = new Rectangle();
                rectangles[i].rect.Width = step;
                rectangles[i].rect.Height = step;
                rectangles[i].rect.Fill = Brushes.Red;
            }
        }

        public bool moveDown()
        {
            
            for (int k = 0; k < 4; k++)
            {
                if (figureCoordinates[k].Y + step > 600)
                    return false; //нижняя граница

                foreach (Point p in MainWindow.filledCubes)
                {
                if (figureCoordinates[k].Y + step == p.Y && figureCoordinates[k].X == p.X)
                    return false;
                }    
            }

            for (int i = 0; i<4; i++)
            {
                figureCoordinates[i]. Y += step;
            }
            updateCanvasLocation();
            return true;
        }

        public void moveLeft()
        {
            for(int k = 0; k < 4; k++)
            {
                if (figureCoordinates[k].X - step < 0) return;
                foreach (Point p in MainWindow.filledCubes)
                {
                    if (figureCoordinates[k].X - step == p.X && figureCoordinates[k].Y == p.Y)
                        return;
                }
            }
        
            for (int i = 0; i < 4; i++)
            {
                figureCoordinates[i].X -= step;
            }
            updateCanvasLocation();
        }

        public void moveRight()
        {
            for (int k = 0; k < 4; k++)
            {
                if (figureCoordinates[k].X + step > 440) return;
                foreach (Point p in MainWindow.filledCubes)
                {
                    if (figureCoordinates[k].X + step == p.X && figureCoordinates[k].Y == p.Y)
                        return;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                figureCoordinates[i].X += step;
            }
            updateCanvasLocation();
        }

        public void updateCanvasLocation()
        {
            for (int i = 0; i < 4; i++)
            {
                Canvas.SetTop(rectangles[i].rect, FigureCoordinates[i].Y);
                Canvas.SetLeft(rectangles[i].rect, FigureCoordinates[i].X);
            }
        }

        protected void adjust()
        {
            while (checkLeftOuting())
            {
                for (int i = 0; i < 4; i++)
                {
                    FigureCoordinates[i].X += step;
                }
            }

            while (checkRightOuting())
            {
                for (int i = 0; i < 4; i++)
                {
                    FigureCoordinates[i].X -= step;
                }
            }

            while (checkDownOuting())
            {
                for (int i = 0; i < 4; i++)
                {
                    FigureCoordinates[i].Y -= step;
                }
            }
        }

        private bool checkLeftOuting()
        {
            foreach (Point p in FigureCoordinates)
            {
                if (p.X < 0) return true;
            }
            return false;
        }

        private bool checkRightOuting()
        {
            foreach (Point p in FigureCoordinates)
            {
                if (p.X > 440) return true;
            }
            return false;
        }

        private bool checkDownOuting()
        {
            foreach (Point p in FigureCoordinates)
            {
                if (p.Y > 600) return true;
            }
            return false;
        }

        protected bool checkOverlapping(Point[] p)
        {
            for (int i = 0; i<4 ;i++)
            {
                foreach(Point cube in MainWindow.filledCubes)
                {
                    if (p[i].X == cube.X && p[i].Y == cube.Y)
                        return true;
                }
            }
            return false;
        }

        public abstract void rotate();

    }
}
