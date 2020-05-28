using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp1
{
   
    class identifier
    {

        private string[] res = { "if", "then", "else", "end", "repeat", "until", "read", "write","int" };
        private string[] spe = { "+", "-", "*", "/", "=", "<", ">", "(", ")", ";", ":=" ,"{","}"};
        private string[] spe2 = { "arthimatic + ", "arthimatic - ", "arthimatic * ", "arthimatic /", "equal", "<", ">", "(", ")", ";", ":=" ,"{","}"};


        public string [] Idn (string[] s)
        {
            ArrayList list1 = new ArrayList();
            for(int i=0;i<s.Length;i++)
            {

                if (s[i].All(char.IsDigit)&&s[i]!=""&&s[i]!=" "&&s[i]!="\n"&&s[i]!="\r"&&s[i]!=null)
                    {
                         list1.Add("number");
                        
                    }
                   
                else
                {
                    for(int j=0;j<res.Length;j++)
                    {
                        
                        if (s[i].Equals(res[j]))
                        {
                            list1.Add(res[j]);
                            goto REPEAT;
                        }

                    }
                    for(int j = 0; j < spe.Length; j++)
                    {   
                        if (s[i].Equals(spe[j]))
                        {
                            list1.Add(spe2[j]);
                            goto REPEAT; 
                        }
                    }
                    if (s[i].Length > 0 &&! char.IsDigit(s[i][0]))
                    {
                        list1.Add("idn");
                        goto REPEAT;
                    }
                    if(s[i]!="")
                    list1.Add("error");



                }
            REPEAT:;
            }
            string[] myArray = (string[])list1.ToArray(typeof(string));



            return myArray;
        }


    }
}
