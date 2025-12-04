# ReciclaERP - Comercial (Proposta & Pedido)

Solução de referência em C#/.NET para o módulo Comercial de um ERP focado em reciclagem de máquinas. A arquitetura segue princípios de Clean Architecture, SOLID e padrões GoF (Strategy, Factory Method e Adapter).

## Estrutura de Projetos

| Projeto | Responsabilidade |
|---------|------------------|
| `ERP.Reciclagem.Comercial.Domain` | Entidades, agregados e contratos (Proposals, Orders, Pricing Strategies, Credit) |
| `ERP.Reciclagem.Comercial.Application` | DTOs e serviços de aplicação (Create, Reprice, Confirm use cases) |
| `ERP.Reciclagem.Comercial.Infrastructure` | Repositórios em memória e adapter de crédito externo |
| `ERP.Reciclagem.Comercial.ConsoleDemo` | Demonstração de uso end-to-end |

## Requisitos Atendidos

- Bounded Context Comercial – Propostas & Pedidos
- Strategy para precificação (`IPricingStrategy`, `StandardPricingStrategy`, `PromotionalPricingStrategy`)
- Factory para criação de pedidos (`IOrderFactory`, `OrderFactory`)
- Adapter para análise de crédito (`CreditAnalysisServiceAdapter` sobre `ExternalCreditApiClient`)
- Clean Code (nomes significativos, métodos curtos, DTOs para entrada/saída)

## Executando

```bash
cd /Users/gabrielbller/Documents/Pos-Graduacao/CleanCode/ReciclaERP
 dotnet build
 dotnet run --project ERP.Reciclagem.Comercial.ConsoleDemo/ERP.Reciclagem.Comercial.ConsoleDemo.csproj
```

A Console Demo imprime:
1. Proposta criada (id, total, validade)
2. Resultado da reprecificação (se expirou, novo total)
3. Pedido confirmado (id e total) após sinal recebido + crédito aprovado.

## Próximos Passos

- Adicionar testes unitários para serviços e estratégias.
- Substituir repositórios em memória por persistência real.
- Expor os casos de uso via API REST.

