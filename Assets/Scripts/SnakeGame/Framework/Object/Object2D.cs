using UnityEngine;

public abstract class Object2D : IGameObject
{
    protected static Transform parent2D;

    public Object2D()
    {
        if (parent2D == null)
        {
            parent2D = GameObject.Find("Parent3D").transform;
            if (parent2D == null)
            {
                parent2D = new GameObject("Parent3D").transform;
                parent2D.position = Vector3.zero;
            }
        }
    }
}