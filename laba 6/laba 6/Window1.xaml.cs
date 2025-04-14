using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using number4_5;
namespace laba_6
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        LogicParser parser = new LogicParser();
        string expression;
        string rpnExpression;
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        bool result;
        
        public Window1()
        {
            InitializeComponent();

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }
        
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                expression = Expression.Text;
                rpnExpression = parser.ConvertToRPN(expression);
                Expression.Text = rpnExpression;

            }
            catch 
            {
                Answer.Text = "Неправильный ввод";
            }

        }

        private void EnterExpress_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int counterTwo = 0;
                string[] args =  Args.Text.Split(',');
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
                counterTwo = 0;
                result = parser.EvaluateRPN(rpnExpression, variables);
                Answer.Text = Convert.ToString(result);

            }
            catch
            {
                argsParam.Text = "Неверный формат ввода";
            }

        }
    }
}
