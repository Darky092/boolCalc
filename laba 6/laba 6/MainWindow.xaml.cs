using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using number4_5;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;

namespace laba_6
{

    public partial class MainWindow : Window
    {
        static string textRnd;

        Random rnd = new Random();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void countButton_Click(object sender, RoutedEventArgs e)
        {

            string textRnd = "";
            for (int p = 0; p <= 6; p++)
            {
                string random = Convert.ToString(rnd.Next(0, 2) + " ");

                textRnd = textRnd + random;
                //if (p == 6) { textRnd = textRnd.Trim(); }


            }
            string[] words = textRnd.Split(' ');
            int[] numbers = new int[words.Length];
            for (int i = 0; i <= words.Length - 2; i++)
            {
                string b = words[i];
                int a = Convert.ToInt32(b);
                numbers[i] = a;
            }



            //int[] listRnd = new int[textRnd.Length];
            //rndTextbolox.Text = textRnd;
            //for(int i = 0; i<listRnd.Length; i++)
            //{
            //    string a = Convert.ToString(words[i]);
            //    int b = Convert.ToInt32(a);
            //    listRnd[i] = b;
            //}          
            //string textRndInfo = "";

            BoolFunctionLibrary objectOne = new BoolFunctionLibrary(numbers);



            //for (int a = 0;a<listRnd.Length; a++)
            //{
            //    textRndInfo = textRndInfo + listRnd[a].ToString() ;
            //}


            //rndInfo.Text = numbers.Length.ToString();

            string[] proccesedList = objectOne.Info();
            string proccesedString = "";
            for (int o = 0; o < proccesedList.Length; o++)
            {
                proccesedString = proccesedString + proccesedList[o] + "\n";

            }
            rndInfo.Text = proccesedString;
        }

        public void countButtonTwoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rnd = new Random();
                string enterText = "";
                enterText = enterReadBox.Text;
                enterText = enterText.Trim();
                string l = "";

                if (enterText.IndexOf(' ') == -1)
                {
                    {

                        foreach (char p in enterText)
                        {

                            l = l + p + " ";
                        }
                        l = l.Trim();
                        enterText = l;
                    }
                }


                string[] enterList = enterText.Split(' ');
                double n = Math.Log(enterList.Length, 2);
                for (int y = 0; y < enterList.Length; y++)
                {
                    if (n != Math.Round(n))
                    {


                        var b = enterList.Take(enterList.Length - 1).ToArray();
                        enterList = b;
                        n = Math.Log(enterList.Length, 2);
                        ttt.Content = "так как введёное количество чисел\n было некоректным мы сократили колво чиселю ";

                    }
                    else
                    {
                        break;

                    }
                }
                int[] numbers = new int[enterList.Length];

                for (int y = 0; y < enterList.Length; y++)
                {
                    numbers[y] = Convert.ToInt32(enterList[y]);

                }


                BoolFunctionLibrary objectTwo = new BoolFunctionLibrary(numbers);
                Sdnf xSdnf = new Sdnf(numbers);
                Sknf xSknf = new Sknf(numbers);
                BoolFunctionWithPolynomial Zhi = new BoolFunctionWithPolynomial(numbers);
                string[] proccesedList = objectTwo.Info();
                string proccesedString = "";
                for (int o = 0; o < proccesedList.Length; o++)
                {
                    proccesedString = proccesedString + proccesedList[o] + "\n";

                }
                enterInfo.Text = proccesedString;
                foreach (int p in numbers)
                {
                    if (p != 0 & p != 1)
                    {
                        enterInfo.Text = "некоректный ввод данных";
                        sdnf.Text = "некоректный ввод данных";
                        sknf.Text = "некоректный ввод данных";
                        Zhigalkin.Text = "некоректный ввод данных";
                        break;
                    }
                }

                sknf.Text = xSknf.GetSknf();
                sdnf.Text = xSdnf.GetSdnf();
                Zhigalkin.Text =Zhi.Info();

            }


            
            catch { enterInfo.Text = "некоректный ввод данных"; };



        }

        private void pageTwo_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window1();
            window.Show();
            this.Close();
        }

        private void fail_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window2();
            window.Show();
            this.Close();
        }
    }
}
