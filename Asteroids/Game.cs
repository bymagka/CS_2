using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        //обобщенный делегат
        public static Action<string> saveLog = new Action<string>(saveLogFunc);


        static public ulong Timer { get; private set; } = 0;
           
        static BufferedGraphicsContext context;
        static public BufferedGraphics Buffer { get; private set; }

       

        static public Random Random { get; } = new Random();

        //4. Сделать проверку на задание размера экрана в классе Game. 
        static int width;
        static public int Width 
        { 
            get 
            {
                return width;
            }
            private set
            {
                if (value > 1000) throw new ArgumentOutOfRangeException("Width can't be more 1000");
                width = value;
            } 
        }

        static int height;
        static public int Height
        {
            get
            {
                return height;
            }
            private set
            {
                if (value > 1000) throw new ArgumentOutOfRangeException("Height can't be more 1000");
                height = value;
            }
        }

        static int points;
        static public int Points
        {
            get
            {
                return points;
            }
            private set
            {

                points = value;
            }
        }

        static public Image background = Image.FromFile("Images\\fon.jpg");

        static public BaseObject[] objs;

        static public Bullet bullet;

        static public Ship ship;

        static public Medical[] medicals = new Medical[2];

        static StreamWriter logWriter = new StreamWriter($"..\\..\\LogFile.txt",true);

        static Timer timer = new Timer();

        static Timer timerForMedicals = new Timer();

        static Game()
        {

        }

        static public void Init(Form form)
        {
            
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            

            
            form.Text = "Asteroids";

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
            Load();
            form.FormClosing += Form_FormClosing;
            form.KeyDown += From_KeyDown;
            Ship.DieMessage += Finish;

            timerForMedicals.Interval = 500;
            timerForMedicals.Tick += timerForMedicals_Tick;
            timerForMedicals.Start();
            // Запускаем консоль.
            AllocConsole();
            
        }

  

        private static void From_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ControlKey) bullet = new Bullet(new Point(ship.rect.X + 25, ship.rect.Y + 20), new Point(10, 0), new Size(10, 5));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
            if (e.KeyCode == Keys.Left) ship.Left();
            if (e.KeyCode == Keys.Right) ship.Right();

        }

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            FreeConsole();
            logWriter.Close();
            timer.Stop();
        }

        private static void timerForMedicals_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i<medicals.Length;i++)
                if(medicals[i] == null) medicals[i] = new Medical(new Point(Width, Random.Next(0, Height)), new Point(i * -Random.Next(1, 2), 0), new Size(10, 10));
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Game.Draw(); 
            Game.Update();
        }

        static void Load()
        {
            ship = new Ship(new Point(0, Height / 2 - 20), new Point(5, 5), new Size(50, 50));
            objs = new BaseObject[40];
            

            try
            {
                bullet = new Bullet(new Point(ship.rect.X + 20, ship.rect.Y + 20), new Point(10, 0), new Size(10, 5));


                //изменил создание звезд, чтобы они летели справа налево
                for (int i = 0; i < 40; i++)
                {
                    objs[i] = new Star(new Point(Width, Random.Next(0, Height)), new Point(i * -Random.Next(1, 2), 0), new Size(20, 20));
                    
                }

                //добавление астероидов
                for (int i = 0; i < 10; i++)
                {
                    objs[i] = new Asteroid(new Point(Width, Random.Next(0, Height)), new Point(i * -Random.Next(1, 2), 0), new Size(70, 95));

                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
          

        }

        static public void Draw()
        {
            //Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawImage(background, 0, 0);
            //Buffer.Graphics.DrawRectangle(Pens.White, 10, 10, 100, 200);
            foreach (var obj in objs)
            {
                obj.Draw();
            }

            foreach (var obj in medicals)
            {
                if (obj == null) continue;
                obj.Draw();
            }

            bullet.Draw();
            ship?.Draw();
            
            if (ship != null)
                Buffer.Graphics.DrawString($"Energy: {ship?.shipEnergy}  Points: {Points}", SystemFonts.DefaultFont, Brushes.White, 0, 0);

            Buffer.Render();
        }
        static public void Update()
        {
          
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].Update();

                //если столкновение пули и астероида, то ставим на начальные позиции объекты
                if (bullet.IsCollision(objs[i]))
                {
                    bullet = new Bullet(new Point(ship.rect.X + 20, ship.rect.Y + 20), new Point(10, 0), new Size(10, 5));
                    objs[i] = new Asteroid(new Point(Width, Random.Next(0, Height)), new Point(i * -Random.Next(1, 2), 0), new Size(70, 95));
                    points++;
                    saveLog($"Asteroid is burned! +1 pts. Total pts.: {points}");

                }

                
                if (ship.IsCollision(objs[i]))
                {
                    ship.shipEnergy--;
                    saveLog($"Ship is attacked! -1 hp. Total hp.: {ship.shipEnergy}");
                }
                if (ship.shipEnergy <= 0) ship?.Die();
            }

            //обработка движения аптечек
            for(int i = 0; i < medicals.Length; i++)
            {
                if(medicals[i] == null) continue;

                medicals[i].Update();

                if (ship.IsCollision(medicals[i]))
                {

                    if (ship.shipEnergy + 20 > 100) ship.shipEnergy = 100;
                    else ship.shipEnergy += 20;
                    saveLog("Medical catched! +20 hp");
                    medicals[i] = null;
                }

               
            }

            bullet.Update();


        }

        public static void Finish()
        {
            saveLogFunc("Ship is dead!");

            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }


        //цель делегата
        private static void saveLogFunc(string logInfo)
        {
            showLogConsole($"{DateTime.Now} {logInfo}");
            logWriter.WriteLine($"{DateTime.Now} {logInfo}");
    
        }
        private static void showLogConsole(string message)
        {
                System.Console.WriteLine(message);             
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();
    }
}
