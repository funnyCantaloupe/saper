using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System;

namespace saper
{
    public partial class Form1 : Form
    {
        public Form2 form2;
        public Form1()
        {
            InitializeComponent();
        }

        public int n;
        public int k;

        public void button1_Click(object sender, EventArgs e)
        {
            int temp_n = int.Parse(textBox1.Text);
            int temp_k = int.Parse(textBox2.Text);
            n = temp_n;
            k = temp_k;
            form2 = new Form2(this);
            form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
