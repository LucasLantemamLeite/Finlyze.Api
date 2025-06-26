# 💸 Finlyze - Backend

**Finlyze** é um sistema de controle financeiro pessoal, focado em ajudar os usuários a gerenciar suas entradas, saídas e categorias de gastos com clareza e praticidade.

> **Arquitetura:** `Domain → Application → Infrastructure → API`

---

## 🔧 Arquitetura

O projeto segue os princípios da **Clean Architecture**, utilizando separação clara entre responsabilidades:

Além das quatro camadas principais, o projeto conta com:

- `Utilities`: recursos e funções auxiliares
- `Migrations`: gerenciamento do banco de dados (via EF Core)
- `Tests`: testes automatizados com **xUnit**

---

## 🧩 Domain

A camada de domínio contém toda a **lógica de negócio** da aplicação. Nela estão definidas as seguintes **entidades**:

- `UserAccount`: representa a conta do usuário
- `Transaction`: representa uma movimentação financeira
- `AppLog`: armazena registros de eventos internos da aplicação

Cada entidade possui seus próprios:

- **Value Objects**: valores imutáveis com regras e validações
- **Exceções** específicas para tratar erros de negócio

---

## 📌 Enums

Os Enums são utilizados para representar informações fixas e controladas no sistema, garantindo clareza e segurança.

- `ERole`: perfis de usuário
- `ETypeTransaction`: tipo da transação (entrada ou saída)
- `ELog`: tipo de log do sistema

---

## 🧪 Testes (xUnit)

A camada de testes automatizados cobre os **Value Objects** das entidades, validando:

- Casos de uso com dados válidos
- Lançamento de exceções em casos inválidos

> Os testes utilizam o **xUnit**, framework moderno e flexível para testes no .NET.

---

## 📚 Mais informações

Acesse a [Wiki do projeto](https://github.com/LucasLantemamLeite/Finlyze.Api.wiki.git) para mais detalhes sobre a estrutura, padrões adotados e convenções de desenvolvimento.
