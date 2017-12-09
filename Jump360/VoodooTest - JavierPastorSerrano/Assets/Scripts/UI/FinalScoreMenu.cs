using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinalScoreMenu : MenuManager
{

  // All the buttons from Final Score menu
  public Button[] FSM_Buttons;
  // Reference to Coins Text
  public TextMeshProUGUI FSM_CoinText;
  // Reference to actual score text
  public TextMeshProUGUI FSM_ActualScore;
  // Reference to high score text
  public TextMeshProUGUI FSM_HighScore;

  void Start()
  {
    for (int i = 0; i < FSM_Buttons.Length; i++)
      FSM_Buttons[i].interactable = false;

    if(GameManager.GetInstance.GetTotalCoins >=100)
      FSM_Buttons[2].interactable = true;

    FSM_Buttons[3].interactable = true;
    FSM_Buttons[5].interactable = true;

    FSM_HighScore.text = "BEST " + GameManager.GetInstance.GetHighScore.ToString() + " KM";
    FSM_ActualScore.text = "SCORE " + GameManager.GetInstance.GetActualScore.ToString() + " KM";

    SetCoinsText();
  }

  void Update()
  {
    ReturnFromCreditsMenu();
  }

  // Return to Main Menu
  public void GoToMainMenu()
  {
    GameManager.GetInstance.SaveData();
    GameManager.GetInstance.RestartValues();
    SceneManager.LoadScene(0, LoadSceneMode.Single);
  }
  // Go to Credits Menu
  public void GoToCreditsMenu()
  {
    MM_MenuToAppear.gameObject.SetActive(true);
  }
  // Return to Final Score Menu
  public void ReturnFromCreditsMenu()
  {
    if (Input.touchCount > 0 && MM_MenuToAppear.gameObject.activeInHierarchy)
    {
      if (Input.GetTouch(0).phase == TouchPhase.Began)
        MM_MenuToAppear.gameObject.SetActive(false);
    }
  }
  // Sets the coins text
  void SetCoinsText()
  {
    FSM_CoinText.text = "<SPRITE=0> " + GameManager.GetInstance.GetTotalCoins.ToString();
  }
  // Buy new hero
  public void BuyHero(int cost)
  {
    if(GameManager.GetInstance.GetTotalCoins <= cost)
      FSM_Buttons[2].interactable = false;
    else
    {
      GameManager.GetInstance.GetAvailableHero[1] = true;
      GameManager.GetInstance.GetTotalCoins -= cost;
    }
  }
}
