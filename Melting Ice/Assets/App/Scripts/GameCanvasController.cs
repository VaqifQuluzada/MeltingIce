using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameCanvasController : MonoBehaviour
{

    #region Panels
    [SerializeField] private GameObject mainMenuButtonsPanel;

    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject shopPanel;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject gamePlayPanel;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    #endregion

    #region SpriteStates

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    #endregion

    [SerializeField] private Button musicButton;

    [SerializeField] private Button soundButton;

    [SerializeField] private AudioSource backgroundMusicSource;

    [SerializeField] private AudioSource buttonPressSFXSource;

    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        GamePlayManager.instance.gamePlayState = GamePlayStates.GameStart;

        SetMusicAndSoundButtons();
    }


    #region MainMenu Buttons Methods
    public void onPlayButtonPressed()
    {
        GamePlayManager.instance.gamePlayState = GamePlayStates.Gaming;
        mainMenuButtonsPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        onButtonSoundPlayed();
    }


    public void onOptionsMenuButtonPressed()
    {
        SetAllPanelsDeactive();

        optionsPanel.SetActive(true);

        onButtonSoundPlayed();
    }


    public void onCreditsMenuPanelPressed()
    {
        SetAllPanelsDeactive();
        creditsPanel.SetActive(true);

        onButtonSoundPlayed();
    }

    public void onReturnToMainMenuButtonPressed()
    {
        SetAllPanelsDeactive();
        mainMenuButtonsPanel.SetActive(true);

        onButtonSoundPlayed();
    }

    public void onShopMenuButtonPressed()
    {
        SetAllPanelsDeactive();
        shopPanel.SetActive(true);

        onButtonSoundPlayed();
    }

    private void SetAllPanelsDeactive()
    {
        optionsPanel.SetActive(false);
        mainMenuButtonsPanel.SetActive(false);
        shopPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    #endregion


    #region OptionsMenu Buttons

    public void onMusicButtonPressed()
    {
        if (playerStats.isMusicOn)
        {
            playerStats.isMusicOn = false;
            musicButton.image.sprite = musicOffSprite;
            backgroundMusicSource.mute = true;
        }
        else
        {
            playerStats.isMusicOn = true;
            musicButton.image.sprite = musicOnSprite;
            backgroundMusicSource.mute = false;

            //to play music from beginning
            backgroundMusicSource.Stop();
            backgroundMusicSource.Play();

        }

        onButtonSoundPlayed();

    }

    public void onSoundButtonPressed()
    {
        if (playerStats.isSoundOn)
        {
            playerStats.isSoundOn = false;
            soundButton.image.sprite = soundOffSprite;
            buttonPressSFXSource.mute = true;
        }
        else
        {
            playerStats.isSoundOn = true;
            soundButton.image.sprite = soundOnSprite;
            buttonPressSFXSource.mute = false;
        }

        onButtonSoundPlayed();
    }

    private void SetMusicAndSoundButtons()
    {
        if (playerStats.isMusicOn == true)
        {
            musicButton.image.sprite = musicOnSprite;
            backgroundMusicSource.mute = false;
        }
        else
        {
            musicButton.image.sprite = musicOffSprite;
            backgroundMusicSource.mute = true;
        }

        if (playerStats.isSoundOn== true)
        {
            soundButton.image.sprite = soundOnSprite;
            buttonPressSFXSource.mute = false;
        }
        else
        {
            soundButton.image.sprite = soundOffSprite;
            buttonPressSFXSource.mute = true;
        }

    }


    private void onButtonSoundPlayed()
    {
        buttonPressSFXSource.Stop();
        buttonPressSFXSource.Play();
    }

    #endregion

    #region Pause Menu Buttons

    public void onPauseButtonPressed()
    {
        Time.timeScale = 0;

        pausePanel.SetActive(true);
    }

    public void onResumeButtonPressed()
    {
        Time.timeScale = 1;

        pausePanel.SetActive(false);
    }

    public void onMainMenuButtonPressed()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GamePlayScene");
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        gameOverScoreText.text = "Your Score:" +GamePlayManager.instance.ReturnCoinCount().ToString();

    }
    #endregion
}
