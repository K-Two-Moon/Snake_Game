using UnityEngine;

public class InputModule : IInputModule
{
    public void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Space key pressed.");
    }
}
