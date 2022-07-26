using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image gameMenuBG;
    [SerializeField] private Image gameHudBG;
    [SerializeField] private Image levelCompleteBG;

    [SerializeField] private Image policeManIMG;
    [SerializeField] private Image policeCarIMG;


    [SerializeField] private TMP_Text playText;

    
    // Start is called before the first frame update
    void Start()
    {
        MenuSceneInit();
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

    public void MenuSceneInit()
    {
        playText.DOColor(Color.clear, 0.5f).SetEase(Ease.InOutExpo).SetLoops(-1, LoopType.Yoyo);
        policeManIMG.transform.DOMoveX(0f, 0.5f).SetEase(Ease.InOutBounce);
        policeCarIMG.transform.DOLocalMoveX(100f, 0.5f).SetEase(Ease.InOutBounce);
    }


    #region ButtonAction
    
    public void PlayButtonPress()
    {
        AudioController.instance.PlayButtonClick();
        AudioController.instance.PlayEngineStartSound();
        policeManIMG.transform.DOMoveX(-800f, 0.5f).SetEase(Ease.InSine);
        policeCarIMG.transform.DOLocalMoveX(800f, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {
            GameManager.instance.gameState = GameState.game;
            AudioController.instance.StopSound();
        });
    }
    
    public void HomeButtonPress()
    {
        GameManager.instance.GoToHome();
       
    }

    public void RestartGamePress()
    {   
        GameManager.instance.RestartGame();
    }
    
    #endregion

    #region UISetUP
    public void GameMenuSetUp()
    { 
        gameMenuBG.gameObject.SetActive(true);
        gameHudBG.gameObject.SetActive(false);
        levelCompleteBG.gameObject.SetActive(false);
        CameraController.instance.SendCameraToMenuPos();
    }
    
    public void GameSetUp()
    { 
        gameMenuBG.gameObject.SetActive(false);
        gameHudBG.gameObject.SetActive(true);
        levelCompleteBG.gameObject.SetActive(false);
        CameraController.instance.SendCameraToGamePos();
    }
    
    public void LevelCompleteSetUp()
    { 
        gameMenuBG.gameObject.SetActive(false);
        gameHudBG.gameObject.SetActive(false);
        levelCompleteBG.gameObject.SetActive(true);
        CameraController.instance.SendCameraToMenuPos();
    }
    
    #endregion
}
