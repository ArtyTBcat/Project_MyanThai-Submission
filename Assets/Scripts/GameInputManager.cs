using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool IsActionPressed(InputActionType action)
    {
        switch (action)
        {
            case InputActionType.SliceLeft:
                return Input.GetKeyDown(KeyCode.X); // replace with controller later

            case InputActionType.SliceRight:
                return Input.GetKeyDown(KeyCode.B); // replace with controller later
        }

        return false;
    }
}