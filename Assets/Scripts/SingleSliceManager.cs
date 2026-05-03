using UnityEngine;
public InputActionType sliceAction1;
public InputActionType sliceAction2;

[DefaultExecutionOrder(-100)]
public class SingleSliceManager : MonoBehaviour
{
    public static SingleSliceManager Instance { get; private set; }

    [SerializeField] private KeyCode sliceKey = KeyCode.X;

    [SerializeField] private Vector3 sliceDirection = Vector3.right;
    [SerializeField] private float sliceForce = 5f;

    public GameObject CurrentActiveObject { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (GameInputManager.Instance != null)
{
    if (GameInputManager.Instance.IsActionPressed(InputActionType.MoveLeft) || GameInputManager.Instance.IsActionPressed(InputActionType.MoveRight))
    {
        Debug.Log("SLICE ACTION PRESSED");

        ForceSliceCurrent();
    }
}
    }

    public void RegisterSpawnedObject(GameObject obj)
    {
        CurrentActiveObject = obj;
    }

    public void ClearCurrentObject(GameObject obj)
    {
        if (CurrentActiveObject == obj)
        {
            CurrentActiveObject = null;
        }
    }

    private void ForceSliceCurrent()
    {
        if (CurrentActiveObject == null)
        {
            Debug.Log("NO OBJECT TO SLICE");
            return;
        }

        Fruit fruit = CurrentActiveObject.GetComponent<Fruit>();
        if (fruit != null)
        {
            fruit.ForceSlice(sliceDirection, sliceForce);
            return;
        }

        Bomb bomb = CurrentActiveObject.GetComponent<Bomb>();
        if (bomb != null)
        {
            bomb.ForceExplode();
        }
    }
}