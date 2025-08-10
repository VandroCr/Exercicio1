using System;

namespace Exercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Calculadora de Média Escolar ===");
            Console.WriteLine();

            // Solicita o nome do aluno
            Console.Write("Digite o nome do aluno: ");
            string nome = Console.ReadLine() ?? "Aluno";

            // Solicita as notas
            Console.Write("Digite a primeira nota: ");
            double nota1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Digite a segunda nota: ");
            double nota2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Digite a terceira nota: ");
            double nota3 = Convert.ToDouble(Console.ReadLine());

            // Calcula a média
            double media = (nota1 + nota2 + nota3) / 3;

            // Exibe o resultado
            Console.WriteLine();
            Console.WriteLine($"Aluno: {nome}");
            Console.WriteLine($"Notas: {nota1}, {nota2}, {nota3}");
            Console.WriteLine($"Média: {media:F2}");

            // Verifica se foi aprovado (média >= 7.0)
            if (media >= 7.0)
            {
                Console.WriteLine("Status: APROVADO!");
            }
            else if (media >= 5.0)
            {
                Console.WriteLine("Status: RECUPERAÇÃO");
            }
            else
            {
                Console.WriteLine("Status: REPROVADO");
            }

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
