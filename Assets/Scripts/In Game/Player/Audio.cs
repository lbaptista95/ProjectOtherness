﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Audio : MonoBehaviour {

    // Use this for initialization
    
    public AudioClip[] clips;
    AudioSource audioSource;
    public System.Random rnd = new System.Random();
	void Awake () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void WalkStepRight()
    {
        audioSource.PlayOneShot(clips[0]);
    }

    private void WalkStepLeft()
    {
        audioSource.PlayOneShot(clips[1]);
    }

    private void RunStepRight()
    {
        audioSource.PlayOneShot(clips[2]);
    }

    private void RunStepLeft()
    {
        audioSource.PlayOneShot(clips[3]);
    }

    private void ShotAudio()
    {
        audioSource.PlayOneShot(clips[4]);
    }

    public AudioClip RandomizeEnemyDamageSound()
    {
        int clipIndex;
        clipIndex = rnd.Next(5, 7);
        return this.clips[clipIndex];
    }

}