const IS_LOCAL = window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1' || window.location.protocol === 'file:';
const API_BASE_URL = IS_LOCAL
  ? 'http://localhost:5240/api/cursos'
  : 'https://SUA-URL-DO-RENDER.onrender.com/api/cursos';

const DEPOIMENTOS_FIGMA = [
  {
    texto: '“A experiência do curso foi incrível! As aulas foram muito bem estruturadas e o cronograma foi cuidadosamente planejado, facilitando muito o aprendizado.”',
    autor: 'Lucas Pereira',
    cargo: 'Analista na Empresa X'
  },
  {
    texto: '“Concluir o curso foi uma jornada enriquecedora. As aulas eram dinâmicas e o horário foi organizado para maximizar nosso tempo de estudo.”',
    autor: 'Mariana Costa',
    cargo: 'Gestora na Empresa X'
  },
  {
    texto: '“O curso superou minhas expectativas! As aulas eram bem elaboradas e o planejamento do horário ajudou a manter o foco e a produtividade.”',
    autor: 'Thiago Almeida',
    cargo: 'Técnico na Empresa X'
  },
  {
    texto: '“Finalizar o curso foi uma experiência motivadora. As aulas foram muito bem preparadas e o cronograma foi ideal para o aprendizado.”',
    autor: 'Fernanda Silva',
    cargo: 'Analista na Empresa X'
  },
  {
    texto: '“A conclusão do curso foi uma etapa muito gratificante. O conteúdo bem organizado e o horário flexível ajudaram a facilitar meu aprendizado.”',
    autor: 'Gabriel Santos',
    cargo: 'Analista na Empresa X'
  }
];

// Elementos da DOM
const viewHome = document.getElementById('view-home');
const viewDetalhes = document.getElementById('view-detalhes');

const inputPesquisa = document.getElementById('input-pesquisa');
const selectCategoria = document.getElementById('select-categoria');
const selectModalidade = document.getElementById('select-modalidade');
const selectStatus = document.getElementById('select-status');
const btnLimparFiltros = document.getElementById('btn-limpar-filtros');
const btnLimparVazio = document.getElementById('btn-limpar-vazio');
const btnTentarNovamente = document.getElementById('btn-tentar-novamente');
const btnVerTodos = document.getElementById('btn-ver-todos');

const contadorResultados = document.getElementById('contador-resultados');
const estadoCarregamento = document.getElementById('estado-carregamento');
const estadoErro = document.getElementById('estado-erro');
const estadoVazio = document.getElementById('estado-vazio');
const gradeCursos = document.getElementById('grade-cursos');

let debounceTimer = null;
let exibindoTodosEmGrade = false;

// Inicialização
async function init() {
  renderizarDepoimentos('depoimentos-home-track', 'testimonial-red');
  renderizarDepoimentos('depoimentos-detalhe-track', 'testimonial-white');
  await carregarOpcoesFiltros();
  await buscarCursos();
}

function renderizarDepoimentos(containerId, classeTema) {
  const container = document.getElementById(containerId);
  if (!container) return;

  container.innerHTML = DEPOIMENTOS_FIGMA.map(
    (d) => `
    <article class="testimonial-card ${classeTema}">
      <p class="testimonial-quote">${d.texto}</p>
      <div class="testimonial-author">
        <strong>${d.autor}</strong>
        <span>${d.cargo}</span>
        <div class="stars" aria-label="Avaliação 5 de 5 estrelas">★★★★★</div>
      </div>
    </article>
  `
  ).join('');
}

window.rolarDepoimentos = function (containerId, direcao) {
  const track = document.getElementById(containerId);
  if (track) {
    track.scrollBy({ left: direcao * 320, behavior: 'smooth' });
  }
};

async function carregarOpcoesFiltros() {
  try {
    const res = await fetch(`${API_BASE_URL}/filtros`);
    if (!res.ok) return;
    const data = await res.json();

    popularSelect(selectCategoria, data.categorias);
    popularSelect(selectModalidade, data.modalidades);
    popularSelect(selectStatus, data.status);
  } catch (err) {
    console.warn('Aviso ao carregar filtros iniciais:', err);
  }
}

function popularSelect(selectEl, lista) {
  const opcaoPadrao = selectEl.options[0];
  selectEl.innerHTML = '';
  selectEl.appendChild(opcaoPadrao);

  (lista || []).forEach((valor) => {
    const option = document.createElement('option');
    option.value = valor;
    option.textContent = valor;
    selectEl.appendChild(option);
  });
}

function obterClasseCategoria(categoria) {
  const cat = (categoria || '').toLowerCase();
  if (cat.includes('ciência') || cat.includes('natureza')) return 'pill-science';
  if (cat.includes('gestão') || cat.includes('produtividade')) return 'pill-management';
  return 'pill-math';
}

function obterIconeCategoria(categoria) {
  const cat = (categoria || '').toLowerCase();
  if (cat.includes('ciência') || cat.includes('natureza')) return '🌿';
  if (cat.includes('gestão') || cat.includes('produtividade')) return '📊';
  return '🧮';
}

async function buscarCursos() {
  alternarEstado('carregamento');

  const params = new URLSearchParams();
  if (inputPesquisa.value.trim()) params.append('pesquisa', inputPesquisa.value.trim());
  if (selectCategoria.value) params.append('categoria', selectCategoria.value);
  if (selectModalidade.value) params.append('modalidade', selectModalidade.value);
  if (selectStatus.value) params.append('status', selectStatus.value);

  try {
    const url = params.toString() ? `${API_BASE_URL}?${params.toString()}` : API_BASE_URL;
    const response = await fetch(url);
    if (!response.ok) throw new Error(`HTTP ${response.status}`);

    const data = await response.json();
    const total = data.quantidadeResultados ?? 0;

    contadorResultados.textContent =
      total === 1 ? 'Exibindo 1 curso encontrado' : `Exibindo ${total} cursos encontrados`;

    if (total === 0) {
      alternarEstado('vazio');
      return;
    }

    renderizarCardsCursos(data.resultados);
    alternarEstado('lista');
  } catch (err) {
    console.error('Erro ao buscar cursos na API:', err);
    contadorResultados.textContent = 'Erro ao conectar com a API de cursos.';
    alternarEstado('erro');
  }
}

function renderizarCardsCursos(cursos) {
  gradeCursos.innerHTML = '';

  cursos.forEach((curso) => {
    const card = document.createElement('article');
    card.className = 'course-card-figma';
    card.tabIndex = 0;
    card.setAttribute('role', 'button');
    card.setAttribute('aria-label', `Ver detalhes do curso ${curso.nomeCompleto}`);

    const classePill = obterClasseCategoria(curso.categoria);
    const iconeCat = obterIconeCategoria(curso.categoria);

    card.innerHTML = `
      <img
        class="course-card-bg"
        src="${curso.imagemApresentacao}"
        alt=""
        onerror="this.src='https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?auto=format&fit=crop&w=800&q=80'"
      />
      <div class="course-card-gradient"></div>
      <div class="course-card-top">
        <span class="category-pill ${classePill}">
          <span aria-hidden="true">${iconeCat}</span> ${curso.categoria}
        </span>
        <span class="status-tag">${curso.status}</span>
      </div>
      <div class="course-card-bottom">
        <h3>${curso.nomeCompleto}</h3>
        <p>${curso.descricao}</p>
        <div class="course-card-meta-mini">
          <span>${curso.modalidade}</span>
          <strong>${curso.cargaHoraria}h</strong>
        </div>
      </div>
    `;

    card.addEventListener('click', () => abrirPaginaDetalhes(curso.id));
    card.addEventListener('keydown', (e) => {
      if (e.key === 'Enter' || e.key === ' ') {
        e.preventDefault();
        abrirPaginaDetalhes(curso.id);
      }
    });

    gradeCursos.appendChild(card);
  });
}

async function abrirPaginaDetalhes(id) {
  try {
    const response = await fetch(`${API_BASE_URL}/${id}`);
    if (!response.ok) throw new Error('Erro ao carregar curso');
    const curso = await response.json();

    const badge = document.getElementById('detalhe-categoria-badge');
    badge.className = `category-pill ${obterClasseCategoria(curso.categoria)}`;
    badge.textContent = `${obterIconeCategoria(curso.categoria)} ${curso.categoria}`;

    document.getElementById('detalhe-titulo').textContent = curso.nomeCompleto;
    document.getElementById('sticky-curso-titulo').textContent = curso.nomeCompleto;
    document.getElementById('detalhe-nome-breve').textContent = curso.nomeBreve;
    document.getElementById('detalhe-carga').textContent = `${curso.cargaHoraria} horas`;
    document.getElementById('detalhe-modalidade').textContent = curso.modalidade;
    document.getElementById('detalhe-status').textContent = curso.status;
    document.getElementById('detalhe-descricao').textContent = curso.descricao;
    document.getElementById('detalhe-publico').textContent = curso.publicoAlvo;

    const dataFormatada = curso.dataDisponibilizacao
      ? new Date(curso.dataDisponibilizacao).toLocaleDateString('pt-BR')
      : 'Disponibilidade imediata / Sob consulta';
    document.getElementById('detalhe-data').textContent = dataFormatada;

    const imgEl = document.getElementById('detalhe-imagem');
    imgEl.src = curso.imagemApresentacao;
    imgEl.alt = `Apresentação do curso ${curso.nomeCompleto}`;

    const listaModulos = document.getElementById('detalhe-modulos-lista');
    if (curso.modulos && curso.modulos.length > 0) {
      listaModulos.innerHTML = curso.modulos
        .map(
          (m) => `
          <div class="module-box">
            <strong>Módulo ${m.ordem}: ${m.titulo}</strong>
            <p>${m.descricaoAulas}</p>
          </div>
        `
        )
        .join('');
    } else {
      listaModulos.innerHTML = '<p>Conteúdo programático em atualização.</p>';
    }

    viewHome.hidden = true;
    viewDetalhes.hidden = false;
    window.scrollTo({ top: 0, behavior: 'smooth' });
  } catch (err) {
    console.error('Erro ao abrir detalhes do curso:', err);
  }
}

function voltarParaCatalogo() {
  viewDetalhes.hidden = true;
  viewHome.hidden = false;
  document.getElementById('secao-cursos').scrollIntoView({ behavior: 'smooth' });
}

function limparFiltros() {
  inputPesquisa.value = '';
  selectCategoria.value = '';
  selectModalidade.value = '';
  selectStatus.value = '';
  buscarCursos();
}

function alternarEstado(estado) {
  estadoCarregamento.hidden = estado !== 'carregamento';
  estadoErro.hidden = estado !== 'erro';
  estadoVazio.hidden = estado !== 'vazio';
  gradeCursos.hidden = estado !== 'lista';
}

// Eventos de Filtro e Navegação
inputPesquisa.addEventListener('input', () => {
  clearTimeout(debounceTimer);
  debounceTimer = setTimeout(buscarCursos, 280);
});

selectCategoria.addEventListener('change', buscarCursos);
selectModalidade.addEventListener('change', buscarCursos);
selectStatus.addEventListener('change', buscarCursos);

btnLimparFiltros.addEventListener('click', limparFiltros);
btnLimparVazio.addEventListener('click', limparFiltros);
btnTentarNovamente.addEventListener('click', buscarCursos);

btnVerTodos.addEventListener('click', () => {
  exibindoTodosEmGrade = !exibindoTodosEmGrade;
  gradeCursos.classList.toggle('show-all-grid', exibindoTodosEmGrade);
  btnVerTodos.textContent = exibindoTodosEmGrade ? 'Exibir em carrossel' : 'Ver todos os cursos';
});

document.getElementById('btn-scroll-prev').addEventListener('click', () => {
  gradeCursos.scrollBy({ left: -300, behavior: 'smooth' });
});

document.getElementById('btn-scroll-next').addEventListener('click', () => {
  gradeCursos.scrollBy({ left: 300, behavior: 'smooth' });
});

document.getElementById('btn-voltar-catalogo').addEventListener('click', voltarParaCatalogo);
document.getElementById('link-logo').addEventListener('click', (e) => {
  e.preventDefault();
  viewDetalhes.hidden = true;
  viewHome.hidden = false;
  window.scrollTo({ top: 0, behavior: 'smooth' });
});

document.querySelectorAll('[data-nav]').forEach((link) => {
  link.addEventListener('click', () => {
    viewDetalhes.hidden = true;
    viewHome.hidden = false;
  });
});

init();