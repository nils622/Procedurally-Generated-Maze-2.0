using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeGenerator
{
    public static List<Tile> tiles = new List<Tile>();
    public static bool isDone = false;

    public static IEnumerator Generate(int mazeWidth, int mazeHeight)
    {
        /* Set values */
        Stack<Tile> stack = new Stack<Tile>();
        Tile currentTile = null;

        /* Prepare all data for algorithm */
        if (currentTile == null)
        {
            int tileCount = 0;
            /* Create all tiles */
            for (int z = 0; z < mazeHeight; z++)
            {
                for (int x = 0; x < mazeWidth; x++)
                {
                    /* Added a count function so it takes less time but still runs good */
                    if (tileCount >= 3000)
                    {
                        yield return null;
                        tileCount = 0;
                    }
                    tileCount++;
                    
                    /* Create tile */
                    Tile tile = new Tile(x, z);
                    tiles.Add(tile);

                    /* Add walls all tiles need east and south walls and only some border tiles need north and west walls
                     * this is so there are no dubble walls */
                    if (z == mazeHeight - 1)
                    {
                        tile.northWall = true;
                    }

                    if (x == 0)
                    {
                        tile.westWall = true;
                    }

                    tile.eastWall = true;
                    tile.southWall = true;
                }
            }

            /* Set neighbours for all tiles */
            int countLeft = 0;
            int countRight = 1;
            for (int i = 0; i < tiles.Count; i++)
            {
                SetVerticalNeighbours(i, mazeWidth);
                SetHorizontalNeighbours(ref countLeft, ref countRight, i, mazeWidth);
            }

            /* 1.Choose starting point */
            currentTile = tiles[Random.Range(0, tiles.Count)];
            currentTile.visited = true;
            stack.Push(currentTile);
        }

        int count = 0;

        /* 2. Algorithm */
        while (stack.Count > 0)
        {
            currentTile = stack.Pop();

            if (currentTile.HasUnvisitedNeighbours())
            {
                /* Added a count function so it takes less time but still runs good */
                if (count >= 3000)
                {
                    yield return null;
                    count = 0;
                }

                count++;

                stack.Push(currentTile);

                Tile neighbourTile = currentTile.GetRandomUnvisitedNeighbour();

                currentTile.UpdateWallData(currentTile, neighbourTile);

                currentTile = neighbourTile;
                currentTile.visited = true;
                stack.Push(currentTile);
            }
        }

        Debug.Log("Done");
        isDone = true;
        yield break;
    }

    public static void Reset()
    {
        tiles = new List<Tile>();
        isDone = false;
    }
    
    private static void SetHorizontalNeighbours(ref int countLeft, ref int countRight, int i, int mazeWidth)
    {
        if (i > (countRight * mazeWidth - 2))
        {
            countRight++;
        }
        else
        {
            tiles[i].neighbours.Add(tiles[i + 1]);
        }

        if (i < (countLeft * mazeWidth))
        {
            tiles[i].neighbours.Add(tiles[i - 1]);
        }
        else
        {
            countLeft++;
        }
    }

    private static void SetVerticalNeighbours(int i, int mazeWidth)
    {
        if (i < tiles.Count - mazeWidth)
        {
            tiles[i].neighbours.Add(tiles[i + mazeWidth]);
        }

        if (i > mazeWidth)
        {
            tiles[i].neighbours.Add(tiles[i - mazeWidth]);
        }
    }
}