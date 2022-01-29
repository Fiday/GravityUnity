using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _OpenXR.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class SongSpawnerScript : MonoBehaviour
{
    //public Dictionary<AudioClip, float> Clips { get; set; }
    public List<AudioClip> Clips;
    [SerializeField] private GameObject planetPrefab;
    public Vector3 spawnOffset = new(-2f, 0f, 0f);
    [SerializeField] public float[] timeOffsets;
    public InputActionReference resetBalls;


    // Start is called before the first frame update
    void Start()
    {
        resetBalls.action.canceled += ResetBalls;
    }

    private void OnDestroy()
    {
        resetBalls.action.canceled -= ResetBalls;
    }

    public void StartBeat()
    {
        StartCoroutine(SpawnBalls());
    }

    private IEnumerator SpawnBalls()
    {
        float elapsedTime = 0;

        for (int i = 0; i < Clips.Count; i++)
        {
            GameObject gravityObject = Instantiate(planetPrefab, transform);
            gravityObject.GetComponent<SoundScript>().AddSound(Clips[i]);

            GameObject blackhole = GameObject.FindWithTag("Blackhole");

            var position = blackhole.transform.position;
            gravityObject.transform.position = position + spawnOffset;
            float distance = Vector3.Distance(position, gravityObject.transform.position);
            gravityObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0,
                Gravity.OrbitVelocity(blackhole.GetComponent<AttractionComponent>().Mass, distance));
            
            while (elapsedTime < timeOffsets[i])
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;
            yield return null;
        }
    }
    
    private void ResetBalls(InputAction.CallbackContext context)
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}