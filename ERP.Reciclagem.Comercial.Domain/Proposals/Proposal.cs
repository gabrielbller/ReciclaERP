using System.Collections.ObjectModel;

namespace ERP.Reciclagem.Comercial.Domain.Proposals;

public sealed class Proposal
{
    private readonly IList<ProposalItem> _items = new List<ProposalItem>();

    public Guid Id { get; }
    public Guid CustomerId { get; }
    public DateTime CreatedAt { get; }
    public DateTime ValidUntil { get; private set; }
    public ProposalStatus Status { get; private set; }
    public IReadOnlyCollection<ProposalItem> Items => new ReadOnlyCollection<ProposalItem>(_items);
    public decimal Total => _items.Sum(item => item.Total);

    public Proposal(Guid customerId, DateTime createdAt, DateTime validUntil)
    {
        if (validUntil <= createdAt)
        {
            throw new ArgumentException("Validity must be after creation date.", nameof(validUntil));
        }

        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = createdAt;
        ValidUntil = validUntil;
        Status = ProposalStatus.Draft;
    }

    public void AddItem(ProposalItem item)
    {
        _items.Add(item);
    }

    public void MarkAsSent()
    {
        Status = ProposalStatus.Sent;
    }

    public bool IsExpiredAt(DateTime referenceDate)
    {
        return referenceDate > ValidUntil;
    }

    public void MarkAsExpired()
    {
        Status = ProposalStatus.Expired;
    }

    public void ExtendValidity(DateTime newValidUntil)
    {
        if (newValidUntil <= CreatedAt)
        {
            throw new ArgumentException("New validity must be after creation date.", nameof(newValidUntil));
        }

        ValidUntil = newValidUntil;
    }

    public void ReplaceItems(IEnumerable<ProposalItem> items)
    {
        _items.Clear();
        foreach (var item in items)
        {
            _items.Add(item);
        }
    }
}

