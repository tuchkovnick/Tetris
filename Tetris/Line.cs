using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tetris
{
    class Line : Figure
    {
        enum POSITION { HORIZONTAL, VERTICAL };
        POSITION position;
        public Line(Point startingPoint)
        {
            for (int i = 0; i < 4; i++)
            {
                FigureCoordinates[i] = startingPoint;
                FigureCoordinates[i].X += step * i;
            }
            updateCanvasLocation();
            position = POSITION.HORIZONTAL;
        }

        override public void rotate()
        {
            Point[] point = new Point[4];

            if (position == POSITION.HORIZONTAL)
            {
                point[0] = FigureCoordinates[1];
                point[1].X = FigureCoordinates[1].X;
                point[1].Y = FigureCoordinates[1].Y + step;
                point[2].X = FigureCoordinates[2].X - step;
                point[2].Y = FigureCoordinates[2].Y + (step * 2);
                point[3].X = FigureCoordinates[3].X - (step * 2);
                point[3].Y = FigureCoordinates[3].Y + (step * 3);
                if (checkOverlapping(point) == false)
                {
                    FigureCoordinates = point;
                    position = POSITION.VERTICAL;
                }
            }
            else
            {
                point[1] = FigureCoordinates[0];
                point[0].X = FigureCoordinates[0].X - step;
                point[0].Y = FigureCoordinates[0].Y;
                point[2].X = FigureCoordinates[2].X + step;
                point[2].Y = FigureCoordinates[2].Y - (step * 2);
                point[3].X = FigureCoordinates[3].X + (step * 2);
                point[3].Y = FigureCoordinates[3].Y - (step * 3);
                if (checkOverlapping(point) == false)
                {
                    FigureCoordinates = point;
                    position = POSITION.HORIZONTAL;
                }
            }
            adjust();
            updateCanvasLocation();
        }

    }
}
