using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKReload : PVR_InteractionObject
{   
    public PVR_InteractionController[] interactionController;

    private Shoot shootScript;
    private GameObject AK47;
    private MeshCollider magSensor;
    private bool magReady;
    private Rigidbody rb;    
    private bool gripWasReleased;
    private bool magGrabbed;

    public override void Awake()
    {
        base.Awake();
        AK47 = GameObject.Find("AK-47");
        shootScript = GameObject.Find("[CameraRig]").GetComponentInChildren<Shoot>();
        cachedTransform = transform;        
    }

    private void Start()
    {
        magSensor = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (magReady)
        {
            shootScript.bulletsReady = true;
        }
        if (!magReady)
        {
            shootScript.bulletsReady = false;
        }
        if(shootScript.bulletsReady)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.name == "AK-47")
        {
            //if(interactionController[0].GetComponent<FixedJoint>())
            {
                ConnectMagToGun(AK47);
            }
            /*else if(interactionController[1].GetComponent<FixedJoint>())
            {
                ConnectMagToGun(AK47);
            }
            else
            {
                return;
            }*/
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "AK-47")
        {
            DisconnectMagFromGun(AK47);
        }
    }

    private void ConnectMagToGun(GameObject gun)
    {
        //var joint = AddFixedJoint(AK47);
        //joint.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
        cachedTransform.SetParent(AK47.transform);

        cachedTransform.rotation = AK47.transform.rotation;
        cachedTransform.Rotate(0, 0, 0);
        cachedTransform.position = AK47.transform.position;
        cachedTransform.Translate(0, 0, 0, Space.Self);

        shootScript.akMagReady = true;
        magReady = true;
        //cachedTransform.transform.rotation = gun.transform.rotation;
        //cachedTransform.transform.position = gun.transform.position;
    }

    public void DisconnectMagFromGun(GameObject gun)
    {
        /*if (gameObject.GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(gameObject.GetComponent<FixedJoint>());
            shootScript.akMagReady = false;
            magReady = false;
        }*/
    }

    private FixedJoint AddFixedJoint(GameObject Gun)
    {
        FixedJoint fx = Gun.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}