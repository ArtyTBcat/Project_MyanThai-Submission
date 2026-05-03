using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool detonated;

    public void ForceExplode()
    {
        if (detonated)
        {
            return;
        }

        detonated = true;

        GetComponent<Collider>().enabled = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ForceExplode();
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