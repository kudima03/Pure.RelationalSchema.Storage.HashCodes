using System.Collections;
using Pure.HashCodes;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.HashCodes;

public sealed record StoredSchemaDataSetHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        132,
        148,
        153,
        1,
        24,
        111,
        215,
        118,
        162,
        181,
        17,
        37,
        133,
        14,
        26,
        37,
    ];

    private readonly IStoredSchemaDataSet _storedSchemaDataset;

    public StoredSchemaDataSetHash(IStoredSchemaDataSet storedSchemaDataset)
    {
        _storedSchemaDataset = storedSchemaDataset;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix.Concat(
                new DeterminedHash(
                    _storedSchemaDataset.TablesDatasets.Select(
                        dataset => new DeterminedHash(
                            new TableHash(dataset.Key).Concat(
                                new StoredTableDataSetHash(dataset.Value)
                            )
                        )
                    )
                )
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
