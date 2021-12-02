// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using A_3;

static class Program
{
    static async Task Main(string[] args)
    {
        var client = LGTM.Our.Client();
        //
        // var sampleUrl = "/api/path/1/hard/Sample";
        // var sampleResponse = await client.GetFromJsonAsync<Response>(sampleUrl);
        // var sampleAnswer = Solve(sampleResponse.Tiles);
        // var samplePostResponse = await client.PostAsJsonAsync<List<int>>(sampleUrl, sampleAnswer);
        // Console.WriteLine(await samplePostResponse.Content.ReadAsStringAsync());

        
        var puzzleUrl = "/api/path/1/hard/Puzzle";
        var puzzleResponse = await client.GetFromJsonAsync<Response>(puzzleUrl);
        var puzzleAnswer = Solve(puzzleResponse.Tiles);
        var puzzlePostResponse = await client.PostAsJsonAsync<List<int>>(puzzleUrl, puzzleAnswer);
        Console.WriteLine(await puzzlePostResponse.Content.ReadAsStringAsync());
        
        
    }

    static List<int> Solve(List<Tile> tiles)
    {
        List<int> start = new List<int>();

        List<List<int>> possibilities = new();
        List<int> solution = null;
        
        start.Add(1);
        possibilities.Add(start);
        do
        {
            List<List<int>> newPossibilities = new List<List<int>>();
            foreach (List<int> possibility in possibilities)
            {
                Tile curTile = tiles.Find(t => t.Id == possibility.Last());
                List<int> horizontalMovement = new();
                List<int> verticalMovement = new();
                int posY = curTile.Y;
                int posX = curTile.X;
                if (curTile.Direction == Directions.End)
                {
                    if (possibility.Count == tiles.Count)
                    {
                        solution = possibility;
                        break;
                    }
                }

                if (curTile.Direction == Directions.Down ||
                    curTile.Direction == Directions.DownLeft ||
                    curTile.Direction == Directions.DownRight
                )
                {
                    posY = curTile.Y;
                    while (posY < tiles.Max(t => t.Y))
                    {
                        verticalMovement.Add(++posY);
                    }
                }
                if (curTile.Direction == Directions.Up ||
                    curTile.Direction == Directions.UpLeft ||
                    curTile.Direction == Directions.UpRight
                )
                {
                    posY = curTile.Y;
                    while (posY > tiles.Min(t => t.Y))
                    {
                        verticalMovement.Add(--posY);
                    }
                }
                if (curTile.Direction == Directions.Right ||
                    curTile.Direction == Directions.UpRight ||
                    curTile.Direction == Directions.DownRight
                )
                {
                    posX = curTile.X;
                    while (posX < tiles.Max(t => t.X))
                    {
                        horizontalMovement.Add(++posX);
                    }
                }
                if (curTile.Direction == Directions.Left ||
                    curTile.Direction == Directions.UpLeft ||
                    curTile.Direction == Directions.DownLeft
                )
                {
                    posX = curTile.X;
                    while (posX > tiles.Min(t => t.X))
                    {
                        horizontalMovement.Add(--posX);
                    }
                }

                List<Tile> possibleTiles = new List<Tile>();
                for (int i = 0; i < horizontalMovement.Count || i < verticalMovement.Count; i++)
                {
                    int newX = (horizontalMovement.Count > i ? horizontalMovement[i] : curTile.X);
                    int newY = (verticalMovement.Count > i ? verticalMovement[i] : curTile.Y);
                    Tile tile = tiles.SingleOrDefault(t => t.X == newX && t.Y == newY);
                    if(!possibility.Contains(tile.Id))
                        possibleTiles.Add(tile);
                }

                foreach (var possibleTile in possibleTiles)
                {
                    List<int> newPossibility = possibility.ToList();
                    newPossibility.Add(possibleTile.Id);
                    newPossibilities.Add(newPossibility);
                }
            }
            possibilities = newPossibilities;
        } while (solution == null);

        foreach (int x in solution)
        {
            Console.WriteLine(x);
        }

        return solution;
    }

}