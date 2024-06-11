# MyStorageApi

Aplicação API para gerencimaneto de estoque e cadastro de produtos.

# Preparando o ambiente de desenvolvimento

## Requisitos
- .NET 8
- Visual Studio 2022 (opcional)

Primeiro vamos clonar o repositorio:
```
git clone git@github.com:drmcarvalho/MyStorageApi.git
```

Após ter clonado basta abrir o projeto no Visual Studio. Em seguida vamos configurar o banco de dados que é um arquivo sqlite `database.sqlite` que vai ser usado pela aplicação.

Baixe o arquivo sqlite [aqui](https://drive.google.com/file/d/1oIRed3Vd_9TjkylZ6iTXieQcgAqU6CEB/view?usp=sharing) ou se preferir execute [este script SQL](https://gist.github.com/drmcarvalho/1abd110ec0552bf394ffa4773357cfc2) para criar o banco.

No Visual Studio no projeto `MyStorageApplication.Api` temos o arquivo appsettings.Development.json, é dentro dele que está definido o caminho do nosso arquivo de banco de dados, só precisamos colocar o arquivo de banco de dados do sqlite baixado ou criado no path que está no `appsettings.Development.json`. 

Veja abaixo o exemplo da ConnectionStrings:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "SqliteConnection": "Data Source=C:\\MyStorageData\\database.sqlite"
  }
}
```

Feito isso compile e execute o projeto da API e navegue para `https://localhost:[porta]/swagger/index.html` e veja os endpoints listados no Swagger.

# Considerações sobre as funcionalidades

O projeto consiste de dois módulos principais onde estão agrupadas as funcionalidades, os módulos são:

## Produto

Permite cadastrar, alterar, consultar e apagar um produto via chamadas na API.

## Gerenciamento de estoque

Permite cadastrar e alterar estoques, permite efetuar movimentações e consultar o historico das movimentações efetuadas.

## Considerações tecnicas e de arquitetura

O projeto usa injeção de dependência para facilitar testes e manutenção do sistema, sendo que o sistema é dividido em três principais partes:
- Controlador que processa uma requisição HTTP e delega para o serviço.
- Serviço é onde o processamento dos dados são feitos, exemplo: `IStorageManagerServiceDomain` no qual possui toda a lógica de domínio da aplicação e se comunica com o banco de dados via repositório.
- Repositórios são divididos em leitura e escrita e tem a única responsabilidade de acessar o banco de dados via Dapper.

