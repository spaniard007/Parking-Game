using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource source;

    #region AudioClipsVar
    
    [SerializeField]private AudioClip playButtonSound;
    [SerializeField]private AudioClip levelCompleteSound;
    [SerializeField]private AudioClip obstacleHitSound;
    [SerializeField]private AudioClip getOutOfParkingSound;
    [SerializeField]private AudioClip drivingSound;
    [SerializeField]private AudioClip sirenDriveAwaySound;
    [SerializeField]private AudioClip hornSound;
    [SerializeField]private AudioClip engineStartSound;
    
    #endregion
    
    
    public static AudioController instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        
    }

    public void PlayButtonClick()
    {
        source.PlayOneShot(playButtonSound);
        source.volume = 1f;
    }

    public void PlayEngineStartSound()
    {
        source.PlayOneShot(engineStartSound);
        source.volume = 1f;
    }
    public void PlayLevelComplete()
    {
        source.PlayOneShot(levelCompleteSound);
        source.volume = .9f;
    }
    
    
    public void PlayObstacleHitSound()
    {
        source.PlayOneShot(obstacleHitSound);
        source.volume = .9f;
    }
    public void PlayGetOutParkingSound()
    {
        source.PlayOneShot(getOutOfParkingSound);
        source.volume = .9f;
    }
    
    
    public void PlayDrivingAwaySound()
    {
        source.PlayOneShot(drivingSound);
        source.volume = 1f;
    }

    public void PlayHornSound()
    {
        source.PlayOneShot(hornSound);
        source.volume = 1f;
    }
    
    public void StopSound()
    {
        source.Stop();
    }

    public void VehicleMovementSound(VehicleMovementController vehicle, VehicleState vehicleState,bool playSound)
    {
        AudioSource vehicleAudioSource = vehicle.GetComponent<AudioSource>();
        if (vehicleAudioSource != null)
        {
            if (playSound)
            {

                if (vehicleState == VehicleState.Moving_In_Parking)
                {
                    vehicleAudioSource.clip = drivingSound;
                }
                else
                {
                    vehicleAudioSource.clip = sirenDriveAwaySound;
                }

                vehicleAudioSource.Play();
            }
            else
            {
                vehicleAudioSource.Stop();
            }
        }
    }



}