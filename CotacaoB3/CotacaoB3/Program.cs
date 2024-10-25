using Newtonsoft.Json;

namespace CotacaoB3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Configurando credenciais da aplicação...");
                Config newConfig = new Config("email@dominio.com", "senha", "apiKey", "passwordGmail");

                Console.WriteLine("Lendo conteúdo do arquivo de ativos...");
                string conteudo = File.ReadAllText(args[0]);

                if (string.IsNullOrEmpty(conteudo))
                {
                    Console.WriteLine("Por favor insira um texto válido...");
                    break;
                }

                Console.WriteLine("Arquivo lido com sucesso, desserializando conteudo...");
                ConfigUser configuracaoAtivos = null;

                try
                {
                    configuracaoAtivos = JsonConvert.DeserializeObject<ConfigUser>(conteudo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Não foi possível desserializar o json no objeto - @ex", ex);
                    break;
                }

                if (configuracaoAtivos == null || !configuracaoAtivos.Ativos.Any() || !configuracaoAtivos.EmailUsuario.Any() || configuracaoAtivos.Ativos.Contains("") || configuracaoAtivos.EmailUsuario.Contains(""))
                {
                    Console.WriteLine("Arquivo possui campos vazios ou nulos...");
                    break;
                }

                string tokenAcessoAPI = newConfig.TokenAPI;
                string apiB3URL = "https://brapi.dev/api/quote/";


                Console.WriteLine($"Conteudo desserializado com sucesso, realizando requisicoes na API {apiB3URL}...");
                foreach (var ativo in configuracaoAtivos.Ativos)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(apiB3URL + ativo + "?range=1d&interval=1d&fundamental=true&token=" + tokenAcessoAPI),
                    };

                    Console.WriteLine($"Fazendo requisicao para o ativo : {ativo}...");
                    var response = new HttpResponseMessage();

                    try
                    {
                        response = await client.SendAsync(request);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Não foi possível obter uma resposta da API - @ex", ex);
                        continue;
                    }

                    var body = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"Serializando resposta da API para o ativo : {ativo}...");
                    ResponseCotacao responseCotacao = null;

                    try
                    {
                        responseCotacao = JsonConvert.DeserializeObject<ResponseCotacao>(body);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Nao foi possivel desserializar o json no objeto - @ex", ex);
                        continue;
                    }

                    if (responseCotacao == null || responseCotacao.results == null || !responseCotacao.results.Any())
                    {
                        Console.WriteLine("Cotacao retornou um resultado nulo ou nao ha resultados para essa cotacao...");
                        continue;
                    }

                    Console.WriteLine($"Verificando limitantes para o ativo : {ativo}...");
                    if (responseCotacao.results[0].regularMarketPrice <= configuracaoAtivos.LimitanteInferior)
                    {
                        Console.WriteLine($"Limitante inferior atingido para o ativo : {ativo}...");
                        EnviarEmail enviarEmail = new EnviarEmail();

                        foreach(var emailUsuario in configuracaoAtivos.EmailUsuario)
                        {
                            Console.WriteLine($"Enviando email para {emailUsuario} sobre o ativo : {ativo}...");
                            enviarEmail.EnviarMensagem($"Compre o ativo {ativo}", emailUsuario, newConfig.emailCredentials.EmailUser, newConfig.emailCredentials.EmailPassword, newConfig.PasswordAPPGmail);
                        }
                    }
                    else if(responseCotacao.results[0].regularMarketPrice >= configuracaoAtivos.LimitanteSuperior)
                    {
                        Console.WriteLine($"Limitante superior atingido para o ativo : {ativo}...");
                        EnviarEmail enviarEmail = new EnviarEmail();

                        foreach (var emailUsuario in configuracaoAtivos.EmailUsuario)
                        {
                            Console.WriteLine($"Enviando email para {emailUsuario} sobre o ativo : {ativo}...");
                            enviarEmail.EnviarMensagem($"Venda o ativo {ativo}", emailUsuario, newConfig.emailCredentials.EmailUser, newConfig.emailCredentials.EmailPassword, newConfig.PasswordAPPGmail);
                        }
                    }
                }
            }
        }
    }
}