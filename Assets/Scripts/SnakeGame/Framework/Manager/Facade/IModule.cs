public interface IModule
{
    void Initialize();
    void Update(float deltaTime);

    void Destroy();
}
