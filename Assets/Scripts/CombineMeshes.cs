using System.Collections.Generic;
using UnityEngine;

public static class CombineMeshes
{
    public static MeshFilter CombineStructureMeshes(List<Structure> structures, MeshFilter meshFilter, MeshRenderer meshRenderer, Material material)
    {
        /* Get all MeshFilters */
        MeshFilter[] meshFilters = new MeshFilter[structures.Count];
        for (int i = 0; i < structures.Count; i++)
        {
            meshFilters[i] = structures[i].meshFilter;
        }

        /* Combine all meshes */
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            StructurePooling.ReturnStructure(structures[i]);
        }

        /* Create new Mesh and assign the combined mesh to it */
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        meshRenderer.material = material;

        return meshFilter;
    }

    public static MeshFilter CombineCombinedMeshes(List<CombinedMesh> combinedMeshes, MeshFilter meshFilter, MeshRenderer meshRenderer, Material material)
    {
        /* Get all MeshFilters */
        MeshFilter[] meshFilters = new MeshFilter[combinedMeshes.Count];
        for (int i = 0; i < combinedMeshes.Count; i++)
        {
            meshFilters[i] = combinedMeshes[i].meshFilter;
        }

        /* Combine all meshes */
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            StructurePooling.ReturnCombinedMesh(combinedMeshes[i]);
        }

        /* Create new Mesh and assign the combined mesh to it */
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        meshRenderer.material = material;

        return meshFilter;
    }
}
