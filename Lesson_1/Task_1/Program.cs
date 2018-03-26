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
//+ Предусмотреть кнопки - Начало игры, Рекорды, Выход. 
//+ Добавьте на заставку имя автора.

namespace Task_1
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

            Form1 form = new Form1();
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
}