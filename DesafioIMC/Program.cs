using System;
using System.Globalization;
using System.Threading;

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
            // Este trecho é utilizado em mais pontos do código e serve para apresentar dinamicamente a tela inicial de triagem do paciente
            //Monta o cabeçalho inicial da tela
            Cabecalho();
            // Monta a tela inicial dinamicamente
            DadosIniciaisPaciente();
            // Cria uma linha divisória na tela de acordo com o caractere passado como parâmetro da função
            DivisoriaHorizontal("__");

            //Preenchimento de variáveis recebidas pelo array de retorno do método de "EntradaDeDados"
            string nome = RecebeNome();
            string sexo = RecebeSexo(nome);
            int idade = RecebeIdade(nome, sexo);
            double altura = RecebeAltura(nome, sexo, idade);
            double peso = RecebePeso(nome, sexo, idade, altura);

            // Chamada da função para calcular o IMC e retornar o valor do IMC como double, considerando as casas decimais.
            double imc = CalculaImc(peso, altura);

            // Chama a função para exibir a tela de diagnósticos prévio
            TelaDiagnostico(nome, sexo, idade, altura, peso, imc);

            // Cria uma linha divisória na tela de acordo com o caractere passado como parâmetro da função
            DivisoriaHorizontal("_+");

            // Término do programa ou voltar à tela inicial para inserir novos dados
            TelaDeDecisaoFinal();
        }

        /// <summary>
        /// <para>Função recebe os argumentos para montar dinamicamente a tela de triagem.</para>
        /// <para>Recebe o nome do paciente e faz a verificação se o dado está correto e se o usuário confirma o dado digitado</para>
        /// </summary>
        /// <returns>Retorna a <b>string</b> com o nome do paciente</returns>
        static string RecebeNome()
        {
            // Recebe o nome da pessoa e verifica se é um nome válido, se há apenas espaços ou se só apertou a tecla "Enter".
            string nome = "";
            bool dadoCorreto = false;
            do
            {
                //Monta o cabeçalho inicial da tela
                Cabecalho();
                // Monta a tela inicial dinamicamente
                DadosIniciaisPaciente();
                // Cria uma linha divisória na tela de acordo com o caractere passado como parâmetro da função
                DivisoriaHorizontal("__");
                do
                {   
                    Console.Write("Insira o nome completo do paciente: ");
                    nome = Console.ReadLine().Trim(); // Função Trim() serve para retirar os espaços em branco para verificar se a entrada de texto está vazia.

                    if (nome == "")
                    {
                        MensagemErro(); // Exibe uma mensagem de erro se o dado for inválido, se há somente espaços em branco ou se foi só apertada a tecla "Enter".
                    }
                } while (string.IsNullOrWhiteSpace(nome)); // Essa função string.IsNullOrWhiteSpace(nome) verifica se foi só apertada a tecla "Enter" ou apenas espaços vazios.
                dadoCorreto = VerificaDadoIndividual("nome"); // Verifica individualmente se o dado está de acordo com o desejado
            } while (dadoCorreto);
            return nome;
        }

        /// <summary>
        /// <para>Função recebe os argumentos para montar dinamicamente a tela de triagem.</para>
        /// <para>Recebe o sexo do paciente e faz a verificação se o dado está correto e se o usuário confirma o dado digitado</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Retorna uma <b>string</b> com o sexo do paciente</returns>
        static string RecebeSexo(string nome)
        {
            bool dadoCorreto = false;
            string sexo = "";
            do
            {
                Cabecalho();
                DadosIniciaisPaciente(nome);
                DivisoriaHorizontal("__");
                // Recebe o sexo da pessoa e verifica se é um sexo válido.
                // Usando o método Do While e conferindo se foi digitado corretamente, como é solicitado.

                bool validaSexo = false;
                do
                {
                    Console.Write("Insira o sexo do paciente (M para Masculino ou F para Feminino): ");
                    sexo = Console.ReadLine().ToLower();

                    // Verifica a entrada convertendo a string para minúsculo e comparando com a string correspondente (masculino ou feminino).
                    // Caso não esteja de acordo, exibe uma mensagem e pede para o usuário digitar da maneira correta.
                    if (sexo == "m")
                    {
                        sexo = "Masculino";
                        validaSexo = true;
                    }
                    else if (sexo == "f")
                    {
                        sexo = "Feminino";
                        validaSexo = true;
                    }
                    else
                    {
                        // Atribui a cor vermelha à fonte do console
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInsira apenas M para Masculino ou F para Feminino");
                        // Retorna a cor anterior à fonte do console
                        Console.ResetColor();
                    }

                } while (!validaSexo);
                dadoCorreto = VerificaDadoIndividual("sexo"); // Verifica individualmente se o dado está de acordo com o desejado
            } while (dadoCorreto);
            return sexo;
        }

        /// <summary>
        /// <para>Função recebe os argumentos para montar dinamicamente a tela de triagem.</para>
        /// <para>Recebe a idade do paciente e faz a verificação se o dado está correto e se o usuário confirma o dado digitado</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sexo"></param>
        /// <returns>Retorna um <b>int</b> com a idade do paciente</returns>
        private static int RecebeIdade(string nome, string sexo)
        {
            bool dadoCorreto = false;
            int idade = 0;
            do
            {
                Cabecalho();
                DadosIniciaisPaciente(nome, sexo);
                DivisoriaHorizontal("__");
                // recebimento da idade e verificação se é um valor inteiro válido (positivo e sem casas decimais).
                bool validaIdade = false;
                while (!validaIdade)
                {
                    Console.Write("Insira a idade do paciente, sem casas decimais: ");
                    int.TryParse(Console.ReadLine(), out idade);

                    // Verifica se a idade inserida é válida. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado.
                    // Chama a função ValidaDados passando o idade inserida e a string "idade" para determinar o tipo do dado a ser validado. 
                    // Retorna true para a variável "validaIdade" se o valor inserido for válido e verifica no while se o dado é válido.
                    validaIdade = ValidaDados(idade, "idade");
                }
                dadoCorreto = VerificaDadoIndividual("idade"); // Verifica individualmente se o dado está de acordo com o desejado
            } while (dadoCorreto);
            return idade;
        }

        /// <summary>
        /// Função recebe os argumentos para montar dinamicamente a tela de triagem.
        /// <para>Recebe a altura do paciente e faz a verificação se o dado está correto e se o usuário confirma o dado digitado</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sexo"></param>
        /// <param name="idade"></param>
        /// <returns>Retorna um <b>double</b> com a Altura do paciente</returns>
        private static double RecebeAltura(string nome, string sexo, int idade)
        {
            bool dadoCorreto = false;
            double altura = 0;
            do
            {
                Cabecalho();
                DadosIniciaisPaciente(nome, sexo, idade);
                DivisoriaHorizontal("__");
                // Recebimento do valor da altura em tipo Double para considerar as casas decimais.

                bool validaAltura = false;
                do
                {
                    Console.Write("Insira a altura do paciente em Metros - 1,65 - por exemplo: ");
                    // Chama a função de conversão do valor inserido pelo usuário para Double com ponto ou com vírgula e retorna um double para a variável altura
                    altura = ConversaoDouble(Console.ReadLine());

                    // Verifica se a altura inserida é válida. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                    // Chama a função ValidaDados passando a altura inserida e a string "altura" para determinar o tipo do dado a ser validado. 
                    // Retorna true para a variável se o valor inserido for válido e verifica no while se o dado é válido
                    validaAltura = ValidaDados(altura, "altura");

                } while (!validaAltura);
                dadoCorreto = VerificaDadoIndividual("altura"); // Verifica individualmente se o dado está de acordo com o desejado
            } while (dadoCorreto);
            return altura;
        }

        /// <summary>
        /// Função recebe os argumentos para montar dinamicamente a tela de triagem.
        /// <para>Recebe o peso do paciente e faz a verificação se o dado está correto e se o usuário confirma o dado digitado</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sexo"></param>
        /// <param name="idade"></param>
        /// <param name="altura"></param>
        /// <returns>Retorna um <b>double</b> com o peso do paciente</returns>
        private static double RecebePeso(string nome, string sexo, int idade, double altura)
        {
            double peso = 0;
            bool dadoCorreto = false;
            do
            {
                Cabecalho();
                DadosIniciaisPaciente(nome, sexo, idade, altura);
                DivisoriaHorizontal("__");
                // Recebimento do peso em double para considerar as casas decimais.

                bool validaPeso = false;
                do
                {
                    Console.Write("Insira o peso do paciente em Quilos - 65,5 - por exemplo: ");
                    // Chama a função de conversão do valor inserido pelo usuário para Double com ponto ou com vírgula e retorna um double para a variável peso
                    peso = ConversaoDouble(Console.ReadLine());

                    // Verifica se o peso inserido é válido. Não pode receber valor negativo, letra ou apenas apertar "Enter" no teclado
                    // Chama a função ValidaDados passando o peso inserido e a string "peso" para determinar o tipo do dado a ser validado. 
                    // Retorna true para a variável se o valor inserido for válido e verifica no while se o dado é válido
                    validaPeso = ValidaDados(peso, "peso");

                } while (!validaPeso);
                dadoCorreto = VerificaDadoIndividual("peso"); // Verifica individualmente se o dado está de acordo com o desejado
            } while (dadoCorreto);
            return peso;
        }

        /// <summary>
        /// Valida o dado recebido de acordo com a entrada digitada pelo usuário
        /// </summary>
        /// <param name="validaDado"></param>
        /// <param name="dado"></param>
        /// <returns>Retorna true ou false de acordo com o valor recebido</returns>
        static bool ValidaDados(double dado, string tipo)
        {
            // Recebe um dado e seu tipo para fazer as verificações de acordo com o que foi passado como argumento da função.
            // Verifica se é idade, altura ou peso e faz a validação de cada tipo individualmente.
            // Variável "validaDado" retorna como true se o dado recebido for válido.
            bool validaDado = true;
            string mensagem = "";

            if (tipo == "idade")
            {
                if (dado < 1 || dado > 120)
                {
                    mensagem = $"\nInforme uma {tipo} apenas com números e com valor positivo entre 1 e 120 anos";
                    validaDado = false;
                }
            }
            if (tipo == "altura")
            {
                if (dado < 0.3 || dado > 2.6)
                {
                    mensagem = $"\nInforme uma {tipo} apenas com números e com valor positivo entre 0,3m e 2,6m";
                    validaDado = false;
                }
            }
            if (tipo == "peso")
            {
                if (dado < 1 || dado > 250)
                {
                    mensagem = $"\nInforme um {tipo} apenas com números e entre 1kg e 250kg";
                    validaDado = false;
                }
            }
            // Atribui a cor vermelha à fonte do console
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            // Retorna a cor anterior à fonte do console
            Console.ResetColor();
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
            // NumberStyles.Number determina que o estilo permitido é número genérico, não importando se é inteiro ou decimal. 
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

            // Foram usadas duas funções separadas. Uma para Risco e uma para Recomendações para separar a responsabilidade de cada item.

            // Chama a função que Calcula os riscos e retorna uma string com o risco de acordo com o IMC passado como parâmetro da função.
            string riscos = CalculaRiscos(imc);

            // Chama a função que calcula a recomendação e retorna uma string com a recomendação de acordo com o IMC passado como parâmetro da função.
            string recomendacoesIniciais = CalculaRecomendacoes(imc);

            string imcDesejavel = "Entre 20 e 24";

            var colorCampos = ConsoleColor.Yellow;

            // Limpa o console
            Console.Clear();

            // Apresentação da tela de Diagnóstico Prévio com os dados inseridos pelo usuário e com os cálculos de acordo com o IMC

            Cabecalho();
            DadosIniciaisPaciente(nome, sexo, idade, altura, peso);
            DivisoriaHorizontal("__");
            // Chama a função para perguntar se os dados inseridos na triagem estão corretos
            VerificaDadosCorretos();

            Cabecalho();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("DIAGNÓSTICO PRÉVIO\n");
            // Retorna a cor anterior à fonte do console
            Console.ResetColor();
            // Imprime os dados iniciais do paciente
            DadosIniciaisPaciente(nome, sexo, idade, altura, peso, preenchido: true);
            // Imprime a Categoria na tela
            Console.Write($"Categoria: \t");
            Console.ForegroundColor = colorCampos;
            Console.Write($"{categoria}\n");
            Console.ResetColor();
            // Imprime o IMC desejável na tela
            Console.Write($"\nIMC Desejável: \t");
            Console.ForegroundColor = colorCampos;
            Console.Write($"{imcDesejavel}\n");
            Console.ResetColor();
            //Imprime o resultado do IMC na tela
            Console.Write($"\nResultado IMC: \t");
            Console.ForegroundColor = colorCampos;
            Console.Write($"{imc.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}\n"); // Apresenta o dado com duas casas decimais e separado por vírgula
            Console.ResetColor();
            // Imprime o risco e a recomendação inicial na tela
            Console.WriteLine($"\nRiscos: {riscos}");
            Console.WriteLine($"\nRecomendação inicial: {recomendacoesIniciais}");
        }

        /// <summary>
        /// Função de confirmação dos dados digitados na triagem. 
        /// <code>Dados corretos? S para Sim e N para Não</code>
        /// </summary>
        static void VerificaDadosCorretos()
        {
            bool dadosCorretos = true;
            do
            {
                Console.WriteLine("Os dados digitados estão todos corretos?\n");
                Console.Write($"Digite ");
                Console.ForegroundColor = ConsoleColor.Green; // Modifica a coloração para identificar S para Sim em verde
                Console.Write($"S para Sim");
                Console.ResetColor();
                Console.Write(" ou ");
                Console.ForegroundColor = ConsoleColor.Red; // Modifica a coloração para identificar N para Não em vermelho
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
        /// Verifica cada dado individualmente. 
        /// <para>Busca evitar que o usuário digite algo indesejado e tenha que digitar todos os campos para poder corrigir o erro</para>
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns>Retorna uma variável do tipo <b>bool</b> com o resultado da validação></returns>
        static bool VerificaDadoIndividual(string tipo)
        {
            // Personaliza a mensagem de acordo com o que for verificado e troca a letra das palavras já definidas.
            string letra = "o";
            if (tipo == "altura" || tipo == "idade")
            {
                letra = "a";
            }
            bool dadoCorreto = true;
            do
            {
                DivisoriaHorizontal("__");
                Console.WriteLine($"\n{letra.ToUpper()} {tipo} digitad{letra} está corret{letra}?\n");
                Console.Write($"Digite ");
                Console.ForegroundColor = ConsoleColor.Green; // Modifica a coloração para identificar S para Sim em verde
                Console.Write($"S para Sim");
                Console.ResetColor();
                Console.Write(" ou ");
                Console.ForegroundColor = ConsoleColor.Red; // Modifica a coloração para identificar N para Não em vermelho
                Console.Write("N para Não");
                Console.ResetColor();
                Console.Write(": ");
                string dados = Console.ReadLine().ToLower();
                if (dados == "s")
                {
                    dadoCorreto = false;
                }
                else if (dados == "n")
                {
                    dadoCorreto = true;
                    break;
                }
                else
                {
                    MensagemErro();
                }
            } while (dadoCorreto);
            return dadoCorreto;
        }

        /// <summary>
        /// Recebe o parâmetro "idade" e faz a conferência para retornar a categoria em que a pessoa se encontra.
        /// </summary>
        /// <param name="idade"></param>
        /// <returns>Retorna a categoria de acordo com a idade do paciente</returns>
        static string IdentificaCategoria(int idade)
        {
            // Testa a variável idade e retorna a categoria que se enquadra.

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

        /// <summary>
        /// Função que retorna o imc calculado com base no peso e na altura 
        /// <code>
        ///     imc = peso/(altura * altura);
        ///         ou
        ///     imc = peso/(Math.Pow(altura, 2));
        /// </code>
        /// </summary>
        /// <param name="peso"></param>
        /// <param name="altura"></param>
        /// <returns>Retorna  um <b>double</b> com o valor do imc de acordo com o cálculo</returns>
        static double CalculaImc(double peso, double altura)
        {
            return peso / (altura * altura);
        }

        /// <summary>
        /// Função que calcula em qual risco a pessoa se enquadra a partir do imc recebido como parâmetro
        /// </summary>
        /// <param name="imc"></param>
        /// <returns>Retorna uma string com o Risco de acordo com o imc fornecido</returns>
        static string CalculaRecomendacoes(double imc)
        {
            // Verifica em qual Recomendação se enquadra o imc recebido como parâmetro e retorna a recomendação correspondente 
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
            // Verifica em qual Risco se enquadra o imc recebido como argumento

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
            // Limpa a tela
            Console.Clear();

            // Função Espacos serve para gerar uma linha divisória
            DivisoriaHorizontal("_+");

            // Atribui a cor azul à fonte do console
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"Cálculo de IMC Para Diagnóstico Prévio".PadLeft(78, ' '));
            // Retorna a cor anterior à fonte do console.
            Console.ResetColor();
            DivisoriaHorizontal("=-");
        }

        /// <summary>
        /// Monta a tela inicial dinamicamente a cada inserção dos dados do paciente.
        /// <para>Caso não haja dado inserido, monta a tela apenas com os títulos dos dados.</para>
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sexo"></param>
        /// <param name="idade"></param>
        /// <param name="altura"></param>
        /// <param name="peso"></param>
        static void DadosIniciaisPaciente(string nome = "", string sexo = "", int idade = -1, double altura = 0D, double peso = 0D, bool preenchido = false)
        {
            if (!preenchido)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("TRIAGEM DO PACIENTE\n");
            }

            // Preenche dinamicamente a tela conforme os valores forem digitados pelo usuário
            Console.ResetColor();
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
            string dadoApresentado;
            // Preenche o título do dado
            Console.Write($"{tipo}:\t\t");
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Função utilizada para deixar a primeira letra do nome em maiúsculo
            dado = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dado.ToLower());

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
            string unidade = "";
            Console.Write($"{tipo}:\t\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string dadoApresentado = "";
            // Se o tipo de dado a ser apresentado for altura, utiliza-se a grandeza m e se for peso utiliza-se a grandeza kq ao final da string "dadoApresentado" 
            if (tipo == "Altura")
                unidade = "m";
            if (tipo == "Peso")
                unidade = "Kg";
            if (dado > 0) // Só preenche dadoApresentado na tela de triagem se o dado for maior do que zero, ou seja, quando é preenchido um dado válido.
                dadoApresentado = $"{dado.ToString("N2", CultureInfo.GetCultureInfo("pt-br"))}{unidade}"; // Apresenta o dado com duas casas decimais e separado por vírgula
            else
                dadoApresentado = "";
            Console.Write($"{dadoApresentado}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Preenche dinamicamente dados do tipo int na tela inicial de triagem do paciente
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="dado"></param>
        static void CamposNumeroInt(string tipo, int dado)
        {
            Console.Write($"{tipo}:\t\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string dadoApresentado;
            // Se o tipo de dado a ser apresentado for maior ou igual a zero, apresenta o dado dinamicamente na tela de triagem
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

            DivisoriaHorizontal(".-");
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
                        Sair("+");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDigite uma opção válida - 1 para Voltar ou 2 para Sair");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDigite uma opção válida - 1 para Voltar ou 2 para Sair");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Exibe uma mensagem de erro com destaque na cor da fonte
        /// </summary>
        static void MensagemErro()
        {
            // Atribui a cor vermelha à fonte do console
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\nInforme um dado válido e não aperte apenas ENTER");

            // Retorna a cor anterior à fonte do console
            Console.ResetColor();
        }

        /// <summary>
        /// Cria uma linha de separação a partir de um símbolo em formato string
        /// </summary>
        /// <param name="simbolo"></param>
        static void DivisoriaHorizontal(string simbolo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Console.WindowWidth / simbolo.Length; i++)
            {
                Console.Write(simbolo);
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        /// <summary>
        /// Função para sair do programa com uma mensagem de despedida
        /// <para>Cria uma "moldura" verde com a mensagem final de acordo com a string "simbolo" que é recebida no argumento</para>
        /// <para>Imprime a mensagem em azul no centro da moldura</para>
        /// </summary>
        static void Sair(string simbolo)
        {
            // Essa função serve para criar a moldura e colocar a frase no meio da tela utilizando o Console.SetCursorPosition

            Console.Clear();
            DivisoriaHorizontal("+");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 20; i++)
            {
                Console.Write(simbolo);
                for (int j = 1; j < 120 - 1; j++)
                {
                    if (i == 10 && j == 48)
                    {
                        Console.SetCursorPosition(0, 10); // Essa função coloca o cursor na posição desejada. Console.SetCursorPosition(horizontal, vertical)
                        Console.Write(simbolo);
                        Console.SetCursorPosition(119, 10);
                        Console.Write(simbolo);
                        Console.SetCursorPosition(47, 10);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Opção selecionada: Sair");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(0, 11);
                        Console.Write(simbolo);
                        Console.SetCursorPosition(119, 11);
                        Console.Write(simbolo);
                        Console.SetCursorPosition(49, 12);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Obrigado e até mais!");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(0, 12);
                        Console.Write(simbolo);
                        Console.SetCursorPosition(119, 12);
                        Console.Write(simbolo);
                        Console.SetCursorPosition(119, 12);
                        break;
                    }
                    Console.Write(" ");
                }
                Console.Write(simbolo);
            }
            DivisoriaHorizontal("+");
            Console.ResetColor();
            Environment.Exit(0); // Essa função sai do programa e encerra a aplicação
        }
    }
}