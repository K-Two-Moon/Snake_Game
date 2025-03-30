using UnityEngine;

public class Food : Object3D
{
    public FoodData data;
    public override void InitializeData(IData data)
    {
        this.data = data as FoodData;
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
