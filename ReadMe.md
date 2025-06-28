# 💸 Finlyze - Backend

**Finlyze** é um sistema de controle financeiro pessoal, focado em ajudar usuários a gerenciar entradas, saídas e categorias de gastos com clareza e praticidade.

> **Arquitetura:** `Domain → Application → Infrastructure → API`

---

## 🛠️ Tecnologias e Organização

- 🔹 **.NET 8**
- 🔹 **Dapper** para leitura
- 🔹 **EF Core** (apenas para Migrations)
- 🔹 **xUnit** para testes
- 🔹 Arquitetura baseada em camadas (Clean Architecture)

---

## 📂 Estrutura do Projeto

- `Domain`: regras de negócio e entidades
- `Application`: interfaces, DTOs, handlers e validações
- `Infrastructure`: implementação das interfaces + Dapper
- `API`: ponto de entrada e configuração
- `Utilities`: ferramentas e helpers reutilizáveis
- `Tests`: testes automatizados com xUnit

---

## 📚 Documentação

Acesse a [📖 Wiki do Projeto](https://github.com/LucasLantemamLeite/Finlyze.Api/wiki)  
para mais detalhes sobre arquitetura, camadas, interfaces e exemplos de uso.

---
