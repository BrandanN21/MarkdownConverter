using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public class MarkdownConverter
{

    public static void Main()
    {
        MainAsync().Wait();

    }

    static async Task MainAsync()
    {
        var file = @"/users/brandannaef/developer/mailchimp-interview/markdown";
        try
        {
            var lines = await GetMarkdown(file);
            var parsed = ParseMarkdown(lines);
            var x = parsed.ToArray();
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:" + ex.Message);
        }
    }


    public async static Task<string[]> GetMarkdown(string file)
    {
        string[] lines = Array.Empty<string>();
        try
        {
            string fileName = file;
            lines = await File.ReadAllLinesAsync(fileName);
            return lines;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }

    }

    public static string[] ParseMarkdown(string[] lines)
    {
        var x = lines.ToArray();
        for (int i = 0; i < x.Length; i++)
        {
            //check if it is a header
            if (x[i].StartsWith('#'))
            {
                // counts the #'s
                var count = Regex.Matches(x[i], "#").Count();
                //clean out the #'s
                var cleanedString = CheckHeaders(x[i]);
                //check for links 
                cleanedString = CheckForLinks(cleanedString);
                //add header tags
                x[i] = $"<h{count}>{cleanedString}</h{count}>";
                //completed up to here
            }
            //enters this logic if not empty line
            else if (x[i] != "")
            {
                var multi = CheckForMultiLine(x[i], x[i-1]);
                if (multi.isTrue)
                {
                    //this leaves blank line, would want to refactor
                    x[i - 1] = "";
                }

                var cleanedString = CheckForLinks(multi.str);
                x[i] = $"<p>{cleanedString}</p>";
            }

        }
        return x;
    }

    public static (String str, bool isTrue) CheckForMultiLine(String str, String prevStr)
    {
        if(prevStr.StartsWith("<p>"))
        {
            prevStr = prevStr.Remove(prevStr.Length - 4);
            prevStr = prevStr.Substring(3);
            var newStr = prevStr + "\n" + str;
            return (newStr, true);
        }
        return (str, false);
    }

    public static String CheckHeaders(String str)
    {
        Regex regex = new Regex(@"#");
        var cleaned = regex.Replace(str, "").Trim();
        return cleaned;
    }

    public static String CheckForLinks(String str)
    {
        var link = Regex.Match(str, @"\[([^]]*)\]").Groups[1].Value;
        var site = Regex.Match(str, @"\(([^]]*)\)").Groups[1].Value;
        var newLink = $"<a href=\"{site}\">{link}</a>";
        str = Regex.Replace(str, @"\(([^]]*)\)", "");
        str = Regex.Replace(str, @"\[([^]]*)\]", newLink);
        return str;
    }
}
