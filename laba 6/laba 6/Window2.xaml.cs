using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using number4_5;

namespace laba_6
{
  
    public partial class Window2 : Window
    {
        int counter = 0;
        LogicParser parser = new LogicParser();
        string expression;
        string rpnExpression;
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        List<string> bools = new List<string>();
        List<string> expr = new List<string>();       
        bool result;
        string filePath = "";


        public Window2()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private async void upload_Click(object sender, RoutedEventArgs e)
        {   
            bools.Clear();
            expr.Clear();
            expression = "";
            rpnExpression = "";
            variables.Clear();
            counter = 0;
           
            string textFromFile = "";
            

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            bool? response = ofd.ShowDialog();
            if(response == true)
            {
               filePath = ofd.FileName;
            }



            filePathBox.Text = filePath;
            await Read(filePath);
            textFromFile = expression;
            filetext.Text = expression;

            string[] strings = textFromFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].Trim();

            }

            gridTwo.Visibility = Visibility.Visible;
            if (counter <= strings.Length)
            {
                quest.Text = strings[counter];
            }
            for (int i = 0; i < strings.Length; i++) 
            {
                if (strings[i] != "")
                {
                    expr.Add(strings[i]);
                }
            }
            

        }


        private bool isCorrect(string expression)     
        {
            
            int isCorrectCounter = 0;

            string[] expressionStrings = expression.Split(new[] {' '});

            string lastOperator = expressionStrings[expressionStrings.Length - 1];


            string firstOperator = expressionStrings[0];

            if (lastOperator != "not" && lastOperator != "xor" && lastOperator != "or" && lastOperator != "and") 
            {
                isCorrectCounter += 1;
            }
            if (firstOperator != "not" && firstOperator != "xor" && firstOperator != "or" && firstOperator != "and")
            {
                isCorrectCounter += 1;
            }


            for (int i = 0; i < expressionStrings.Length; i++)
            {
                if (i != 0) 
                {
                    if (expressionStrings[i] != expressionStrings[i - 1])
                    {


                    }
                    else 
                    {
                        isCorrectCounter -= 1;
                        break;
                    }

                }
            }



            if (isCorrectCounter == 2) 
            {
                return true;
            }

            return false;
        }


        private async void enter_Click(object sender, RoutedEventArgs e)
        {
            
                string newFileText = "";
                string trueFalse;
                int counterTwo = 0;






            if (isCorrect(quest.Text) == true)
            {



                trueFalse = Booleans.Text;

                string[] args = trueFalse.Split(new char[] { ',' });
                rpnExpression = parser.ConvertToRPN(quest.Text);



                string[] rpnExpressionArgs = rpnExpression.Split(' ');
                for (int i = 0; i < rpnExpressionArgs.Length; i++)
                {
                    if (char.IsLetter(Convert.ToChar(rpnExpressionArgs[i])))
                    {
                        try
                        {
                            variables[rpnExpressionArgs[i]] = Convert.ToBoolean(args[counterTwo]);
                            counterTwo++;
                        }
                        catch { }
                    }

                }

                try
                {
                    result = parser.EvaluateRPN(rpnExpression, variables);
                    bools.Add(Convert.ToString(result));
                }
                catch
                {
                    bools.Add("неверный формат ввода");
                }
            }
            else 
            {
                bools.Add("неверный формат ввода");
            }



                parser = new LogicParser();
                if (expr.Count == bools.Count)
                {
                    for (int i = 0; i < expr.Count; i++)
                    {

                        newFileText = newFileText + expr[i] + " = " + bools[i]+"\n";
                    }
                    filetext.Text = newFileText;
                    File.WriteAllText(filePath, newFileText);
                    gridTwo.Visibility = Visibility.Hidden;

                }

                counter += 1;
            if (counter < expr.Count)
            {
                quest.Text = expr[counter];
            }
            else if (counter >= expr.Count)
            {
                
            } 
            Booleans.Text = "";

        }


        private async Task Read(string filePath) 
        {
            using (FileStream fstream = File.OpenRead(filePath))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string TFL = Encoding.Default.GetString(buffer);
                expression = TFL;
            }

        }
        private async void Write(string filePath, string text)
        {
            using (FileStream fstream = File.OpenRead(filePath))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);
                
            }
        }
    


    }
}
