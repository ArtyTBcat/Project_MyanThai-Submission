using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceEffect;

    public int points = 1;

    private bool hasBeenSliced;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void ForceSlice(Vector3 direction, float force)
    {
        if (hasBeenSliced)
        {
            return;
        }

        Slice(direction, transform.position, force);
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        if (hasBeenSliced)
        {
            return;
        }

        hasBeenSliced = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IncreaseScore(points);
        }

        fruitCollider.enabled = false;
        whole.SetActive(false);

        sliced.SetActive(true);
        juiceEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.linearVelocity = fruitRigidbody.linearVelocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
    private void OnDestroy()
{
    if (SingleSliceManager.Instance != null)
    {
        SingleSliceManager.Instance.ClearCurrentObject(gameObject);
    }
}
}