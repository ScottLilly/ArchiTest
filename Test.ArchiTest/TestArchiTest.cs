using SampleProject;

namespace Test.ArchiTest;

public class TestArchiTest
{
    [Fact]
    public void Test_Assemblies_SpecificName()
    {
        var assemblies = Verify.That().Assemblies("ArchiTest.SampleProject");
        Assert.Single(assemblies);
    }

    [Fact]
    public void Test_Assemblies_Wildcard_Single()
    {
        var assemblies = Verify.That().Assemblies("ArchiTest.*");
        Assert.Single(assemblies);
    }

    [Fact]
    public void Test_Assemblies_Wildcard_Double()
    {
        var assemblies = Verify.That().Assemblies("ArchiTest*");
        Assert.Equal(2, assemblies.Count());
    }
}