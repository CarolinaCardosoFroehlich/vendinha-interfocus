💸 DividaFácil

Sistema web para controle de clientes, dívidas e pagamentos.

Projeto desenvolvido para fins de estudo utilizando:

🌐 HTML

🎨 CSS

⚙️ JavaScript

🔷 C# (ASP.NET Web API)

🗄 PostgreSQL

🐘 NHibernate

📌 Sobre o Projeto

O DividaFácil é uma aplicação simples e intuitiva para gerenciamento de dívidas.

O sistema permite:

✅ Cadastro de clientes

✅ Cadastro de dívidas

✅ Listagem de clientes ordenados por valor devedor

✅ Listagem de dívidas

✅ Marcar dívida como paga

✅ Cálculo automático do total de dívidas por cliente

✅ Dashboard com indicadores gerais

🖥️ Estrutura do Projeto
🔹 Backend

API REST em C#

ORM: NHibernate

Banco de dados: PostgreSQL

🔹 Frontend

HTML puro

CSS customizado (layout colorido e moderno)

JavaScript vanilla consumindo a API via fetch

📊 Funcionalidades
📋 Tela de Clientes

Busca por nome

Paginação de 10 em 10 registros

Exibe:

Nome

CPF

Total de dívidas

Idade (calculada automaticamente)

Soma total geral das dívidas ao final da lista

💸 Tela de Dívidas

Lista todas as dívidas cadastradas

Exibe:

Nome do cliente

Descrição

Valor

Situação (pendente ou paga)

Botão para marcar dívida como paga

Soma total das dívidas agrupadas por cliente

🚀 Como Executar
🔹 Backend

Configurar string de conexão com PostgreSQL

Rodar as migrations / criar banco

Executar a API

A API ficará disponível em:

http://localhost:5000
🔹 Frontend

Abrir o arquivo index.html

Certificar que a API está rodando

Navegar entre:

clientes.html

dividas.html

🗂 Banco de Dados

Principais entidades:

Cliente

Divida

Pagamento

Relacionamentos:

Um Cliente pode ter várias Dívidas

Uma Dívida pode ter vários Pagamentos

🎯 Objetivo do Projeto

Projeto desenvolvido com foco em:

Prática de CRUD completo

Integração Frontend + Backend

Organização de código

Estruturação de API REST

Uso de ORM (NHibernate)

Boas práticas com PostgreSQL
