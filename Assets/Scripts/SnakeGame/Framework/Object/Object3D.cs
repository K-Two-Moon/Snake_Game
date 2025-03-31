using UnityEngine;

public abstract class Object3D : IGameObject
{
    protected static Transform parent3D;

    public Object3D()
    {
        if (parent3D == null)
        {
            parent3D = GameObject.Find("Parent3D")?.transform;
            if (parent3D == null)
            {
                parent3D = new GameObject("Parent3D").transform;
                parent3D.position = Vector3.zero;
            }
        }
    }

    public override void Create()
    {
        base.Create();
        obj.name = Id.ToString();
    }
}