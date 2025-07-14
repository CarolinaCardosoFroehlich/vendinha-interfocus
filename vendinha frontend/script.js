function mostrarFormularioCliente() {
  fecharFormularios();
  document.getElementById('formCliente').classList.remove('oculto');
}
function mostrarFormularioDivida() {
  fecharFormularios();
  document.getElementById('formDivida').classList.remove('oculto');
}
function mostrarFormularioPagamento() {
  fecharFormularios();
  document.getElementById('formPagamento').classList.remove('oculto');
}
function fecharFormularios() {
  document.getElementById('formCliente').classList.add('oculto');
  document.getElementById('formDivida').classList.add('oculto');
  document.getElementById('formPagamento').classList.add('oculto');
}

async function salvarCliente(event) {
  event.preventDefault();

  const cliente = {
    nome: document.getElementById('nome').value,
    cpf: document.getElementById('cpf').value,
    email: document.getElementById('email').value,
    dataNascimento: document.getElementById('dataNascimento').value
  };

  try {
    const resposta = await fetch("http://localhost:5221", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(cliente)
    });

    if (resposta.ok) {
      alert("Cliente salvo!");
      fecharFormularios();
      event.target.reset();
      carregarClientes();
    } else {
      alert("Erro ao salvar cliente");
    }
  } catch (erro) {
    console.error("Erro:", erro);
  }
}

let todosClientes = [];
let paginaAtual = 0;
const clientesPorPagina = 10;

async function carregarClientes() {
  try {
    const resposta = await fetch("http://localhost:5221");
    if (resposta.ok) {
      todosClientes = await resposta.json();
      exibirClientes();
    } else {
      console.error("Erro ao carregar clientes");
    }
  } catch (erro) {
    console.error("Erro ao carregar clientes:", erro);
  }
}

function exibirClientes() {
  const busca = inputBusca.value.toLowerCase();
  const filtrados = todosClientes.filter(c => c.nome.toLowerCase().includes(busca));

  const inicio = paginaAtual * clientesPorPagina;
  const fim = inicio + clientesPorPagina;
  const pagina = filtrados.slice(inicio, fim);

  listaClientes.innerHTML = "";
  pagina.forEach(cliente => {
    const div = document.createElement("div");
    div.className = "cliente";
    div.textContent = `${cliente.nome} — R$ ${cliente.valor.toFixed(2)}`;
    listaClientes.appendChild(div);
  });

  btnAnterior.disabled = paginaAtual === 0;
  btnProximo.disabled = fim >= filtrados.length;
}

inputBusca.addEventListener("input", () => {
  paginaAtual = 0;
  exibirClientes();
});

btnAnterior.addEventListener("click", () => {
  if (paginaAtual > 0) {
    paginaAtual--;
    exibirClientes();
  }
});

btnProximo.addEventListener("click", () => {
  paginaAtual++;
  exibirClientes();
});

document.addEventListener("DOMContentLoaded", () => {
  carregarClientes();
  carregarDividas();
});
