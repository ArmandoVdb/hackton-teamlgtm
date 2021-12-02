using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using B_1;


static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("B1 - Easy");
        HttpClient client = LGTM.Our.Client();
        
        var startUrl = "api/path/2/easy/Start";
        var startResponse = await client.GetAsync(startUrl);

        var sampleUrl = "api/path/2/easy/Sample";
        var sampleGetResponse = await client.GetFromJsonAsync<Response>(sampleUrl);
        List<string> strings = new List<string>();
        strings.Add(sampleGetResponse.date1);
        strings.Add(sampleGetResponse.date2);
        var answer = Solve(strings);
        
        var samplePostResponse = await client.PostAsJsonAsync<string>(sampleUrl, answer);
        var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();

        // De url om de puzzle challenge data op te halen
        var puzzleUrl = "api/path/2/easy/Puzzle";
        // We doen de GET request en wachten op de het antwoord
        // De response die we verwachten is een lijst van getallen dus gebruiken we List<int>
        var puzzleGetResponse = await client.GetFromJsonAsync<Response>(puzzleUrl);



        // Je zoekt het antwoord
        List<string> stringsPuzzle = new List<string>();
        stringsPuzzle.Add(puzzleGetResponse.date1);
        stringsPuzzle.Add(puzzleGetResponse.date2);
        var puzzleAnswer = Solve(stringsPuzzle);
        
        
        // We sturen het antwoord met een POST request
        // Het antwoord dat we moeten versturen is een getal dus gebruiken we int
        // De response die we krijgen zal ons zeggen of ons antwoord juist was
        var puzzlePostResponse = await client.PostAsJsonAsync<string>(puzzleUrl, puzzleAnswer);
        // Om te zien of ons antwoord juist was moeten we de response uitlezen
        // Een 200 status code betekent dus niet dat je antwoord juist was!
        var puzzlePostResponseValue = await puzzlePostResponse.Content.ReadAsStringAsync();



    }

    public static string Solve(List<string> dates)
    {
        DateTime dt1 = default;
        DateTime dt2 = default;
        int j = 0;
        foreach (string date in dates)
        {
            List<Match> digits = Regex.Matches(date, @"[0-9]+").ToList();
            List<Match> letters = Regex.Matches(date, @"[^0-9]+").ToList();
            int year = 0;
            int day = 0;
            int month = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            for (int i = 0; i < letters.Count; i++)
            {
                switch (letters[i].ToString())
                {
                    case "YYYY":
                        year = int.Parse(digits[i].ToString());
                        break;
                    case "DD":
                        day = int.Parse(digits[i].ToString());
                        break;
                    case "MM":
                        month = int.Parse(digits[i].ToString());
                        break;
                    case "hh":
                        hours = int.Parse(digits[i].ToString());
                        break;
                    case "mm":
                        minutes = int.Parse(digits[i].ToString());
                        break;
                    case "ss":
                        seconds = int.Parse(digits[i].ToString());
                        break;
                    default:
                        Console.WriteLine("Couldn't parse: " + letters[i]);
                        break;
                }
            }
            if (j == 0)
            {
                dt1 = new DateTime(year, month, day, hours, minutes, seconds);
            }
            else
            {
                dt2 = new DateTime(year, month, day, hours, minutes, seconds);
            }
            j++;
        }
        return  Math.Abs((dt1 - dt2).TotalSeconds).ToString();
    }
}