// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

static class Program
{
    static async Task Main(string[] args)
    {
        var client = LGTM.Our.Client();
        var sampleUrl = "/api/path/1/easy/Sample";
        var sampleNumbers = await client.GetFromJsonAsync<List<int>>(sampleUrl);
        var sampleAnswer = Solve(sampleNumbers);
        Console.WriteLine("Sample answer is: " + sampleAnswer);
        // var samplePostResponse = await client.PostAsJsonAsync<int>(sampleUrl, sampleAnswer);
        // Console.WriteLine(await samplePostResponse.Content.ReadAsStringAsync());
        //
        // var puzzleUrl = "api/path/1/easy/Puzzle";
        // var puzzleNumbers = await client.GetFromJsonAsync<List<int>>(puzzleUrl);
        // var puzzleAnswer = Solve(puzzleNumbers);
        // Console.WriteLine("Puzzle answer is: " + puzzleAnswer);
        // var puzzlePostResponse = await client.PostAsJsonAsync<int>(puzzleUrl, puzzleAnswer);
        // Console.WriteLine(await puzzlePostResponse.Content.ReadAsStringAsync());
    }

    // algorithm is
    // take sum
    // split into digits
    // take sum of digits
    // if > 9
    // split into digits
    // take sum of digits
        
    // solve with recursion
    // base case: sum <= 9
    public static int Solve(List<int> list)
    {

        int sum = 0;
        foreach (var number in list)
        {
            sum += number;
        }
        return Recurse(sum);
    }

    public static int Recurse(int x)
    {
        char[] digits = x.ToString().ToCharArray();
        int sum = 0;
        foreach (var digit in digits)
        {
            sum += Int32.Parse(digit.ToString());
        }
        if (sum >= 10)
            return Recurse(sum);
        return sum;
    }
}