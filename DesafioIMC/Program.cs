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
            Espacos("_+");

            Console.WriteLine("\t\tCálculo de IMC\n");
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
            for (int i = 0; i < 25; i++)
            {
                Console.Write(simbolo);
            }
            Console.ForegroundColor = color;
            Console.WriteLine("\n");
        }
    }
}
