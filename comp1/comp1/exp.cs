using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp1
{
    public class commexp : System.Exception
    {

    }
    public class error : System.Exception
    {
        static string token;
        static int ponint;
        public error(string s, int p)
        {
            token = s;
            ponint = p;
        }
        public string tok()
        {
            int p = ponint + 1;
            return (error.token + " at token" + p);
        }
    }
}
