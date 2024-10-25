# Cotação B3

## Descrição

O projeto **Cotação B3** é uma aplicação console em C# que monitora a cotação de ativos da B3 (Bolsa de Valores Brasileira) e envia notificações por e-mail quando os preços de mercado atingem limites definidos. Utiliza a API do BrAPI para obter dados de cotação e o Newtonsoft.Json para manipulação de JSON.

## Funcionalidades

- Configuração de credenciais de e-mail e API.
- Leitura de um arquivo JSON que contém informações sobre os ativos e e-mails dos usuários.
- Monitoramento contínuo da cotação dos ativos.
- Envio de e-mails de notificação quando os preços atingem limites superiores ou inferiores definidos.

## Pré-requisitos

- .NET SDK (versão 6 ou superior)
- Bibliotecas:
  - Newtonsoft.Json
- Senha de aplicações gerado pelo próprio Gmail.

## Estrutura do Arquivo JSON

O arquivo de ativos deve ser estruturado da seguinte forma:

```json
{
  "Ativos": ["ativo1", "ativo2"],
  "EmailUsuario": ["email1@dominio.com", "email2@dominio.com"],
  "LimitanteInferior": valorInferior,
  "LimitanteSuperior": valorSuperior
}
```

### Exemplos de Valores

- `Ativos`: Lista de códigos de ativos da B3 (por exemplo, `["PETR3", "VALE3"]`).
- `EmailUsuario`: Lista de e-mails dos usuários para os quais as notificações serão enviadas.
- `LimitanteInferior`: Valor abaixo do qual uma notificação de compra será enviada.
- `LimitanteSuperior`: Valor acima do qual uma notificação de venda será enviada.

## Como Usar

1. Clone este repositório:
   ```bash
   git clone https://github.com/seu_usuario/CotacaoB3.git
   cd CotacaoB3
   ```

2. Crie um arquivo JSON com a estrutura mencionada e salve-o na mesma pasta do projeto.

3. Execute o projeto, passando o caminho do arquivo JSON como argumento:
   ```bash
   dotnet run caminho/do/arquivo.json
   ```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir um pull request ou issue.

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para detalhes.
