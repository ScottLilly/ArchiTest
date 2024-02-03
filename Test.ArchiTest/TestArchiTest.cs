using SampleProject;

namespace Test.ArchiTest;

public class TestArchiTest
{
    [Fact]
    public void Test_Assemblies_SpecificName1()
    {
        var assemblies = Verify.InAssemblies("ArchiTest");
        Assert.Single(assemblies);
    }

    [Fact]
    public void Test_Assemblies_SpecificName2()
    {
        var assemblies = Verify.InAssemblies("ArchiTest.SampleProject");
        Assert.Single(assemblies);
    }

    [Fact]
    public void Test_Assemblies_SpecificNames()
    {
        var assemblies = Verify.InAssemblies("ArchiTest", "ArchiTest.SampleProject");
        Assert.Equal(2, assemblies.Count());
    }

    [Fact]
    public void Test_Assemblies_Wildcard_Single()
    {
        var assemblies = Verify.InAssemblies("ArchiTest.*");
        Assert.Single(assemblies);
    }

    [Fact]
    public void Test_Assemblies_Wildcard_Double()
    {
        var assemblies = Verify.InAssemblies("ArchiTest*");
        Assert.Equal(2, assemblies.Count());
    }

    [Fact]
    public void Test_Assemblies_NoMatch()
    {
        var assemblies = Verify.InAssemblies("iojwfoiej390248230948");
        Assert.Empty(assemblies);
    }
}