using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Pure.Collections.Generic;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Table;
using Pure.RelationalSchema.HashCodes;
using Pure.RelationalSchema.Storage.Abstractions;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests.Fakes;

internal sealed record FakeSchemaDataset : IStoredSchemaDataSet
{
    private readonly IReadOnlyDictionary<ITable, IStoredTableDataSet> _tableDatasets;

    public FakeSchemaDataset()
        : this(
            new Schema.Schema(
                new String("Test"),
                [new FakeTableDataset().TableSchema],
                []
            )
        )
    { }

    public FakeSchemaDataset(ISchema schema)
        : this(
            schema,
            new Dictionary<ITable, ITable, IStoredTableDataSet>(
                schema.Tables,
                x => x,
                x => new FakeTableDataset(x),
                x => new TableHash(x)
            )
        )
    { }

    public FakeSchemaDataset(
        ISchema schema,
        IReadOnlyDictionary<ITable, IStoredTableDataSet> tableDatasets
    )
    {
        _tableDatasets = tableDatasets;
        Schema = schema;
    }

    public IStoredTableDataSet this[ITable key] => _tableDatasets[key];

    public IEnumerable<ITable> Keys => _tableDatasets.Keys;

    public IEnumerable<IStoredTableDataSet> Values => _tableDatasets.Values;

    public ISchema Schema { get; }

    public int Count => _tableDatasets.Count;

    public IEnumerator<KeyValuePair<ITable, IStoredTableDataSet>> GetEnumerator()
    {
        return _tableDatasets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool ContainsKey(ITable key)
    {
        return _tableDatasets.ContainsKey(key);
    }

    public bool TryGetValue(
        ITable key,
        [MaybeNullWhen(false)] out IStoredTableDataSet value
    )
    {
        return _tableDatasets.TryGetValue(key, out value);
    }
}
