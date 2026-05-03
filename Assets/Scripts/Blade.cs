using UnityEngine;

public class Blade : MonoBehaviour
{
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    [SerializeField] private bool legacyMouseInput = false;

    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public Vector3 direction { get; private set; }
    public bool slicing { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        // ✅ SIMPLE KEY INPUT (X and B)
        if (GameInputManager.Instance.IsActionPressed(Input.GetKeyDown(KeyCode.X)) || GameInputManager.Instance.IsActionPressed(Input.GetKeyDown(KeyCode.B)))
        {
            SliceAnyObject();
        }

        // KEEP OLD SYSTEM (optional toggle)
        if (!legacyMouseInput)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if (slicing)
        {
            ContinueSlice();
        }
    }

    private void SliceAnyObject()
    {
        Fruit fruit = FindFirstObjectByType<Fruit>();
        if (fruit != null)
        {
            fruit.ForceSlice(Vector3.right, sliceForce);
            return;
        }

        Bomb bomb = FindFirstObjectByType<Bomb>();
        if (bomb != null)
        {
            bomb.ForceExplode();
            return;
        }

        Debug.Log("No object found to slice");
    }

    private void StartSlice()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}