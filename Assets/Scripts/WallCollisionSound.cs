using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionSound : MonoBehaviour {

    public AudioClip walkingPlayerCollidesAudioClip;
    private AudioSource audioSource;
    
    // Use this for initialization
	void Start () {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void StopPlayingSounds()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
	
	public void PlaySound() {
        StopPlayingSounds();
        audioSource.clip = walkingPlayerCollidesAudioClip;
        audioSource.Play();
    }
}
