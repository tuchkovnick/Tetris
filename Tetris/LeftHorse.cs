using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris
{
    class LeftHorse:Figure
    {
        enum POSITION { FIRST, SECOND, THIRD,FORTH };
        POSITION position;
        public LeftHorse(Point startingPoint)
        {
            FigureCoordinates[0] = startingPoint;
            FigureCoordinates[1] = startingPoint;
            FigureCoordinates[1].X += step;
            FigureCoordinates[2] = FigureCoordinates[1];
            FigureCoordinates[2].Y += step;
            FigureCoordinates[3] = FigureCoordinates[2];
            FigureCoordinates[3].Y += step;
            updateCanvasLocation();
            position = POSITION.FIRST;
        }

        override public void rotate()
        {
            Point[] point = new Point[4];
            switch (position)
            {
                case POSITION.FIRST:
                {
                    point[0] = FigureCoordinates[0];
                    point[0].X += step*3;
                    point[1] = point[0];
                    point[1].Y += step;
                    point[2] = point[1];
                    point[2].X -= step;
                    point[3] = point[2];
                    point[3].X -= step;

                    if (checkOverlapping(point) == false)
                    {
                        FigureCoordinates = point;
                        position = POSITION.SECOND;
                    }
                        break;
                }
                case POSITION.SECOND:
                    {
                        point[0] = FigureCoordinates[1];
                        point[0].Y += step;
                        point[1] = point[0];
                        point[1].X -= step;
                        point[2] = point[1];
                        point[2].Y -= step;
                        point[3] = point[2];
                        point[3].Y -= step; ;

                        if (checkOverlapping(point) == false)
                        {
                            FigureCoordinates = point;
                            position = POSITION.THIRD;
                        }
                        break;
                    }
                case POSITION.THIRD:
                {
                    point[0] = FigureCoordinates[1];
                    point[1] = point[0];
                    point[1].Y -= step;
                    point[2] = point[1];
                    point[2].X += step;
                    point[3] = point[2];
                    point[3].X += step;

                    if (checkOverlapping(point) == false)
                    {
                        FigureCoordinates = point;
                        position = POSITION.FORTH;
                    }
                        break;
                }

                case POSITION.FORTH:
                {
                    point[0] = FigureCoordinates[1];
                    point[1] = FigureCoordinates[1]; ;
                    point[1].X += step;
                    point[2] = point[1];
                    point[2].Y += step;
                    point[3] = point[2];
                    point[3].Y += step;
                    if (checkOverlapping(point) == false)
                    {
                        FigureCoordinates = point;
                        position = POSITION.FIRST;
                    }
                    break;
                }
            }
            adjust();
            updateCanvasLocation();
        }
    }
}
