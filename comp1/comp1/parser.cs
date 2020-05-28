using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;



    class parser
    {
        //List<token> lis;
        private string[] tokeens;
        string[] cod;
        string curenttoken;
        int point = 0;
        bool done = false;


        public void nexttoken()
        {
            try
            {
                curenttoken = tokeens[++point];
            }
            catch
            {
                done = true;
            }
        }

        public parser(string[] t1, string[] t2)
        {
            tokeens = t2;
            cod = t1;
            curenttoken = tokeens[0];
         


        }

        public void Match(string s)
        {
            if (s == curenttoken)
            {

                nexttoken();

            }
            else
            {
                //error(curenttoken, point);

            }
            //if (tokeens[point - 1] == "number")
            //    return cod[point - 1]; 

            //return tokeens[point - 1];

        }


        public TreeNode program()
        {
            return stmt_seq();
        }
        public TreeNode stmt_seq()
        {
            TreeNode temp;
            TreeNode temptemp=new TreeNode("stmt-seq");
            temp = stmt();
            try 
            { 
            temptemp.Nodes.Add(temp);
            }
            catch
            {
                throw new error(curenttoken, point);
            }
            while (curenttoken == ";" && done ==false)
            {
                Match(";");
                //temp.Nodes.Add(cod[point - 1]);
                try
                {
                    temptemp.Nodes.Add(stmt());
                }
                catch
                {
                    //end of code
                }
            }
            return temptemp;
        }
        public TreeNode stmt()
        {
            TreeNode temp;
            if (curenttoken == ("reserved word if"))
                temp = ifstmt();
            else if (curenttoken == "reserved word repeat")
                temp = repeat_stmt();
            else if (curenttoken == "identifier")
                temp = assign_stmt();
            else if (curenttoken == "reserved word read")
                temp = read_stmt();
            else if (curenttoken == "reserved word write")
                temp = write_stmt();
            else if (curenttoken == "comm")
            {
                Match("comm");
                temp = stmt();
            }
            else if(point==tokeens.Length)
            {
                return null;
            }
            else
            {
                throw new error(curenttoken, point);

            }
            return temp;

        }
        public TreeNode ifstmt()
        {
            TreeNode temp;
            Match("reserved word if");
            temp = new TreeNode(cod[point - 1]);
            temp.Nodes.Add(exp());
            Match("reserved word then");
            temp.Nodes.Add(new TreeNode(cod[point - 1]));
            temp.Nodes.Add(stmt_seq());
            if (curenttoken == "reserved word else")
            {
                Match("reserved word else");
                temp.Nodes.Add(new TreeNode( cod[point - 1]));

                temp.Nodes.Add(stmt_seq());
                Match("reserved word end");
                temp.Nodes.Add(new TreeNode(cod[point - 1]));

                return temp;

            }
            else if (curenttoken == "reserved word end")
            {
                Match("reserved word end");
                temp.Nodes.Add(new TreeNode(cod[point - 1]));
                return temp;
            }
            else
            {
                throw new error(curenttoken,point);
            }
        }
        public TreeNode repeat_stmt()
        {
            TreeNode temp;
            Match("reserved word repeat");
            temp = new TreeNode(cod[point - 1]);
            temp.Nodes.Add(stmt_seq());
            Match("reserved word until");
            temp.Nodes.Add("until");

            temp.Nodes.Add(exp());
            return temp;

        }
        public TreeNode assign_stmt()
        {
            TreeNode temp;
            Match("identifier");
            Match("assi");
                
            temp = new TreeNode(cod[point - 1]);
            temp.Nodes.Add( cod[point - 2]);
            temp.Nodes.Add(exp());
            return temp;


        }
        public TreeNode read_stmt()
        {
            TreeNode temp;
            Match("reserved word read");
            temp = new TreeNode(cod[point - 1]);
            Match("identifier");
            temp.Nodes.Add(new TreeNode(cod[point - 1]));
            return temp;
        }
        public TreeNode write_stmt()
        {
            TreeNode temp;
            Match("reserved word write");
            temp = new TreeNode(cod[point - 1]);
            temp.Nodes.Add(exp());
            return temp;

        }
        public TreeNode exp()
        {
            TreeNode temp = null ;
            
            TreeNode temptemp;
            temptemp = simple_exp();

            if (curenttoken == "symbol<" || curenttoken == "symbol>" || curenttoken == "symbol=")
            {
                temp=(comp_op());
                temp.Nodes.Add(temptemp);
                temp.Nodes.Add(simple_exp());
            }
            if (temp == null)
                return temptemp;
            return temp;
        }
        public TreeNode comp_op()
        {

            if (curenttoken == "symbol>")
            {
                Match("symbol>");
                return new TreeNode( ">");
            }
            else if (curenttoken == "symbol<")
            {
                Match("symbol<");
                return new TreeNode( "<");
            }
            else if (curenttoken == "symbol=")
            {
                Match("symbol=");
                return new TreeNode( "=");
            }
            else
            {
                throw new error(curenttoken,point);

            }
        }
        public TreeNode simple_exp()
        {
            TreeNode temp=null;
            TreeNode temptemp;

            temptemp = term();
            while (curenttoken == "symbol+" || curenttoken == "symbol-")
            {
                temp = add_op();
                temp.Nodes.Add(temptemp);
                temp.Nodes.Add(term());
            }
            if (temp == null)
            {
                return temptemp;

            }
            return temp;
        }
        public TreeNode add_op()
        {
            if (curenttoken == "symbol+")
            {
                Match("symbol+");
                return new TreeNode("+");
            }
            else if (curenttoken == "symbol-")
            {
                Match("symbol-");
                return new TreeNode( "-");

            }
            else
            {
                throw new error(curenttoken, point);

            }
        }
        public TreeNode term()
        {
            TreeNode temp=null;
            TreeNode temptemp;

            temptemp = factor();

            while (curenttoken == "symbol*" || curenttoken == "symbol/")
            {
                temp=mul_op();
                temp.Nodes.Add(temptemp);
                temp.Nodes.Add(factor());
            }
            if (temp == null)
                return temptemp;
            return temp;
        }
        public TreeNode mul_op()
        {

            if (curenttoken == "symbol*")
            {
                Match("symbol*");
                return new TreeNode( "*");
            }
            else if (curenttoken == "symbol/")
            {
                Match("symbol/");
                return new TreeNode( "/");

            }
            else
            {
                throw new error(curenttoken, point);
            }

        }
        public TreeNode factor()
        {
            TreeNode temp;
            if (curenttoken == "symbol(")
            {
                Match("symbol(");
                temp = exp();
                Match("symbol)");
            }
            else if (curenttoken == "number")
            {
                Match("number");
                temp = new TreeNode(cod[point - 1]);
            }
            else if (curenttoken == "identifier")
            {
                Match("identifier");
                temp = new TreeNode (cod[point - 1]);


            }
            else
            {
                throw new error(curenttoken, point);

            }
            return temp;
        }

    }
}




