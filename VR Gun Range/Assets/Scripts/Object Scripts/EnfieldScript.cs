using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnfieldScript : PVR_InteractionController
{
    public GameObject muzzleSight;
    public AudioClip shotclips;

    private float shootingDelay = 1;
    private Shoot shootScript;
    private int pointValue = 100;

    public override void Awake()
    {
        base.Awake();
        shootScript = GetComponent<Shoot>();
    }

    public override void Start()
    {
        base.Start();

        shootScript.SetupSound(shotclips);
    }

    public override void Update()
    {
        Vector3 lineOrigin = muzzleSight.transform.position;
        Debug.DrawRay(lineOrigin, muzzleSight.transform.forward * shootScript.range, Color.green);

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && shootScript.timer >= shootingDelay)
        {
            shootScript.gunSoundAllowed = true;
            shootScript.ShootGun(muzzleSight, pointValue, shootingDelay);
        }
    }
}