using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace CalculatorQuest
{
    public partial class MainWindow : Window
    {
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

    switch (content)
    {
        case "C":
            Display.Text = string.Empty;
            break;
        case "=":
            try
            {
                // Vérifiez que Display.Text n'est pas vide avant d'appeler EvaluateExpression
                if (!string.IsNullOrEmpty(Display.Text))
                {
                    string expression = Display.Text.Replace("÷", "/").Replace("×", "*").Replace(",", ".");

                    var result = EvaluateExpression(expression);
                    Display.Text = result.ToString();
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
        default:
            Display.Text += content;
            break;
    }
}



        private double EvaluateExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                // Retournez une valeur par défaut ou gérez l'erreur de manière appropriée
                throw new ArgumentException("L'expression ne peut pas être vide ou nulle.");
            }

            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
        }

    }
}

