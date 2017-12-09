using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameMenu : MenuManager
{
  // Pause Menu Reference
  public Transform GM_PauseMenu;
  // Character reference
  public GameObject GM_Player;
  // Reference to the slider
  public Slider GM_PlayerSlider;
  // Reference to the rotating image
  public Image GM_Rotating;
  // Reference to the ReadyGo Text
  public TextMeshProUGUI GM_ReadyGoText;
  // Reference to the Step Feedback Text
  public TextMeshProUGUI GM_StepFeedBack;
  // Reference to the Kilometers Text
  public TextMeshProUGUI GM_Kilometers;
  // Reference to the Actual Score Text
  public TextMeshProUGUI GM_ActualScore;
  // Reference to the High Score Text
  public TextMeshProUGUI GM_HighScore;
  // Reference to the Feedback Text
  public TextMeshProUGUI GM_Feedback;
  // Reference to the Feedback Loop Text
  public TextMeshProUGUI GM_FeedLoop;
  // Reference to the Power Up Text
  public TextMeshProUGUI GM_PowerUp;
  // Count down timer
  private float GM_CountDownValue;
  // Checks if the player has loop
  private bool GM_HasLoop;

  void Start()
  {
    GM_HasLoop = false;
    GM_HighScore.text = "BEST " + GameManager.GetInstance.GetHighScore.ToString() + " KM";
    GM_ActualScore.text = "HIGH " + GameManager.GetInstance.GetActualScore.ToString() + " KM";

    Color playerColor = GM_PlayerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color;
    playerColor.a = 0.0f;
    GM_PlayerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = playerColor;

    StartCoroutine(StartCountdown(2));
  }

  void Update()
  {
    if (!GameManager.GetInstance.GetMainMenuActive)
    {
      PlayerSlider();
      SetKilometersText();
      RotatingImage();
    }
  }
  // Updates de slider depending of the character rotation. If it is 0, the player obtains benefits.
  void PlayerSlider()
  {
    Color playerColor = GM_PlayerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color;

    if (GM_PlayerSlider.value == 1.0f)
    {
      if (!GM_HasLoop)
      {
        GM_PlayerSlider.value = 0;
        GM_Player.gameObject.GetComponent<Player>().PL_LoopTimes += (2 * this.GetComponent<PowerUp>().PU_LoopMultiplier);
        GM_Player.GetComponent<Player>().Spawn();
        GM_FeedLoop.text = "FLIP BONUS x " + GM_Player.GetComponent<Player>().PL_LoopTimes.ToString();
        playerColor.a = 0.0f;
        GM_PlayerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = playerColor;
        GM_HasLoop = true;
      }
      else
        GM_PlayerSlider.value = 0;
    }
    else
    {
      if(playerColor.a != 1.0f && GM_PlayerSlider.value > 0.0f)
      {
        playerColor.a = 1.0f;
        GM_PlayerSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = playerColor;
        GM_HasLoop = false;
      }
      GM_PlayerSlider.value = (GM_Player.transform.rotation.eulerAngles.z / 360.0f) + 0.02f;
    }
  }
  // Starts the countdowm
  IEnumerator StartCountdown(float time)
  {
    GM_CountDownValue = time;
    while (GM_CountDownValue > 0)
    {
      if (GM_CountDownValue == 2)
        GM_ReadyGoText.text = "Ready";
      else
        GM_ReadyGoText.text = "Go";

      yield return new WaitForSeconds(1.0f);
      GM_CountDownValue--;
    }
    GameManager.GetInstance.GetMainMenuActive = false;
    GM_ReadyGoText.text = "";
  }
  // Appears a step feedback depending of the angle
  public IEnumerator AppearStepFeedback(float rotation)
  {
    GM_FeedLoop.text = "";

    if (rotation >= 359.0f || rotation <= 1.0f)
      SetStepFeedbackText("PERFECT", "<br>LAND BONUS x 16", 16);
    else if (rotation >= 335.0f || rotation <= 25.0f)
      SetStepFeedbackText("EXCELLENT", "<br>LAND BONUS x 4", 4);
    else
      SetStepFeedbackText("GOOOD", "<br>LAND BONUS x 2", 2);

    GameManager.GetInstance.GetTotalCoins += (int)((float)GM_Player.GetComponent<Player>().PL_LoopTimes * 0.5f);

    yield return new WaitForSeconds(1.0f);
    GM_StepFeedBack.text = "";
    GM_Feedback.text = "";
  }
  // Sets the feedback text
  void SetStepFeedbackText(string step_feedback, string land_bonus, int multiplier)
  {
    GM_StepFeedBack.text = step_feedback;
    GM_Feedback.text = "FLIP BONUS x " + GM_Player.GetComponent<Player>().PL_LoopTimes.ToString() + land_bonus + "<br>TOTAL x " + (GM_Player.GetComponent<Player>().PL_LoopTimes * multiplier).ToString();
  }
  // Sets the kilometers text
  void SetKilometersText()
  {
    int kilometer = Mathf.RoundToInt(GM_Player.transform.position.y * 2);

    if (kilometer <= 0)
      GM_Kilometers.text = "0 KM";
    else
      GM_Kilometers.text = kilometer.ToString() + " KM";

    if (kilometer > GameManager.GetInstance.GetHighScore)
    {
      GM_HighScore.text = "BEST " + GameManager.GetInstance.GetHighScore.ToString() + " KM";
      GameManager.GetInstance.GetHighScore = kilometer;
    }

    if(kilometer > GameManager.GetInstance.GetActualScore)
    {
      GM_ActualScore.text = "HIGH " + GameManager.GetInstance.GetActualScore.ToString() + " KM";
      GameManager.GetInstance.GetActualScore = kilometer;
    }

  }
  //Appear an image when the player touches the screen
  void RotatingImage()
  {
    if (GameManager.GetInstance.GetIsTouchingScreen && Time.timeScale != 0)
    {
      if (!GM_Rotating.IsActive())
        GM_Rotating.gameObject.SetActive(true);

      GM_Rotating.transform.Rotate(0.0f, 0.0f, Time.deltaTime * 50.0f);
    }
    else
    {
      if (GM_Rotating.IsActive())
        GM_Rotating.gameObject.SetActive(false);
    }
      
  }
  // Activates the pause menu
  public void ActivePauseMenu()
  {
    Time.timeScale = 0.0f;
    GM_PauseMenu.gameObject.SetActive(true);
  }
}
