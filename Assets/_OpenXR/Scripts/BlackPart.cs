using UnityEngine;

public class BlackPart : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
