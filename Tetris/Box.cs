using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tetris
{
    class Box : Figure
    {
        public Box(Point startingPoint)
        {
            FigureCoordinates[0] = startingPoint;
            FigureCoordinates[1].X = startingPoint.X + step;
            FigureCoordinates[1].Y = startingPoint.Y;
            FigureCoordinates[2].X = startingPoint.X;
            FigureCoordinates[2].Y = startingPoint.Y + step;
            FigureCoordinates[3].X = startingPoint.X + step;
            FigureCoordinates[3].Y = startingPoint.Y + step;
            updateCanvasLocation();
        }

        override public void rotate() { }
    }
}
