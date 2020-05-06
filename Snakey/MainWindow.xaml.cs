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
using System.Windows.Threading;

namespace Snakey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int foodCounter;
        static Point startingPoint = new Point(50, 50);
        //Point[] colidablePoints;
        static DispatcherTimer gameTimer = new DispatcherTimer();
        Snake playerSnake;
        Food CurrentFood=null;
        List<IDrawable> gameObjects;
        public MainWindow()
        {
            InitializeComponent();
            //Create list for game objects to draw
            gameObjects = new List<IDrawable>();
            //Instantiate game objects
            playerSnake = new Snake(startingPoint);
            gameObjects.Add(playerSnake);
            foodCounter = 0;
            CurrentFood = new Food(200, 200);
            gameObjects.Add(CurrentFood);
            //
            //Set game ticker
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            gameTimer.Start();
            gameTimer.Tick += GameTimer_Tick;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            GameScreen.Children.Clear();
            //Spawn food, if there is none and the timer is full
            if (foodCounter>=50 && CurrentFood==null)
            {
                var random1 = new Random();
                var random2 = new Random();
                CurrentFood = new Food(random1.Next(10, 400), random2.Next(10, 400));
                gameObjects.Add(CurrentFood);
                foodCounter = 0;
            }
            foodCounter++;
            ///Continue here Switch...
            ///
            switch(playerSnake.CheckCollision(CurrentFood.Position))
            {
                case Snake.CollisionType.WithBody:
                    {
                        gameTimer.Stop();
                        MessageBox.Show("Aw, I've bit my tail. GAME OVER.");
                        break;
                    }
                case Snake.CollisionType.WithFood:
                    {
                        playerSnake.Grow(CurrentFood);
                        //CurrentFood.Dispose(GameScreen);
                        break;
                    }
                case Snake.CollisionType.NoCollision:
                    {
                        break;
                    }
            }
            playerSnake.Move();
            foreach (IDrawable gameObject in gameObjects )
            {
                gameObject.Draw(GameScreen);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            playerSnake.ChangeDirection(e.Key);
        }
        private void DrawFrame()
        {
            GameScreen.Children.Clear();
            playerSnake.Draw(GameScreen);
        }
    }
}
