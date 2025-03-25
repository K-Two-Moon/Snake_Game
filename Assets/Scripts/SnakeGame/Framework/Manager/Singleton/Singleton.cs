public class Singleton<T> where T : class, new()
{
    private static T instance;
    static object obj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}