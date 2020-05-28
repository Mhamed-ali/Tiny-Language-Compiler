using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        static string ss;
        public error(string s, int p,string s2)
        {
            token = s;
            ponint = p;
            ss = s2;

        }
        public string tok()
        {
            int p = ponint + 1;
            return (error.token + " at token" + p+ ss);
        }
    }
}
