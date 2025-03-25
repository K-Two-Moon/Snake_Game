using UnityEngine;

public class UpdateModule : IUpdateModule
{
    public void Tick(float deltaTime)
    {
        // Example logic
        Debug.Log($"Update tick: {deltaTime}");
    }
}
