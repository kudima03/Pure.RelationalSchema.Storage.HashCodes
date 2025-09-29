using System.Collections;
using System.Linq.Expressions;
using Pure.Collections.Generic;
using Pure.RelationalSchema.Abstractions.Column;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.ColumnType;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests.Fakes;

internal sealed record FakeTableDataset : IStoredTableDataSet
{
    private readonly IQueryable<IRow> _rows;

    public FakeTableDataset()
        : this(
            new Table.Table(
                new String("Table"),
                [
                    new Column.Column(new String("Column1"), new StringColumnType()),
                    new Column.Column(new String("Column2"), new LongColumnType()),
                    new Column.Column(new String("Column3"), new IntColumnType()),
                    new Column.Column(new String("Column4"), new UIntColumnType()),
                    new Column.Column(new String("Column5"), new ULongColumnType()),
                ],
                []
            )
        )
    { }

    public FakeTableDataset(ITable table)
        : this(
            table,
            Enumerable
                .Range(0, 100)
                .Select(c => new Row(
                    new Dictionary<IColumn, IColumn, ICell>(
                        table.Columns,
                        x => x,
                        x => new Cell(new String($"Value {c}")),
                        x => new ColumnHash(x)
                    )
                ))
                .AsQueryable()
        )
    { }

    public FakeTableDataset(IEnumerable<IRow> rows)
        : this(
            new Table.Table(
                new String("Table"),
                [
                    new Column.Column(new String("Column1"), new StringColumnType()),
                    new Column.Column(new String("Column2"), new LongColumnType()),
                    new Column.Column(new String("Column3"), new IntColumnType()),
                    new Column.Column(new String("Column4"), new UIntColumnType()),
                    new Column.Column(new String("Column5"), new ULongColumnType()),
                ],
                []
            ),
            rows.AsQueryable()
        )
    { }

    public FakeTableDataset(ITable table, IQueryable<IRow> rows)
    {
        TableSchema = table;
        _rows = rows;
    }

    public IEnumerator<IRow> GetEnumerator()
    {
        return _rows.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ITable TableSchema { get; }

    public Type ElementType => _rows.ElementType;

    public Expression Expression => _rows.Expression;

    public IQueryProvider Provider => _rows.Provider;

    public IAsyncEnumerator<IRow> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        return _rows.ToAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
    }
}
