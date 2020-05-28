using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace comp1
{
    public partial class comp1 : Form
    {
        string ls;
        string[] nl = { "" };
        string[] ni = { "" };
        parser_window f2;
        public comp1()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)

        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ls = saveFileDialog1.FileName;
                File.WriteAllText(ls, textBox1.Text);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(ls, textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save file \nOriginal error: " + ex.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure you want to exit ?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";

            lex2 l = new lex2();
            //identifier i = new identifier();

            

            try
    {
                l.rlex(textBox1.Text, ref nl, ref ni);
            }
            catch (commexp)
            {
                MessageBox.Show("comment error");
            }
            catch(Exception ex)
            {

            }
            //    ni = i.Idn(nl);


            foreach (string s in nl)
            {
                if (s != " " && s != "" && s != "\n" && s != "\r" && s != null)
                {
                    label1.Text = label1.Text + s + "\n";
                }
            }
            foreach (string s in ni)
            {
                if (s != " " && s != "" && s != "\n" && s != "\r" && s != null)
                {
                    label2.Text = label2.Text + s + "\n";
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = "";
            label2.Text = "";
        }

        private void comp1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        public string[] tokensssss()
        {
           return ni ;
        }

        public string[] lexsssssss()
        {
            return nl;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if ((f2 == null))
            {
                f2 = new parser_window();
            }
            else
            {
                f2.Close();
                f2 = new parser_window();

            }

            f2.aa(ni, nl);
            f2.Show();

        }
    }


}
