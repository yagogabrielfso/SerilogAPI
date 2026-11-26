# SerilogAPI

![.NET 6](https://img.shields.io/badge/.NET-6-512BD4?logo=dotnet)
![Serilog](https://img.shields.io/badge/logging-Serilog-1F6FEB)

API ASP.NET Core com **logging estruturado** usando Serilog. A configuração dos destinos de log fica no `appsettings.json`, sem precisar recompilar para mudar o nível ou o destino.

## O que o projeto mostra
- Serilog no lugar do logger padrão do ASP.NET Core
- Configuração lida do `appsettings.json` com `Serilog.Settings.Configuration`
- Saída no console e em arquivo (`logs/log.txt`)
- Logs estruturados, com propriedades em vez de texto concatenado. Por exemplo:

```csharp
_logger.LogInformation("Pedido {PedidoId} criado para o cliente {ClienteId}", pedido.Id, cliente.Id);
```

## Pacotes
- `Serilog.AspNetCore`
- `Serilog.Settings.Configuration`
- `Serilog.Sinks.Console`
- `Serilog.Sinks.File`

## Como rodar

Pré-requisito: [.NET SDK 6](https://dotnet.microsoft.com/download/dotnet/6.0).

```bash
git clone https://github.com/yagogabrielfso/SerilogAPI.git
cd SerilogAPI/SerilogAPI/SerilogAPI
dotnet run
```

Depois de fazer uma requisição, confira os logs no console e em `logs/log.txt`.

## Próximos passos
- [ ] Enviar os logs para o Seq ou para um coletor OpenTelemetry
- [ ] Enriquecer os logs com o identificador da requisição (correlation id)
