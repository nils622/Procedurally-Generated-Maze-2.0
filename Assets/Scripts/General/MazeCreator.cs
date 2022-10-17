using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    [Range(5, 250)]
    [SerializeField] private int width = 10;

    [Range(5, 250)]
    [SerializeField] private int height = 10;

    [SerializeField] private InputLogic inputLogicScript;

    private List<Tile> tiles = new List<Tile>();

    [SerializeField] private GameObject wallPrefab;

    [SerializeField] private Material wallMaterial;

    private IEnumerator mazeGenerationRoutine;
    private IEnumerator mazeVisualizerRoutine;


    private void OnEnable()
    {
        /* Reset all scripts */
        MazeGenerator.Reset();
        StructurePooling.Reset();

        /* Sets data for pooling */
        StructurePooling.initiate(wallPrefab);

        /* Sets data for MazeVisualizer */
        MazeVisualizer.isDone = true;
    }

    private void Update()
    {
        /* Get values from input fields */
        width = int.Parse(inputLogicScript.lastValueInputs[0]);
        height = int.Parse(inputLogicScript.lastValueInputs[1]);

        /* If generation is done add structures/visuals */
        if (MazeGenerator.isDone)
        {
            /* Get generated tiles from MazeGenerator */
            tiles = MazeGenerator.tiles;

            StartCoroutine(MazeVisualizer.AddStructures(tiles, wallMaterial));
            MazeGenerator.Reset();
        }

    }

    /* Function is used in Generate Button object*/
    public void GenerateMaze()
    {
        if (MazeVisualizer.isDone)
        {
            StopAllRoutines();

            /* Reset all data that was created by last generation */
            MazeGenerator.Reset();
            MazeVisualizer.Reset();

            /* Start new generation */
            mazeGenerationRoutine = MazeGenerator.Generate(width, height);
            StartCoroutine(mazeGenerationRoutine);
            MazeVisualizer.isDone = false;
        }
    }

    public void StopAllRoutines()
    {
        if (mazeGenerationRoutine != null)
        {
            StopCoroutine(mazeGenerationRoutine);
        }

        if (mazeVisualizerRoutine != null)
        {
            StopCoroutine(mazeVisualizerRoutine);
        }
    }
}
