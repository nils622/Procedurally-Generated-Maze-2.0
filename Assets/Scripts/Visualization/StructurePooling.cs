using System.Collections.Generic;
using UnityEngine;

public static class StructurePooling
{
    public static GameObject wallPrefab;

    private static Transform structureParent;
    private static Transform combinedMeshesParent;
    public static Stack<Structure> wallPool = new Stack<Structure>();
    private static Stack<CombinedMesh> combinedMeshPool = new Stack<CombinedMesh>();


    public static void initiate(GameObject wall)
    {
        wallPrefab = wall;
        CreateParents();
    }

    public static void CreateParents()
    {
        /* Creates parent for all structures that are spawned */
        structureParent = new GameObject("Structures").transform;
        combinedMeshesParent = new GameObject("CombinedSmallMeshes").transform;
    }

    public static void Reset()
    {
        wallPool = new Stack<Structure>();
        combinedMeshPool = new Stack<CombinedMesh>();
    }

    public static Structure GetStructure()
    {
        /* If structure is in wallPool than take it activate it and return it */
        if (wallPool.Count > 0)
        {
            Structure wall = wallPool.Pop();
            wall.gameObject.SetActive(true);
            return wall;
        }

        /* Else create a new one */

        GameObject WallObject = Object.Instantiate(wallPrefab, new Vector3(0, 0, 0), Quaternion.identity, structureParent);
        MeshFilter meshFilter = WallObject.GetComponentInChildren<MeshFilter>();

        Structure newWall = new Structure(meshFilter, WallObject);
        return newWall;
    }

    public static void ReturnStructure(Structure structure)
    {
        if (wallPool.Contains(structure))
        {
            return;
        }

        if (structure.gameObject == null)
        {
            return;
        }

        wallPool.Push(structure);
        structure.gameObject.SetActive(false);
    }

    public static CombinedMesh GetCombinedMesh()
    {
        /* Same as GetStructure but returns CombinedMesh instead of structure */
        if (combinedMeshPool.Count > 0)
        {
            CombinedMesh combinedMesh = combinedMeshPool.Pop();
            combinedMesh.gameObject.SetActive(true);
            combinedMesh.meshFilter.mesh = null;
            return combinedMesh;
        }

        GameObject gameObject = new GameObject("SmallMesh");
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer MeshRenderer = gameObject.AddComponent<MeshRenderer>();
        gameObject.transform.parent = combinedMeshesParent;

        CombinedMesh newCombinedMesh = new CombinedMesh(gameObject, meshFilter, MeshRenderer);
        return newCombinedMesh;
    }

    public static void ReturnCombinedMesh(CombinedMesh combinedMesh)
    {
        if (combinedMeshPool.Contains(combinedMesh))
        {
            return;
        }

        if (combinedMesh.gameObject == null)
        {
            return;
        }

        combinedMeshPool.Push(combinedMesh);
        combinedMesh.gameObject.SetActive(false);
    }
}