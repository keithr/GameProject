using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        private List<IGameObject> gameObjects = new();
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        /// <summary>
        /// Initializes the game with game elements.
        /// </summary>
        private void InitializeGame()
        {
            // AddStars();
            // AddAsteroids();
            AddShip();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddShip()
        {
            gameObjects.Add(new Ship {Location = new Point(100, 100), Size = new Size(50, 50)});
        }

        /// <summary>
        /// Called by Timer (30 times a second).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameUpdate(object sender, EventArgs e)
        {
            foreach (var o in gameObjects)
            {
                o.Update(gameObjects, ClientRectangle);
            }
            Invalidate(); // Forces a paint.
        }


        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var o in gameObjects)
            {
                o.Draw(e, ClientRectangle);
            }
        }

        /// <inheritdoc />
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    foreach (var o in Enumerable.Reverse(gameObjects))
                    {
                        if (o is not IInput input) continue;
                        if (input.PewPew()) return;
                    }
                    break;
                case Keys.Left:
                    foreach (var o in Enumerable.Reverse(gameObjects))
                    {
                        if (o is not IInput input) continue;
                        if (input.Left()) return;
                    }
                    break;
                case Keys.Right:
                    foreach (var o in Enumerable.Reverse(gameObjects))
                    {
                        if (o is not IInput input) continue;
                        if (input.Right()) return;
                    }
                    break;
                case Keys.Down:
                    foreach (var o in Enumerable.Reverse(gameObjects))
                    {
                        if (o is not IInput input) continue;
                        if (input.Down()) return;
                    }
                    break;
                case Keys.Up:
                    foreach (var o in Enumerable.Reverse(gameObjects))
                    {
                        if (o is not IInput input) continue;
                        if (input.Up()) return;
                    }
                    break;
            }
        }
    }

    internal interface IGameObject
    {
        public PointF Location { get; set; }
        void Draw(PaintEventArgs e, Rectangle bounds);
        void Update(List<IGameObject> gameObjects, Rectangle bounds);

        bool IsCollision(IGameObject obj);
    }

    internal interface IInput
    {
        bool Up();
        bool Down();
        bool Left();
        bool Right();
        bool PewPew();
    }

    internal class Ship : IGameObject, IInput
    {
        /// <inheritdoc />
        public PointF Location { get; set; }

        public Size Size { get; set; }

        /// <inheritdoc />
        public void Draw(PaintEventArgs e, Rectangle bounds)
        {
            var p = new Pen(Color.Black);
            e.Graphics.DrawEllipse(p, new RectangleF{Location=new PointF(Location.X-Size.Width/2.0f, Location.Y-Size.Height/2.0f), Width=Size.Width, Height = Size.Height});
            p.Dispose();
        }

        /// <inheritdoc />
        public void Update(List<IGameObject> gameObjects, Rectangle bounds)
        {
            Location = new PointF(Location.X+1f, Location.Y+1f);
        }

        /// <inheritdoc />
        public bool IsCollision(IGameObject obj)
        {
            return false;
        }

        /// <inheritdoc />
        public bool Up()
        {
            System.Diagnostics.Debug.WriteLine("UP");
            return true;
        }

        /// <inheritdoc />
        public bool Down()
        {
            System.Diagnostics.Debug.WriteLine("Down");
            return true;
        }

        /// <inheritdoc />
        public bool Left()
        {
            System.Diagnostics.Debug.WriteLine("Left");
            return true;
        }

        /// <inheritdoc />
        public bool Right()
        {
            System.Diagnostics.Debug.WriteLine("Right");
            return true;
        }

        /// <inheritdoc />
        public bool PewPew()
        {
            System.Diagnostics.Debug.WriteLine("PewPew");
            return true;
        }
    }
}
