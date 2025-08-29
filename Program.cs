using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculadora
{
    internal class Program
    {

        static readonly Queue<double> FilaResultados  = new Queue<double>();
        static readonly Stack<double> PilhaResultados = new Stack<double>();
        static readonly List<double>  ListaResultados = new List<double>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n___CALCULADORA (CMD)___");
                Console.WriteLine("1) Somar");
                Console.WriteLine("2) Subtrair");
                Console.WriteLine("3) Multiplicar");
                Console.WriteLine("4) Dividir");
                Console.WriteLine("5) Mostrar resultados (Fila/Pilha/Lista)");
                Console.WriteLine("6) Limpar resultados");
                Console.WriteLine("0) Sair");
                Console.Write("Escolha uma opção: ");

                string? opcao = Console.ReadLine();
                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        ExecutarOperacao(opcao!);
                        break;

                    case "5":
                        MostrarResultados();
                        break;

                    case "6":
                        LimparResultados();
                        break;

                    case "0":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }

            Console.WriteLine("Até mais!");
        }
        static void ExecutarOperacao(string opcao)
        {
            double n1 = LerDouble("Digite o primeiro número: ");
            double n2 = LerDouble("Digite o segundo número: ");

            double resultado;
            string simbolo;

            switch (opcao)
            {
                case "1":
                    resultado = Somar(n1, n2);
                    simbolo = "+";
                    break;
                case "2":
                    resultado = Subtrair(n1, n2);
                    simbolo = "-";
                    break;
                case "3":
                    resultado = Multiplicar(n1, n2);
                    simbolo = "×";
                    break;
                case "4":
                    resultado = Dividir(n1, n2);
                    simbolo = "÷";
                    break;
                default:
                    Console.WriteLine("Operação desconhecida.");
                    return;
            }

            Console.WriteLine($"\nResultado: {n1} {simbolo} {n2} = {resultado}");

            FilaResultados.Enqueue(resultado);
            PilhaResultados.Push(resultado);
            ListaResultados.Add(resultado);
        }


        static double Somar(double a, double b) => a + b;
        static double Subtrair(double a, double b) => a - b;
        static double Multiplicar(double a, double b) => a * b;

        static double Dividir(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("Erro: divisão por zero. Resultado definido como NaN.");
                return double.NaN;
            }
            return a / b;
        }
        static double LerDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? entrada = Console.ReadLine();


                if (double.TryParse(
                        entrada?.Replace(',', '.') ?? string.Empty,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double valor))
                {
                    return valor;
                }

                Console.WriteLine("Entrada inválida. Digite um número (ex: 10,5 ou 10.5).");
            }
        }
        static void MostrarResultados()
        {
            Console.WriteLine("--- Mostrar resultados ---");
            Console.WriteLine("1) Fila (ordem de chegada)");
            Console.WriteLine("2) Pilha (último primeiro)");
            Console.WriteLine("3) Lista (ordem de inserção)");
            Console.WriteLine("4) Todas");
            Console.Write("Escolha: ");

            string? escolha = Console.ReadLine();
            Console.WriteLine();

            switch (escolha)
            {
                case "1":
                    ImprimirEstrutura("Fila", FilaResultados);
                    break;

                case "2":
                    ImprimirEstrutura("Pilha", PilhaResultados);
                    break;

                case "3":
                    ImprimirEstrutura("Lista", ListaResultados);
                    break;

                case "4":
                    ImprimirEstrutura("Fila", FilaResultados);
                    ImprimirEstrutura("Pilha", PilhaResultados);
                    ImprimirEstrutura("Lista", ListaResultados);
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void ImprimirEstrutura(string nome, IEnumerable<double> estrutura)
        {
            Console.WriteLine($"[{nome}]");
            int i = 0;
            foreach (var r in estrutura)
            {
                Console.WriteLine($"  {++i}. {FormatarNumero(r)}");
            }
            if (i == 0)
            {
                Console.WriteLine("  (vazia)");
            }
            Console.WriteLine();
        }

        static string FormatarNumero(double valor)
        {
            if (double.IsNaN(valor)) return "NaN";
            if (double.IsInfinity(valor)) return valor > 0 ? "+∞" : "-∞";
            if (Math.Abs(valor % 1) < 1e-12) return ((long)Math.Round(valor)).ToString();
            return valor.ToString("0.########", CultureInfo.InvariantCulture);
        }

        static void LimparResultados()
        {
            Console.Write("Tem certeza que deseja limpar todas as estruturas? (s/n): ");
            var resp = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (resp == "s" || resp == "sim")
            {
                FilaResultados.Clear();
                PilhaResultados.Clear();
                ListaResultados.Clear();
                Console.WriteLine("Resultados limpos com sucesso.");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }
    }
}
