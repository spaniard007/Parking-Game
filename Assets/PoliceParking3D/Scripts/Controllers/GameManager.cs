using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    menu,game,complete
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    [SerializeField] private Transform vehicleParent;
    
    [SerializeField] private List<VehicleMovementController> vehicles = new List<VehicleMovementController>();
    
    
    
    #region Singletone
    
    public static GameManager instance;
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
        gameState = GameState.menu;
        foreach (Transform vehicle in vehicleParent)
        {
            vehicles.Add(vehicle.GetComponent<VehicleMovementController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (vehicles.Count == 0)
        {
            gameState = GameState.complete;
            CameraController.instance.SendCameraToMenuPos();
            AudioController.instance.PlayLevelComplete();
        }
    }

    public void DeleteVehicle(VehicleMovementController vehicle)
    {
        if (vehicles.Count > 0)
        {
            vehicles.RemoveAt(vehicles.IndexOf(vehicle));
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
