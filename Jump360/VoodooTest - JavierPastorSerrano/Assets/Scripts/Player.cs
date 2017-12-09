using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Player : MonoBehaviour {

  // Point where the coins will spawn
  private GameObject PL_SpawnPoint;
  // Rotation Speed of the character;
  private float PL_RotationSpeed;
  // Coin reference
  public GameObject PL_Coin;
  // Access to the physics of the character
  [HideInInspector] public Rigidbody2D PL_RB;
  // Jump power
  [HideInInspector] public int PL_JumpPower;
  // Times of loops done on each jump
  [HideInInspector] public int PL_LoopTimes;
  // Checks if the character is dead
  [HideInInspector] public bool PL_IsDead;
  // Maximum rotation speed
  [HideInInspector] public float PL_MaxRotationSpeed;
  // Minimum rotation speed
  [HideInInspector] public float PL_MinRotationSpeed;
  // Acceleration to increase the rotation speed
  [HideInInspector] public float PL_Acceleration;

  void Start ()
  {
    PL_SpawnPoint = this.gameObject.transform.GetChild(2).gameObject;
    PL_RB = GetComponent<Rigidbody2D>();
    PL_JumpPower = 10;
    PL_LoopTimes = 0;
    PL_IsDead = false;
    PL_MaxRotationSpeed = 350.0f;
    PL_MinRotationSpeed = 175.0f;
    PL_Acceleration = 2.0f;

    SetHeroSprite();
  }
	
	void Update ()
  {
    ModifyCameraView();
    TouchScreen();
	}

  // Set the character sprite
  void SetHeroSprite()
  {
    switch (GameManager.GetInstance.GetSelectedHero)
    {
      case 0:
        if (GameManager.GetInstance.GetAvailableHero[GameManager.GetInstance.GetSelectedHero])
          HeroSpritePath("Sprites/Character/Body 0", "Sprites/Character/Shoes 0");
        break;
      case 1:
        if (GameManager.GetInstance.GetAvailableHero[GameManager.GetInstance.GetSelectedHero])
          HeroSpritePath("Sprites/Character/Body 1", "Sprites/Character/Shoes 1");
        break;
      default:
        break;
    }
  }

  // Look the sprite inside the path and attaches it to the character
  void HeroSpritePath(string body_path, string shoes_path)
  {
    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(body_path);
    this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(shoes_path);
  }

  // Detects when the user is touching the screen. If it is touching it, makes the character rotate, if not the character will stop rotating.
  void TouchScreen()
  {
    if (!GameManager.GetInstance.GetMainMenuActive && !PL_IsDead)
    {
      if (Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null)
      {
        if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)
        {
          GameManager.GetInstance.GetIsTouchingScreen = true;
          Rotate();
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
          GameManager.GetInstance.GetIsTouchingScreen = false;

        this.transform.Rotate(0.0f, 0.0f, Time.deltaTime * PL_RotationSpeed);
      }
      else
      {
        if (PL_RotationSpeed > 0.0f)
        {
          PL_RotationSpeed -= PL_Acceleration;
          this.transform.Rotate(0.0f, 0.0f, Time.deltaTime * PL_RotationSpeed);
        }
      }
    }
  }

  // Sets the rotation speed.
  void Rotate()
  {
    if (PL_RotationSpeed < PL_MinRotationSpeed)
      PL_RotationSpeed = PL_MinRotationSpeed;

    if (PL_RotationSpeed <= PL_MaxRotationSpeed)
      PL_RotationSpeed += PL_Acceleration;
    else
      PL_RotationSpeed = PL_MaxRotationSpeed;
  }

  // Modify the camera view depending on the 'Y' position of the character
  void ModifyCameraView()
  {
    Camera.main.orthographicSize = 6.0f + (this.transform.position.y * 1.5f);
  }

  // Spawn coins depending on the number of loops
  public void Spawn()
  {
    for(int i = 0; i < PL_LoopTimes; i++)
     Instantiate(PL_Coin, new Vector3(Random.Range(PL_SpawnPoint.transform.position.x - 2.0f, PL_SpawnPoint.transform.position.x + 2.0f),
               Random.Range(PL_SpawnPoint.transform.position.y, PL_SpawnPoint.transform.position.y + 3.0f),
               PL_SpawnPoint.transform.position.z), PL_Coin.transform.rotation);
  }
}
