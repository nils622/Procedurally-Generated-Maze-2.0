using UnityEngine;


/* This class is used to save all the data in 1 step so you dont need to create new ones every new generation */
public class CombinedMesh
{
    public GameObject gameObject;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public CombinedMesh(GameObject gameObject, MeshFilter meshFilter, MeshRenderer meshRenderer)
    {
        this.gameObject = gameObject;
        this.meshFilter = meshFilter;
        this.meshRenderer = meshRenderer;
    }
}
