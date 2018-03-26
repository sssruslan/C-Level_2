using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Task_1
{
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
                    if (i >= 409)
                    {
                        break;
                    }
                }
            }
        }
    }
}
