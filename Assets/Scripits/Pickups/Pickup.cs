using UnityEngine;

public class Pickup : MonoBehaviour
{

    private const string collectibleTag = "Player";
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(collectibleTag))
        {
            // Debug.Log("Pickup collected by: " + other.gameObject.name);
        }
    }
}
