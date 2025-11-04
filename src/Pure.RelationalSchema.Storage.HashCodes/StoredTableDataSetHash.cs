using System.Collections;
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.HashCodes;

public sealed record StoredTableDataSetHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        96,
        148,
        153,
        1,
        104,
        241,
        216,
        118,
        172,
        38,
        66,
        11,
        180,
        175,
        181,
        106,
    ];

    private readonly IStoredTableDataSet _storedTableDataset;

    public StoredTableDataSetHash(IStoredTableDataSet storedTableDataset)
    {
        _storedTableDataset = storedTableDataset;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix
                .Concat(new TableHash(_storedTableDataset.TableSchema))
                .Concat(
                    new DeterminedHash(_storedTableDataset.Select(x => new RowHash(x)))
                )
        ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
