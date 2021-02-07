using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public delegate void Message();

    public interface ICollision
    {
        bool IsCollision(ICollision obj);
        Rectangle rect { get; }
    }

    class Ship : BaseObject
    {
        public static event Message DieMessage;

        static int energy = 100;

        public int shipEnergy { get => energy; set => energy = value; }

        static Image img { get; } = Image.FromFile("Images\\ship.png");
       
        public override bool IsCollision(ICollision obj)
        {
            return rect.IntersectsWith(obj.rect) && ((obj is Asteroid) || (obj is Medical));
        }

        public Ship(Point Pos,Point Dir,Size Size) : base(Pos,Dir,Size)
        {
            Game.saveLog("Ship is created");
        }
        public override void Update()
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos);
        }

        public void Up()
        {
            Pos = new Point(Pos.X,Pos.Y - Dir.Y);
        }
        public void Down()
        {
             Pos = new Point(Pos.X,Pos.Y + Dir.Y);
        }
        
        public void Left()
        {
            Pos = new Point(Pos.X - Dir.X, Pos.Y);
        }
        public void Right()
        {
            Pos = new Point(Pos.X + Dir.X,Pos.Y);
        }
        public void Die()
        {
            DieMessage?.Invoke();
        }
    }

    abstract class BaseObject : ICollision
    {
        protected Point Pos { get; set; }//X,Y
        protected Point Dir { get; set; }//X,Y
        protected Size Size { get; set; }

        public virtual Rectangle rect => new Rectangle(Pos,Size);

        public BaseObject()
        {
            Pos = new Point(0, 0);
            Dir = new Point(0, 0);
            Size = new Size(0, 0);
        }
        public BaseObject(Point pos, Point dir, Size size)
        {
            if (pos.X < 0 || pos.X > Game.Width || pos.Y < 0 || pos.Y > Game.Height || dir.X > 1000 || dir.Y > 1000)
                throw new MyException();

            this.Pos = pos;
            this.Dir = dir;
            this.Size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public abstract void Update();

        public abstract bool IsCollision(ICollision obj);
    }



    class Star: BaseObject
    {


        static Random random = new Random();

        static Image Image { get; } = Image.FromFile("Images\\star.png");

        

        public Star(Point pos, Point dir, Size size):base(pos,dir,size)
        {
            Game.saveLog("Star is created");
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

        public override bool IsCollision(ICollision obj)
        {
            return false;
        }
    }

    class Bullet : BaseObject
    {

        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.Red, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override bool IsCollision(ICollision obj)
        {
            return rect.IntersectsWith(obj.rect) && (obj is Asteroid);
        }



        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
            if (Pos.X > Game.Width)
                Pos = new Point(Game.ship.rect.X + 20, Game.ship.rect.Y + 20);
        }
    }

    class Asteroid : BaseObject
    {
        
        static Image Image { get; } = Image.FromFile("Images\\Asteroid.png");



        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Game.saveLog("Asteroid is created");
        }

        //переопределил для звезды движение, начинается движение от правого конца экрана.
        public override void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
            if (Pos.X < -Size.Width)
                Pos = new Point(Game.Width, Game.Random.Next(0, Game.Height));
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos);
        }

        public override bool IsCollision(ICollision obj)
        {
            return rect.IntersectsWith(obj.rect);
        }
    }


    class Medical : BaseObject
    {

        static Image Image { get; } = Image.FromFile("Images\\Medical.png");



        public Medical(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Game.saveLog("Medical is created");
        }

        //переопределил для звезды движение, начинается движение от правого конца экрана.
        public override void Update()
        {
            Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
            if (Pos.X < -Size.Width)
                Pos = new Point(Game.Width, Game.Random.Next(0, Game.Height));
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos);
        }

        public override bool IsCollision(ICollision obj)
        {
            return rect.IntersectsWith(obj.rect);
        }
    }






    public class MyException : ApplicationException
    {
        public MyException()
        {
            Console.WriteLine("Bad parameters");
        }
    }

}
