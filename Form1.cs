using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Коврик_рандом
{
    public partial class Form1 : Form
    {
        Color[] mas_col = { Color.Teal, Color.Pink, Color.Magenta, Color.Blue,
            Color.Coral,Color.LightGreen, Color.Yellow, Color.Cyan };
        const int x0 = 10;   // Такое описание задает константу.
        const int y0 = 10;
        int klik, a, i1, j1;
        const int n = 5;  // Размер массива
        const int h = 40;  // Размер клеточки
        int[,] pole = new int[n, n];
        Random rnd = new Random();
        Graphics gr;
        SolidBrush br1 = new SolidBrush(Color.White);
        Pen Pen1 = new Pen(Color.Black, 1);    // для контура квадратика 
        public Form1()
        {
            InitializeComponent();
            gr = this.CreateGraphics();
            
        }
        public void zapoln2(int n)
        {
            int i, j, c, d, k;
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    pole[i, j] = 0;

            for (k = 0; k < n; k++)
                for (i = 1; i <= n; i++)
                {// Ищем случайное свободное место
                    do
                    {
                        c = rnd.Next(0, 5);
                        d = rnd.Next(0, 5);
                    }
                    while (pole[c, d] != 0);

                    pole[c, d] = k + 1;
                }
        }
            public void kletka(int i, int k, Color col)
        {
            int x, y;
            br1.Color = col;
            x = x0 + k * h;
            y = y0 + i * h;
            gr.FillRectangle(br1, x, y, h, h);   // Красим клетку
            gr.DrawRectangle(Pen1, x, y, h, h);
        }
        public void kovrik(int x0, int y0)
        {
            int i, j, k;
            for (k = 0; k < n; k++)
                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++)
                    {                        
                        kletka(i, j, mas_col[pole[i,j]]);
                    }
        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x, y, i, j;
            x = e.X;
            y = e.Y;
            if ((x - x0) * (x - x0 - n * h) <= 0 && (y - y0) * (y - y0 - n * h) <= 0)
            {
                i = (y - y0) / h;
                j = (x - x0) / h;
                //kletka(i, j, Color.White);
                if (klik == 0)
                {
                    klik = 1;
                    j1 = j;//Запомнили номер клетки
                    i1 = i;
                }
                else
                {
                    klik = 0;  // Восстанавливаем klik
                    // Обмен swap()
                    a = pole[i, j];
                    pole[i, j] = pole[i1, j1];
                    pole[i1, j1] = a;
                    kletka(i, j, mas_col[pole[i, j]]);
                    kletka(i1, j1, mas_col[pole[i1, j1]]);

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            zapoln2(n);
            klik = 0;
            kovrik(x0, y0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
