using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class AudiController : MonoBehaviour
{
    public AudioSource source;

    #region AudioClipsVar
    
    [SerializeField]private AudioClip playButtonSound;
    [SerializeField]private AudioClip levelCompleteSound;
    [SerializeField]private AudioClip obstacleHitSound;
    [SerializeField]private AudioClip getOutOfParkingSound;
    [SerializeField]private AudioClip drivingAwaySound;
    [SerializeField]private AudioClip sirenSound;
    [SerializeField]private AudioClip hornSound;
    
    #endregion
    
    
    public static AudiController instance;
    
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void PlayButtonClick()
    {
        source.PlayOneShot(playButtonSound);
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
        source.PlayOneShot(drivingAwaySound,.9f);
        source.volume = 1f;
    }
    
}