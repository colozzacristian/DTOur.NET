namespace DTOur.Tests;

using DTOur;
using DTOur.Attributes;

public class MappingTests
{
    [Fact]
    public void MapBack_Works()
    {
        TestDto td = new();
        DtoMapper<TestClass> mapper = new();
        td.Numero = 3;
        var a = mapper.MapBack<TestDto>(td);
        Assert.Equal(3, a.Number);
    }
}
class TestClass
{
    public int Number { get; set; }
}
class TestDto
{
    [MapTo("Number")]
    public int Numero { get; set; }
}
