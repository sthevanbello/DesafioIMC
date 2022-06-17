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
            // Chama a função para imprimir o cabeçalho padrão das telas
            Cabecalho();

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

            // Chama a função para exibir a tela de diagnósticos
            TelaDiagnostico(nome, sexo, idade, altura, peso, imc);

            Espacos("_+");

            // Término do programa ou voltar à tela inicial para inserir novos dados
            TelaDeDecisaoFinal();
        }

        /// <summary>
        /// Entrada dos dados informados pelo usuário e monstagem da tela de triagem dinamicamente
        /// </summary>
        /// <returns>Retorna um Array com os dados informados</returns>
        static object[] EntradaDeDados()
        {
            //Monta o cabeçalho inicial da tela
            Cabecalho();
            // Monta a tela inicial dinamicamente
            DadosIniciaisPaciente();
            Espacos("__");
            // Recebe a cor atual das letras do console
            var color = Console.ForegroundColor;

            string nome = "";
            do
            {   // Recebe o nome da pessoa e verifica se é um nome válido, se há apenas espaços ou se só apertou a tecla "Enter".
                Console.Write("Insira o nome completo do paciente: ");
                nome = Console.ReadLine().Trim(); // Função Trim() serve para retirar os espaços em branco para verificar se a entrada de texto está vazia.

                if (nome == "")
                {
                    MensagemErro(); // Exibe uma mensagem de erro se o dado for inválido - Espaços em branco ou se foi só apertada a tecla "Enter".
                }
            } while (string.IsNullOrWhiteSpace(nome)); // Essa função string.IsNullOrWhiteSpace(nome) verifica se foi só apertada a tecla "Enter" ou apenas espaços vazios.

            // Limpa a tela
            Console.Clear();
            Cabecalho();
            DadosIniciaisPaciente(nome);
            Espacos("__");
            // Recebe o sexo da pessoa e verifica se é um sexo válido
            // Usando o método Do While e conferindo se foi digitado corretamente, como é solicitado.
            string sexo = "";
            bool validaSexo = false;
            do
            {
                Console.Write("Insira o sexo do paciente (M para Masculino ou F para Feminino): ");
                sexo = Console.ReadLine();

                // Verifica a entrada convertendo a string para minúsculo e comparando com a string correspondente (masculino ou feminino)
                // Caso não esteja de acordo, exibe uma mensagem e pede para o usuário digitar da maneira correta
                if (sexo.ToLower() == "m")
                {
                    sexo = "Masculino";
                    validaSexo = true;
                }
                else if (sexo.ToLower() == "f")
                {
                    sexo = "Feminino";
                    validaSexo = true;
                }
                else
                {
                    // Recebe a cor atual da fonte do console
                    color = Console.ForegroundColor;
                    // Atribui a cor vermelha à fonte do console
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("\nInsira apenas M para Masculino ou F para Feminino");
                    // Retorna a cor anterior à fonte do console
                    Console.ForegroundColor = color;
                }
            } while (!validaSexo);

            Console.Clear();
            Cabecalho();
            DadosIniciaisPaciente(nome, sexo);
            Espacos("__");
            // recebimento da idade e verificação se é um valor inteiro válido (positivo e sem casas decimais) 
            int idade = 0;
            bool validaIdade = false;
            while (!validaIdade)
            {
                Console.Write("Insira a idade completa do paciente, sem casas decimais: ");
                validaIdade = int.TryParse(Console.ReadLine(), out idade);

                // Verifica se a idade inserida é válida. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                // Chama a função ValidaDados passando o resultado do TryParse e a idade inserida. 
                // Retorna true para a variável se o valor inserido for válido
                validaIdade = ValidaDados(validaIdade, idade, "idade");
            }

            Console.Clear();
            Cabecalho();
            DadosIniciaisPaciente(nome, sexo, idade);
            Espacos("__");
            // Recebimento do valor da altura em tipo Double para considerar as casas decimais.
            double altura = 0;
            bool validaAltura = false;
            do
            {
                Console.Write("Insira a altura do paciente em Metros - 1,65 - por exemplo: ");
                // Chama a função de conversão do valor inserido pelo usuário para Double com ponto ou com vírgula e retorna um double para a variável altura
                altura = ConversaoDouble(Console.ReadLine());

                // Verifica se a altura inserida é válida. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                // Chama a função ValidaDados passando a comparação da altura com o zero (garantir que não seja valor zero) e a altura inserida. 
                // Retorna true para a variável se o valor inserido for válido

                validaAltura = ValidaDados(altura != 0, altura, "altura");

            } while (!validaAltura);

            Console.Clear();
            Cabecalho();
            DadosIniciaisPaciente(nome, sexo, idade, altura);
            Espacos("__");
            // Recebimento do peso em double para considerar as casas decimais.
            double peso = 0;
            bool validaPeso = false;
            do
            {
                Console.Write("Insira o peso do paciente em Quilos - 65,5 - por exemplo: ");
                // Chama a função de conversão do valor inserido pelo usuário para Double com ponto ou com vírgula e retorna um double para a variável peso
                peso = ConversaoDouble(Console.ReadLine());

                // Verifica se o peso inserido é válido. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                // Chama a função ValidaDados passando a comparação do peso com o zero (garantir que não seja valor zero) e o peso inserido. 
                // Retorna true para a variável se o valor inserido for válido

                validaPeso = ValidaDados(peso != 0, peso, "peso");

            } while (!validaPeso);

            // Retorna um Array com todas as variáveis recebidas pelo usuário
            // (Foi utilizado um array de object para poder retornar todos os dados sem a necessidade de conversão)
            return new object[] { nome, sexo, idade, altura, peso };
        }

        /// <summary>
        /// Valida o dado recebido de acordo com a entrada digitada pelo usuário
        /// </summary>
        /// <param name="validaDado"></param>
        /// <param name="dado"></param>
        /// <returns>Retorna true ou false de acordo com o valor recebido</returns>
        static bool ValidaDados(bool validaDado, double dado, string tipo)
        {
            // Recebe a cor atual da fonte do console
            var color = Console.ForegroundColor;
            string mensagem = "";

            if (tipo == "idade")
            {
                if (!validaDado || dado < 0 || dado >= 120)
                {
                    mensagem = $"\nInforme uma {tipo} válida apenas com números e com valor positivo Informe um valor entre 0 e 120 anos";
                    validaDado = false;
                }
               
            }
            if (tipo == "altura")
            {
                if (!validaDado || dado == 0 || dado <= 0.3 || dado >= 2.6)
                {
                    mensagem = $"\nInforme uma {tipo} válida apenas com números e com valor positivo - Informe um valor entre 0,3m e 2,6m";
                    validaDado = false;
                }
            }
            if (tipo == "peso")
            {
                if (!validaDado || dado <= 1 || dado >= 250)
                {
                    mensagem = $"\nInforme um {tipo} válido apenas com números e entre 1kg e 250kg";
                    validaDado = false;
                }
            }
            // Atribui a cor vermelha à fonte do console
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(mensagem);

            // Retorna a cor anterior à fonte do console
            Console.ForegroundColor = color;
            return validaDado;
        }

        /// <summary>
        /// Converte a string valor para double usando separador de casas decimais com ponto ou com vírgula
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retorna um valor string convertido para double</returns>
        static double ConversaoDouble(string valor)
        {
            // Faz a conversão para double da string valor recebida como argumento  da função
            // Verifica se é utilizada a vírgula ou o ponto
            // valor.Replace(",", ".") Faz a troca da vírgula por ponto, para não haver erro na conversão
            // NumberStyles.Number determina que o estilo permitido é número

            // CultureInfo.InvariantCulture considera apenas o idioma padrão do Windows (inglês) e não o idioma do Windows instalado
            double.TryParse(valor.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out double valorDouble);
            return valorDouble;
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

            // Chama a função que Calcula os riscos e retorna uma string com o risco de acordo com o IMC passado como argumento da função.
            string riscos = CalculaRiscos(imc);

            // Chama a função que calcula a recomendação e retorna uma string com a recomendação de acordo com o IMC passado como argumento da função.
            string recomendacoesIniciais = CalculaRecomendacoes(imc);

            string imcDesejavel = "Entre 20 e 24";

            // Limpa o console
            Console.Clear();

            // Recebe a cor atual da fonte do console
            var color = Console.ForegroundColor;
            var colorCampos = ConsoleColor.Yellow;

            // Monta a tela de Diagnóstico Prévio com os dados inseridos pelo usuário e com os cálculos de acordo com o IMC
            Cabecalho();
            DadosIniciaisPaciente(nome, sexo, idade, altura, peso);
            Espacos("__");
            // Chama a função para perguntar se os dados inseridos na triagem estão corretos
            VerificaDadosCorretos();

            Cabecalho();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("DIAGNÓSTICO PRÉVIO\n");
            // Retorna a cor anterior à fonte do console
            Console.ForegroundColor = color;
            DadosIniciaisPaciente(nome, sexo, idade, altura, peso, preenchido: true);

            Console.Write($"Categoria: \t");
            Console.ForegroundColor = colorCampos;
            Console.Write($"{categoria}\n");
            Console.ResetColor();

            Console.Write($"\nIMC Desejável: \t");
            Console.ForegroundColor = colorCampos;
            Console.Write($"{imcDesejavel}\n");
            Console.ResetColor();

            Console.Write($"\nResultado IMC: \t");
            Console.ForegroundColor = colorCampos;
            Console.Write($"{imc.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}\n");
            Console.ResetColor();

            Console.WriteLine($"\nRiscos: {riscos}");
            Console.WriteLine($"\nRecomendação inicial: {recomendacoesIniciais}");
        }

        /// <summary>
        /// Função de confirmação dos dados digitados na triagem. Dados corretos? S para Sim e N para Não
        /// </summary>
        static void VerificaDadosCorretos()
        {
            bool dadosCorretos = true;
            do
            {
                Console.WriteLine("Os dados digitados estão todos corretos?\n");
                Console.Write($"Digite ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"S para Sim");
                Console.ResetColor();
                Console.Write(" ou ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("N para Não");
                Console.ResetColor();
                Console.Write(": ");
                string dados = Console.ReadLine().ToLower();
                if (dados == "s")
                {
                    dadosCorretos = false;
                }
                else if (dados == "n")
                {
                    TelaInicial();
                }
                else
                {
                    MensagemErro();
                }
            } while (dadosCorretos);
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
        /// Função que calcula em qual risco a pessoa se enquadra a partir do imc recebido como parâmetro
        /// </summary>
        /// <param name="imc"></param>
        /// <returns>Retorna uma string com o Risco de acordo com o imc fornecido</returns>
        static string CalculaRecomendacoes(double imc)
        {
            // Verifica em qual Risco se enquadra o imc recebido como parâmetro
            if (imc < 20)
            {
                return "Inclua carboidratos simples em sua dieta, além de proteínas indispensáveis para ganho de \nmassa magra. Procure um profissional.";
            }
            else if (imc >= 20 && imc < 25)
            {
                return "Mantenha uma dieta saudável e faça seus exames periódicos.";
            }
            else if (imc >= 25 && imc < 30)
            {
                return "Adote um tratamento baseado em dieta balanceada, exercício físico e medicação. \nA ajuda de um profissional pode ser interessante";
            }
            else if (imc >= 30 && imc < 35)
            {
                return "Adote uma dieta alimentar rigorosa, com o acompanhamento de um nutricionista \ne de um médico especialista (endócrino).";
            }
            else
            {
                return "Procure com urgência o acompanhamento de um nutricionista para realizar reeducação \nalimentar, um psicólogo e um médico especialista (endócrino).";
            }
        }

        /// <summary>
        /// Função que calcula qual faixa de recomendação a pessoa se enquadra a partir do imc recebido como parâmetro
        /// </summary>
        /// <param name="imc"></param>
        /// <returns>Retorna uma string com a Recomendação de acordo com o imc fornecido</returns>
        static string CalculaRiscos(double imc)
        {
            // Verifica em qual recomendação se enquadra o imc recebido como argumento

            if (imc < 20)
            {
                return "Muitas complicações de saúde como doenças pulmonares e cardiovasculares \npodem estar associadas ao baixo peso.";
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
        /// Monta o cabeçalho padrão das telas
        /// </summary>
        static void Cabecalho()
        {
            // Recebe a cor atual da fonte do console
            var color = Console.ForegroundColor;
            // Limpa a tela
            Console.Clear();
            // Função Espacos serve para gerar uma linha divisória
            Espacos("_+");
            // Atribui a cor azul à fonte do console
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Cálculo de IMC Para Diagnóstico Prévio".PadLeft(75));
            // Retorna a cor anterior à fonte do console
            Console.ForegroundColor = color;
            Espacos("=-");
        }

        /// <summary>
        /// Monta a tela inicial dinamicamente a cada inserção dos dados do paciente.
        /// Caso não haja dado inserido, monta a tela apenas com os títulos dos dados.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sexo"></param>
        /// <param name="idade"></param>
        /// <param name="altura"></param>
        /// <param name="peso"></param>
        static void DadosIniciaisPaciente(string nome = "", string sexo = "", int idade = -1, double altura = 0D, double peso = 0D, bool preenchido = false)
        {
            var color = Console.ForegroundColor;
            if (!preenchido)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("TRIAGEM DO PACIENTE\n");
            }

            // Preenche dinamicamente a tela conforme os valores forem digitados pelo usuário
            Console.ForegroundColor = color;
            CamposTexto("Nome", nome);
            CamposTexto("Sexo", sexo);
            CamposNumeroInt("Idade", idade);
            CamposNumeroDouble("Altura", altura);
            CamposNumeroDouble("Peso", peso);
        }

        /// <summary>
        /// Preenche dinamicamente os campos de texto na tela inicial de triagem do paciente
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="dado"></param>
        static void CamposTexto(string tipo, string dado)
        {
            var color = Console.ForegroundColor;
            var colorCampos = ConsoleColor.Yellow;
            string dadoApresentado;
            // Preenche o título do dado
            Console.Write($"{tipo}:\t\t");
            Console.ForegroundColor = colorCampos;

            // Preenche o dado após apertar o ENTER caso esteja correto (Não seja apenas espaço em branco ou apenas apertar ENTER) 
            if (!string.IsNullOrWhiteSpace(dado))
                dadoApresentado = dado;
            else
                dadoApresentado = "";
            Console.Write($"{dadoApresentado}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Preenche dinamicamente os dados do tipo Double na tela de triagem do paciente
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="dado"></param>
        static void CamposNumeroDouble(string tipo, double dado)
        {
            var color = Console.ForegroundColor;
            var colorCampos = ConsoleColor.Yellow;
            string dadoApresentado = "";
            string unidade = "";
            Console.Write($"{tipo}:\t\t");
            Console.ForegroundColor = colorCampos;
            if (tipo == "Altura")
                unidade = "m";
            if (tipo == "Peso")
                unidade = "Kg";
            if (dado > 0)
                dadoApresentado = $"{dado.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}{unidade}";
            else
                dadoApresentado = "";
            Console.Write($"{dadoApresentado}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Preenche dinamicamente a idade na tela inicial de triagem do paciente
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="dado"></param>
        static void CamposNumeroInt(string tipo, int dado)
        {
            var color = Console.ForegroundColor;
            var colorCampos = ConsoleColor.Yellow;
            string dadoApresentado;
            Console.Write($"{tipo}:\t\t");
            Console.ForegroundColor = colorCampos;
            if (dado >= 0)
                dadoApresentado = $"{dado} anos";
            else
                dadoApresentado = "";
            Console.Write($"{dadoApresentado}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Função para escolher se quer voltar à tela inicial ou se quer sair do programa
        /// </summary>
        static void TelaDeDecisaoFinal()
        {
            Console.WriteLine();
            var color = Console.ForegroundColor;
            bool fim = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Insira a opção desejada\n");
            Console.ResetColor();
            Console.Write("Digite ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1 ");
            Console.ResetColor();
            Console.Write("- Para voltar à tela inicial\n");
            Console.Write("Digite ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("2 ");
            Console.ResetColor();
            Console.Write("- Para sair do programa\n");

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
        /// Exibe uma mensagem de erro com destaque na cor da fonte
        /// </summary>
        static void MensagemErro()
        {
            // Recebe a cor atual da fonte do console
            var color = Console.ForegroundColor;

            // Atribui a cor vermelha à fonte do console
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\nInforme um dado válido e não aperte apenas ENTER");

            // Retorna a cor anterior à fonte do console
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Cria uma linha de separação a partir de um símbolo em formato string
        /// </summary>
        /// <param name="simbolo"></param>
        static void Espacos(string simbolo)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Console.WindowWidth / simbolo.Length; i++)
            {
                Console.Write(simbolo);
            }
            Console.ForegroundColor = color;
            Console.WriteLine();
        }

        /// <summary>
        /// Função para sair do programa
        /// </summary>
        static void Sair()
        {
            Espacos(".*");
            Console.WriteLine("\nOpção selecionada: Sair");
            Console.WriteLine("Até mais!");
            Environment.Exit(0);
        }
    }
}
