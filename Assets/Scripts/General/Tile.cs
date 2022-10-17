using System.Collections.Generic;
using UnityEngine;

public class Tile
{   
    public int XPosition;
    public int ZPosition;

    public bool northWall, eastWall, southWall, westWall;

    public bool visited;

    public List<Tile> neighbours;

    public Tile(int XPosition, int ZPosition)
    {
        this.XPosition = XPosition;
        this.ZPosition = ZPosition;

        visited = false;

        northWall = false;
        eastWall = false;
        southWall = false;
        westWall = false;

        neighbours = new List<Tile>();
    }

    public bool HasUnvisitedNeighbours()
    {
        foreach (Tile tile in neighbours)
        {
            if (tile.visited == false)
            {
                return true;
            }
        }
        return false;
    }

    public Tile GetRandomUnvisitedNeighbour()
    {
        List<Tile> unvisitedTiles = new List<Tile>();

        foreach (Tile tile in neighbours)
        {
            if (tile.visited == false)
            {
                unvisitedTiles.Add(tile);
            }
        }

        int index = Random.Range(0, unvisitedTiles.Count);
        return unvisitedTiles[index];
    }

    public void UpdateWallData(Tile currentTile, Tile neighbourTile)
    {
        if (currentTile.XPosition > neighbourTile.XPosition)
        {
            westWall = false;
            neighbourTile.eastWall = false;
        }

        if (currentTile.XPosition < neighbourTile.XPosition)
        {
            eastWall = false;
            neighbourTile.westWall = false;
        }

        if (currentTile.ZPosition > neighbourTile.ZPosition)
        {
            southWall = false;
            neighbourTile.northWall = false;
        }

        if (currentTile.ZPosition < neighbourTile.ZPosition)
        {
            northWall = false;
            neighbourTile.southWall = false;
        }
    }
}