﻿using vendinha_backend.Interfaces;

namespace vendinha_backend.Repository.Implementations
{
    public class RepositoryInMemory : IDisposable
    {
        private static long contador = 10000;

        public static Dictionary<string, List<object>> Dados =
            new Dictionary<string, List<object>>();
        public IQueryable<T> Consultar<T>()
        {
            var nomeEntidade = typeof(T).Name;
            if (!Dados.ContainsKey(nomeEntidade))
            {
                return new List<T>().AsQueryable();
            }
            return Dados[nomeEntidade]
                .Cast<T>()
                .AsQueryable();
        }

        public T ConsultarPorId<T>(long id)
        {
            var nomeEntidade = typeof(T).Name;
            if (!Dados.ContainsKey(nomeEntidade))
                return default;
            var entidade = Dados[nomeEntidade].
                FirstOrDefault(
                    e => (e as IEntidade).Id == id
                );
            return (T)entidade;
        }

        public void Excluir(object model)
        {
            GetList(model).Remove(model);
        }

        public void Incluir(object model)
        {
            var entidade = model as IEntidade;
            entidade.Id = contador++;

            GetList(model).Add(model);
        }

        private List<object> GetList(object model)
        {
            var nomeEntidade = model.GetType().Name;
            if (!Dados.ContainsKey(nomeEntidade))
            {
                Dados[nomeEntidade] = new List<object>();
            }
            return Dados[nomeEntidade];
        }


        public void Salvar(object model)
        {
        }

        public IDisposable IniciarTransacao()
        {
            return this;
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
        }

        public void Dispose()
        {
            Console.WriteLine("Transação finalizada com sucesso.");
        }
    }
}
