using System.Collections;
using Pure.RelationalSchema.Storage.HashCodes.Tests.Fakes;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests;

public sealed record StoredSchemaDataSetHashTests
{
    [Fact]
    public void Determined()
    {
        Assert.Equal(
            "105819F00941DDCF78E2D9CC6B85E6E9F672001110DAF15417BECE0CF5FBF56A",
            Convert.ToHexString([.. new StoredSchemaDataSetHash(new FakeSchemaDataset())])
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable hash = new StoredSchemaDataSetHash(new FakeSchemaDataset());

        int count = 0;

        foreach (object _ in hash)
        {
            count++;
        }

        Assert.Equal(32, count);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredSchemaDataSetHash(new FakeSchemaDataset()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new StoredSchemaDataSetHash(new FakeSchemaDataset()).ToString()
        );
    }
}
