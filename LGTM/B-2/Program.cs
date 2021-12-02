// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

static class Program
{
    static async Task Main(string[] args)
    {
        var client = LGTM.Our.Client();

        // De url om de challenge te starten
        var startUrl = "api/path/2/medium/Start";
        var startResponse = await client.GetAsync(startUrl);


        // De url om de sample challenge data op te halen
        var sampleUrl = "api/path/2/medium/Sample";
        var sampleGetResponse = await client.GetStringAsync(sampleUrl);

        // Je zoekt het antwoord


        string solution = Solve(sampleGetResponse);


        //Sample
        var samplePostResponse = await client.PostAsJsonAsync<string>(sampleUrl, solution);
        var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();
        Console.WriteLine(samplePostResponseValue);


        //Puzzle
        var puzzleUrl = "api/path/2/medium/Puzzle";
        var puzzleGetResponse = await client.GetStringAsync(puzzleUrl);

        string puzzleSolution = Solve(puzzleGetResponse);

        var puzzlePostResponse = await client.PostAsJsonAsync<string>(puzzleUrl, puzzleSolution);
        var puzzlePostResponseValue = await puzzlePostResponse.Content.ReadAsStringAsync();
        Console.WriteLine(puzzlePostResponseValue);
    }


    public static string Solve(string digits)
    {
        Console.WriteLine("number to recognize patterns in: " + digits);
        Console.WriteLine();
        return getPatterns(digits.ToString());
    }

    public static string getPatterns(string strDig)
    {
        var patterns = new Dictionary<string, int>();
        int amountOfPatterns = 0;

        //Loop over digits starting at length of 2
        for (int i = 0; i < strDig.Length; i++)
        {
            for (int j = 2; j <= strDig.Length; j++)
            {
                if (i + j <= strDig.Length)
                {
                    string pattern = strDig.Substring(i, j);
                    if (patterns.ContainsKey(pattern))
                    {
                        patterns[pattern] += 1;
                    }
                    else
                    {
                        patterns.Add(pattern, 1);
                    }
                }
            }
        }

        
        //check amount of patterns with usage higher than 1
        foreach (KeyValuePair<string, int> keyValuePair in patterns)
        {
            if (keyValuePair.Value > 1)
            {
                amountOfPatterns++;
            }
        }

        string solution = amountOfPatterns + patterns.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        Console.WriteLine("Solution: " + solution);

        return solution;
    }
}