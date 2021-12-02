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
        var sampleUrl = "/api/path/1/hard/Sample";
        var sampleResponse = await client.GetFromJsonAsync<Response>(sampleUrl);

        foreach (var tile in sampleResponse.Tiles)
        {
            Console.WriteLine(tile);
        }
        
        Solve(sampleResponse.Tiles);
    }

    static void Solve(List<Tile> tiles)
    {
        Tile[,] grid = new Tile[tiles.Max(t => t.X), tiles.Max(t => t.Y)];
        foreach (var tile in tiles)
        {
            grid[tile.X, tile.Y] = tile;
        }

        List<int> start = new List<int>();

        List<List<int>> possibilities = new();
        List<int> solution = null;
        
        start.Add(0);

        do
        {
            List<List<int>> newPossibilities = new List<List<int>>();
            foreach (List<int> possibility in possibilities)
            {
                Tile curTile = tiles.Find(t => t.Id == possibility.Last());
                List<int> horizontalMovement = new();
                List<int> verticalMovement = new();
                int posY;
                int posX;
                if (curTile.Direction == Directions.End)
                {
                    if (possibility.Count == grid.Length)
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
                        verticalMovement.Add(posY++);
                    }
                    break;
                }
                if (curTile.Direction == Directions.Up ||
                    curTile.Direction == Directions.UpLeft ||
                    curTile.Direction == Directions.UpRight
                )
                {
                    posY = curTile.Y;
                    while (posY < tiles.Min(t => t.Y))
                    {
                        verticalMovement.Add(posY--);
                    }
                    break;
                }
                if (curTile.Direction == Directions.Right ||
                    curTile.Direction == Directions.UpRight ||
                    curTile.Direction == Directions.DownRight
                )
                {
                    posX = curTile.X;
                    while (posX < tiles.Max(t => t.X))
                    {
                        horizontalMovement.Add(posX++);
                    }
                    break;
                }
                if (curTile.Direction == Directions.Left ||
                    curTile.Direction == Directions.UpLeft ||
                    curTile.Direction == Directions.DownLeft
                )
                {
                    posX = curTile.X;
                    while (posX < tiles.Min(t => t.X))
                    {
                        horizontalMovement.Add(posX--);
                    }
                    break;
                }

                List<Tile> possibleTiles = new List<Tile>();
                for (int i = 0; i < horizontalMovement.Count && i < verticalMovement.Count; i++)
                {
                    Tile tile = grid[verticalMovement[i], horizontalMovement[i]];
                    if(!tile.Visited)
                        possibleTiles.Add(tile);
                }

                foreach (var possibleTile in possibleTiles)
                {
                    List<int> newPossibility = possibility.ToList();
                    newPossibility.Add(possibleTile.Id);
                    newPossibilities.Add(newPossibility);
                }
                possibilities = newPossibilities;
            }
        } while (solution == null);

        foreach (int x in solution)
        {
            Console.WriteLine(x);
        }

    }

}