using System.Collections;
using Pure.Collections.Generic;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;
using Pure.RelationalSchema.Storage.HashCodes.Tests.Fakes;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests;

public sealed record StoredSchemaDataSetHashTests
{
    [Fact]
    public void Determined()
    {
        FakeTableDataset source = new FakeTableDataset();

        IEnumerable<IStoredTableDataSet> tableDatasets = Enumerable
            .Range(0, 5)
            .Select(x => new FakeTableDataset(
                new Table.Table(
                    source.TableSchema.Name,
                    source.TableSchema.Columns.Take(x),
                    source.TableSchema.Indexes
                ),
                source.AsQueryable().Take(x)
            ));

        Assert.Equal(
            "292DC2E398FB5B0517DC8B858643F6754795098E29A4620DEC17DA29FF3E13CD",
            Convert.ToHexString(
                [
                    .. new StoredSchemaDataSetHash(
                        new StoredSchemaDataset(
                            new Dictionary<
                                IStoredTableDataSet,
                                ITable,
                                IStoredTableDataSet
                            >(
                                tableDatasets,
                                x => x.TableSchema,
                                x => x,
                                x => new TableHash(x)
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
            "AC1EA91D0A53A0BDD8A83CC494DF6592F8EF72E5D35CEE7DE361906429D3910B",
            Convert.ToHexString(
                [
                    .. new StoredSchemaDataSetHash(
                        new StoredSchemaDataset(
                            new Dictionary<ITable, IStoredTableDataSet>()
                        )
                    ),
                ]
            )
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable hash = new StoredSchemaDataSetHash(
            new StoredSchemaDataset(new Dictionary<ITable, IStoredTableDataSet>())
        );

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
            new StoredSchemaDataSetHash(
                new StoredSchemaDataset(new Dictionary<ITable, IStoredTableDataSet>())
            ).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredSchemaDataSetHash(
                new StoredSchemaDataset(new Dictionary<ITable, IStoredTableDataSet>())
            ).ToString()
        );
    }
}
