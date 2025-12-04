using ERP.Reciclagem.Comercial.Domain.Proposals;

namespace ERP.Reciclagem.Comercial.Domain.Repositories;

public interface IProposalRepository
{
    Proposal GetById(Guid id);
    void Save(Proposal proposal);
}

