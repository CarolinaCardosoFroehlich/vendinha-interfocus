namespace vendinha_backend.Repository
{
    public interface IRepository
    {
        void Salvar(object model);
        void Excluir(object model);
        T ConsultarPorId<T>(long id);
        IQueryable<T> Consultar<T>();

        IDisposable IniciarTransacao();
        void Commit();
        void Rollback();

    }
}
