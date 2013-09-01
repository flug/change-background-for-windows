using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace ChangeMyBackground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numberCombox();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 5000;
            timer1.Start();

            //   b.background();

            //background();
        }

        private int tcb;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                //timer1.Stop();
                bw.RunWorkerAsync();
            }
        }

        private void btmini_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Hide();
                notifyIcon1.BalloonTipTitle = "Application minimizer";
                notifyIcon1.BalloonTipText = "Your application has been minimized to the taskbar.";
                notifyIcon1.ShowBalloonTip(3000);
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void numberCombox()
        {
            for (int i = 1000; i < 10000; i++)
            {
                comboBox1.Items.Add(i);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tcb = int.Parse(comboBox1.Text);

            timer1.Interval = tcb;

            //    MessageBox.Show(timer1.Interval.ToString());
        }

        private void btstop_Click(object sender, EventArgs e)
        {
            if (btstop.Text == "Start")
            {
                timer1.Start();
                btstop.Text = "Stop";
                MessageBox.Show("C'est reparti !");
            }
            else
            {
                timer1.Stop();
                btstop.Text = "Start";
                MessageBox.Show("Bien arrêté");
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Background b = new Background();
            b.background();
        }
    }
}