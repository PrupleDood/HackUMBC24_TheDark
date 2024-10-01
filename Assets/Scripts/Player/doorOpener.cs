using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpener : MonoBehaviour
{
    public float maxTriggerDistance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            CheckForDoor();
        }
    }

    public void CheckForDoor() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(
            ray, out hit, 
            maxTriggerDistance * transform.localScale.x, 
            Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore
        )) {
            if (!hit.collider.CompareTag("Door")) {
                Debug.Log(hit.collider.name);
                return;
            }
            
            GameObject door = hit.collider.gameObject;

            doorAnimator doorScript = door.GetComponentInChildren<doorAnimator>();

            if (doorScript.isTriggered) {
                doorScript.isTriggered = false;
            
                doorScript.TriggerClose();
            }

            else {
                doorScript.isTriggered = true;

                doorScript.TriggerOpen();
            }
            
        }
    }

}

