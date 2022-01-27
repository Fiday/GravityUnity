using UnityEngine;

namespace _OpenXR.Scripts
{
    public class Orbiting : MonoBehaviour
    {
        [SerializeField] private float orbitRadius = 1f;
        [SerializeField] private float size = 1f;


        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(0f, 0f, -orbitRadius);
            transform.localScale = new Vector3(size, size, size);

            GameObject blackhole = GameObject.FindWithTag("Blackhole");
            GetComponent<Rigidbody>().velocity = new Vector3(
                Gravity.OrbitVelocity(blackhole.GetComponent<AttractionComponent>().Mass, orbitRadius), 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}