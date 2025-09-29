using System.Collections;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.HashCodes.Tests.Fakes;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests;

public sealed record StoredTableDataSetHashTests
{
    [Fact]
    public void RowOrderNotMatters()
    {
        FakeTableDataset fakeDataset = new FakeTableDataset();
        IQueryable<IRow> rows = fakeDataset;
        Assert.Equal(
            "4820A248E2710C9A33E9617E56FF65E543676849F9039047B36786BCEE71F4DF",
            Convert.ToHexString(
                [
                    .. new StoredTableDataSetHash(
                        new FakeTableDataset(fakeDataset.TableSchema, rows.Reverse())
                    ),
                ]
            )
        );
    }

    [Fact]
    public void Determined()
    {
        Assert.Equal(
            "4820A248E2710C9A33E9617E56FF65E543676849F9039047B36786BCEE71F4DF",
            Convert.ToHexString([.. new StoredTableDataSetHash(new FakeTableDataset())])
        );
    }

    [Fact]
    public void DeterminedOnEmptyRows()
    {
        Assert.Equal(
            "A7DCEDA5178287ED5FE3A184915150D0BBD91876E6EFC323E8242AD8F9DBF2B5",
            Convert.ToHexString([.. new StoredTableDataSetHash(new FakeTableDataset([]))])
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable hash = new StoredTableDataSetHash(new FakeTableDataset());

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
            new StoredTableDataSetHash(new FakeTableDataset()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredTableDataSetHash(new FakeTableDataset()).ToString()
        );
    }
}
