// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using A_3;

static class Program
{
    static async Task Main(string[] args)
    {
        var client = LGTM.Our.Client();
        var sampleUrl = "/api/path/1/hard/Sample";
        var sampleResponse = await client.GetFromJsonAsync<Response>(sampleUrl);
        Console.WriteLine(sampleResponse);

    }

}