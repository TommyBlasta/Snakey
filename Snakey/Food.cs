using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snakey
{
    class Food : IDrawable
    {
        int removeIndex;
        public Point Position { get;private set; }
        //private Rectangle shape { get; set; }
        public Food(int x,int y)
        {
            Position = new Point(x,y);
        }
        public void Draw(Canvas canvas)
        {
            Rectangle rectangle = new Rectangle { Height = 10, Width = 10 };
            rectangle.Fill = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(rectangle, Position.X);
            Canvas.SetTop(rectangle, Position.Y);
            rectangle.Uid = "Food";
            removeIndex=canvas.Children.Add(rectangle);
        }
        public void Dispose(Canvas canvas)
        {
            Rectangle toRemove=null;
            foreach(Rectangle element in canvas.Children)
            {
                if(element.Uid.Equals("Food"))
                {
                    toRemove = element;
                    break;
                }
            }
            if (toRemove != null)
            {
                canvas.Children.Remove(toRemove);
            }
            
        }
    }
}
