using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snakey
{
    internal class Snake: IDrawable
    {
        public enum CollisionType
        {
            WithFood,
            WithTerrain,
            WithBody,
            NoCollision
        }
        public Point Head { get;private set; }
        public int StartingLength { get;private set; } = 3;
        public int CurrentLength { get;private set; }
        public enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }
        public Queue<Point> Body { get;private set; }
        public Direction CurrentDirection { get; private set; } = Direction.Right;
        public void Draw(Canvas canvas)
        {
                foreach (Point bodyPoint in this.Body)
                {
                    Rectangle rectangle = new Rectangle { Height = 10, Width = 10 };
                    rectangle.Fill = new SolidColorBrush(Colors.Black);
                    Canvas.SetLeft(rectangle, bodyPoint.X);
                    Canvas.SetTop(rectangle, bodyPoint.Y);
                    canvas.Children.Add(rectangle);

                }
        }
        public void Grow(Food food)
        {
            Body.Enqueue(food.Position);
            Head = food.Position;
        }
        public void ChangeDirection(Key inputKey)
        {
            switch (inputKey)
            {
                case Key.Up:
                    {
                        if (CurrentDirection.Equals(Direction.Down))
                        {
                            break;
                        }
                        CurrentDirection = Direction.Up;
                        break;
                    }
                case Key.Down:
                    {
                        if (CurrentDirection.Equals(Direction.Up))
                        {
                            break;
                        }
                        CurrentDirection = Direction.Down;
                        break;
                    }
                case Key.Left:
                    {
                        if (CurrentDirection.Equals(Direction.Right))
                        {
                            break;
                        }
                        CurrentDirection = Direction.Left;
                        break;
                    }
                case Key.Right:
                    {
                        if (CurrentDirection.Equals(Direction.Left))
                        {
                            break;
                        }
                        CurrentDirection = Direction.Right;
                        break;
                    }
                default:
                    break;
            }
        }
        public Point Move()
        {
            //TODO Make new method to use here and in CheckCollision
            switch (CurrentDirection)
            {
                case Direction.Up:
                    {
                        Body.Enqueue(new Point(Head.X, Head.Y - 10));
                        Head = new Point(Head.X, Head.Y - 10);
                        Body.Dequeue();
                        break;
                    }
                case Direction.Down:
                    {
                        Body.Enqueue(new Point(Head.X, Head.Y + 10));
                        Head = new Point(Head.X, Head.Y + 10);
                        Body.Dequeue();
                        break;
                    }
                case Direction.Left:
                    {
                        Body.Enqueue(new Point(Head.X - 10,Head.Y));
                        Head = new Point(Head.X - 10, Head.Y);
                        Body.Dequeue();
                        break;
                    }
                case Direction.Right:
                    {
                        Body.Enqueue(new Point(Head.X + 10, Head.Y));
                        Head = new Point(Head.X + 10, Head.Y);
                        Body.Dequeue();
                        break;
                    }
            }
            return Body.Peek();
        }
        public CollisionType CheckCollision(Point foodPoint)
        {
            Point movesTo;
            switch (CurrentDirection)
            {
                case Direction.Up:
                    {
                        movesTo = new Point(Head.X, Head.Y - 10);
                        break;
                       
                    }
                case Direction.Down:
                    {
                        movesTo = new Point(Head.X, Head.Y + 10);
                        break;
                       
                    }
                case Direction.Left:
                    {
                        movesTo = new Point(Head.X-10, Head.Y);
                        break;
                      
                    }
                case Direction.Right:
                    {
                        movesTo = new Point(Head.X+10, Head.Y);
                        break;
                        
                    }
                default:
                    return CollisionType.NoCollision;
            }
            if(Body.Contains(movesTo))
            {
                return CollisionType.WithBody;
            }
            if(foodPoint.Equals(movesTo))
            {
                return CollisionType.WithFood;
            }
            return CollisionType.NoCollision;
        }
        public Snake(Point startingposition)
        {
            Body = new Queue<Point>();
            CurrentLength = StartingLength;
            for (int i = CurrentLength; i > 0; i--)
            {
                switch (CurrentDirection)
                {
                    case Direction.Right:
                        {
                            Body.Enqueue(new Point(startingposition.X - (i * 10), startingposition.Y));
                            break;
                        }
                    case Direction.Left:
                        {
                            Body.Enqueue(new Point(startingposition.X + (i * 10), startingposition.Y));
                            break;
                        }
                    case Direction.Up:
                        {
                            Body.Enqueue(new Point(startingposition.X, startingposition.Y + (i * 10)));
                            break;
                        }
                    case Direction.Down:
                        {
                            Body.Enqueue(new Point(startingposition.X, startingposition.Y - (i * 10)));
                            break;
                        }
                }
            }
            Body.Enqueue(startingposition);
            Head = startingposition;
        }
        public Snake(Point startingposition, int length) : this(startingposition)
        {
            StartingLength = length;
        }
    }
}
