using System;

namespace PerguntasExer2
{
    class MinhaCalculadora
    {
        static void Main(string[] args)
        {
            bool continuar = true;
            
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== CALCULADORA ===");
                Console.WriteLine();
                Console.WriteLine("Escolha uma operação:");
                Console.WriteLine("1 - Somar");
                Console.WriteLine("2 - Subtrair");
                Console.WriteLine("3 - Multiplicar");
                Console.WriteLine("4 - Dividir");
                Console.WriteLine("5 - Módulo (Resto da divisão)");
                Console.WriteLine("6 - Exponenciação");
                Console.WriteLine("0 - Sair");
                Console.WriteLine();
                Console.Write("Digite sua opção: ");
                
                string opcao = Console.ReadLine() ?? "";
                
                switch (opcao)
                {
                    case "1":
                        Somar();
                        break;
                    case "2":
                        Subtrair();
                        break;
                    case "3":
                        Multiplicar();
                        break;
                    case "4":
                        Dividir();
                        break;
                    case "5":
                        Modulo();
                        break;
                    case "6":
                        Exponenciacao();
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("Saindo da calculadora...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Pressione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }
        
        static void Somar()
        {
            Console.WriteLine();
            Console.WriteLine("=== SOMA ===");
            
            Console.Write("Digite o primeiro valor: ");
            double valor1 = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Digite o segundo valor: ");
            double valor2 = Convert.ToDouble(Console.ReadLine());
            
            double resultado = valor1 + valor2;
            
            Console.WriteLine($"Resultado: {valor1} + {valor2} = {resultado}");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        
        static void Subtrair()
        {
            Console.WriteLine();
            Console.WriteLine("=== SUBTRAÇÃO ===");
            
            Console.Write("Digite o primeiro valor: ");
            double valor1 = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Digite o segundo valor: ");
            double valor2 = Convert.ToDouble(Console.ReadLine());
            
            double resultado = valor1 - valor2;
            
            Console.WriteLine($"Resultado: {valor1} - {valor2} = {resultado}");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        
        static void Multiplicar()
        {
            Console.WriteLine();
            Console.WriteLine("=== MULTIPLICAÇÃO ===");
            
            Console.Write("Digite o primeiro valor: ");
            double valor1 = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Digite o segundo valor: ");
            double valor2 = Convert.ToDouble(Console.ReadLine());
            
            double resultado = valor1 * valor2;
            
            Console.WriteLine($"Resultado: {valor1} × {valor2} = {resultado}");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        
        static void Dividir()
        {
            Console.WriteLine();
            Console.WriteLine("=== DIVISÃO ===");
            
            Console.Write("Digite o primeiro valor (dividendo): ");
            double valor1 = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Digite o segundo valor (divisor): ");
            double valor2 = Convert.ToDouble(Console.ReadLine());
            
            if (valor2 == 0)
            {
                Console.WriteLine("Não é possível dividir por zero.");
            }
            else
            {
                double resultado = valor1 / valor2;
                Console.WriteLine($"Resultado: {valor1} ÷ {valor2} = {resultado:F2}");
            }
            
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        
        static void Modulo()
        {
            Console.WriteLine();
            Console.WriteLine("=== MÓDULO (Resto da divisão) ===");
            
            Console.Write("Digite o primeiro valor (dividendo): ");
            double valor1 = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Digite o segundo valor (divisor): ");
            double valor2 = Convert.ToDouble(Console.ReadLine());
            
            if (valor2 == 0)
            {
                Console.WriteLine("Não é possível dividir por zero.");
            }
            else
            {
                double resultado = valor1 % valor2;
                Console.WriteLine($"Resultado: {valor1} % {valor2} = {resultado}");
            }
            
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
        
        static void Exponenciacao()
        {
            Console.WriteLine();
            Console.WriteLine("=== EXPONENCIAÇÃO ===");
            
            Console.Write("Digite a base: ");
            double baseNum = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Digite o expoente: ");
            double expoente = Convert.ToDouble(Console.ReadLine());
            
            double resultado = Math.Pow(baseNum, expoente);
            
            Console.WriteLine($"Resultado: {baseNum}^{expoente} = {resultado}");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
    }
}
