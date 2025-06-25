using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Fuzzy
{
    enum OPERATII { SUM = 1, SUB, MUL, DIV, INT, REU, CONCENTRARE, PUTERE, DILATARE, INTENSIFICARE }

    delegate void Switch(PictureBox a, PictureBox b);

    public partial class FormFuzzy : Form
    {
        private bool isInFront = false;
        private readonly Point pos1, pos2;
        private readonly Size size1, size2;
        private double alfa;

        private OPERATII op = 0;
        private Switch mySwitch;
        private NumarFuzzy A, B;
        private Randare gr;

        public FormFuzzy()
        {
            InitializeComponent();
            comboBoxDelay.SelectedIndex = 0;
            mySwitch += MotionDown;
            mySwitch += MotionUp;
            gr = new Randare(pictureBox1.Width, pictureBox1.Height, 0, 100, 1, 25);
            pos1 = pictureBox1.Location;
            pos2 = pictureBox2.Location;
            size1 = pictureBox1.Size;
            size2 = pictureBox1.Size;
            try
            {
                A = new NumarFuzzy(Convert.ToInt32(textBoxCentruA.Text), Convert.ToInt32(textBoxAlfaA.Text), Convert.ToInt32(textBoxBetaA.Text), gr.Max);
                B = new NumarFuzzy(Convert.ToInt32(textBoxCentruB.Text), Convert.ToInt32(textBoxAlfaB.Text), Convert.ToInt32(textBoxBetaB.Text), gr.Max);
                alfa = Convert.ToDouble(textBoxAlfa.Text);
                if (alfa < 0 || alfa > 1)
                    throw new Exception();
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                ValidareButoane(true);
            }
            catch
            {
                MessageBox.Show("Date gresite !");
                A = null;
                B = null;
            }
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            try
            {
                A = new NumarFuzzy(Convert.ToInt32(textBoxCentruA.Text), Convert.ToInt32(textBoxAlfaA.Text), Convert.ToInt32(textBoxBetaA.Text), gr.Max);
                B = new NumarFuzzy(Convert.ToInt32(textBoxCentruB.Text), Convert.ToInt32(textBoxAlfaB.Text), Convert.ToInt32(textBoxBetaB.Text), gr.Max);
                alfa = Convert.ToDouble(textBoxAlfa.Text);
                if (alfa < 0 || alfa > 1)
                    throw new Exception();
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Date gresite !");
                A = null;
                B = null;
            }
        }

        private void Motion()
        {
            while (pictureBox1.Location.X < pictureBox2.Location.X)
            {
                pictureBox1.Location = new System.Drawing.Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y);
                pictureBox2.Location = new System.Drawing.Point(pictureBox2.Location.X - 1, pictureBox2.Location.Y);
                Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;

            if (null != A && 0 == op)
            {
                Grafics.Sistem(g, gr);
                Functie.Calc(ref A);
                Grafics.Grafic(g, gr, A, Pens.Blue);
                Grafics.AlfaCut(g, gr, A, alfa);
            }
            if (null != A && null != B && 0 != op)
            {
                switch (op)
                {
                    case OPERATII.SUM:
                        Operatii.Sum(A, B, g, gr, alfa);
                        break;
                    case OPERATII.SUB:
                        Operatii.Sub(A, B, g, gr, alfa);
                        break;
                    case OPERATII.MUL:
                        Operatii.Mul(A, B, g, gr, alfa);
                        break;
                    case OPERATII.DIV:
                        Operatii.Div(A, B, g, gr, alfa);
                        break;
                    case OPERATII.INT:
                        Operatii.Int(A, B, g, gr);
                        break;
                    case OPERATII.REU:
                        Operatii.Reu(A, B, g, gr);
                        break;
                    case OPERATII.CONCENTRARE:
                        Operatii.Concentrare(A, g, gr);
                        break;
                    case OPERATII.PUTERE:
                        Operatii.Putere(A, 3, g, gr);
                        break;
                    case OPERATII.DILATARE:
                        Operatii.Dilatare(A, g, gr);
                        break;
                    case OPERATII.INTENSIFICARE:
                        Operatii.Intensificare(A, g, gr);
                        break;
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;

            if (null != B)
            {
                Grafics.Sistem(g, gr);
                Functie.Calc(ref B);
                Grafics.Grafic(g, gr, B, Pens.Pink);
                Grafics.AlfaCut(g, gr, B, alfa);
                MotionGrafic(g);
            }
        }

        private void buttonSum_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.SUM;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.SUB;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonMul_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.MUL;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);

        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.DIV;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonInt_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.INT;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonReu_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.REU;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonConcentrare_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.CONCENTRARE;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonDilatare_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.DILATARE;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonIntensificare_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.INTENSIFICARE;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATII.PUTERE;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = pos1;
            pictureBox2.Location = pos2;
            op = 0;
            button10.Enabled = false;
            pictureBox2.BringToFront();
            isInFront = false;
            Refresh();
        }

        private void ValidareButoane(bool valid)
        {
            button3.Enabled = valid;
            button4.Enabled = valid;
            button5.Enabled = valid;
            button6.Enabled = valid;
            button7.Enabled = valid;
            button8.Enabled = valid;
            button9.Enabled = valid;
        }

        private void CountDown(Graphics g)
        {
            for (int i = 6; i > 0; --i)
            {
                g.FillPie(Brushes.White, gr.RandeazaW(gr.Max), gr.RandeazaH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 0, 90);
                g.FillPie(Brushes.Gray, gr.RandeazaW(gr.Max), gr.RandeazaH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 90, 90);
                g.FillPie(Brushes.White, gr.RandeazaW(gr.Max), gr.RandeazaH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 180, 90);
                g.FillPie(Brushes.Gray, gr.RandeazaW(gr.Max), gr.RandeazaH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 270, 90);
                g.DrawString(i.ToString(), new Font("Console", gr.Offset + 5), Brushes.Gold, gr.RandeazaW(gr.Max) - 5, gr.RandeazaH(1) - gr.Offset);
                for (int j = 0; j < 360; ++j)
                {
                    g.FillPie(Brushes.Aqua, gr.RandeazaW(gr.Max), gr.RandeazaH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 0, j);
                    Thread.Sleep(comboBoxDelay.SelectedIndex);
                }
            }
        }

        private void MotionGrafic(Graphics g)
        {
            float m = pictureBox1.Location.X + pictureBox1.Width - pictureBox2.Location.X - gr.Offset;
            if (m > 0)
            {
                for (int i = gr.Max - 1; i > gr.Max - m / gr.WFactor && i > 1; --i)
                {
                    g.DrawLine(Pens.Green, gr.RandeazaW(i) - gr.RandeazaW(gr.Max) + m, gr.RandeazaH(A.vect[i]), gr.RandeazaW(i - 1) - gr.RandeazaW(gr.Max) + m, gr.RandeazaH(A.vect[i - 1]));
                }
            }
        }

        private void MotionDown(PictureBox b1, PictureBox b2)
        {
            while (b1.Location.X + size1.Width > b2.Location.X && b1.Location.Y < b2.Location.Y + size2.Height)
            {
                b1.Location = new Point(b1.Location.X - 1, b1.Location.Y + 1);
                b2.Location = new Point(b2.Location.X + 1, b2.Location.Y - 1);
                Refresh();
            }
            b1.BringToFront();
        }

        private void MotionUp(PictureBox b1, PictureBox b2)
        {
            while (b1.Location.X < pos2.X && b1.Location.Y > pos2.Y)
            {
                b1.Location = new Point(b1.Location.X + 1, b1.Location.Y - 1);
                b2.Location = new Point(b2.Location.X - 1, b2.Location.Y + 1);
                Refresh();
            }
            isInFront = !isInFront;
        }

        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            if (isInFront)
                ((Control)sender).BeginInvoke(mySwitch, pictureBox2, pictureBox1);
            else
                ((Control)sender).BeginInvoke(mySwitch, pictureBox1, pictureBox2);
        }

    }

}
