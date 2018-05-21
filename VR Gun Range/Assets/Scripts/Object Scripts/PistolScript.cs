using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : PVR_InteractionController {

    public GameObject barrelEnd;
    public AudioClip shotClips;

    private float shootingDelay = .2f;
    private Shoot shootScript;
    private int pointValue = 50;

    public override void Awake()
    {
        base.Awake();
        shootScript = GetComponent<Shoot>();
    }

    public override void Start()
    {
        base.Start();
        shootScript.SetupSound(shotClips);
    }

    public override void Update()
    {
        Vector3 lineOrigin = barrelEnd.transform.position;
        Debug.DrawRay(lineOrigin, barrelEnd.transform.forward * shootScript.range, Color.green);

        if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && shootScript.timer >= shootingDelay)
        {
            shootScript.gunSoundAllowed = true;
            shootScript.ShootGun(barrelEnd, pointValue, shootingDelay);
        }
    }
}
