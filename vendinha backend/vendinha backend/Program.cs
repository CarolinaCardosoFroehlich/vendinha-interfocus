using System;
using System.Collections.Generic;
using vendinha_backend;
using vendinha_backend.Models;
using vendinha_backend.Services;
using vendinha_backend.Repository.Implementations;
using vendinha_backend.Interfaces;

class Program
{
    static void Main()
    {
        var servico = new ClienteService(new RepositoryInMemory());
        var servico2 = new ClienteService(new RepositoryInMemory());
        var servico3 = new ClienteService(new RepositoryInMemory());
        var servico4 = new ClienteService(new RepositoryInMemory());

        Metodos.PrintNome(new Cliente());

        while (true)
        {
            Console.WriteLine("1 - cadastrar");
            Console.WriteLine("2 - listar");
            Console.WriteLine("3 - pesquisar");
            Console.Write("Opção: ");
            var opcao = LerInteiro();

            switch (opcao)
            {
                case 1:
                    Console.Write("Nome: ");
                    var nome = Console.ReadLine();

                    Console.Write("Código: ");
                    var codigo = LerInteiro();

                    Console.Write("CPF: ");
                    var cpf = Console.ReadLine();

                    Console.Write("Email: ");
                    var email = Console.ReadLine();

                    var novoCliente = new Cliente
                    {
                        Nome = nome,
                        Id = codigo,
                        Cpf = cpf,
                        Email = email
                    };

                    servico.Adicionar(novoCliente);

                    if (servico.Cadastrar(novoCliente, out _))
                    {
                        Console.WriteLine("Cliente cadastrado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Dados inválidos para cadastrar aluno");
                    }
                    break;

                case 2:
                    Metodos.PrintarLista(servico4.ConsultarClientesOrdenadosPorDivida(), "Cliente: ");
                    break;

                case 3:
                    var pesquisa = Console.ReadLine();
                    var resultado = servico2.Consultar(pesquisa);
                    Metodos.PrintarLista(resultado, true);
                    break;
                case 4:
                    Console.Write("Código: ");
                    var codigoBusca = LerInteiro();

                    var clientePesquisado = servico3.ConsultarPorCodigo(codigoBusca);

                    if (clientePesquisado == null)
                    {
                        Console.WriteLine("Aluno não encontrado");
                    }
                    else
                    {
                        Console.Write("Nome: ");
                        var novoNome = Console.ReadLine();
                        var oldNome = clientePesquisado.Nome;
                        clientePesquisado.Nome = novoNome;
                        if (!ClienteService.Validar(clientePesquisado, out _))
                        {
                            clientePesquisado.Nome = oldNome;
                        }
                        Console.WriteLine(clientePesquisado.DataCadastro);
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static int LerInteiro()
    {
        while (true)
        {
            var entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int resultado))
            {
                return resultado;
            }
            else
            {
                Console.WriteLine("Entrada inválida, digite um número inteiro:");
            }
        }
    }
}

class Metodos
{
    public static void PrintarLista(List<Cliente> lista, string prefix = "")
    {
        foreach (var item in lista)
        {
            Console.WriteLine(prefix + item.GetPrintMessage());
        }
    }

    public static void PrintarLista(List<Cliente> clientes, bool header)
    {
        Console.WriteLine("codigo - nome - cpf - email - divida");
        PrintarLista(clientes);
    }

    public static void PrintNome(INomeavel objeto)
    {
        Console.WriteLine(objeto.Nome);
    }
}
