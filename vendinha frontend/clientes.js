function atualizarTabela() {
  const termo = document.getElementById("buscaNome").value.toLowerCase();
  const clientesFiltrados = clientes.filter(c => c.nome.toLowerCase().includes(termo));

  const inicio = (paginaAtual - 1) * itensPorPagina;
  const fim = inicio + itensPorPagina;
  const pagina = clientesFiltrados.slice(inicio, fim);

  const corpo = document.getElementById("lista-clientes");
  corpo.innerHTML = "";

  const idade = calcularIdade(c.dataNascimento);

  tr.innerHTML = `
    <td>${c.nome}</td>
    <td>${c.cpf}</td>
    <td>R$ ${c.totalDividas.toFixed(2)}</td>
    <td>${idade} anos</td>
  `;
  corpo.appendChild(tr);
};

  // Mostra os clientes da página atual
  pagina.forEach(c => {
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td>${c.nome}</td>
      <td>${c.cpf}</td>
      <td>R$ ${c.totalDividas.toFixed(2)}</td>
    `;
    corpo.appendChild(tr);
  });
  const totalGeral = clientes.reduce((soma, c) => soma + c.totalDividas, 0);

  document.getElementById("valor-total").innerText = totalGeral.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  }).replace("R$", "");
  
  atualizarPaginacao(clientesFiltrados.length);

  function calcularIdade(dataNasc) {
  if (!dataNasc) return "-";
  const hoje = new Date();
  const nasc = new Date(dataNasc);
  let idade = hoje.getFullYear() - nasc.getFullYear();
  const m = hoje.getMonth() - nasc.getMonth();
  if (m < 0 || (m === 0 && hoje.getDate() < nasc.getDate())) {
    idade--;
  }
  return idade;
}
