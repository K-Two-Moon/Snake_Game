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
        World.Instance.RemoveFood(this);
        base.Destroy();
    }

    public override void Create()
    {
        //创建食物
        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<Renderer>().material = data.material;
        obj.transform.SetParent(parent);
        base.Create();
        World.Instance.AddFood(this);
    }

    protected override void OnCreate()
    {
        AddComponent(ComponentType.FoodEatComponent);
        base.OnCreate();
    }
}
