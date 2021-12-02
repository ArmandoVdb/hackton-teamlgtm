// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Xml;
using A_2;

static class Program
{
    static async Task Main(string[] args)
    {
        var client = LGTM.Our.Client();
        // var sampleUrl = "/api/path/1/medium/Sample";
        // var sampleResponse = await client.GetFromJsonAsync<Response>(sampleUrl);
        // var sampleAnswer = Solve(sampleResponse.Start, sampleResponse.Destination);
        // foreach (var a in sampleAnswer)
        // {
        //     Console.WriteLine(a);
        // }
        // var samplePostResponse = await client.PostAsJsonAsync<List<int>>(sampleUrl, sampleAnswer);
        // Console.WriteLine(await samplePostResponse.Content.ReadAsStringAsync());
        
        var puzzleUrl = "/api/path/1/medium/Puzzle";
        var puzzleResponse = await client.GetFromJsonAsync<Response>(puzzleUrl);
        Console.WriteLine(puzzleResponse.Start);
        Console.WriteLine(puzzleResponse.Destination);
        var puzzleAnswer = Solve(puzzleResponse.Start, puzzleResponse.Destination);
        foreach (var a in puzzleAnswer)
        {
            Console.WriteLine(a);
        }
        // var puzzlePostResponse = await client.PostAsJsonAsync<List<int>>(puzzleUrl, puzzleAnswer);
        // Console.WriteLine(await puzzlePostResponse.Content.ReadAsStringAsync());
        
    }
    
    // recursion again
    // base case: cur = destination
    // other vars: nextStepSize
    // check if the next stepSize can get us to base case
    // if not, check if stepSize after that can get us to base case if we go down
    // if not, check if stepSize after that can get us to base case if we go up
    public static List<int> Solve(int start, int destination)
    {
        List<int> steps = new ();
        int maxLength = destination;
        
        Recurse(start, destination, 1, steps);
        
        steps.Add(start);
        steps.Reverse();
        
        
        return steps;
    }

    public static bool Recurse(int cur, int destination, int stepSize, List<int> steps)
    {
        // To find the shortest route we just limit our algorithm
        // allow a failure if no paths have been found yet at this depth
        // Console.WriteLine(stepSize);
        if (stepSize >= destination) return false;
        if (cur + stepSize == destination || cur - stepSize == destination)
        {
            steps.Add(destination);
            return true;
        }
        {
            if (cur + stepSize <= destination)
            {
                if (Recurse(cur + stepSize, destination, stepSize + 1, steps))
                {
                    steps.Add(cur + stepSize);
                    return true;
                }
            }

            {
                if (Recurse(cur - stepSize, destination, stepSize + 1, steps))
                {
                    steps.Add(cur - stepSize);
                    return true;
                }
            }
            return false;
        }
    }
}