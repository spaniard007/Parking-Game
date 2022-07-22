using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image gameMenuBG;
    [SerializeField] private Image dragBG;
    [SerializeField] private Image levelCompleteBG;

    [SerializeField] private TMP_Text playText;

    
    // Start is called before the first frame update
    void Start()
    {
        playText.DOColor(Color.clear, 0.5f).SetEase(Ease.InOutExpo).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameState.menu)
        {
            GameMenuSetUp();
        }
        else if (GameManager.instance.gameState == GameState.game)
        {
            GameSetUp();
        }
        else if (GameManager.instance.gameState == GameState.complete)
        {
            LevelCompleteSetUp();
        }

        
    }

    public void PlayButtonPress()
    {
        GameManager.instance.gameState = GameState.game;
    }
    
    public void RestartButtonPress()
    {
        GameManager.instance.RestartGame();
    }

    public void GameMenuSetUp()
    { //Refactor and tween later on
        gameMenuBG.gameObject.SetActive(true);
        dragBG.gameObject.SetActive(false);
        levelCompleteBG.gameObject.SetActive(false);
        CameraController.instance.SendCameraToMenuPos();
    }
    
    public void GameSetUp()
    { //Refactor and tween later on
        gameMenuBG.gameObject.SetActive(false);
        dragBG.gameObject.SetActive(true);
        levelCompleteBG.gameObject.SetActive(false);
        CameraController.instance.SendCameraToGamePos();
    }
    
    public void LevelCompleteSetUp()
    { //Refactor and tween later on
        gameMenuBG.gameObject.SetActive(false);
        dragBG.gameObject.SetActive(false);
        levelCompleteBG.gameObject.SetActive(true);
        CameraController.instance.SendCameraToMenuPos();
    }
}
