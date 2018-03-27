﻿using System;
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
            Form1 form = new Form1
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };                      

            try
            {
                if (form.Width > 1000 || form.Width < 0 || form.Height > 1000 || form.Height < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(form.Width + " " + form.Height);
                Console.WriteLine(e.Message);
            }

            Game game = new Game();
            Game.Init(form);
            form.Show();
            game.Load();
            Game.Draw();
            Application.Run(form);
        }        
    }
}