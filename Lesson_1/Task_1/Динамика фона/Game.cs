using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Task_1
{
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
            for (int i = 0; i < _objs.Length / 4; i++)
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

            for (int i = _objs.Length / 4; i < 2 * _objs.Length / 4; i++)
            {
                _objs[i] = new Star(new Point(600, i * 10), new Point(i / 2, 0), new Size(i / 3, i / 3));
            }

            for (int i = 2 * _objs.Length / 4; i < 3 * _objs.Length / 4; i++)
            {
                _objs[i] = new Square(new Point(i * 5, i * 5), new Point(20, 20), new Size(1, 1));
            }

            int count = 0;
            for (int i = 3 * _objs.Length / 4; i < _objs.Length; i++)
            {
                count += 50;
                _objs[i] = new Rain(new Point(count, 10), new Point(10, 10 + count / 50 * 10), new Size(2, 2));

            }
        }

    }
}
