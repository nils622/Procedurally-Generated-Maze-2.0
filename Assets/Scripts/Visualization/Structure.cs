using UnityEngine;

/* This class is used to save all the data in 1 step so you dont need to create new ones every new generation */
public class Structure
{
    public GameObject gameObject;
    public MeshFilter meshFilter;

    public Structure(MeshFilter meshFilter, GameObject gameObject = null)
    {
        this.gameObject = gameObject;
        this.meshFilter = meshFilter;
    }
}
