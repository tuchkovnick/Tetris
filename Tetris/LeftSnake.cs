using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris
{
    class LeftSnake : Figure
    {
        enum POSITION { HORIZONTAL, VERTICAL };
        POSITION position;
        public LeftSnake(Point startingPoint)
        {
            FigureCoordinates[0] = startingPoint;
            FigureCoordinates[1] = startingPoint;
            FigureCoordinates[1].X += + step;
            FigureCoordinates[2] = FigureCoordinates[1];
            FigureCoordinates[2].Y += step;
            FigureCoordinates[3] = FigureCoordinates[2];
            FigureCoordinates[3].X += step;
            updateCanvasLocation();
            position = POSITION.HORIZONTAL;
        }

        override public void rotate()
        {
            if (position == POSITION.HORIZONTAL)
            {
                Point[] outPoint = new Point[4];
                outPoint[0] = FigureCoordinates[1];
                outPoint[1] = FigureCoordinates[2];
                outPoint[2] = FigureCoordinates[0];
                outPoint[2].Y += step;
                outPoint[3] = outPoint[2];
                outPoint[3].Y += step;
                if(checkOverlapping(outPoint) == false)
                {
                    FigureCoordinates = outPoint;
                    position = POSITION.VERTICAL;
                }
            }
            else
            {
                Point[] outPoint = new Point[4];
                outPoint[0] = FigureCoordinates[0];
                outPoint[0].X = FigureCoordinates[0].X-step;
                outPoint[1] = FigureCoordinates[0];
                outPoint[2] = FigureCoordinates[1];
                outPoint[3] = FigureCoordinates[2];
                outPoint[3].X = FigureCoordinates[2].X + step + step;

                if (checkOverlapping(outPoint) == false)
                {
                    FigureCoordinates = outPoint;
                    position = POSITION.HORIZONTAL;
                }
            }
            adjust();
            updateCanvasLocation();
        }

    }
}
