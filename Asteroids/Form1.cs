using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Width = 1024;
            form.Height = 768;
            form.FormClosed += Form_FormClosed;
            Game.Init(form);
            form.Show();
            this.Hide();
            Game.Draw();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
