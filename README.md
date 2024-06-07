# MyStorageApi

Aplicação API para gerencimaneto de estoque e cadastro de produtos.

# Preparando o ambiente

## Requisitos
- .NET 8
- Visual Studio 2022 (opcional)

Primeiro vamos clonar o repositorio:
```
git clone git@github.com:drmcarvalho/MyStorageApi.git
```

Apos ter clonado basta abrir o projeto no Visual Studio, em seguinda vamos configurar o banco de dados que é um arquivo sqlite `database.sqlite` que vai ser usado pela aplicação.

Baixe o arquivo sqlite [aqui]() ou se preferir execute [este script SQL](https://gist.github.com/drmcarvalho/1abd110ec0552bf394ffa4773357cfc2) para criar o banco.

No Visual Studio no projeto `MyStorageApplication.Api` temos o arquivo appsettings.Development.json, é dentro dele que esta definido o caminho do nosso arquivo de banco de dados, só precisamos colocar o arquivo de banco de dados do sqlite baixado ou criado no path que esta no appsettings.Development.json, veja abaixo o exemplo da ConnectionStrings:
```
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

Feito isso compile e execute o projeto da API e nevegue para `https://localhost:[porta]/swagger/index.html` e veja os métodos listados no Swagger.