using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task_1
{
    class Rain : BaseObject
    {
        public Rain(Point pos, Point dir, Size size) : base(pos, dir, size) //делаем конструктор на основе базового класса
        {
        }

        // переопределяем методы 
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 20, Pos.Y + 20, Pos.X + 20 + Size.Width, Pos.Y + 20 + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 40, Pos.Y + 40, Pos.X + 40 + Size.Width, Pos.Y + 40 + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 60, Pos.Y + 60, Pos.X + 60 + Size.Width, Pos.Y + 60 + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + 80, Pos.Y + 80, Pos.X + 80 + Size.Width, Pos.Y + 80 + Size.Height);
        }
        public override void Update()
        {
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.Y > Game.Height) Pos.Y = Size.Height;
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width) Pos.X = Size.Width;
        }
    }
}
