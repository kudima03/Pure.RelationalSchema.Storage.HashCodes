using System.Collections;
using Pure.Primitives.String;
using String = Pure.Primitives.String.String;

namespace Pure.RelationalSchema.Storage.HashCodes.Tests;

public sealed record CellHashTests
{
    [Fact]
    public void Determined()
    {
        Assert.Equal(
            "F36353B82720379B6E942870062217530FDAA90C16DE9D2EC4A386A273A9AE0A",
            Convert.ToHexString([.. new CellHash(new Cell(new String("Test value")))])
        );
    }

    [Fact]
    public void DeterminedOnEmpty()
    {
        Assert.Equal(
            "656A50E856562706FE2B42BDA2B0517374B864DA27FA05B1AEF184F2DE28B1DB",
            Convert.ToHexString([.. new CellHash(new Cell(new EmptyString()))])
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IEnumerable hash = new CellHash(new Cell(new EmptyString()));

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
            new CellHash(new Cell(new EmptyString())).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new CellHash(new Cell(new EmptyString())).ToString()
        );
    }
}
