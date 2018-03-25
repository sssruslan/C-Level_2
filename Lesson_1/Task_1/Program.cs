using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

//Сагитов Руслан Равилевич

//+ 1. Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон,
//     похожий на полёт в звёздном пространстве.
//+ 2. * Заменить кружочки картинками, используя метод DrawImage.
//+ 3. **Разработать собственный класс заставка SplashScreen, аналогичный классу Game в котором
//  создайте собственную иерархию объектов и задайте их движение. 
//- Предусмотреть кнопки - Начало игры, Рекорды, Выход. //- Добавьте на заставку имя автора.


namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Form form = new Form();
            //Game game = new Game();
            //form.Width = 800;
            //form.Height = 600;
            //Game.Init(form);
            //game.Load(); // ссылаемся на объект, т.к. метод виртуал
            //form.Show();
            //Game.Draw();
            //Application.Run(form);

            Form form = new Form();
            SplashScreen ss_game = new SplashScreen();
            form.Width = 800;
            form.Height = 600;
            SplashScreen.Init(form);
            ss_game.Load(); // ссылаемся на объект, т.к. метод виртуал
            form.Show();
            SplashScreen.Draw();
            Application.Run(form);
        }
    }

    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs; // объявляем массив объектов

        static Game()
        {
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        /// <param name="form">К какой форме привязываемся</param>
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            //Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Таймер для прорисовки и обновления
        /// </summary>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Прорисовываем объекты
        /// </summary>
        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Обновляем все объекты
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

        /// <summary>
        /// Создаем массив из 80 объектов и далее запускаем их со сдвигом позиции
        /// </summary>
        public virtual void Load()
        {
            _objs = new BaseObject[80]; 
            for (int i = 0; i < _objs.Length/4; i++)
            {
                if (i<10)
                {
                    _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - 1 * i, 15 - 1 * i), new Size(i, i));
                }
                else if (i>=10 && i<=20)
                {
                    _objs[i] = new BaseObject(new Point(600, i * 30), new Point(20 - 2 * i, 20 - 1 * i), new Size(i, i));
                }
                else
                {
                    _objs[i] = new BaseObject(new Point(600, i * 40), new Point(30 - 1 * i, 30 - 2 * i), new Size(i, i));
                }
            }

            for (int i = _objs.Length/4; i < 2*_objs.Length/4; i++)
            {
                _objs[i] = new Star(new Point(600, i * 10), new Point(i/2, 0), new Size(i/3, i/3));
            }

            for (int i = 2*_objs.Length / 4; i < 3*_objs.Length/4; i++)
            {
                _objs[i] = new Square(new Point(i * 5, i * 5), new Point(20, 20), new Size(1, 1));
            }

            int count = 0;
            for (int i = 3 * _objs.Length / 4; i < _objs.Length; i++)
            {
                count += 50;
                _objs[i] = new Rain(new Point(count, 10), new Point(10, 10+count/50*10), new Size(2, 2));
                
            }
        }
    }
    /// <summary>
    /// Базовый класс на примере кругов
    /// </summary>
    class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        Image newImage = Image.FromFile("planet.jpg");

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        /// <summary>
        /// Прорисовка объекта
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Движение объекта при обновлении
        /// </summary>
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }    class Star : BaseObject //наследуем
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) //делаем конструктор на основе базового класса
        {
        }        
        // переопределяем методы 
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }    class Square : BaseObject
    {
        public Square(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Yellow, Pos.X, Pos.Y, Size.Width, Size.Height);
        }        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.Y < 0) Pos.Y = Game.Height + Size.Height;
        }


    }    class Rain : BaseObject
    {
        public Rain(Point pos, Point dir, Size size) : base(pos, dir, size) //делаем конструктор на основе базового класса
        {
        }
        // переопределяем методы 
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X+20, Pos.Y+20, Pos.X+20 + Size.Width, Pos.Y+20 + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 40, Pos.Y + 40, Pos.X + 40 + Size.Width, Pos.Y + 40 + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 60, Pos.Y + 60, Pos.X + 60 + Size.Width, Pos.Y + 60 + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 80, Pos.Y + 80, Pos.X + 80 + Size.Width, Pos.Y + 80 + Size.Height);
        }        public override void Update()
        {
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.Y > Game.Height) Pos.Y = Size.Height;
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width) Pos.X = Size.Width;
        }
    }    class Grid : BaseObject
    {
        public Grid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Blue, Pos.X, Pos.Y, Size.Width, Size.Height);
        }        public override void Update()
        {
            Pos.X = Pos.X;
            Pos.Y = Pos.Y;
        }
    }

    // выполняем задание 3
    class SplashScreen : Game
    {
        public override void Load()
        {
            _objs = new BaseObject[409];
            for (int i = 0; i < 30; i++)
            {
                if (i < 10)
                {
                    _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - 1 * i, 15 - 1 * i), new Size(i, i));
                }
                else if (i >= 10 && i <= 20)
                {
                    _objs[i] = new BaseObject(new Point(600, i * 30), new Point(20 - 2 * i, 20 - 1 * i), new Size(i, i));
                }
                else
                {
                    _objs[i] = new BaseObject(new Point(600, i * 40), new Point(30 - 1 * i, 30 - 2 * i), new Size(i, i));
                }
            }

            for (int i = 30; i < 60; i++)
            {
                _objs[i] = new Star(new Point(600, i * 10), new Point(i / 2, 0), new Size(i / 3, i / 3));
            }

            for (int i = 60; i < 90; i++)
            {
                _objs[i] = new Square(new Point(i * 5, i * 5), new Point(20, 20), new Size(1, 1));
            }

            int count = 0;
            for (int i = 90; i < 120; i++)
            {
                count += 50;
                _objs[i] = new Rain(new Point(count, 10), new Point(10, 10 + count / 50 * 10), new Size(2, 2));

            }

            int x = 0;
            int y = 0;
            for (int i = 120; i < 409;)
            {
                x += 47;
                y = 0;
                for (int j = 0; j < 17; j++)
                {
                    y += 35;
                    _objs[i] = new Grid(new Point(x, y), new Point(47, 35), new Size(1, 1));
                    i++;
                    if (i>=409)
                    {
                        break;
                    }
                }
            }
        }
    }  
}