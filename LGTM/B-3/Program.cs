using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using B3;

class Program
{
    static async Task Main(string[] args)
    {
        var client = LGTM.Our.Client();


        // De url om de challenge te starten
        var startUrl = "api/path/2/hard/Start";
        var startResponse = await client.GetAsync(startUrl);


        var sampleUrl = "api/path/2/hard/Sample";
        var sampleGetResponse = await client.GetFromJsonAsync<Response>(sampleUrl);

        // Je zoekt het antwoord


        Solve(sampleGetResponse);

        //var samplePostResponse = await client.PostAsJsonAsync<int>(sampleUrl, sampleAnswer);
        //var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();


/*
        var puzzleUrl = "api/path/1/hard/Puzzle";
        var puzzleGetResponse = await client.GetFromJsonAsync<List<int>>(puzzleUrl);

        var puzzleAnswer = GetAnswer(puzzleGetResponse);

        var puzzlePostResponse = await client.PostAsJsonAsync<int>(sampleUrl, puzzleAnswer);
        var puzzlePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();
        */
    }

    public static void Solve(Response response)
    {
        char[] word = response.cipheredWords[0].ToCharArray();
        string tempString = "";
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < word.Length; j++)
            {
                tempString += word[j] + (char) i;
            }
            // Doesn't work because of a lack of time :( 
            //Console.WriteLine(tempString);
        }
        
        
    }
}