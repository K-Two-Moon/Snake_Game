using System.Windows.Input;
using UnityEngine;

public interface ICommand
{
    void Execute();
}

public class IntoFoodCommand : ICommand
{
    Snake snake;
    int index;
    public IntoFoodCommand(IGameObject obj, string bodyName)
    {
        snake = obj as Snake;

        //查找身体的索引
        index = snake.list.FindIndex(x => x.name.Equals(bodyName));
    }

    public IntoFoodCommand(IGameObject obj)
    {
        snake = obj as Snake;
        index = 0;
    }


    public void Execute()
    {
        if (index >= 0)
        {
            for (int i = snake.list.Count - 2; i >= index; i--)
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
