using UnityEngine;
using System.Collections;

public class Teleport_Vive : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject Camera;
    SteamVR_TrackedController controller;
    bool triggerDown = false;
    GameObject grabbedObj;
    [SerializeField] float movementSpeed;

    void Start()
    {
        controller = gameObject.GetComponent<SteamVR_TrackedController>();
    }

    void Update ()
    {
        if (controller.triggerPressed && !triggerDown)
        {
            triggerDown = true;
            StartCoroutine(MoveForward());
        }
    }

    IEnumerator MoveForward()
    {
        while(controller.triggerPressed)
        {
            player.GetComponent<Rigidbody>().AddForce(Camera.transform.forward *  movementSpeed);
            yield return null;
        }

        triggerDown = false;
    }

}
