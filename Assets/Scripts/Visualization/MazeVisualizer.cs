using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MazeVisualizer
{
    private static List<CombinedMesh> finalMeshes = new List<CombinedMesh>();

    public static bool isDone;

    public static void Reset()
    {
        foreach (CombinedMesh combinedMesh in finalMeshes)
        {
            StructurePooling.ReturnCombinedMesh(combinedMesh);
        }
        finalMeshes = new List<CombinedMesh>();
    }

    public static IEnumerator AddStructures(List<Tile> tiles, Material wallMaterial)
    {
        List<CombinedMesh> allCombinedStructureMeshes = new List<CombinedMesh>();
        List<Structure> structures = new List<Structure>();
        
        Vector3 rotation;

        int StructureToMeshCount = 0;
        int meshesToBigMeshCount = 0;
        int yieldReturnCount = 0;

        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            if (tile.northWall)
            {
                rotation = new Vector3(0, 270, 0);
                structures.Add(SpawnStructure(tile, rotation));
            }
            if (tile.eastWall)
            {
                rotation = new Vector3(0, 0, 0);
                structures.Add(SpawnStructure(tile, rotation));
            }
            if (tile.southWall)
            {
                rotation = new Vector3(0, 90, 0);
                structures.Add(SpawnStructure(tile, rotation));
            }
            if (tile.westWall)
            {
                rotation = new Vector3(0, 180, 0);
                structures.Add(SpawnStructure(tile, rotation));
            }

            StructureToMeshCount++;
            yieldReturnCount++;

            /* Returns every 750 round */
            if (yieldReturnCount >= 750)
            {
                yield return null;
                yieldReturnCount = 0;
            }


            /* Turns structures into 1 combined mesh every 20 rounds */
            if (StructureToMeshCount >= 20)
            {
                CombineStructuresToMesh(wallMaterial, allCombinedStructureMeshes, ref structures);

                StructureToMeshCount = 0;
                meshesToBigMeshCount++;
            }

            /* Turns all the combined meshes from above to 1 big mesh every 750 times it combines */
            if (meshesToBigMeshCount >= 750)
            {
                CombineCombinedStructureMeshIntoBigMesh(wallMaterial, ref allCombinedStructureMeshes);

                meshesToBigMeshCount = 0;
            }
        }

        /* For the last couple of structures */
        CombineStructuresToMesh(wallMaterial, allCombinedStructureMeshes, ref structures);


        CombinedMesh combinedBigMesh = CombineCombinedStructureMeshIntoBigMesh(wallMaterial, ref allCombinedStructureMeshes);
        finalMeshes.Add(combinedBigMesh);

        isDone = true;
        yield break;
    }

    private static CombinedMesh CombineStructuresToMesh(Material wallMaterial, List<CombinedMesh> allCombinedStructureMeshes, ref List<Structure> structures)
    {
        CombinedMesh tempCombinedMesh = StructurePooling.GetCombinedMesh();
        CombineMeshes.CombineStructureMeshes(structures, tempCombinedMesh.meshFilter, tempCombinedMesh.meshRenderer, wallMaterial);
        structures = new List<Structure>();
        allCombinedStructureMeshes.Add(tempCombinedMesh);
        return tempCombinedMesh;
    }

    private static CombinedMesh CombineCombinedStructureMeshIntoBigMesh(Material wallMaterial, ref List<CombinedMesh> allCombinedStructureMeshes)
    {
        CombinedMesh tempCombinedMesh = StructurePooling.GetCombinedMesh();
        CombineMeshes.CombineCombinedMeshes(allCombinedStructureMeshes, tempCombinedMesh.meshFilter, tempCombinedMesh.meshRenderer, wallMaterial);
        allCombinedStructureMeshes = new List<CombinedMesh>();
        finalMeshes.Add(tempCombinedMesh);
        return tempCombinedMesh;
    }

    private static Structure SpawnStructure(Tile tile, Vector3 rotation)
    {
        /* Set variable */
        Vector3 structurePosition = new Vector3(tile.XPosition, 0, tile.ZPosition);
        Quaternion structureRotation = Quaternion.Euler(rotation);
        Structure structure = StructurePooling.GetStructure();

        /* Set position and rotation */
        structure.gameObject.transform.SetPositionAndRotation(structurePosition, structureRotation);

        return structure;
    }
}