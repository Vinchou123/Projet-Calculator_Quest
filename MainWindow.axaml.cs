using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace CalculatorQuest
{
    public partial class MainWindow : Window
    {
        private bool _isResultDisplayed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string? content = button.Content?.ToString();
                if (content != null)
                {
                    HandleButtonClick(content);
                }
                else
                {
                    Console.WriteLine("Erreur : le contenu du bouton est null");
                }
            }
            else
            {
                Console.WriteLine("Erreur : le sender n'est pas un bouton");
            }
        }

        private void HandleButtonClick(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            if (_isResultDisplayed && content != "=" && content != "C" && content != "Effacer")
            {
                Display.Text = string.Empty;
                _isResultDisplayed = false;
            }

            switch (content)
            {
                case "C":
                    Display.Text = string.Empty;
                    _isResultDisplayed = false;
                    break;
                case "=":
                    try
                    {
                        if (!string.IsNullOrEmpty(Display.Text))
                        {
                            string expression = Display.Text.Replace("÷", "/").Replace("×", "*").Replace(",", ".");

                            var result = EvaluateExpression(expression);
                            Display.Text = result.ToString();
                            _isResultDisplayed = true;
                        }
                        else
                        {
                            Display.Text = "Erreur";
                        }
                    }
                    catch (Exception)
                    {
                        Display.Text = "Erreur";
                    }
                    break;
                case "Eff":
                    if (Display.Text?.Length > 0)
                    {
                        Display.Text = Display.Text.Remove(Display.Text.Length - 1);
                    }
                    break;
                case "←":
                    if (Display.SelectionStart > 0)
                    {
                        Display.SelectionStart--;
                    }
                    break;
                case "→":
                    if (Display.SelectionStart < Display.Text.Length)
                    {
                        Display.SelectionStart++;
                    }
                    break;
                default:
                    Display.Text += content;
                    break;
            }
        }

        private double EvaluateExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException("L'expression ne peut pas être vide ou nulle.");
            }

            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
        }
    }
}
