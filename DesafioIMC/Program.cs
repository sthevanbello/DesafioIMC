using System;
using System.Globalization;

namespace DesafioIMC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaInicial();
        }
        static void TelaInicial()
        {
            // Função Espacos serve para gerar uma linha divisória
            Espacos("_+");
            Console.WriteLine("\tCálculo de IMC para diagnóstico prévio");
            Espacos("=-");

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

            // Recebimento da altura em Double para considerar as casas decimais.
            // Faz a validação do dado recebido e força a cultura "pt-br" para trabalhar tanto com vírgula, quanto com ponto.
            // Faz a validação também se foi digitado algo que não seja um número
            double altura = 0;
            bool validaAltura = false;
            do
            {
                Console.Write("Insira a sua altura: ");
                validaAltura = double.TryParse(Console.ReadLine().Replace(",", ".").ToString(CultureInfo.GetCultureInfo("pt-br")), out altura);

            } while (!validaAltura);


            // Recebimento do peso em double para considerar as casas decimais.
            // Faz a validação do dado recebido e força a cultura "pt-br" para trabalhar tanto com vírgula, quanto com ponto.
            // Faz a validação também se foi digitado algo que não seja um número 
            double peso = 0;
            bool validaPeso = false;
            do
            {
                Console.Write("Insira o peso do paciente: ");
                validaPeso = double.TryParse(Console.ReadLine().Replace(",", ".").ToString(CultureInfo.GetCultureInfo("pt-br")), out peso);
            } while (!validaPeso);

            // Chamada da função para calcular o IMC e retornar o valor do IMC como double, considerando as casas decimais.
            double imc = CalculaImc(peso, altura);

            // Chama a função para exibir a primeira parte da tela de diagnósticos com nome, sexo, idade, altura e peso
            TelaIniciallDiagnostico(nome, sexo, idade, altura, peso);

            // Chama a função que exibe a segunda parte da tela de diagnósticos com imc desejável, resultado, riscos e recomendação inicial
            TelaFinalDiagnostico(imc, peso, altura);

            Espacos("_+");
        }

        // Função que exibe a segunda parte da tela de diagnósticos com imc desejável, resultado, riscos e recomendação inicial
        static void TelaIniciallDiagnostico(string nome, string sexo, int idade, double altura, double peso)
        {
            // Chama a função IdentificaCategoria passando a idade da pessoa e retornando uma string de acordo com a faixa etária.
            string categoria = IdentificaCategoria(idade);

            // Limpa o console
            Console.Clear();

            Espacos("_+");

            Console.WriteLine("\t\tCálculo de IMC");
            Espacos("=-");
            Console.WriteLine("DIAGNÓSTICO PRÉVIO\n");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Sexo: {sexo}");
            Console.WriteLine($"Idade: {idade}");
            Console.WriteLine($"Altura: {altura.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}");
            Console.WriteLine($"Peso: {peso.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}");
            Console.WriteLine($"Categoria: {categoria}");
        }

        // Função que apresenta a segunda parte da tela de diagnóstico prévio
        static void TelaFinalDiagnostico(double imc, double peso, double altura)
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

        // Recebe o parâmetro "idade" e faz a conferência para retornar a categoria em que a pessoa se encontra.
        static string IdentificaCategoria(int idade)
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

        // Função que retorna o imc calculado com base no peso e na altura - imc = peso/(altura * altura) ou imc = peso/(Math.Pow(altura, 2))
        static double CalculaImc(double peso, double altura)
        {
            // Poderia ser utilizada a função Math.Pow(base, expoente)
            //return peso / (Math.Pow(altura, 2)); 
            return peso / (altura * altura);
        }

        // Função que calcula qual faixa de recomendação a pessoa se enquadra a partir do imc recebido como argumento
        static string CalculaRecomendacao(double imc)
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

        // Função que calcula em qual risco a pessoa se enquadra a partir do imc recebido como argumento
        static string CalculaRiscos(double imc)
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

        static void Sair()
        {
            Espacos(".*");
            Console.WriteLine("Opção selecionada: Sair");
            Console.WriteLine("Até mais!");
            Environment.Exit(0);
        }
        // Cria uma linha de separação a partir de um símbolo em formato string
        static void Espacos(string simbolo)
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
