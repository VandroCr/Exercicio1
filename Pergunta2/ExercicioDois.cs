using System;
using System.Linq;
using System.Collections.Generic;

class ExercicioDois
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== EXERCÍCIO 2 - CALCULADORA DE SOMA E MÉDIA ===");
        Console.WriteLine();

        // Solicita a quantidade de números
        int quantidade = SolicitarQuantidade();
        
        // Solicita os números
        List<double> numeros = SolicitarNumeros(quantidade);
        
        // Calcula usando LINQ
        double soma = numeros.Sum();
        double media = numeros.Average();
        
        // Exibe os resultados
        ExibirResultados(numeros, soma, media);
        
        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
    
    static int SolicitarQuantidade()
    {
        int quantidade;
        bool quantidadeValida = false;
        
        do
        {
            Console.Write("Digite a quantidade de números (entre 3 e 10): ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out quantidade) && quantidade >= 3 && quantidade <= 10)
            {
                quantidadeValida = true;
            }
            else
            {
                Console.WriteLine("Erro: A quantidade deve ser um número inteiro entre 3 e 10.");
                Console.WriteLine();
            }
        } while (!quantidadeValida);
        
        Console.WriteLine($"Quantidade válida: {quantidade}");
        Console.WriteLine();
        return quantidade;
    }
    
    static List<double> SolicitarNumeros(int quantidade)
    {
        List<double> numeros = new List<double>();
        
        Console.WriteLine($"Digite {quantidade} números (decimais, positivos ou negativos):");
        Console.WriteLine();
        
        for (int i = 1; i <= quantidade; i++)
        {
            double numero;
            bool numeroValido = false;
            
            do
            {
                Console.Write($"Número {i}: ");
                string input = Console.ReadLine();
                
                if (double.TryParse(input, out numero))
                {
                    numeros.Add(numero);
                    numeroValido = true;
                }
                else
                {
                    Console.WriteLine("Erro: Digite um número válido (ex: 5, -3.5, 10.75)");
                }
            } while (!numeroValido);
        }
        
        return numeros;
    }
    
    static void ExibirResultados(List<double> numeros, double soma, double media)
    {
        Console.WriteLine();
        Console.WriteLine("=== RESULTADOS ===");
        Console.WriteLine($"Números digitados: {string.Join(", ", numeros)}");
        Console.WriteLine($"Quantidade de números: {numeros.Count}");
        Console.WriteLine($"Soma total: {soma:F2}");
        Console.WriteLine($"Média: {media:F2}");
    }
}

