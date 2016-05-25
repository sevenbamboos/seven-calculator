using samw.Calculator.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            CheckBox box = (CheckBox)lbl.Target;
            box.IsChecked = !box.IsChecked;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)App.Current;
            Exam exam = app.Exam;
            if (exam == null)
            {
                exam = new Exam();
            }

            if (checkAddLevel1.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = addLevel1Name.Content.ToString();
                if (int.TryParse(countAddLevel1.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitAdd, 10, 10, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countAddLevel1.Text, name);
                }
                
            }

            if (checkAddLevel2.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = addLevel2Name.Content.ToString();
                if (int.TryParse(countAddLevel2.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitAdd, 100, 10, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countAddLevel2.Text, name);
                }

            }

            if (checkAddLevel3.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = addLevel3Name.Content.ToString();
                if (int.TryParse(countAddLevel3.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitAdd, 25, 25, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countAddLevel3.Text, name);
                }

            }

            if (checkAddLevel4.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = addLevel4Name.Content.ToString();
                if (int.TryParse(countAddLevel4.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitAdd, 100, 100, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countAddLevel4.Text, name);
                }

            }

            if (checkMinusLevel1.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = minusLevel1Name.Content.ToString();
                if (int.TryParse(countMinusLevel1.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitSubtract, 10, 10, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countMinusLevel1.Text, name);
                }

            }

            if (checkMinusLevel2.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = minusLevel2Name.Content.ToString();
                if (int.TryParse(countMinusLevel2.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitSubtract, 20, 10, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countMinusLevel2.Text, name);
                }

            }

            if (checkMinusLevel3.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = minusLevel3Name.Content.ToString();
                if (int.TryParse(countMinusLevel3.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitSubtract, 50, 10, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countMinusLevel3.Text, name);
                }

            }

            if (checkMinusLevel4.IsChecked.GetValueOrDefault(false))
            {
                int count;
                string name = minusLevel4Name.Content.ToString();
                if (int.TryParse(countMinusLevel4.Text, out count))
                {
                    exam.Add(new Exercise(name, samw.Calculator.Model.Expression.InitSubtract, 100, 100, count));
                }
                else
                {
                    log("[error] Can't parse count {0} for {1}", countMinusLevel4.Text, name);
                }

            }

            StringBuilder sb = new StringBuilder();
            foreach (IEvaluable exe in exam.Generate(false))
            {
                sb.AppendLine(exe.ToString());
            }
            MessageBox.Show(sb.ToString());
        }

        private void log(string format, params object[] arg)
        {
            //TODO decent log?
            Console.WriteLine(format, arg);
        }
    }
}
