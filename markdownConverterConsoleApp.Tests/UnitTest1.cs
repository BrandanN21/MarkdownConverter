namespace markdownConverterConsoleApp.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void GetMarkdown_Successful()
    {
        //arrange
        var file = @"../../../Files/markdown";
        //act
        var result = MarkdownConverter.GetMarkdown(file);

        //assert
        Assert.IsNotNull(result.Result);
    }

    [TestMethod]
    public void GetMarkdown_Failure()
    {
        //arrange
        var file = @"../../../Files/markd";

        //act
        var result = MarkdownConverter.GetMarkdown(file);
        //assert
        //not sure how to write this assert to evaluate if exception was thrown..
        Assert.Fail();   
    }

    [TestMethod]
    public void ParseMarkdown_Successful()
    {
        //arrange
        string[] line = { "## Header 2"};
        string expected = "<h2>Header 2</h2>";

        //act
        var result = MarkdownConverter.ParseMarkdown(line);

        //assert
        Assert.AreEqual(expected, result[0]);
    }

    [TestMethod]
    public void CheckMultiline_Successful()
    {
        //timboxed to 4ish hours
        //arrange
        

        //act
       

        //assert
      
    }
}
