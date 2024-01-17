using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace console;
class Program
{
    static void Main(string[] args)
    {
        string test = "40 I'm only40 40 _fucking years old. but unluckly66 I cannot find a job at all. ";   
        IDocumentProcessor p = new DocProcessor();
        var ret = p.Analyze(test);
        Console.WriteLine(ret.NumberOfAllWords);
        Console.WriteLine(ret.NumberOfWordsThatContainOnlyDigits);
        Console.WriteLine(ret.NumberOfWordsStartingWithSmallLetter);
        Console.WriteLine(ret.NumberOfWordsStartingWithCaptailLetter);
        Console.WriteLine(ret.TheShortestWord);
        Console.WriteLine(ret.TheLongestWord);

    }
}

public interface IDocumentProcessor
{
    Stats Analyze(string document);
}

public class Stats 
{
    public int NumberOfAllWords {get; set;}
    public int NumberOfWordsThatContainOnlyDigits {get;set;}
    public int NumberOfWordsStartingWithSmallLetter {get;set;}
    public int NumberOfWordsStartingWithCaptailLetter {get;set;}
    public string TheLongestWord {get;set;}
    public string TheShortestWord {get;set;}
}

public class DocProcessor : IDocumentProcessor
{   

    public Stats Analyze(string document)
    {
        //find all words between by space
        var pattern = @"(?<=\s|^)\w\S*(?=\s|$)";
        
        var ret = new Stats();
        var matches = Regex.Matches(document, pattern);
        
        if (matches.Count >0 )
        {
            ret.NumberOfAllWords = matches.Count;
            ret.TheShortestWord = matches.First().Value;
            ret.TheLongestWord = matches.First().Value;
            foreach (Match match in matches)
            {
                if (match.Value.Length > ret.TheLongestWord.Length)
                    ret.TheLongestWord = match.Value;
                else if (match.Value.Length < ret.TheShortestWord.Length)
                    ret.TheShortestWord = match.Value;

                int first = (int)match.Value.First();
                if (first >= 65 && first <= 90)
                    ret.NumberOfWordsStartingWithCaptailLetter++;
                else if (first >= 97 && first <= 122)
                    ret.NumberOfWordsStartingWithSmallLetter++;

                if (decimal.TryParse(match.Value, out _))
                {
                    ret.NumberOfWordsThatContainOnlyDigits++;
                }
            }
        }
        return ret;
    }
}