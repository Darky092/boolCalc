using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace number4_5
{
    public class BoolFunctionLibrary
    {
        protected int[] _list;


        public BoolFunctionLibrary(int[] list)
        {
            _list = list;
        }





        public int ValueCount()
        {
            double n = Math.Log(_list.Length, 2);
            if (n != Math.Round(n))
                throw new ArgumentException(nameof(_list));

            return (int)n;
        }

        virtual public int[,] TruthTable()
        {
            int[,] newMatrix = new int[_list.Length, ValueCount() + 1];
            for (int i = 0; i < _list.Length; i++)
            {

                int n = i;

                int m = 2;
                string s = "";

                if (n == 0)
                {
                    if (s.Length < ValueCount() + 1)
                    {
                        for (int p = s.Length; p < ValueCount(); p++)
                        {
                            s = "0" + s;
                        }
                    }

                }
                else
                {
                    while (n > 0)
                    {
                        s = Convert.ToString(n % m) + s;

                        n /= m;
                    }
                    if (s.Length < ValueCount() + 1)
                    {
                        for (int p = s.Length; p < ValueCount(); p++)
                        {

                            s = "0" + s;
                        }
                    }


                }
                s = s + Convert.ToString(_list[i]);






                for (int j = 0; j < s.Length; j++)
                {
                    newMatrix[i, j] = Convert.ToInt32(s[j].ToString());

                }


            }
            return newMatrix;
        }

        public string Weight()
        {
            int a = 0;
            foreach (int i in _list)
            {
                if (i == 1)
                {
                    a += 1;
                }
            }
            return Convert.ToString(a);
        }

        public string[] Info()
        {
            int[,] matrix = TruthTable();
            string[] matrixStrings = new string[_list.Length + 2];

            int a;
            for (a = 0; a < _list.Length; a++)
            {
                string s = "";
                for (int p = 0; p < ValueCount() + 1; p++)
                {
                    s = s + $"{matrix[a, p]}";

                }

                matrixStrings[a] = s;
            }
            matrixStrings[a] = Weight();

            return matrixStrings;

        }


    }

    public class Sdnf : BoolFunctionLibrary
    {

        public Sdnf(int[] list) : base(list) { }

        public string GetSdnf()
        {
            int[,] truthTable = TruthTable();
            string sdnf = "";
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i] == 1)
                {
                    string minterm = "";
                    for (int j = 0; j < ValueCount(); j++)
                    {
                        if (truthTable[i, j] == 1)
                        {
                            minterm += $"x{j + 1}";
                        }
                        else
                        {
                            minterm += $"!x{j + 1}";
                        }
                        if (j < ValueCount() - 1) minterm += " & ";
                    }
                    sdnf += $"({minterm})";
                    if (i < _list.Length - 1) sdnf += " | ";
                }
            }
            return sdnf;
        }
    }


    public class Sknf : BoolFunctionLibrary
    {
        public Sknf(int[] list) : base(list) { }

        public string GetSknf()
        {
            int[,] truthTable = TruthTable();
            string sknf = "";
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i] == 0)
                {
                    string maxterm = "";
                    for (int j = 0; j < ValueCount(); j++)
                    {
                        if (truthTable[i, j] == 1)
                        {
                            maxterm += $"x{j + 1}";
                        }
                        else
                        {
                            maxterm += $"!x{j + 1}";
                        }
                        if (j < ValueCount() - 1) maxterm += " | ";
                    }
                    sknf += $"({maxterm})";
                    if (i < _list.Length - 1) sknf += " & ";
                }
            }
            return sknf;
        }
    }
    public class BoolFunctionWithPolynomial : BoolFunctionLibrary
    {
        List<int> _ListForPolinome = new List<int>();

        List<int> _ListForNewTable = new List<int>();

        List<int> _NewList = new List<int>();

        List<int> _ints = new List<int>();
        int lenght;
        public BoolFunctionWithPolynomial(int[] list) : base(list) { }
        public List<int> PolinomeList()
        {
            foreach (int key in _list)
            {
                _ListForPolinome.Add(key);

            }
            return _ListForPolinome;
        }
        public int lenth()
        {
            lenght = _ListForPolinome.Count;
            return lenght;
        }
        public List<int> refd()
        {
            _ListForPolinome.Clear();
            return _ListForPolinome;
        }
        public List<int> refg()
        {
            _NewList.Clear();
            return _NewList;
        }

        public List<int> Xor()
        {
            for (int i = 0; i < _ListForPolinome.Count; i++)
            {
                if (i + 1 < _ListForPolinome.Count)
                {
                    if (_ListForPolinome[i] + _ListForPolinome[i + 1] == 0)
                    {
                        _NewList.Add(0);
                    }
                    else if (_ListForPolinome[i] + _ListForPolinome[i + 1] == 1)
                    {
                        _NewList.Add(1);
                    }
                    else if (_ListForPolinome[i] + _ListForPolinome[i + 1] == 2)
                    {
                        _NewList.Add(0);
                    }

                }

            }
            foreach (int key in _NewList)
            {
                _ints.Add(key);

            }
            refd();
            foreach (int key in _NewList)
            {
                _ListForPolinome.Add(key);
            }
            refg();

            return _ListForPolinome;
        }

        public List<int> AddList()
        {


            _ListForNewTable.Add(_ListForPolinome[0]);



            return _ListForNewTable;
        }
        public List<int> FormingList()
        {
            if (_ListForNewTable.Count < lenth())
            {
                int _Length = lenth();
                for (int i = 0; i < _Length; i++)
                {


                    AddList();
                    Xor();

                }



            }
            return _ListForNewTable;
        }
        virtual public int[,] TruthTableXor()
        {
            PolinomeList();
            int lenght = lenth();
            FormingList();
            int[,] newMatrix = new int[lenght, ValueCount() + 1];
            for (int i = 0; i < lenght; i++)
            {

                int n = i;

                int m = 2;
                string s = "";

                if (n == 0)
                {
                    if (s.Length < ValueCount() + 1)
                    {
                        for (int p = s.Length; p < ValueCount(); p++)
                        {
                            s = "0" + s;
                        }
                    }

                }
                else
                {
                    while (n > 0)
                    {
                        s = Convert.ToString(n % m) + s;

                        n /= m;
                    }
                    if (s.Length < ValueCount() + 1)
                    {
                        for (int p = s.Length; p < ValueCount(); p++)
                        {

                            s = "0" + s;
                        }
                    }


                }
                s = s + Convert.ToString(_ListForNewTable[i]);






                for (int j = 0; j < s.Length; j++)
                {
                    newMatrix[i, j] = Convert.ToInt32(s[j].ToString());

                }


            }
            refd();

            return newMatrix;
        }





        public string GetZhegalkinPolynomial()
        {
            int[,] truthTable = TruthTableXor();
            StringBuilder polynomial = new StringBuilder();

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                if (truthTable[i, truthTable.GetLength(1) - 1] == 1)
                {
                    if (polynomial.Length > 0)
                    {
                        polynomial.Append(" + ");
                    }
                    string monomial = GetMonomial(i);
                    polynomial.Append(monomial);
                }
            }
            return polynomial.Length > 0 ? polynomial.ToString() : "0";
        }


        private string GetMonomial(int rowIndex)
        {
            string monomial = "";
            int valueCount = ValueCount();

            for (int i = 0; i < valueCount; i++)
            {
                if (TruthTableXor()[rowIndex, i] == 1)
                {
                    monomial += $"x{i + 1}";
                }
            }

            return monomial == "" ? "1" : monomial;
        }


        public new string Info()
        {
            var polynomial = GetZhegalkinPolynomial();
            return $"{polynomial}";
        }
        public string[] Infotwo()
        {
            int[,] matrix = TruthTableXor();
            string[] matrixStrings = new string[_list.Length + 2];

            int a;
            for (a = 0; a < _list.Length; a++)
            {
                string s = "";
                for (int p = 0; p < ValueCount() + 1; p++)
                {
                    s = s + $"{matrix[a, p]}";

                }

                matrixStrings[a] = s;
            }
            matrixStrings[a] = Weight();

            return matrixStrings;

        }

    }




        public class LogicParser
        {
            private Stack<char> operatorsStack = new Stack<char>();
            private List<string> outputQueue = new List<string>();


            public string ConvertToRPN(string expression)
            {
                expression = NormalizeExpression(expression);
                int i = 0;

                while (i < expression.Length)
                {
                    char current = expression[i];

                    if (IsVariable(current))
                    {
                        string variable = "";
                        while (i < expression.Length && IsVariable(expression[i]))
                        {
                            variable += expression[i];
                            i++;
                        }
                        outputQueue.Add(variable);
                    }
                    else if (current == '(')
                    {
                        operatorsStack.Push('(');
                        i++;
                    }
                    else if (current == ')')
                    {

                        while (operatorsStack.Count > 0 && operatorsStack.Peek() != '(')
                        {
                            outputQueue.Add(operatorsStack.Pop().ToString());
                        }
                        operatorsStack.Pop();
                        i++;
                    }
                    else if (IsOperator(current))
                    {

                        while (operatorsStack.Count > 0 && GetPriority(current) <= GetPriority(operatorsStack.Peek()))
                        {
                            outputQueue.Add(operatorsStack.Pop().ToString());
                        }
                        operatorsStack.Push(current);
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }


                while (operatorsStack.Count > 0)
                {
                    outputQueue.Add(operatorsStack.Pop().ToString());
                }

                return string.Join(" ", outputQueue);
            }


            public bool EvaluateRPN(string rpnExpression, Dictionary<string, bool> variables)
            {
                Stack<bool> stack = new Stack<bool>();
                string[] tokens = rpnExpression.Split(' ');

                foreach (string token in tokens)
                {
                    if (variables.ContainsKey(token))
                    {
                        stack.Push(variables[token]);
                    }
                    else if (IsOperator(token[0]))
                    {
                        bool b2 = stack.Pop();
                        bool b1 = false;

                        if (token[0] != '!')
                        {
                            b1 = stack.Pop();
                        }

                        switch (token[0])
                        {
                            case '&':
                                stack.Push(b1 && b2);
                                break;
                            case '|':
                                stack.Push(b1 || b2);
                                break;
                            case '^':
                                stack.Push(b1 ^ b2);
                                break;
                            case '!':
                                stack.Push(!b2);
                                break;
                        }
                    }
                }

                return stack.Pop();
            }


            private bool IsVariable(char c)
            {
                return char.IsLetter(c);
            }

            private bool IsOperator(char c)
            {
                return "&|!^".Contains(c);
            }

            private int GetPriority(char op)
            {
                switch (op)
                {
                    case '!': return 4;
                    case '&': return 3;
                    case '^': return 2;
                    case '|': return 1;
                    default: return 0;
                }
            }


            private string NormalizeExpression(string expression)
            {
                expression = expression.Replace(" ", "").ToUpper();
                expression = expression.Replace("AND", "&");
                expression = expression.Replace("XOR", "^");
                expression = expression.Replace("OR", "|");
                expression = expression.Replace("NOT", "!");

                return expression;
            }
        }


    
}

