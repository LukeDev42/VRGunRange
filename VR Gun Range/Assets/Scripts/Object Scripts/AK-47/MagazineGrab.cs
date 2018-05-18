using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineGrab : MonoBehaviour {

    public PVR_InteractionController[] interactionController;
    public Vector3 snapRotationOffset;
    public Vector3 snapTranslationOffset;

    private Transform cachedTransform;
    private GameObject collidingController;
    private GameObject controllerInHand;

    private void Awake()
    {
        cachedTransform = transform;
    }

    private void Update()
    {
        if (interactionController[0].Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) || 
            interactionController[1].Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (collidingController)
            {
                GrabObject();
            }
        }

        if(interactionController[0].Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) || 
           interactionController[1].Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if(controllerInHand)
            {
                ReleaseObject();
            }
        }
        Debug.Log(collidingController);
    }

    private void SetCollidingController(Collider col)
    {
        if ((collidingController || !col.GetComponent<Rigidbody>()) && !col.GetComponent<PVR_InteractionController>())
        {
            return;
        }
        collidingController = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingController(other);
        Debug.Log(other);
    }
    public void OnTriggerStay(Collider other)
    {
        SetCollidingController(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!collidingController)
        {
            return;
        }
        collidingController = null;
    }

    private void GrabObject()
    {
        controllerInHand = collidingController;
        collidingController = null;

        cachedTransform.Rotate(snapRotationOffset);
        cachedTransform.Translate(snapTranslationOffset);

        var joint = AddFixedJoint();
        joint.connectedBody = gameObject.GetComponent<Rigidbody>();
    }

    private void ReleaseObject()
    {
        if(controllerInHand.GetComponent<FixedJoint>())
        {
            controllerInHand.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(controllerInHand.GetComponent<FixedJoint>());
            GetComponent<Rigidbody>().velocity = controllerInHand.GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().angularVelocity = controllerInHand.GetComponent<Rigidbody>().angularVelocity;
        }

        controllerInHand = null;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = controllerInHand.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}
