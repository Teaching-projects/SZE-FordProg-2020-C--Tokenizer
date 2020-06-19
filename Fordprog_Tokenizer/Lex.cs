using System;
using System.Collections.Generic;
using System.IO;

namespace Fordprog_Tokenizer
{
    class Lex
    {
        private List<string> keywordsList = new List<string>();

        private List<string> separatorList = new List<string>();

        private List<string> commentsList = new List<string>();

        private List<string> operatorList = new List<string>();

        public Lex(string FileName)
        {
            StreamReader sr = new StreamReader(FileName);
            while(!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(';');
                switch (line[1])
                {
                    case "keyword":
                        keywordsList.Add(line[0]);
                        break;
                    case "separator":
                        separatorList.Add(line[0]);
                        break;   
                    case "operator":
                        operatorList.Add(line[0]);
                        break;
                }
            }
            separatorList.Add(";");
        }

        private string Parse(string item)
        {
            string str = "";
            if (int.TryParse(item, out int isNumber))
            {
                str += "(numerical constant, " + item + ")";
                return str;
            }
            if (item.Equals("\r\n"))
            {
                return "\r\n";
            }
            if (isKeyword(item) == true)
            {
                str += "(keyword, " + item + ")";
                return str;
            }
            if (isOperator(item) == true)
            {
                str += "(operator, " + item + ")";
                return str;
            }
            if (isSeparator(item) == true)
            {
                str += "(separator, " + item + ")";
                return str;
            }
            str += "(identifier, " + item + ")";
            return str.ToString();
        }

        private bool isOperator(string str)
        {
            return operatorList.Contains(str);
        }
        private bool isSeparator(string str)
        {
            return separatorList.Contains(str);
        }
        private bool isKeyword(string str)
        {
            return keywordsList.Contains(str);
        }

        public string getNextElement(ref string item)
        {
            string tmpSubString = "";
            string token = "";
            for (int i = 0; i < item.Length; i++)
            {
                tmpSubString = item.Substring(i, 2);
                if (isSeparator(item[i].ToString()))
                {
                    if (i != item.Length && isSeparator(tmpSubString))
                    {
                        token += tmpSubString;
                        item = item.Remove(i, 2);
                        return Parse(token.ToString());
                    }
                    else
                    {
                        token += item[i];
                        item = item.Remove(i, 1);
                        return Parse(token.ToString());
                    }

                }
                else if (isOperator(item[i].ToString()))
                {
                    if (i != item.Length && (isOperator(tmpSubString)))
                        if (i + 2 < item.Length && isOperator(item.Substring(i, 3)))
                        {
                            token += item.Substring(i, 3);
                            item = item.Remove(i, 3);
                            return Parse(token.ToString());
                        }
                        else
                        {
                            token += tmpSubString;
                            item = item.Remove(i, 2);
                            return Parse(token.ToString());
                        }                   
                    else
                    {
                        if (item[i] == '-' && int.TryParse(item[i + 1].ToString(), out int isNumber))
                            continue;
                        token += item[i];
                        item = item.Remove(i, 1);
                        return Parse(token.ToString());
                    }
                }
                else
                    if (item[i] == '\'')
                    {
                        int j = i + 1;
                        if (item[j] == '\\')
                            j += 2;
                        else
                            j++;

                        token += "[literal constant, " + item.Substring(i, j - i + 1) +"]";
                        item = item.Remove(i, j - i + 1);
                        return token.ToString();
                    }
                    else
                        if (item[i] == '"')
                        {
                            int j = i + 1;
                            while (item[j] != '"')
                                j++;
                            token += "[literal constant, " + item.Substring(i, j - i + 1) + "] ";
                            item = item.Remove(i, j - i + 1);
                            return token.ToString();
                        }   
                    else
                    if (item[i + 1].ToString().Equals(" ") || isSeparator(item[i + 1].ToString()) || isOperator(item[i + 1].ToString()))
                    {
                        if (Parse(item.Substring(0, i + 1)).Contains("numerical constant") && item[i + 1] == '.')
                        {
                            int j = i + 2;
                            while (!item[j].ToString().Equals(" ") && !isSeparator(item[j].ToString()) && !isOperator(item[j].ToString()))
                                j++;
                            if (int.TryParse(item.Substring(i + 2, j - i - 2), out int isNumber))
                            {
                                token += "[numerical constant, " + item.Substring(0, j) + "]";
                                item = item.Remove(0, j);
                                return token.ToString();
                            }

                        }
                        token += item.Substring(0, i + 1);
                        item = item.Remove(0, i + 1);
                        return Parse(token.ToString());
                }
            }
            return null;
        }
    }
}