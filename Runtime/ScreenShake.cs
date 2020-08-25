using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mlaikhram.Common
{
    [RequireComponent(typeof(Camera))]
    public class ScreenShake : MonoBehaviour
    {
        /// <summary>
        /// Gets the ScreenShake associated with the main Camera, or null if either the ScreenShake component or the main Camera do not exist.
        /// </summary>
        public static ScreenShake main;

        /// <summary>
        /// Determines how fast the screen will shake.
        /// </summary>
        public float shakeFrequency;

        private Camera myCamera;
        private Vector3 pivot;

        private Vector3 intensity;
        private float duration;
        private float timer;


        void Start()
        {
            myCamera = GetComponent<Camera>();
            pivot = transform.localPosition;
            if (Camera.main == myCamera)
            {
                main = this;
            }
        }

        void FixedUpdate()
        {
            if (timer > 0)
            {
                float unitOffset = (timer / duration) * Mathf.Sin(2f * Mathf.PI * shakeFrequency * (duration - timer));
                transform.localPosition = new Vector3(
                    pivot.x + unitOffset * intensity.x,
                    pivot.y + unitOffset * intensity.y,
                    pivot.z + unitOffset * intensity.z
                    );
                timer -= Time.deltaTime;
            }
            else
            {
                transform.localPosition = pivot;
                timer = 0;
            }
        }

        /// <summary>
        /// Shakes the camera.
        /// </summary>
        /// <param name="intensity">How strong and in what direction to shake.</param>
        /// <param name="duration">How long the shake should last.</param>
        public void ShakeCamera(Vector3 intensity, float duration)
        {
            this.intensity = intensity;
            this.duration = duration;
            this.timer = duration;
        }
    }
}
