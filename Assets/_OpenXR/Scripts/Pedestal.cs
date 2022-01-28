using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _OpenXR.Scripts
{
    public class Pedestal : MonoBehaviour
    {
        public void StartMovement()
        {
            StartCoroutine(SmoothLerp(0.75f));
        }
        
        private IEnumerator SmoothLerp(float time)
        {
            var position = transform.position;
            Vector3 startingPos = position;
            Vector3 finalPos = position + new Vector3(0f, 90f, 0f);
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}