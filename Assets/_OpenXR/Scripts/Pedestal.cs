using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _OpenXR.Scripts
{
    public class Pedestal : MonoBehaviour
    {
        public Vector3 StartPos = new Vector3(0,-100,-4);
        public Vector3 EndPos= new Vector3(0,-10,-4);

        public void StartMovement(bool up)
        {
            StartCoroutine(!up ? MoveUpSmoothLerp(0.75f) : MoveDownSmoothLerp(0.75f));
        }

        private IEnumerator MoveUpSmoothLerp(float time)
        {
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(StartPos, EndPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator MoveDownSmoothLerp(float time)
        {
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(EndPos, StartPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}