using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class BaseObject
    {
        protected Point Pos { get; set; }//X,Y
        protected Point Dir { get; set; }//X,Y
        protected Size Size { get; set; }

        public BaseObject()
        {
            Pos = new Point(0, 0);
            Dir = new Point(0, 0);
            Size = new Size(0, 0);
        }
        public BaseObject(Point pos, Point dir, Size size)
        {
            this.Pos = pos;
            this.Dir = dir;
            this.Size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
            if (Pos.X < 0 || Pos.X > Game.Width)
                Dir = new Point(-Dir.X, Dir.Y);
            if (Pos.Y < 0 || Pos.Y > Game.Height)
                Dir = new Point(Dir.X, -Dir.Y);
        }
    }

    class Star: BaseObject
    {
        static Random random = new Random();

        static Image Image { get; } = Image.FromFile("Images\\star.png");

        

        public Star(Point pos, Point dir, Size size):base(pos,dir,size)
        {

        }

        //переопределил для звезды движение, начинается движение от правого конца экрана.
        public override void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
            if (Pos.X < 0)
                Pos = new Point(Game.Width, Game.Random.Next(0,Game.Height));
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos);
        }
    }


}
