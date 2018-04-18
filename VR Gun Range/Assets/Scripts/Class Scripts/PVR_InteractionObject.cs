using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVR_InteractionObject : MonoBehaviour {

    protected Transform cachedTransform;
    [HideInInspector]
    public PVR_InteractionController currentController;

    public virtual void OnGripWasPressed(PVR_InteractionController controller)
    {
        currentController = controller;
    }

    public virtual void OnGripIsBeingPressed(PVR_InteractionController controller)
    {
    }

    public virtual void OnGripWasReleased(PVR_InteractionController controller)
    {
        currentController = null;
    }

    public virtual void Awake()
    {
        cachedTransform = transform;
        if(!gameObject.CompareTag("InteractionObject"))
        {
            Debug.LogWarning("This InteractionObject does not hav the correct tag, setting it now.", gameObject);
            gameObject.tag = "InteractionObject";
        }
    }

    public bool IsFree()
    {
        return currentController == null;
    }

    public virtual void OnDestroy()
    {
        if(currentController)
        {
            OnGripWasReleased(currentController);
        }
    }
}
