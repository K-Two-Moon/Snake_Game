using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : IData
{
    public Material material;

    public FoodData(Material material)
    {
        this.material = material;
    }
}
