using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1
{
    public class tree
    {
        string token;
        string val;

        List<tree> child = new List<tree>();

        public tree(string t, string v)
        {
            token = t;
            val = v;
        }
        public void addch(tree t)
        {
            child.Add(t);
        }
        public string gtoken()
        {
            return token;
        }
        public List<tree> gchild()
        {
            return child ;
        }

    }
}
