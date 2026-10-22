# Registo do Uso de Inteligência Artificial

Este documento descreve a utilização de ferramentas de Inteligência Artificial durante o desenvolvimento da atividade prática para a vaga de **Desenvolvedor de Sistemas I (CTED / SESI-SP)**, em conformidade com o item `4.d` do edital.

---

## 1. Ferramenta Utilizada
* **Google Gemini** (Assistente de IA Generativa).

## 2. Objetivo do Uso
A ferramenta foi utilizada como copiloto técnico para:
1. Analisar e estruturar os requisitos funcionais e não funcionais descritos no edital da atividade prática.
2. Auxiliar na criação do esqueleto inicial (*boilerplate*) da Web API em C#/.NET com Entity Framework Core e SQLite.
3. Estruturar a massa de dados fictícia inicial (`DbInitializer.cs`) alinhada aos temas industriais do protótipo do Figma (Matemática, Ciências da Natureza e Gestão).
4. Agilizar a codificação da estrutura HTML5, estilização CSS3 responsiva baseada nas capturas do Figma e lógica de filtros combinados em JavaScript puro (`fetch`).
5. Apoiar no diagnóstico de erros de compilação do SDK do .NET e na organização da documentação técnica (`README.md`).

## 3. Prompts Relevantes: O que foi Aproveitado ou Descartado

### Prompt 1 — Estruturação Inicial e Escolha da Stack
> *"Quero que você me auxilie a desenvolver uma aplicação web para apresentar um catálogo de curso para o ambiente virtual de aprendizagem com base no pdf anexado. Front-end: HTML/CSS/JS. Banco de Dados: SQLite."*

* **O que foi aproveitado:** A modelagem das entidades `Curso` e `ModuloCurso`, a lógica de consulta com `IQueryable` no `CursosController` para combinar pesquisa textual e filtros (`categoria`, `modalidade`, `status`), e a configuração de CORS.
* **O que foi descartado:** O primeiro layout genérico de cartões proposto pela IA antes da análise visual detalhada do protótipo do Figma, bem como os dados fictícios genéricos iniciais (que incluíam temas como NR-12 e NR-17 que não correspondiam aos cursos reais ilustrados no protótipo).

### Prompt 2 — Adequação Fiel ao Protótipo do Figma
> *Envio das capturas de ecrã do protótipo no Figma ("SESI+INDÚSTRIA") contendo a página inicial, carrossel de cursos, secção "Impacto transformador", depoimentos e página interna de detalhes do curso.*

* **O que foi aproveitado:** A reestruturação completa do HTML/CSS para refletir fielmente o design do Figma (Hero vermelho, cartões com imagem de fundo e gradiente vermelho escuro, selos de categoria por cor, acordeão de módulos, secção de depoimentos e barra preta flutuante de matrícula no rodapé), além da atualização do `DbInitializer.cs` com os cursos do protótipo (*Matemática para a Precisão*, *Geometria e Modelagem no Design Têxtil*, *Estatística e Controle de Qualidade*, *Física do Movimento*, etc.).

### Prompt 3 — Diagnóstico de Compilação do Back-end
> *"Erro (CS1061) ocorre na versão do SDK do .NET 10* seguido da imagem do terminal com o erro ao executar `dotnet run`.

* **O que foi aproveitado:** A identificação imediata da ausência do pacote `Swashbuckle.AspNetCore` no template recente do .NET e o comando de correção.

## 4. Como o Conteúdo foi Validado
1. **Validação de Compilação e Execução:** O código C# gerado foi compilado e testado localmente via CLI (`dotnet build` e `dotnet run`), verificando a criação automática do ficheiro `catalogo_cted.db` pelo SQLite.
2. **Validação de Contrato de API:** Os endpoints `/api/cursos`, `/api/cursos/{id}` e `/api/cursos/filtros` foram testados no navegador e via Swagger para garantir que os filtros combinados retornam a contagem exata (`quantidadeResultados`) e os objetos JSON esperados.
3. **Validação Visual e Funcional (Front-end):** Os estados de carregamento, erro (simulando a paragem da API), busca sem resultados ("Nenhum curso encontrado"), limpeza de filtros, responsividade em diferentes resoluções e navegação para a página de detalhes foram inspecionados manualmente no navegador em comparação direta com o protótipo do Figma.

## 5. Exemplo de Erro ou Inadequação Identificado
Durante o desenvolvimento, foram identificados dois pontos de inadequação no conteúdo inicialmente gerado pela IA:

1. **Erro de Compilação `CS1061` no `Program.cs` (Swagger):**
   A IA gerou o ficheiro `Program.cs` utilizando os métodos `builder.Services.AddSwaggerGen()`, `app.UseSwagger()` e `app.UseSwaggerUI()`, assumindo que o pacote `Swashbuckle.AspNetCore` já vinha pré-instalado por padrão no template `dotnet new webapi`. Ao executar `dotnet run` no PowerShell, o compilador falhou com três erros `CS1061`:
   * `'IServiceCollection' não contém uma definição para "AddSwaggerGen"`
   * `'WebApplication' não contém uma definição para "UseSwagger"`
   * `'WebApplication' não contém uma definição para "UseSwaggerUI"`
2. **Erro de Sintaxe em Propriedade Anónima no Seed Inicial:**
   Na primeira versão do `DbInitializer.cs`, a IA incluiu acidentalmente um fragmento inválido (`Logica: false,`) dentro da inicialização de um objeto `ModuloCurso`.

## 6. Correção Realizada
1. **Correção do Swagger (`CS1061`):** Foi realizada a instalação explícita da biblioteca `Swashbuckle.AspNetCore` no projeto `CtedCatalogo.Api` através do comando:
   ```powershell
   dotnet add package Swashbuckle.AspNetCore