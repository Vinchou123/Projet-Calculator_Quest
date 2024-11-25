using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace CalculatorQuest
{
    public partial class MainWindow : Window
    {
        private bool _isResultDisplayed = false; // Ajouter un flag pour vérifier si un résultat est affiché

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

            // Si un résultat est affiché, on réinitialise l'affichage avant d'ajouter un nouveau chiffre
            if (_isResultDisplayed && content != "=" && content != "C" && content != "Effacer")
            {
                Display.Text = string.Empty;
                _isResultDisplayed = false;
            }

            switch (content)
            {
                case "C":
                    Display.Text = string.Empty;
                    _isResultDisplayed = false; // Réinitialiser le flag
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
                            _isResultDisplayed = true; // Marquer qu'un résultat est affiché
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
                    // Supprimer le dernier caractère de l'affichage
                    if (Display.Text?.Length > 0)
                    {
                        Display.Text = Display.Text.Remove(Display.Text.Length - 1);
                    }
                    break;
                case "←":
                    // Gérer la flèche gauche pour revenir en arrière dans le texte
                    if (Display.SelectionStart > 0)
                    {
                        Display.SelectionStart--;  // Déplacer le curseur vers la gauche
                    }
                    break;
                case "→":
                    // Gérer la flèche droite pour avancer dans le texte
                    if (Display.SelectionStart < Display.Text.Length)
                    {
                        Display.SelectionStart++;  // Déplacer le curseur vers la droite
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
