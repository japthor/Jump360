using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu : MenuManager
{
  // Reference to the Game Menu
  public Transform GOM_GameMenu;

  void Start (){}
	
	void Update (){}
  // Go to the main menu
  public void GoToMainMenu()
  {
    GameManager.GetInstance.SaveData();
    GameManager.GetInstance.RestartValues();
    SceneManager.LoadScene(0, LoadSceneMode.Single);
  }
  // Go to the final score menu
  public void GoToFinalScoreMenu()
  {
    GOM_GameMenu.gameObject.SetActive(false);
    ChangeMenu();
  }

}
