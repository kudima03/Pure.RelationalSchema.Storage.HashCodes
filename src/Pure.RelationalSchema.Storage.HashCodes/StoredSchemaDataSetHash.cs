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
            TypePrefix
                .Concat(new SchemaHash(_storedSchemaDataset.Schema))
                .Concat(
                    new DeterminedHash(
                        _storedSchemaDataset.Select(pair => new DeterminedHash(
                            new TableHash(pair.Key).Concat(
                                new StoredTableDataSetHash(pair.Value)
                            )
                        ))
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
