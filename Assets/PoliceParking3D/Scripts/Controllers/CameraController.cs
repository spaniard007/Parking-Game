using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private Transform camMenuPos;
    [SerializeField] private Transform camGamePos;
    

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
        Debug.Log("Test");
        if(mainCamera==null)
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendCameraToMenuPos()
    {
        mainCamera.transform.DOMove(camMenuPos.position, 0.5f);
        mainCamera.transform.DORotate(camMenuPos.eulerAngles, 0.5f);

    }
    
    public void SendCameraToGamePos()
    {
        mainCamera.transform.DOMove(camGamePos.position, 0.5f);
        mainCamera.transform.DORotate(camGamePos.eulerAngles, 0.5f);
    }
}
