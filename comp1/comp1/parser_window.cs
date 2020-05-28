using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1
{
    public partial class parser_window : Form
    {
       static string[] token;
       static string[] lexem;
        parser final;
        


        public void aa(string[] t1, string[] t2)
        {

            token = t2;
            lexem = t1;
            final = new parser(token, lexem);

            InitializeComponent();

        }




        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

           
        }

        private void Parser_window_Load(object sender, EventArgs e)
        {
            try { 
            treeView1.Nodes.Add(final.program());
            }
            catch (error eeee)
            {
                MessageBox.Show(eeee.tok());

            }
            catch (System.ArgumentNullException )
            {
                MessageBox.Show("unknown error");

            }


        }




    }
}
