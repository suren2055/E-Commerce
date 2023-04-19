using E_Commerce.Payment.Concrete;

namespace E_Commerce.Payment.Repositories;

public class PaymentRepository : BaseRepository<Entities.Payment>, IPaymentRepository
{
    
    public PaymentRepository(EFDBContext context) : base(context)
    {
    }
}