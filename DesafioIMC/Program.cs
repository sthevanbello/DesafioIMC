﻿using System;
using System.Globalization;

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

            Console.WriteLine("\tCálculo de IMC para diagnóstico prévio\n");

            Console.Write("Insira o nome do paciente: ");
            string nome = Console.ReadLine();

            Console.Write("Insira o sexo do paciente (Masculino / Feminino): ");
            string sexo = Console.ReadLine();

            // recebimento da idade e verificação se é um valor inteiro válido
            int idade = 0;
            bool idadeValida = false;
            while (!idadeValida)
            {
                Console.Write("Insira a idade completa do paciente, sem casas decimais: ");
                idadeValida = int.TryParse(Console.ReadLine(), out idade);
            }

            Console.Write("Insira a sua altura: ");
            double altura = Convert.ToDouble(Console.ReadLine());

            Console.Write("Insira o peso do paciente: ");
            double peso = Convert.ToDouble(Console.ReadLine());

            double imc = CalculaImc(peso, altura);

            // Chama a função para exibir a primeira parte da tela de diagnósticos
            TelaInfoIniciais(nome, sexo, idade, altura, peso);
            // Chama a função que exibe a segunda parte da tela de diagnósticos
            DiagnosticoImc(imc, peso, altura);

            Espacos("_+");
        }
        private static void TelaInfoIniciais(string nome, string sexo, int idade, double altura, double peso)
        {
            string categoria = IdentificaCategoria(idade);
            Console.Clear();
            Espacos("_+");

            Console.WriteLine("\t\tCálculo de IMC\n");
            Espacos("=-");
            Console.WriteLine("DIAGNÓSTICO PRÉVIO\n");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Sexo: {sexo}");
            Console.WriteLine($"Idade: {idade}");
            Console.WriteLine($"Altura: {altura.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}");
            Console.WriteLine($"Peso: {peso.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}");
            Console.WriteLine($"Categoria: {categoria}");
        }

        private static string IdentificaCategoria(int idade)
        {
            if (idade > 65)
            {
                return "Idoso";
            }
            else if (idade >= 21 && idade <= 65)
            {
                return "Adulto";
            }
            else if (idade >= 12 && idade <= 20)
            {
                return "Juvenil";
            }
            else
            {
                return "Infantil";
            }
        }

        private static double CalculaImc(double peso, double altura)
        {
            return peso / (altura * altura);
        }

        public static void DiagnosticoImc(double imc, double peso, double altura)
        {
            string imcDesejavel = "Entre 20 e 24";

            // Chama a função que Calcula os riscos e retorna uma string com o risco de acordo com o IMC passado como parâmetro da função.
            string riscos = CalculaRiscos(imc);

            // Chama a função que calcula a recomendação e retorna uma string com a recomendação de acordo com o IMC passado como parâmetro da função.
            string recomendacoesIniciais = CalculaRecomendacao(imc);

            Console.WriteLine($"\nIMC Desejável: {imcDesejavel}");
            Console.WriteLine($"Resultado IMC: {imc.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}");
            Console.WriteLine($"Riscos: {riscos}");
            Console.WriteLine($"Recomendação inicial: {recomendacoesIniciais}");
        }

        private static string CalculaRecomendacao(double imc)
        {
            if (imc < 20)
            {
                return "Muitas complicações de saúde como doenças pulmonares e cardiovasculares podem estar associadas ao baixo peso.";
            }
            else if (imc >= 20 && imc < 25)
            {
                return "Seu peso está ideal para suas referências.";
            }
            else if (imc >= 25 && imc < 30)
            {
                return "Aumento de peso apresenta risco moderado para outras doenças crônicas e cardiovasculares.";
            }
            else if (imc >= 30 && imc < 35)
            {
                return "Quem tem obesidade vai estar mais exposto a doenças graves e ao risco de mortalidade.";
            }
            else
            {
                return "O obeso mórbido vive menos, tem alto risco de mortalidade geral por diversas causas.";
            }
        }

        private static string CalculaRiscos(double imc)
        {
            if (imc < 20)
            {
                return "Inclua carboidratos simples em sua dieta, além de proteínas indispensáveis para ganho de massa magra. Procure um profissional.";
            }
            else if (imc >= 20 && imc < 25)
            {
                return "Mantenha uma dieta saudável e faça seus exames periódicos.";
            }
            else if (imc >= 25 && imc < 30)
            {
                return "Adote um tratamento baseado em dieta balanceada, exercício físico e medicação. A ajuda de um profissional pode ser interessante";
            }
            else if (imc >= 30 && imc < 35)
            {
                return "Adote uma dieta alimentar rigorosa, com o acompanhamento de um nutricionista e um médico especialista(endócrino).";
            }
            else
            {
                return "Procure com urgência o acompanhamento de um nutricionista para realizar reeducação alimentar, um psicólogo e um médicoespecialista(endócrino).";
            }
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
