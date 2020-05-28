using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace comp1
{
   

    class lex2
    {

        bool isDigit(char d) { return (d >= '0' && d <= '9'); }
        bool isLetter(char l) { return (l >= 'a' && l <= 'z' || l >= 'A' && l <= 'Z'); }
        bool isSymbol(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/' || c == '<' || c == '>' || c == '(' || c == ')' || c == ';' || c == '"' || c == ':' || c == '=' || c == '&' || c == '|' || c == '{' || c =='}'||c== ','||c== '–') ;
        }
        

        bool isSpace(char s) { return (s == ' ' || s == '\t' || s == '\n' || s == '\r'); }

        enum states { START, COMMENT, NUM, IDINTIFIER, DONE, STRING };
        string[] RES_WORDS = { "int", "float", "string", "read", "write", "repeat", "until", "if", "elseif", "else", "then", "return", "end" };


        public void rlex(string c, ref string[] lex1, ref string[] idn)
        {
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            string mytoken = "";
            states state = new states();
            bool res_flag = false;
            int i = 0;
            while (state != states.DONE)
            {
                switch (state)
                {
                    case states.START:
                        if (isSpace(c[i]))
                        {
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        else if (isDigit(c[i]) || c[i] == '.')
                        {
                            state = states.NUM;
                        }
                        else if (isLetter(c[i]))
                        {
                            state = states.IDINTIFIER;
                        }

                        else if (isSymbol(c[i]))
                        {
                            switch (c[i])
                            {
                                case ';':

                                    list1.Add(Char.ToString(c[i]));
                                    list2.Add(";");
                                    break;

                                case ':':

                                    try
                                    {
                                        if (c[++i] == '=')
                                        {

                                            list1.Add(":=");
                                            list2.Add("assi");
                                            if (i == c.Length) state = states.DONE;
                                            else state = states.START;
                                        }
                                    }
                                    catch
                                    {
                                        list1.Add(Char.ToString(c[i]));
                                        list2.Add("symbol" + c[i]);
                                    }
                                    break;
                                case '/':

                                    try
                                    {
                                        if (c[++i] == '*')
                                        {
                                            i++;
                                            list1.Add("/**/");
                                            list2.Add("comm");
                                            state = states.COMMENT;
                                            goto ss;


                                        }
                                    }
                                    catch
                                    {
                                        i--;
                                        goto aa;
                                        //list1.Add(Char.ToString(c[--i]));
                                        //list2.Add("symbol  " + c[i]);
                                        
                                    }
                                    i--;
                                    goto aa;
                                  //  break;                                    

                                case '"':
                                    i++;
                                    state = states.STRING;
                                    goto ss;
                                case '<':

                                    try
                                    {
                                        if (c[i + 1] == '>')
                                        {
                                            i += 2;
                                            list1.Add("<>");
                                            list2.Add("not equal");
                                            if (i == c.Length) state = states.DONE;
                                            else state = states.START;
                                            goto ss;
                                           
                                            
                                        }
                                        else
                                        {
                                            goto aa;
                                        }
                                    }
                                    catch
                                    {
                                        goto aa;

                                    }
                                    break;

                                case '&':

                                    try
                                    {
                                        if (c[i + 1] == '&')
                                        {
                                            i += 2;
                                            list1.Add("&&&&");
                                            list2.Add("AND operator");
                                            if (i == c.Length) state = states.DONE;
                                            else state = states.START;
                                            goto ss;

                                        }
                                    }
                                    catch
                                    {
                                        goto aa;

                                    }
                                    
                                    break;


                                case '|':

                                    try
                                    {
                                        if (c[i + 1] == '|')
                                        {
                                            i += 2;
                                            list1.Add("||");
                                            list2.Add("OR operator");
                                            if (i == c.Length) state = states.DONE;
                                            else state = states.START;
                                            goto ss;

                                        }
                                    }
                                    catch
                                    {
                                        goto aa;

                                    }
                                    break;
                                default:
                                    aa:
                                    list1.Add(Char.ToString(c[i]));
                                    list2.Add("symbol" + c[i]);
                                    break;

                            }
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                            ss:;
                        }
                        else state = states.DONE;
                        break;

                    case states.STRING:
                        if (state == states.STRING)
                        {
                            mytoken = "\"";
                            while (c[i] != '"')
                            {
                                mytoken += c[i];
                                i++;
                                if (i >= c.Length) break;

                            }
                            mytoken += "\"";
                            i++;
                            if (i > c.Length)  //  string - (")
                            {
                                state = states.DONE;
                                list1.Add(mytoken);
                                list2.Add("error missint \"");
                                break;
                            }
                            list1.Add(mytoken);
                            list2.Add("String");

                            if (i == c.Length) state = states.DONE;

                            else state = states.START;
                        }
                        break;
                    case states.COMMENT:
                        bool fend = false;
                        try
                        {
                            while (!(c[i] == '*' && c[i + 1] == '/'))
                            {
                                i++;
                            }

                            if ((c[i] == '*' && c[i + 1] == '/'))

                            { fend = true; }
                        }
                        catch
                        {
                            throw new commexp();

                        }

                        if (fend != true)
                        {
                            i++;
                            throw new commexp();
                        }
                        else i += 2;

                        if (i == c.Length) state = states.DONE;
                        else state = states.START;

                        break;

                    case states.NUM:
                        while (isDigit(c[i]))
                        {
                            mytoken += c[i];
                            i++;
                            if (i >= c.Length) break;

                        }
                        if (i < c.Length)
                        {
                            if (c[i] == '.')
                            {
                                mytoken += c[i];
                                i++;
                                if (i >= c.Length) break;
                                while (isDigit(c[i]))
                                {

                                    mytoken += c[i];
                                    i++;
                                    if (i >= c.Length) break;


                                }

                            }
                            if (isLetter(c[i]))
                            {
                                mytoken += c[i];
                                i++;
                                list1.Add(mytoken);
                                list2.Add("identifier can't begin with number.");
                                MessageBox.Show("identifier can't begin with number");
                                goto err;
                            }
                        }
                        list1.Add(mytoken);
                        list2.Add("number");
                    err:;
                        mytoken = "";
                        if (i == c.Length) state = states.DONE;
                        else state = states.START;
                        break;

                    case states.IDINTIFIER:

                        while (isLetter(c[i]) || isDigit(c[i]))
                        {
                            mytoken += c[i];
                            i++;
                            if (i >= c.Length) break;
                        }
                        int count = 0;
                        for (; count < 13; count++)
                        {
                            if (RES_WORDS[count] == mytoken)
                            {
                                res_flag = true;
                                break;
                            }
                        }
                        if (res_flag)
                        {
                            list1.Add(mytoken);
                            list2.Add("reserved word " + RES_WORDS[count]);
                        }
                        else
                        {

                            list1.Add(mytoken);
                            list2.Add("identifier");

                        }
                        mytoken = "";
                        res_flag = false;
                        if (i == c.Length) state = states.DONE;
                        else state = states.START;
                        break;


                    case states.DONE:
                        break;

                }


            }
            lex1 = (string[])list1.ToArray(typeof(string));
            idn = (string[])list2.ToArray(typeof(string));

        }


    }
}
