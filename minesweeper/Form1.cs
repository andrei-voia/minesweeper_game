using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button3.Enabled = false;
        }

        int n, bombe, any, ramas, numaratoare = 0;
        int ox, oy;
        
        Button[,] b = new Button[20, 20];

        private void x_click(object sender, EventArgs e)
        {
            int x, y, tag;
            tag = Convert.ToInt32(((Button)sender).Tag);

            if (tag == 0)
            {
                afisare_integrala_blocare();
                MessageBox.Show("Ai pierdut !");
            }
            else
            {
                x = tag / 10;
                y = tag % 10;
                vecini(x, y);
                b[x, y].Enabled = false;
                ramas--;
                if (ramas == 0)
                {
                    afisare_integrala_blocare();
                    MessageBox.Show("Ai castigat !");
                }
            }
        }

        void vecini(int i, int j)
        {
            int contor = 0;

            if (i > 1 && j > 1) if (Convert.ToInt32(b[i - 1, j - 1].Tag) == 0) contor++;
            if (i > 1) if (Convert.ToInt32(b[i - 1, j].Tag) == 0) contor++;
            if (i > 1 && j < n) if (Convert.ToInt32(b[i - 1, j + 1].Tag) == 0) contor++;
            if (j > 1) if (Convert.ToInt32(b[i, j - 1].Tag) == 0) contor++;
            if (j < n) if (Convert.ToInt32(b[i, j + 1].Tag) == 0) contor++;
            if (i < n && j > 1) if (Convert.ToInt32(b[i + 1, j - 1].Tag) == 0) contor++;
            if (i < n) if (Convert.ToInt32(b[i + 1, j].Tag) == 0) contor++;
            if (i < n && j < n) if (Convert.ToInt32(b[i + 1, j + 1].Tag) == 0) contor++;

            if (contor != 0) b[i, j].Text = Convert.ToString(contor);
        }

        void afisare_integrala_blocare()
        {
            int contor;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    contor = 0;
                    if (Convert.ToInt32(b[i, j].Tag) != 0)
                    {
                        if (i > 1 && j > 1) if (Convert.ToInt32(b[i - 1, j - 1].Tag) == 0) contor++;
                        if (i > 1) if (Convert.ToInt32(b[i - 1, j].Tag) == 0) contor++;
                        if (i > 1 && j < n) if (Convert.ToInt32(b[i - 1, j + 1].Tag) == 0) contor++;
                        if (j > 1) if (Convert.ToInt32(b[i, j - 1].Tag) == 0) contor++;
                        if (j < n) if (Convert.ToInt32(b[i, j + 1].Tag) == 0) contor++;
                        if (i < n && j > 1) if (Convert.ToInt32(b[i + 1, j - 1].Tag) == 0) contor++;
                        if (i < n) if (Convert.ToInt32(b[i + 1, j].Tag) == 0) contor++;
                        if (i < n && j < n) if (Convert.ToInt32(b[i + 1, j + 1].Tag) == 0) contor++;

                        if (contor != 0) b[i, j].Text = Convert.ToString(contor);
                    }
                    else b[i, j].Text = "X";

                    b[i, j].Enabled = false;
                    button3.Enabled = false;
                }
        }

        void construire ()
        {
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    Controls.Remove(b[i, j]);

            n = Convert.ToInt32(textBox1.Text);

            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    b[i, j] = new Button();
                    b[i, j].Top = i * 28 + 35;
                    b[i, j].Left = j * 28 + 30;
                    b[i, j].Height = 30;
                    b[i, j].Width = 30;
                    b[i, j].Click += new EventHandler(x_click);
                    b[i, j].Tag = 10 * i + j;
                    Controls.Add(b[i, j]);
                }
        }

        void bombardament(int bombe)
        {
            int i=1;

            while (i <= bombe)
            {
                Random random = new Random();
                any = random.Next(1, n * n);
                oy = any / n + 1;
                ox = any % n;
                if (ox == 0) ox = n;
                if (Convert.ToInt32(b[oy, ox].Tag)!=0)
                {
                    b[oy, ox].Tag = 0;
                    i++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox1.Text) > Convert.ToInt32(textBox2.Text))
                {
                    if (Convert.ToInt32(textBox1.Text) > 13)
                    {
                        textBox1.Text = Convert.ToString(13);
                        MessageBox.Show("Maxim 13 linii / coloane");
                    }
                    bombe = Convert.ToInt32(textBox2.Text);

                    construire();
                    bombardament(bombe);
                    ramas = n * n - bombe;
                    button3.Enabled = true;
                }
                else MessageBox.Show("Sunt prea multe bombe");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                    Controls.Remove(b[i, j]);
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (numaratoare == 0)
            {
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                        if (Convert.ToInt32(b[i, j].Tag) == 0) b[i, j].Text = "X";
                numaratoare = 1;
            }
            else
            {
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                        if (Convert.ToInt32(b[i, j].Tag) == 0) b[i, j].Text = "";
                numaratoare = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
