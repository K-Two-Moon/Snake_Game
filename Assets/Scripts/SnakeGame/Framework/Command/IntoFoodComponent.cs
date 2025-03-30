using System.Windows.Input;
using UnityEngine;

public interface ICommand
{
    void Execute(params object[] args);
}

public class IntoFoodComponent : ICommand
{
    Snake snake;
    public IntoFoodComponent(IGameObject obj)
    {
        snake = obj as Snake;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="args">传入一个参数，参数类型为string，表示身体的名称</param>
    public void Execute(params object[] args)
    {
        string bodyName = args[0] as string;

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
