using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKScript : PVR_InteractionController
{
    public GameObject muzzleSight;
    public AudioClip shotclips;

    private MagazineController magazineController;
    private float shootingDelay = 0.1f;
    private Shoot shootScript;
    private int pointValue = 10;   

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

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) && shootScript.timer >= shootingDelay)
        {
            shootScript.gunSoundAllowed = true;
            shootScript.ShootGun(muzzleSight, pointValue, shootingDelay);
            //magazineController.UpdateMagInventory(1);
        }
    }
}
