using System;
using System.Globalization;

namespace DesafioIMC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Chamada da função da Tela Inicial para iniciar a inserção de dados 
            TelaInicial();
        }

        /// <summary>
        /// Exibe a tela inicial para inserir os dados do paciente
        /// </summary>
        static void TelaInicial()
        {
            var color = Console.ForegroundColor;
            // Limpa a tela
            Console.Clear();
            // Função Espacos serve para gerar uma linha divisória
            Espacos("_+");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Cálculo de IMC Para Diagnóstico Prévio");
            Console.ForegroundColor = color;
            Espacos("=-");

            // Chamada da função que retorna os valores inseridos pelo usuário (nome, sexo, idade, altura e peso)
            var dadosDiagnostico = EntradaDeDados();

            //Preenchimento de variáveis recebidas pelo array de retorno do método de "EntradaDeDados"
            string nome = (string)dadosDiagnostico[0];
            string sexo = (string)dadosDiagnostico[1];
            int idade = (int)dadosDiagnostico[2];
            double altura = (double)dadosDiagnostico[3];
            double peso = (double)dadosDiagnostico[4];

            // Chamada da função para calcular o IMC e retornar o valor do IMC como double, considerando as casas decimais.
            double imc = CalculaImc(peso, altura);

            // Chama a função para exibir a primeira parte da tela de diagnósticos com nome, sexo, idade, altura e peso
            TelaDiagnostico(nome, sexo, idade, altura, peso, imc);

            Espacos("_+");

            // Término do programa ou voltar à tela inicial para inserir novos dados
            TelaDeDecisaoFinal();
        }

        /// <summary>
        /// Entrada dos dados informados pelo usuário
        /// </summary>
        /// <returns>Retorna um Array com os dados informados</returns>
        static object[] EntradaDeDados()
        {
            var color = Console.ForegroundColor;
            // Recebe o nome da pessoa e verifica se é um nome válido, se há apenas espaços ou se só apertou a tecla "Enter"
            string nome = "";
            do
            {
                Console.Write("Insira o nome completo do paciente: ");
                nome = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nome));


            // Recebe o sexo da pessoa e verifica se é um sexo válido
            // Usando o método Do While e conferindo se foi digitado corretamente, como é solicitado.
            string sexo = "";
            bool validaSexo = false;
            do
            {
                Console.Write("Insira o sexo do paciente (Masculino ou Feminino): ");
                sexo = Console.ReadLine();

                // Verifica a entrada convertendo a string para minúsculo e comparando com a string correspondente (masculino ou feminino)
                // Caso não esteja de acordo, exibe uma mensagem e pede para o usuário digitar da maneira correta
                if (sexo.ToLower() == "masculino")
                {
                    sexo = "Masculino";
                    validaSexo = true;
                }
                else if (sexo.ToLower() == "feminino")
                {
                    sexo = "Masculino";
                    validaSexo = true;
                }
                else
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInsira apenas Masculino ou Feminino");
                    Console.ForegroundColor = color;
                }
            } while (!validaSexo);

            // recebimento da idade e verificação se é um valor inteiro válido (positivo e sem casas decimais) 
            // Usando o método While
            int idade = 0;
            bool validaIdade = false;
            while (!validaIdade)
            {
                Console.Write("Insira a idade completa do paciente, sem casas decimais: ");
                validaIdade = int.TryParse(Console.ReadLine(), out idade);

                // Verifica se a idade inserida é válida. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                if (!validaIdade || idade <= 0)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInforme uma idade válida. Maior ou igual a zero!");
                    Console.ForegroundColor = color;
                    validaIdade = false;
                }
            }

            // Recebimento do valor da altura em tipo Double para considerar as casas decimais.
            // Faz a validação do dado recebido e força a cultura "pt-br" para trabalhar tanto com vírgula, quanto com ponto.
            // Faz a validação também se foi digitado algo que não seja um número
            double altura = 0;
            bool validaAltura = false;
            do
            {
                Console.Write("Insira a altura do paciente: ");
                validaAltura = double.TryParse(Console.ReadLine().Replace(",", ".").ToString(CultureInfo.GetCultureInfo("pt-br")), out altura);

                // Verifica se a altura inserida é válida. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                if (!validaAltura || altura <= 0)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInforme uma altura válida e com valor maior ou igual a zero!");
                    Console.ForegroundColor = color;
                    validaAltura = false;
                }
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

                // Verifica se o peso inserido é válido. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                if (!validaPeso || peso < 0)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInforme um peso válido e com valor maior ou igual a zero!");
                    Console.ForegroundColor = color;
                    validaPeso = false;
                }
            } while (!validaPeso);

            // Retorna um Array com todas as variáveis recebidas pelo usuário
            return new object[] { nome, sexo, idade, altura, peso };
        }

        /// <summary>
        /// Função que exibe a tela de Diagnóstico Prévio com base no imc calculado
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sexo"></param>
        /// <param name="idade"></param>
        /// <param name="altura"></param>
        /// <param name="peso"></param>
        /// <param name="imc"></param>
        static void TelaDiagnostico(string nome, string sexo, int idade, double altura, double peso, double imc)
        {
            // Chama a função IdentificaCategoria passando a idade da pessoa e retornando uma string de acordo com a faixa etária.
            string categoria = IdentificaCategoria(idade);

            // Chama a função que Calcula os riscos e retorna uma string com o risco de acordo com o IMC passado como parâmetro da função.
            string riscos = CalculaRiscos(imc);

            // Chama a função que calcula a recomendação e retorna uma string com a recomendação de acordo com o IMC passado como parâmetro da função.
            string recomendacoesIniciais = CalculaRecomendacao(imc);

            string imcDesejavel = "Entre 20 e 24";

            // Limpa o console
            Console.Clear();

            var color = Console.ForegroundColor;
            // Monta a tela de Diagnóstico Prévio com os dados inseridos pelo usuário e com os cálculos de acordo com o IMC
            // Função Espacos serve para gerar uma linha divisória
            Espacos("_+");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Cálculo de IMC Para Diagnóstico Prévio");
            Espacos("=-");
            Console.WriteLine("DIAGNÓSTICO PRÉVIO\n");
            Console.ForegroundColor = color;
            Console.WriteLine($"Nome: \t\t{nome}");
            Console.WriteLine($"Sexo: \t\t{sexo}");
            Console.WriteLine($"Idade: \t\t{idade} anos");
            Console.WriteLine($"Altura: \t{altura.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}m");
            Console.WriteLine($"Peso: \t\t{peso.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}Kg");
            Console.WriteLine($"Categoria: \t{categoria}");
            Console.WriteLine($"\nIMC Desejável: \t{imcDesejavel}");
            Console.WriteLine($"Resultado IMC: \t{imc.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}");
            Console.WriteLine($"Riscos: {riscos}");
            Console.WriteLine($"Recomendação inicial: {recomendacoesIniciais}");
        }
        
        /// <summary>
        /// Recebe o parâmetro "idade" e faz a conferência para retornar a categoria em que a pessoa se encontra.
        /// </summary>
        /// <param name="idade"></param>
        /// <returns>Retorna a categoria de acordo com a idade do paciente</returns>
        static string IdentificaCategoria(int idade)
        {
            // Idade acima de 65 anos retorna "Idoso" para a categoria
            if (idade > 65)
            {
                return "Idoso";
            }
            // Idade entre 21 anos e 65 anos retorna "Adulto" para a categoria
            else if (idade >= 21 && idade <= 65)
            {
                return "Adulto";
            }
            // Idade entre 12 anos e 20 anos retorna "Juvenil" para a categoria
            else if (idade >= 12 && idade <= 20)
            {
                return "Juvenil";
            }
            // Idade abaixo de 12 anos retorna "Infantil" para a categoria
            else
            {
                return "Infantil";
            }
        }

        /// <summary>
        /// Função que retorna o imc calculado com base no peso e na altura  -> imc = peso/(altura * altura) ou imc = peso/(Math.Pow(altura, 2))
        /// </summary>
        /// <param name="peso"></param>
        /// <param name="altura"></param>
        /// <returns>Retorna um double com o imc de acordo com o cálculo de peso / (altura * altura) ou imc = peso/(Math.Pow(altura, 2)</returns>
        static double CalculaImc(double peso, double altura)
        {
            // Poderia ser utilizada a função Math.Pow(base, expoente)
            //return peso / (Math.Pow(altura, 2)); 
            return peso / (altura * altura);
        }

        /// <summary>
        /// Função que calcula qual faixa de recomendação a pessoa se enquadra a partir do imc recebido como argumento
        /// </summary>
        /// <param name="imc"></param>
        /// <returns>Retorna uma string com a Recomendação de acordo com o imc fornecido</returns>
        static string CalculaRecomendacao(double imc)
        {
            // Verifica em qual recomendação se enquadra o imc recebido como argumento

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

        /// <summary>
        /// Função que calcula em qual risco a pessoa se enquadra a partir do imc recebido como argumento
        /// </summary>
        /// <param name="imc"></param>
        /// <returns>Retorna uma string com o Risco de acordo com o imc fornecido</returns>
        static string CalculaRiscos(double imc)
        {
            // Verifica em qual Risco se enquadra o imc recebido como argumento
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
        
        /// <summary>
        /// Função para escolher se quer voltar à tela inicial ou se quer sair do programa
        /// </summary>
        static void TelaDeDecisaoFinal()
        {
            bool fim = false;
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Insira a opção desejada\n");
            Console.ForegroundColor = color;
            Console.WriteLine("Opção 1 - Voltar à tela inicial");
            Console.WriteLine("Opção 2 - Sair do programa");
            Espacos(".*");
            while (!fim)
            {
                Console.Write("Opção: ");
                int escolha = 0;
                bool digitou = int.TryParse(Console.ReadLine(), out escolha);

                if (digitou)
                {
                    if (escolha == 1)
                    {
                        TelaInicial();
                    }
                    else if (escolha == 2)
                    {
                        Sair();
                    }
                    else
                    {
                        color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDigite uma opção válida");
                        Console.ForegroundColor = color;
                    }
                }
                else
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDigite uma opção válida");
                    Console.ForegroundColor = color;
                }

            }
        }
        
        /// <summary>
        /// Função para sair do programa
        /// </summary>
        static void Sair()
        {
            Espacos(".*");
            Console.WriteLine("Opção selecionada: Sair");
            Console.WriteLine("Até mais!");
            Environment.Exit(0);
        }

        /// <summary>
        /// Cria uma linha de separação a partir de um símbolo em formato string
        /// </summary>
        /// <param name="simbolo"></param>
        static void Espacos(string simbolo)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            for (int i = 0; i < Console.WindowWidth/simbolo.Length; i++)
            {
                Console.Write(simbolo);
            }
            Console.ForegroundColor = color;
            Console.WriteLine("\n");
        }
    }
}
