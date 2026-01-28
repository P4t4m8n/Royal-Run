using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    private const string collectibleTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Apple collected!");

        if (other.gameObject.CompareTag(collectibleTag))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    protected abstract void OnPickup();
}
