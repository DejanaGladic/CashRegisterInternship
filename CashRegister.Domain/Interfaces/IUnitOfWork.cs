namespace CashRegister.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IBillRepository BillRepository { get; }
        IProductBillRepository ProductBillsRepository { get; }

        int Save();
    }
}
