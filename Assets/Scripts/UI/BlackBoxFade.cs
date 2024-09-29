using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace UI
{
    public class BlackBoxFade : MonoBehaviour
    {

        public float t;
        private float tStart;
        public bool goingToInvisible = true;
        private Image image;
        public bool needToBeActive;

        public void Start()
        {
            tStart = t;
            image = GetComponent<Image>();
        }
        
        public void Update()
        {
            if (goingToInvisible) t = Mathf.Max(t - Time.deltaTime, 0);
            else t = Mathf.Min(t + Time.deltaTime, tStart);
            if (!needToBeActive) gameObject.SetActive(t > 0);
            image.color = new Color(image.color.r, image.color.g, image.color.b,  Easing.InCirc(t/tStart));
        }

        public void setInvisibilityFade(bool val)
        {
            if (!needToBeActive) gameObject.SetActive(true);
            goingToInvisible = val;
        }
        
        
        
        
    }
}