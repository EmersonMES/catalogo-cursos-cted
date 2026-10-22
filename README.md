# Catálogo de Cursos CTED — SESI+INDÚSTRIA

## 1. Objetivo da Aplicação
Aplicação web desenvolvida para o Centro de Tecnologias e Educação Digital (CTED) do SESI-SP com o objetivo de apresentar o catálogo de cursos disponíveis em ambiente educacional Moodle. A plataforma permite consultar ofertas formativas, pesquisar cursos pelo nome, aplicar filtros combinados (categoria, modalidade e status), visualizar a contagem de resultados em tempo real e aceder à página detalhada de cada curso com os respetivos módulos e conteúdos programáticos.

## 2. Tecnologias Utilizadas
* **Front-end:** HTML5 Semântico, CSS3 (Custom Properties, CSS Grid, Flexbox), JavaScript (ES6+ Fetch API) e tipografia *Open Sans*.
* **Back-end:** C# / .NET Web API (ASP.NET Core).
* **Base de Dados e ORM:** SQLite (base de dados relacional de código aberto) e Entity Framework Core.
* **Documentação da API:** Swagger / OpenAPI (`Swashbuckle.AspNetCore`).

## 3. Arquitetura da Solução
A solução adota uma arquitetura cliente-servidor desacoplada (RESTful):
1. **Camada de Apresentação (Front-end):** Aplicação SPA (*Single Page Application*) ligeira em HTML/CSS/JS que consome os dados de forma assíncrona via HTTP (`fetch`), gerindo estados de carregamento (*loading*), erro, busca vazia e alternância entre a vista do catálogo principal e a vista de detalhes do curso.
2. **Camada de Serviços (API REST em .NET):** Disponibiliza endpoints dedicados à listagem com filtragem dinâmica combinada (`IQueryable`), extração de metadados de filtros e consulta individual de detalhes.
3. **Camada de Persistência (SQLite + EF Core):** Gere o armazenamento relacional em ficheiro local (`catalogo_cted.db`) com *Data Seeding* automático na inicialização (`DbInitializer`).

## 4. Estrutura das Pastas
```text
catalogo-cursos-cted/
├── backend/
│   └── CtedCatalogo.Api/
│       ├── Controllers/
│       │   └── CursosController.cs    # Endpoints REST de consulta e filtros
│       ├── Data/
│       │   ├── AppDbContext.cs        # Contexto do Entity Framework Core
│       │   └── DbInitializer.cs       # Seed automático com 6 cursos fictícios
│       ├── Models/
│       │   ├── Curso.cs               # Entidade principal do curso
│       │   └── ModuloCurso.cs         # Entidade de módulos e descrição de aulas
│       ├── Program.cs                 # Configuração de DI, CORS, Swagger e SQLite
│       ├── catalogo_cted.db           # Ficheiro da base de dados SQLite
│       └── CtedCatalogo.Api.csproj
├── frontend/
│   ├── assets/                        # Ícones SVG, imagens e fontes Open Sans
│   ├── index.html                     # Estrutura semântica do catálogo e detalhes
│   ├── styles.css                     # Estilização fiel ao Figma e media queries
│   └── app.js                         # Consumo da API, filtros e manipulação DOM
├── README.md                          # Documentação técnica da solução
└── USO_DE_IA.md                       # Registo do uso de Inteligência Artificial