﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PVR_InteractionController {

    public float range = 100;
    public ScoreController scoreController;

    [HideInInspector]
    public float shootingDelay = 0.1f;
    [HideInInspector]
    public float timer;
    [HideInInspector]
    public bool gunSoundAllowed;
    [HideInInspector]
    public AudioSource audioSource;
    
    private AKScript akScript;
    private EnfieldScript enfieldScript;
    

    public override void Awake()
    {
        base.Awake();

        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        enfieldScript = GetComponent<EnfieldScript>();
        akScript = GetComponent<AKScript>();
        enfieldScript.enabled = false;
        akScript.enabled = false;
    }

    public override void Update()
    {
        base.Update();


        timer += Time.deltaTime;

        if (objectBeingInteractedWith.name == "Enfield")
        {
            enfieldScript.enabled = true;
        }
        else if(objectBeingInteractedWith.name != "Enfield")
        {
            enfieldScript.enabled = false;
        }
        if (objectBeingInteractedWith.name == "AK-47")
        {
            akScript.enabled = true;
        }
        else if(objectBeingInteractedWith.name != "AK-47")
        {
            akScript.enabled = false;
        }

        Debug.Log(objectBeingInteractedWith.name);
    }

    public void ShootGun(GameObject raycastBegin, int points)
    {
        timer = 0;
        Vector3 lineOrigin = raycastBegin.transform.position;
        RaycastHit hit = new RaycastHit();
        scoreController.AddToBulletCount(1);
        audioSource.Play();

        if (Physics.Raycast(lineOrigin, raycastBegin.transform.forward, out hit, range))
        {
            print("hit " + hit.collider.gameObject);
        }
        if(hit.collider.gameObject.name == "KnightCollider")
        {
            scoreController.AddScore(points);
        }
    }

    public void PlayGunSound()
    {
        if (gunSoundAllowed && timer >= shootingDelay)
        {
            audioSource.Play();
            Debug.Log("Sound should be happening");
        }
    }

    public void SetupSound(AudioClip shotClips)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.2f;
        audioSource.clip = shotClips;
    }
}
