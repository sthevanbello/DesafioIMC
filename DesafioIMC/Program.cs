using System;

namespace DesafioIMC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaInicial();
        }
        private static void TelaInicial()
        {
            Console.WriteLine("\tCálculo de IMC\n");
            Console.Write("Insira o nome do paciente: ");
            string nome = Console.ReadLine();

            Console.Write("Insira o sexo do paciente: ");
            string sexo = Console.ReadLine();

            Console.Write("Insira a idade do paciente: ");
            int idade = Convert.ToInt16(Console.ReadLine());

            Console.Write("Insira o peso do paciente: ");
            double peso = Convert.ToDouble(Console.ReadLine());

            Console.Write("Insira a sua altura: ");
            double altura = Convert.ToDouble(Console.ReadLine());

            double imc = CalculaImc(peso, altura);
            // Chama a função para exibir a primeira parte da tela de diagnósticos
            TelaInfoIniciais(nome, sexo, idade, altura, peso);
            // Chama a função que exibe a segunda parte da tela de diagnósticos
            DiagnosticoImc(imc, peso, altura);

            Espacos("_+");
        }

        public static void Sair()
        {
            Espacos(".*");
            Console.WriteLine("Opção selecionada: Sair");
            Console.WriteLine("Até mais!");
            Environment.Exit(0);
        }
        public static void Espacos(string simbolo)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            for (int i = 0; i < 30; i++)
            {
                Console.Write(simbolo);
            }
            Console.ForegroundColor = color;
            Console.WriteLine("\n");
        }
    }
}
