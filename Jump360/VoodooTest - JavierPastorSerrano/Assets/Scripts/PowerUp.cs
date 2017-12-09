using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour {

  public GameObject PU_Player;
  public Canvas PU_Canvas;
  public Button PU_Button;

  private float PU_CanvasWidth;
  private float PU_CanvasHeight;

  private float PU_PowerUpWidth;
  private float PU_PowerUpHeight;

  private Vector3 PU_Random;

  enum PowerUps { LoopMultiplier, BouncePower, Gravity, Acceleration, None};
  private PowerUps PU_PowerUp;

  public TextMeshProUGUI GM_PowerUpText;

  [HideInInspector] public int PU_LoopMultiplier;
  private bool PU_Activated;

  // Use PU_Button for initialization
  void Start ()
  {
    GM_PowerUpText.text = "";
    PU_LoopMultiplier = 1;
    PU_PowerUp = PowerUps.None;
    RectTransform();
    PU_Activated = false;
  }
	
	void Update ()
  {
    RectTransform();
    if (!GameManager.GetInstance.GetMainMenuActive && Time.timeScale !=0)
    {
      switch (PU_PowerUp)
      {
        case PowerUps.None:
          if (PU_Activated)
          {
            CheckLimit();
            PU_Button.transform.Translate(PU_Random);
          }
          else
            ShowPowerUp();
          break;

        default:
          break;
      }
    }
  }

  void ShowPowerUp()
  {
    int random = Random.Range(0, 500);

    if(random == 250)
    {
      PU_Button.transform.position = new Vector3(Random.Range(PU_PowerUpWidth, PU_CanvasWidth - PU_PowerUpWidth), Random.Range(PU_PowerUpHeight, PU_CanvasHeight - PU_PowerUpHeight), 0.0f);
      PU_Random = new Vector3(Random.Range(-3.0f,3.0f), Random.Range(-3.0f, 3.0f), 0.0f);
      PU_Button.gameObject.SetActive(true);
      PU_Activated = true;
    }
  }

  void CheckLimit()
  {
    if (PU_Button.transform.position.y - PU_PowerUpHeight / 2 <= 0)
      PU_Random.y *= -1.0f;
    else if (PU_Button.transform.position.y + PU_PowerUpHeight / 2 >= PU_CanvasHeight)
      PU_Random.y *= -1.0f;

    if (PU_Button.transform.position.x - PU_PowerUpWidth / 2 <= 0)
      PU_Random.x *= -1.0f;
    else if (PU_Button.transform.position.x + PU_PowerUpWidth / 2 >= PU_CanvasWidth)
      PU_Random.x *= -1.0f;
  }

  void RectTransform()
  {
    RectTransform rtCanvas = PU_Canvas.transform.GetComponent<RectTransform>();
    PU_CanvasWidth = rtCanvas.sizeDelta.x * rtCanvas.localScale.x;
    PU_CanvasHeight = rtCanvas.sizeDelta.y * rtCanvas.localScale.y;

    RectTransform rtPowerUp = PU_Button.transform.GetComponent<RectTransform>();
    PU_PowerUpWidth = rtPowerUp.rect.width;
    PU_PowerUpHeight = rtPowerUp.rect.height;
  }

  public void ActivatePowerUp()
  {
    int random = Random.Range(0, 4);

    switch (random)
    {
      case 0:
        PU_PowerUp = PowerUps.LoopMultiplier;
        PU_LoopMultiplier = Random.Range(2, 5);
        GM_PowerUpText.text = "ACTIVE POWER UP:<BR>+ LOOP COINS";
        break;
      case 1:
        PU_PowerUp = PowerUps.BouncePower;
        PU_Player.GetComponent<Player>().PL_JumpPower += 5;
        GM_PowerUpText.text = "ACTIVE POWER UP:<BR>+ BOUNCE POWER";
        break;
      case 2:
        PU_PowerUp = PowerUps.Gravity;
        PU_Player.GetComponent<Player>().PL_RB.gravityScale = 0.5f;
        GM_PowerUpText.text = "ACTIVE POWER UP:<BR>- GRAVITY";
        break;
      case 3:
        PU_PowerUp = PowerUps.Acceleration;
        PU_Player.GetComponent<Player>().PL_Acceleration += 2;
        GM_PowerUpText.text = "ACTIVE POWER UP:<BR>+ ACCELERATION";
        break;
      default:
        break;
    }

    StartCoroutine(PowerUpTimer(5.0f));
    PU_Button.gameObject.SetActive(false);
  }

  IEnumerator PowerUpTimer(float time)
  {
    yield return new WaitForSeconds(time);

    PU_LoopMultiplier = 1;
    PU_Player.GetComponent<Player>().PL_MaxRotationSpeed = 350;
    PU_Player.GetComponent<Player>().PL_Acceleration = 2;
    PU_Player.GetComponent<Player>().PL_RB.gravityScale = 1.1f;
    GM_PowerUpText.text = "";
    PU_PowerUp = PowerUps.None;
    PU_Activated = false;
  }
}
