using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVR_SnapToController : PVR_InteractionObject {
    
    public Vector3 snapPositionOffset;
    public Vector3 snapRotationOffset;

    private bool hideControllerModel = true;
    private Rigidbody rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnGripWasPressed(PVR_InteractionController controller)
    {
        base.OnGripWasPressed(controller);

        if (hideControllerModel)
        {
            controller.HideControllerModel();
        }

        ConnectToController(controller);
    }

    public override void OnGripWasReleased(PVR_InteractionController controller)
    {
        base.OnGripWasReleased(controller);
        if (hideControllerModel)
        {
            controller.ShowControllerModel();
        }

        ReleaseFromController(controller);
    }

    private void ConnectToController(PVR_InteractionController controller)
    {
        cachedTransform.SetParent(controller.transform);

        cachedTransform.rotation = controller.transform.rotation;
        cachedTransform.Rotate(snapRotationOffset);
        cachedTransform.position = controller.snapColliderOrigin.transform.position;
        cachedTransform.Translate(snapPositionOffset, Space.Self);

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void ReleaseFromController(PVR_InteractionController controller)
    {
        cachedTransform.SetParent(null);

        rb.useGravity = true;
        rb.isKinematic = false;

        rb.velocity = controller.velocity;
        rb.angularVelocity = controller.angularVelocity;
    }
}
