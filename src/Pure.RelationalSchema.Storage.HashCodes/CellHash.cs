using System.Collections;
using Pure.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.HashCodes;

public sealed record CellHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        176,
        144,
        153,
        1,
        188,
        145,
        5,
        122,
        167,
        254,
        235,
        192,
        138,
        25,
        68,
        171,
    ];

    private readonly ICell _cell;

    public CellHash(ICell cell)
    {
        _cell = cell;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix.Concat(new DeterminedHash(_cell.Value))
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
