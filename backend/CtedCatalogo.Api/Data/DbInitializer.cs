using CtedCatalogo.Api.Models;

namespace CtedCatalogo.Api.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Cursos.Any())
            return;

        var cursos = new List<Curso>
        {
            new Curso
            {
                NomeCompleto = "Matemática para a Precisão: Medidas e Escalas na Metalurgia",
                NomeBreve = "MAT-MET-01",
                Descricao = "Aplicação de grandezas, proporções e escalas em processos de corte, furação e usinagem. Explore nosso curso perfeito para quem deseja se destacar nesse campo e garantir medições precisas e eficientes no ambiente metalúrgico.",
                Categoria = "Matemática",
                Modalidade = "Híbrido (Presencial e Online)",
                CargaHoraria = 40,
                PublicoAlvo = "Operadores de usinagem, técnicos em metalurgia, inspetores de medidas e estudantes técnicos.",
                Status = "Inscrições Abertas",
                ImagemApresentacao = "https://images.unsplash.com/photo-1504917599217-d4dc5ebe6122?auto=format&fit=crop&w=800&q=80",
                DataDisponibilizacao = new DateTime(2026, 1, 5),
                Modulos = new List<ModuloCurso>
                {
                    new() { Ordem = 1, Titulo = "Grandezas e Sistemas de Unidades", DescricaoAulas = "Conversão entre sistema métrico e polegadas, tolerâncias dimensionais e leitura de instrumentos." },
                    new() { Ordem = 2, Titulo = "Proporções e Escalas em Desenho Técnico", DescricaoAulas = "Escalas de redução e ampliação aplicadas a plantas e peças usinadas." },
                    new() { Ordem = 3, Titulo = "Cálculos para Corte, Furação e Usinagem", DescricaoAulas = "Velocidade de corte, avanço, RPM e cálculos trigonométricos no chão de fábrica." }
                }
            },
            new Curso
            {
                NomeCompleto = "Geometria e Modelagem no Design Têxtil",
                NomeBreve = "MAT-TEX-02",
                Descricao = "Explora formas geométricas, simetria e padrões matemáticos presentes em tecidos e estampas, otimizando o aproveitamento de matéria-prima no corte industrial.",
                Categoria = "Matemática",
                Modalidade = "Ensino a Distância",
                CargaHoraria = 32,
                PublicoAlvo = "Modelistas, designers têxteis, operadores de encaixe e profissionais da indústria de confecção.",
                Status = "Inscrições Abertas",
                ImagemApresentacao = "https://images.unsplash.com/photo-1558769132-cb1aea458c5e?auto=format&fit=crop&w=800&q=80",
                DataDisponibilizacao = new DateTime(2026, 2, 10),
                Modulos = new List<ModuloCurso>
                {
                    new() { Ordem = 1, Titulo = "Geometria Plana e Simetria", DescricaoAulas = "Estudo de polígonos, pavimentações do plano e repetição de padrões (rapport)." },
                    new() { Ordem = 2, Titulo = "Modelagem Tridimensional e Graduação", DescricaoAulas = "Proporções antropométricas, ampliação de moldes e cálculo de consumo de tecido." }
                }
            },
            new Curso
            {
                NomeCompleto = "Estatística e Controle de Qualidade na Produção",
                NomeBreve = "MAT-EST-03",
                Descricao = "Leitura e interpretação de dados de controle de qualidade e produtividade para redução de variabilidade e defeitos nas linhas industriais.",
                Categoria = "Matemática",
                Modalidade = "Ensino a Distância",
                CargaHoraria = 48,
                PublicoAlvo = "Analistas de qualidade, supervisores de produção, técnicos industriais e operadores líderes.",
                Status = "Inscrições Abertas",
                ImagemApresentacao = "https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?auto=format&fit=crop&w=800&q=80",
                DataDisponibilizacao = new DateTime(2026, 3, 15),
                Modulos = new List<ModuloCurso>
                {
                    new() { Ordem = 1, Titulo = "Estatística Descritiva na Indústria", DescricaoAulas = "Média, mediana, desvio padrão e amostragem em lotes de produção." },
                    new() { Ordem = 2, Titulo = "Controle Estatístico de Processo (CEP)", DescricaoAulas = "Cartas de controle, índices de capacidade (Cp e Cpk) e análise de Pareto." },
                    new() { Ordem = 3, Titulo = "Indicadores de Produtividade (OEE)", DescricaoAulas = "Cálculo de disponibilidade, performance e qualidade em tempo real." }
                }
            },
            new Curso
            {
                NomeCompleto = "Física do Movimento e da Energia na Metalurgia",
                NomeBreve = "CNT-FIS-04",
                Descricao = "Estudo de forças, atrito, calor e energia em máquinas, fornos e prensas, focando na eficiência térmica e mecânica dos equipamentos.",
                Categoria = "Ciências da Natureza",
                Modalidade = "Híbrido (Presencial e Online)",
                CargaHoraria = 60,
                PublicoAlvo = "Técnicos de manutenção mecânica, operadores de fornos e prensas e engenheiros juniores.",
                Status = "Inscrições Abertas",
                ImagemApresentacao = "https://images.unsplash.com/photo-1504328345606-18bbc8c9d7d1?auto=format&fit=crop&w=800&q=80",
                DataDisponibilizacao = new DateTime(2026, 4, 1),
                Modulos = new List<ModuloCurso>
                {
                    new() { Ordem = 1, Titulo = "Mecânica Aplicada: Forças, Torque e Atrito", DescricaoAulas = "Leis de Newton em sistemas mecânicos, transmissão de potência e lubrificação." },
                    new() { Ordem = 2, Titulo = "Termodinâmica e Transferência de Calor", DescricaoAulas = "Balanço térmico em fornos metalúrgicos, dilatação e tratamentos térmicos." }
                }
            },
            new Curso
            {
                NomeCompleto = "Química Aplicada e Sustentabilidade nos Processos Industriais",
                NomeBreve = "CNT-QUI-05",
                Descricao = "Estudo das reações químicas envolvidas em galvanoplastia, pintura, tratamento de efluentes e acabamento superficial sustentável.",
                Categoria = "Ciências da Natureza",
                Modalidade = "Ensino a Distância",
                CargaHoraria = 36,
                PublicoAlvo = "Técnicos químicos, operadores de tratamento de superfície e analistas de meio ambiente.",
                Status = "Em Breve",
                ImagemApresentacao = "https://images.unsplash.com/photo-1532187863486-abf9dbad1b69?auto=format&fit=crop&w=800&q=80",
                DataDisponibilizacao = new DateTime(2026, 11, 20),
                Modulos = new List<ModuloCurso>
                {
                    new() { Ordem = 1, Titulo = "Corrosão e Tratamento de Superfícies", DescricaoAulas = "Oxirredução, revestimentos protetivos e controle de pH em banhos químicos." },
                    new() { Ordem = 2, Titulo = "Gestão de Resíduos e Efluentes", DescricaoAulas = "Neutralização de efluentes industriais, química verde e normas ambientais." }
                }
            },
            new Curso
            {
                NomeCompleto = "Cultura Organizacional e Gestão do Tempo na Produtividade",
                NomeBreve = "GES-ORG-06",
                Descricao = "Desenvolva habilidades de trabalho em equipe, comunicação assertiva, inteligência emocional e gestão ágil da rotina no setor industrial.",
                Categoria = "Gestão e Produtividade",
                Modalidade = "Ensino a Distância",
                CargaHoraria = 24,
                PublicoAlvo = "Líderes operacionais, analistas administrativos, gestores de equipe e profissionais em transição.",
                Status = "Encerrado",
                ImagemApresentacao = "https://images.unsplash.com/photo-1522071820081-009f0129c71c?auto=format&fit=crop&w=800&q=80",
                DataDisponibilizacao = null,
                Modulos = new List<ModuloCurso>
                {
                    new() { Ordem = 1, Titulo = "Cultura de Excelência e Colaboração", DescricaoAulas = "Comunicação assertiva, segurança psicológica e alinhamento de metas em turnos." },
                    new() { Ordem = 2, Titulo = "Gestão do Tempo e Priorização", DescricaoAulas = "Matrizes de priorização, eliminação de gargalos de rotina e foco em resultados." }
                }
            }
        };

        context.Cursos.AddRange(cursos);
        context.SaveChanges();
    }
}