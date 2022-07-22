using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;

    #region Singletone
    
    public static CameraController instance;
    private void Awake() 
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
