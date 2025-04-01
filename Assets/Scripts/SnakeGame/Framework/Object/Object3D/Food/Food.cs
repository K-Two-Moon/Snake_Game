using UnityEngine;

public class Food : Object3D
{
    public FoodData data;

    new Transform parent;
    public Food()
    {
        if (parent == null)
        {
            parent = GameObject.Find("ParentFood")?.transform;
            if (parent == null)
            {
                parent = new GameObject("ParentFood").transform;
                parent.position = Vector3.zero;
            }
        }
    }
    public override void InitializeData(IData data)
    {
        this.data = data as FoodData;

        
    }

    public override void Destroy()
    {

        base.Destroy();
    }

    public override void Create()
    {
        //创建食物
        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.localScale = Vector3.one * 0.5f;
        obj.GetComponent<Renderer>().material = data.material;
        base.Create();
    }
}
