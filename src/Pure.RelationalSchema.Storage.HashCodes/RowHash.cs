using System.Collections;
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;

namespace Pure.RelationalSchema.Storage.HashCodes;

public sealed record RowHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        101,
        145,
        153,
        1,
        63,
        200,
        87,
        116,
        181,
        38,
        218,
        14,
        226,
        12,
        19,
        178,
    ];

    private readonly IRow _row;

    public RowHash(IRow row)
    {
        _row = row;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix.Concat(
                new DeterminedHash(
                    _row.Cells.Select(cell => new DeterminedHash(
                        new ColumnHash(cell.Key).Concat(new CellHash(cell.Value))
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
