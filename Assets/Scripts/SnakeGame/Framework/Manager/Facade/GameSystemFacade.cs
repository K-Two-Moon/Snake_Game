public class GameSystemFacade
{
    private readonly IInputModule _inputModule;
    private readonly IUpdateModule _updateModule;

    public GameSystemFacade(IInputModule inputModule, IUpdateModule updateModule)
    {
        _inputModule = inputModule;
        _updateModule = updateModule;

    }

    // 统一对外方法
    public void Update(float deltaTime)
    {
        _inputModule.ProcessInput();
        _updateModule.Tick(deltaTime);
    }
}
