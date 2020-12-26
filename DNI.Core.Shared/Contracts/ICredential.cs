using System;

namespace DNI.Core.Shared.Contracts
{
    public interface ICredential<TKey>
    {
        TKey Id { get; }
        bool IsMaster { get; }
        string PassPhrase { get; }
        DateTimeOffset? LastAccessed { get; set; }
    }
}
