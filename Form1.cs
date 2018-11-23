using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rand = new Random();
        const byte some = 0;
        byte[] k = new byte[3];
        byte[] a = new byte[] { 0, 0, 0, 0 };
        byte[] a2 = new byte[] { 0, 0, 0, 0 };
        byte[] crc = new byte[7];
        byte[] bugs = new byte[] { 1, 0, 0, 0 };

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            for (int i = 0; i <= k.Length - 1; i++)
                k[i] = (byte)rand.Next(0, 2);
            foreach (byte val in k)
                textBox2.Text += val.ToString()+"\n";
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            Array.Clear(a, 0, 4);
            Array.Clear(a2, 0, 4);
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    switch (j)
                    {
                        case 0:
                            a[j] = k[i] != a2[3] ? (byte)1 : (byte)0;
                            textBox1.Text += a[j] + "   ";
                            break;
                        case 1:
                            a[j] = a[0] != a2[0] ? (byte)1 : (byte)0;
                            textBox1.Text += a[j] + "   ";
                            break;
                        case 2:
                            a[j] = a2[1] != a[0] ? (byte)1 : (byte)0;
                            textBox1.Text += a[j] + "   ";
                            break;
                        case 3:
                            a[j] = a2[2];
                            textBox1.Text += a[j] + "\n" +"\t";
                            a2[j] = a[j];
                            a2[0] = a[0];
                            a2[1] = a[1];
                            a2[2] = a[2];
                            break;
                        default:
                            textBox1.Text += "ERROR";
                            break;
                    }
                }
                button4.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)    // Make CRC array
        {
            textBox3.Clear();
            int someIndx = 3;
            button3.Enabled = true;
            for (int i = 0; i < k.Length; i++)
                crc[i] = k[i];
            for (int i = 3; i >= 0; i--)
            {
                crc[someIndx] = a[i];
                someIndx++;
            }
            foreach (byte i in crc)
                textBox3.Text += i + "\t";
            someIndx = 3;
        }

        private void button3_Click(object sender, EventArgs e)      // Decodering
        {
            Array.Clear(a, 0, 4);
            Array.Clear(a2, 0, 4);
            textBox4.Clear();
            int ind = 0;
            foreach (byte i in crc)
                decoder(i);
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == 0)
                {
                    label6.Text = "OK";
                }
                else
                {
                    for(int j = 0;j<crc.Length; j++)
                    {
                        decoder(some);
                        ind++;
                        if (a[0] == 1 && a[1] == 0 && a[2] == 0 && a[3] == 0)
                            break;         
                    }
                    label6.Text = $"Bug in :{ind} line";
                    break;
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            int b = rand.Next(0, 7);
            crc[b] = crc[b] == 1 ? (byte)0 : (byte)1;
            textBox3.Clear();
            foreach (byte i in crc)
                textBox3.Text += i + "\t";

        }

        private byte[] decoder(byte srs)
        {
                for (int j = 0; j <= 3; j++)
                {
                    switch (j)
                    {
                        case 0:
                            a[j] = srs != a2[3] ? (byte)1 : (byte)0;
                            textBox4.Text += a[j] + "  ";
                            break;
                        case 1:
                            a[j] = a2[0] != a2[3] ? (byte)1 : (byte)0;
                            textBox4.Text += a[j] + "  ";
                            break;
                        case 2:
                            a[j] = a2[1] != a2[3] ? (byte)1 : (byte)0;
                            textBox4.Text += a[j] + "  ";
                            break;
                        case 3:
                            a[j] = a2[2];
                            textBox4.Text += a[j] + "\t \n";
                            a2[0] = a[0];
                            a2[1] = a[1];
                            a2[2] = a[2];
                            a2[3] = a[3];
                            break;
                        default:
                            textBox4.Text = "ERROR";
                            break;
                    }
                }

            return a;
        }

    }
}
