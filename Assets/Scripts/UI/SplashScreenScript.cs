using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

namespace UI
{
    public class SplashScreenScript : MonoBehaviour
    {
        
        public RectTransform logo;
        public RectTransform words;
        public RectTransform blackCover;

        public BlackBoxFade blackBoxFade;
        
        private float t;
        private float prog;
        
        
        void Update()
        {
            t += Time.deltaTime;

            if (t >= 1 && t <= 2)
            {
                prog += Time.deltaTime;
                
                float easing = Easing.InOutCirc(prog);
                
                logo.anchoredPosition = new Vector2(easing * -250, 0);
                words.anchoredPosition = new Vector2(easing * 400 - 250, 0);
                blackCover.anchoredPosition = new Vector2(easing * -250 - 250, 0);
            
            }

            if (t >= 2 && t <= 3)
            {
                
                blackBoxFade.setInvisibilityFade(false);
            }

            if (t >= 4)
            {
                SceneManager.LoadScene(0);
            }
        }
    

        
        
        

    }
}