using System.Collections;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private String currentEntry = "0";
        private double subtotal = 0;
        private String operador = "";
        private double memoria = 0;

        public MainPage()
        {
            InitializeComponent();

        }

        private void OnNumberClicked (object sender, EventArgs e)
        {
            Button button = sender as Button;
            String number = button.Text;

            if (number == ".")
            {
                // Solo agregar el punto si aún no existe en el número actual
                if (!currentEntry.Contains("."))
                    currentEntry += ".";
            }
            else
            {
                // Si el display tiene "0", lo reemplaza (excepto si es un decimal)
                if (currentEntry == "0")
                    currentEntry = number;
                else
                    currentEntry += number;
            }

            Display.Text = currentEntry;
        }
        

        private void OnOperatorClicked (object sender, EventArgs e)
        {
            Button button = (Button)sender;
            String newOperator = button.Text;

            double currentNumber = Convert.ToDouble(currentEntry);
            
            if (!string.IsNullOrEmpty(operador))
            {
                subtotal = calcular(subtotal, currentNumber, operador);
                Display.Text = subtotal.ToString();
            } else
            {
                subtotal = currentNumber;
            }

            operador = newOperator;
            currentEntry = "0";

        }

        private double calcular (double a, double b,string op)
        {
            return op switch
            {
                "+" => a + b,
                "-" => a - b,
                "x" => a * b,
                "/" => b != 0 ? a/b : 0,
                _ => b
            };
        }

        private void OnEqualClicked (object sender, EventArgs e)
        {
            double currentNumber = Convert.ToDouble(currentEntry);

            subtotal = calcular(subtotal, currentNumber, operador);
            Display.Text = subtotal.ToString();
            operador = "";
            currentEntry = subtotal.ToString();

        }

        private void OnClearClicked (object sender, EventArgs e)
        {
            currentEntry = "0";
            subtotal = 0;
            operador = "";
            Display.Text = "0";
        }

        private void OnMemoryClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            double numeroActual = Convert.ToDouble(Display.Text);

            switch (button.Text)
            {
                case "MC":
                    memoria = 0;
            break;

            case "M":
                    currentEntry = memoria.ToString();
                    Display.Text = currentEntry;
                    break;
            

            case "M+":
                    memoria += numeroActual;
                    break;
            case "M-":
                    memoria -= numeroActual;
                    break;


            }

        }

    
    }
}
