using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSoundsManager : MonoBehaviour {
    [System.Serializable]
    public class FloorSoundsEntry
    {
        public GameObject gameObject;
        public AudioClip walkingAudioClip;
        public AudioClip runningAudioClip;
    }

    public FloorSoundsEntry[] floorWalkingSounds;

    private class AudioClipPair
    {
        public AudioClip walkingAudioClip;
        public AudioClip runningAudioClip;

        public AudioClipPair(AudioClip walkingAudioClip, AudioClip runningAudioClip)
        {
            this.walkingAudioClip = walkingAudioClip;
            this.runningAudioClip = runningAudioClip;
        }
    }

    private Dictionary<GameObject, AudioClipPair> floorWalkingSoundsDict;
    private GameObject currentFloor;
    private AudioSource audioSource;

    void Start()
    {
        floorWalkingSoundsDict = new Dictionary<GameObject, AudioClipPair>();
        for (int i = 0; i < floorWalkingSounds.Length; i++)
        {
            FloorSoundsEntry floorSoundsEntry = floorWalkingSounds[i];
            floorWalkingSoundsDict.Add(floorSoundsEntry.gameObject, new AudioClipPair(floorSoundsEntry.walkingAudioClip, floorSoundsEntry.runningAudioClip));
        }
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void OnEnable()
    {
        PlayerController.OnStopped += StopSound;
        PlayerController.OnWalking += PlayWalkingSound;
        PlayerController.OnRunning += PlayRunningSound;
    }

    private void OnDisable()
    {
        PlayerController.OnStopped -= StopSound;
        PlayerController.OnWalking -= PlayWalkingSound;
        PlayerController.OnRunning -= PlayRunningSound;
    }

    public void SetCurrentFloor(GameObject floor)
    {
        currentFloor = floor;
    }

    public void StopSound()
    {
        audioSource.Pause();
    }

    public void PlayWalkingSound()
    {
        if (currentFloor != null)
        { 
            AudioClipPair audioClipPair = GetAudioClipPairForCurrentFloor();
            audioSource.clip = audioClipPair.walkingAudioClip;
            PlayOrUnPauseAudioSource();
        }
    }

    public void PlayRunningSound()
    {
        if (currentFloor != null)
        {
            AudioClipPair audioClipPair = GetAudioClipPairForCurrentFloor();
            audioSource.clip = audioClipPair.runningAudioClip;
            PlayOrUnPauseAudioSource();
        }

    }

    private void PlayOrUnPauseAudioSource()
    {
        if (audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Play();
        }
    }

    private AudioClipPair GetAudioClipPairForCurrentFloor()
    {
        AudioClipPair audioClipPair;
        if (!floorWalkingSoundsDict.TryGetValue(currentFloor, out audioClipPair)) throw new Exception("There are no sounds for " + currentFloor.name);
        return audioClipPair;
    }
}
