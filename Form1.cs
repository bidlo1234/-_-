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
        Color[] mas_col = { Color.White, Color.Blue, Color.Red,
            Color.Yellow, Color.Green, Color.Cyan };
        
        const int x0 = 10, y0 = 10;
        const int n = 5;  // Размер массива
        const int h = 40;  // Размер клеточки
        int NClick, a, i1, j1;

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

        public void Filling(int n) // Заполнение массива случайными значениями
        {
            int c, d;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    pole[i, j] = 0;

            // Заполняем случайными значениями
            for(int i = 0; i < n; i++)
                for (int j = 1; j <= n; j++)
                { 
                    // Ищем случайное свободное место
                    do
                    {
                        c = rnd.Next(0, 5);
                        d = rnd.Next(0, 5);
                    }
                    while (pole[c, d] != 0);

                    pole[c, d] = i + 1;
                }
        }

        public void Cell(int i, int j, Color color) // Рисование отдельной клетки 
        {
            int x, y;

            br1.Color = color;
            x = x0 + j * h;
            y = y0 + i * h;

            gr.FillRectangle(br1, x, y, h, h);   // Красим клетку
            gr.DrawRectangle(Pen1, x, y, h, h);
        }

        public void Carpet(int x0, int y0) // Рисование всего коврика
        {
            int i, j, k;
            for (k = 0; k < n; k++)
                for (i = 0; i < n; i++)
                    for (j = 0; j < n; j++) {
                        Cell(i, j, mas_col[pole[i,j]]);
                    }
        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x, y, i, j;
            x = e.X;
            y = e.Y;
            if ((x - x0) * (x - x0 - n * h) <= 0 && (y - y0) * (y - y0 - n * h) <= 0) { // Проверка нажатия на клетку ковра
                i = (y - y0) / h; // Индексы ковра
                j = (x - x0) / h;

                if (NClick == 0) {
                    NClick = 1;
                    j1 = j;
                    i1 = i;
                } else {
                    NClick = 0;

                    // Перемещение клеток ковра
                    a = pole[i, j];
                    pole[i, j] = pole[i1, j1];
                    pole[i1, j1] = a; 

                    // Их окрашивание 
                    Cell(i, j, mas_col[pole[i, j]]);
                    Cell(i1, j1, mas_col[pole[i1, j1]]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Filling(n);
            NClick = 0;
            Carpet(x0, y0);
        }       
    }
}
