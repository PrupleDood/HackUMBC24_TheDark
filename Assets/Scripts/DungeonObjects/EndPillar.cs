using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonObjects
{
    public class EndPillar : Pillar
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameObject obj = GameObject.FindWithTag("BlackBox");
                obj.SetActive(true);
                obj.GetComponent<BlackBoxFade>().setInvisibilityFade(false);
                
                SceneManager.LoadScene(2);
            }
        }
    }
}