using System.Windows.Input;
using UnityEngine;

public interface ICommand
{
    void Execute(float deltaTime);
}

public class IntoFoodComponent : ICommand
{
    Snake snake;
    string bodyName;
    public IntoFoodComponent(IGameObject obj, string bodyName)
    {
        snake = obj as Snake;
        this.bodyName = bodyName;
    }


    public void Execute(float deltaTime)
    {
        //查找身体的索引
        int index = snake.list.FindIndex(x => x.name.Equals(bodyName));


        if (index >= 0)
        {
            for (int i = snake.list.Count - 1; i >= index; i--)
            {
                //变成食物
                IGameObject food = Object3DFactory.CreateProduct(Object3DType.Food);
                food.InitializeData(new FoodData(snake.head.GetComponent<Renderer>().material));
                food.Create();
                food.Obj.transform.position = snake.list[i].position;
                //删除身体
                Object.Destroy(snake.list[i].gameObject);
                snake.list.RemoveAt(i);
            }
        }
    }
}
