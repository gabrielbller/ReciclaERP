using ERP.Reciclagem.Comercial.Domain.Proposals;
using ERP.Reciclagem.Comercial.Domain.Repositories;

namespace ERP.Reciclagem.Comercial.Infrastructure.Persistence;

public sealed class InMemoryProposalRepository : IProposalRepository
{
    private readonly Dictionary<Guid, Proposal> _store = new();

    public Proposal GetById(Guid id)
    {
        if (!_store.TryGetValue(id, out var proposal))
        {
            throw new KeyNotFoundException($"Proposal {id} not found.");
        }

        return proposal;
    }

    public void Save(Proposal proposal)
    {
        _store[proposal.Id] = proposal;
    }
}

