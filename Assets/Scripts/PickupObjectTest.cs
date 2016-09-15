using UnityEngine;
using System.Collections.Generic;

public class PickupObjectTest : MonoBehaviour
{
    SteamVR_TrackedController controller;
    bool triggerDown = false;
    GameObject grabbedObj;

    void Start()
    {
        controller = gameObject.GetComponent<SteamVR_TrackedController>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!controller.triggerPressed && triggerDown)
        {
            triggerDown = false;
            grabbedObj.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObj.transform.parent = null;
            grabbedObj.GetComponent<Rigidbody>().velocity = HandVelocity;
            grabbedObj = null;
        }
        HandVelocity = (transform.localPosition - HandPrevious) / Time.deltaTime;
        HandPrevious = transform.localPosition;
    }

    Vector3 HandVelocity = Vector3.zero;
    Vector3 HandPrevious = Vector3.zero;


    void OnTriggerStay(Collider c)
    {
        if (controller.triggerPressed && !triggerDown)
        {
            if (c.gameObject.CompareTag("Pickup"))
            {
                Debug.Log("Hit");
                grabbedObj = c.gameObject;
                grabbedObj.transform.parent = gameObject.transform;
                grabbedObj.GetComponent<Rigidbody>().isKinematic = true;
                triggerDown = true;
            }
        }
    }
}
