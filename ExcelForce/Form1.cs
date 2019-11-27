using System;
using System.Windows.Forms;

namespace ExcelForce
{
    public partial class Form1 : Form
    {
        ExcelForce ex1;
        public Form1(ExcelForce ex)
        {
            ex1 = ex;
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                Form2 f2 = new Form2(ex1, textBox2.Text, textBox3.Text, checkBox1.Checked);
                this.Close();
                f2.Show();
            }
            else
            {
                label6.Visible = true;
            }
        }
    }
}
