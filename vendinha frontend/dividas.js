async function carregarDividas() {
  try {
    const resposta = await fetch("http://localhost:5000/api/divida");

    if (!resposta.ok) throw new Error("Erro ao buscar dívidas");

    const dividas = await resposta.json();
    renderizarTabela(dividas);
    calcularSomaPorCliente(dividas);

  } catch (erro) {
    console.error(erro);
  }
}

function renderizarTabela(dividas) {
  const corpo = document.getElementById("lista-dividas");
  corpo.innerHTML = "";

  dividas.forEach(d => {
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td>${d.nomeCliente}</td>
      <td>${d.descricao}</td>
      <td>R$ ${parseFloat(d.valorTotal).toFixed(2)}</td>
      <td>${d.situacao}</td>
      <td>
        ${d.situacao === "pendente"
          ? `<button onclick="marcarComoPaga(${d.id})">Marcar como paga</button>`
          : `<span>✓</span>`}
      </td>
    `;
    corpo.appendChild(tr);
  });
}

function calcularSomaPorCliente(dividas) {
  const lista = document.getElementById("soma-clientes");
  lista.innerHTML = "";

  const somaPorCliente = {};

  dividas.forEach(d => {
    if (!somaPorCliente[d.nomeCliente]) {
      somaPorCliente[d.nomeCliente] = 0;
    }
    somaPorCliente[d.nomeCliente] += parseFloat(d.valorTotal);
  });

  Object.entries(somaPorCliente).forEach(([nome, total]) => {
    const li = document.createElement("li");
    li.innerText = `${nome}: R$ ${total.toFixed(2)}`;
    lista.appendChild(li);
  });
}

async function marcarComoPaga(id) {
  try {
    const resposta = await fetch(`http://localhost:5000/api/divida/${id}/pagar`, {
      method: "PUT"
    });

    if (resposta.ok) {
      alert("Dívida marcada como paga!");
      carregarDividas();
    } else {
      alert("Erro ao atualizar dívida.");
    }
  } catch (erro) {
    console.error("Erro:", erro);
  }
}

document.addEventListener("DOMContentLoaded", carregarDividas);
