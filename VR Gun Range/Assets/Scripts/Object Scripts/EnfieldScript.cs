using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnfieldScript : PVR_InteractionController {

    public float range = 100;
    public AudioClip shotClips;
    public GameObject muzzleSight;

    private float timer;
    private AudioSource audioSource;
    
    public override void Awake()
    {
        base.Awake();

        SetupSound();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        Vector3 lineOrigin = muzzleSight.transform.position;
        Debug.DrawRay(lineOrigin, muzzleSight.transform.forward * range, Color.green);

        timer += Time.deltaTime;
        
        if(objectBeingInteractedWith.name == "Enfield")
        {
            Debug.Log("You made it in");
            if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("You should be shooting now");
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        timer = 0;
        Vector3 lineOrigin = muzzleSight.transform.position;
        RaycastHit hit = new RaycastHit();
        audioSource.Play();

        if (Physics.Raycast(lineOrigin, muzzleSight.transform.forward, out hit, range))
        {
            print("hit " + hit.collider.gameObject);
        }
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
        audioSource.clip = shotClips;
    }
}
