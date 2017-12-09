using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MenuManager
{
  // AudioMixer Reference
  public AudioMixer PM_AudioMixer;
  // BGM Image Reference
  public Image PM_BGM;
  // SFX Image Reference
  public Image PM_SFX;
  // All the sprites for modifying the bgm and sfx image
  public Sprite[] PM_SoundImages;

	void Start ()
  {
    if (!GameManager.GetInstance.GetIsBGMMuted)
      PM_BGM.sprite = PM_SoundImages[0];
    else
      PM_BGM.sprite = PM_SoundImages[1];

    if (!GameManager.GetInstance.GetIsSFXMuted)
      PM_SFX.sprite = PM_SoundImages[2];
    else
      PM_SFX.sprite = PM_SoundImages[3];
  }
	
	void Update ()
  {
    DesactiveTutorialMenu();
  }
  // Go to main menu
  public void GoToMainMenu()
  {
    GameManager.GetInstance.SaveData();
    GameManager.GetInstance.RestartValues();
    SceneManager.LoadScene(0, LoadSceneMode.Single);
  }
  // Calls to ModifyBGM function
  public void BGM()
  {
    if (!GameManager.GetInstance.GetIsBGMMuted)
      ModifyBGM(-80.0f, 1, true);
    else
      ModifyBGM(0.0f, 0, false);
  }
  // Modify the BGM volume
  void ModifyBGM(float new_value, int image, bool isMuted)
  {
    PM_AudioMixer.SetFloat("AmbientSound", new_value);
    PM_BGM.sprite = PM_SoundImages[image];
    GameManager.GetInstance.GetIsBGMMuted = isMuted;
  }
  // Calls to SFX function
  public void SFX()
  {
    if (!GameManager.GetInstance.GetIsSFXMuted)
      ModifySFX(-80.0f, 3, true);
    else
      ModifySFX(0.0f, 2, false);
  }
  // Modify the SFX volume
  void ModifySFX(float new_value, int image, bool isMuted)
  {
    PM_AudioMixer.SetFloat("SFXSound", new_value);
    PM_SFX.sprite = PM_SoundImages[image];
    GameManager.GetInstance.GetIsSFXMuted = isMuted;
  }
  // Go To tutorial Menu
  public void ActiveTutorialMenu()
  {
    MM_MenuToAppear.gameObject.SetActive(true);
  }
  // Continues the game
  public void ContinueGame()
  {
    Time.timeScale = 1.0f;
    MM_MenuToDisappear.gameObject.SetActive(false);
  }
  // Returns to pause menu 
  void DesactiveTutorialMenu()
  {
    if (Input.touchCount > 0 && MM_MenuToAppear.gameObject.activeInHierarchy)
    {
      if (Input.GetTouch(0).phase == TouchPhase.Began)
        MM_MenuToAppear.gameObject.SetActive(false);
    }
  }
  // Quits the application
  public void QuitGame()
  {
    GameManager.GetInstance.SaveData();
    Application.Quit();
  }

}
