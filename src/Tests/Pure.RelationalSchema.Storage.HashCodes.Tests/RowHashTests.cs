using System.Collections;
using Pure.Collections.Generic;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests;

public sealed record RowHashTests
{
    [Fact]
    public void OrderNotMatters()
    {
        IEnumerable<KeyValuePair<IColumn, ICell>> cells =
        [
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column1"), new StringColumnType()),
                new Cell(new String("Column1_Value"))
            ),
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column2"), new IntColumnType()),
                new Cell(new String("1"))
            ),
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column3"), new DateTimeColumnType()),
                new Cell(new String("15.07.2021 14:30:00"))
            ),
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column4"), new DateColumnType()),
                new Cell(new String("15.07.2021"))
            ),
        ];

        Assert.Equal(
            "4BE7E929C917E2D1B7E2F8193E667EE50C1877F73BB22EBC792F1C20363BAA33",
            Convert.ToHexString(
                [
                    .. new RowHash(
                        new Row(
                            new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                                cells.Reverse(),
                                x => x.Key,
                                x => x.Value,
                                x => new ColumnHash(x)
                            )
                        )
                    ),
                ]
            )
        );
    }

    [Fact]
    public void Determined()
    {
        IEnumerable<KeyValuePair<IColumn, ICell>> cells =
        [
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column1"), new StringColumnType()),
                new Cell(new String("Column1_Value"))
            ),
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column2"), new IntColumnType()),
                new Cell(new String("1"))
            ),
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column3"), new DateTimeColumnType()),
                new Cell(new String("15.07.2021 14:30:00"))
            ),
            new KeyValuePair<IColumn, ICell>(
                new Column.Column(new String("Column4"), new DateColumnType()),
                new Cell(new String("15.07.2021"))
            ),
        ];

        Assert.Equal(
            "4BE7E929C917E2D1B7E2F8193E667EE50C1877F73BB22EBC792F1C20363BAA33",
            Convert.ToHexString(
                [
                    .. new RowHash(
                        new Row(
                            new Dictionary<KeyValuePair<IColumn, ICell>, IColumn, ICell>(
                                cells,
                                x => x.Key,
                                x => x.Value,
                                x => new ColumnHash(x)
                            )
                        )
                    ),
                ]
            )
        );
    }

    [Fact]
    public void DeterminedOnEmpty()
    {
        Assert.Equal(
            "B12BB80EC5C589B317C568BA69253D732E0206DB93ED24D42EEF1CDA51478FF8",
            Convert.ToHexString(
                [.. new RowHash(new Row(new Dictionary<IColumn, ICell>()))]
            )
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable hash = new RowHash(new Row(new Dictionary<IColumn, ICell>()));

        int count = 0;

        foreach (object item in hash)
        {
            count++;
        }

        Assert.Equal(32, count);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RowHash(new Row(new Dictionary<IColumn, ICell>())).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new RowHash(new Row(new Dictionary<IColumn, ICell>())).ToString()
        );
    }
}
