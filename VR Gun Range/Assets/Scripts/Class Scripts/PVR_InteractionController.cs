using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVR_InteractionController : MonoBehaviour {

    public GameObject snapColliderOrigin;
    public GameObject controllerModel;
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Vector3 angularVelocity;
    [HideInInspector]
    public PVR_InteractionObject objectBeingInteractedWith;

    private SteamVR_TrackedObject trackedObj;

    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public PVR_InteractionObject InteractionObject
    {
        get { return objectBeingInteractedWith; }
    }

    public virtual void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controllerModel = GameObject.FindGameObjectWithTag("ControllerModel");
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            CheckForInteractionObject();
        }

        if(Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            if (objectBeingInteractedWith)
            {
                if (objectBeingInteractedWith)
                {
                    objectBeingInteractedWith.OnGripWasPressed(this);
                }
            }
        }

        if(Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if(objectBeingInteractedWith)
            {
                objectBeingInteractedWith.OnGripWasReleased(this);
                objectBeingInteractedWith = null;
            }
        }
    }

    public void CheckForInteractionObject()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(snapColliderOrigin.transform.position,
            snapColliderOrigin.transform.lossyScale.x / 2f);

        foreach(Collider overlappedCollider in overlappedColliders)
        {
            if(overlappedCollider.CompareTag("InteractionObject") &&
                overlappedCollider.GetComponent<PVR_InteractionObject>().IsFree())
            {
                objectBeingInteractedWith = overlappedCollider.GetComponent<PVR_InteractionObject>();
                objectBeingInteractedWith.OnGripWasPressed(this);
                return;
            }
        }
    }

    public virtual void FixedUpdate()
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        velocity = Controller.velocity;
        angularVelocity = Controller.angularVelocity;
    }

    public void HideControllerModel()
    {
        controllerModel.SetActive(false);
        snapColliderOrigin.SetActive(false);
    }

    public void ShowControllerModel()
    {
        snapColliderOrigin.SetActive(true);
        controllerModel.SetActive(true);
    }

    public void SwitchInteractionObjectTo(PVR_InteractionObject interactionObject)
    {
        objectBeingInteractedWith = interactionObject;
        objectBeingInteractedWith.OnGripWasPressed(this);
    }
}
