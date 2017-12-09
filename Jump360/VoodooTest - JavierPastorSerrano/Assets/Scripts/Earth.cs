using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Earth : MonoBehaviour {

  // Reference to the player game object
  private GameObject E_Player;
  // Check if it is the first time colliding with the 'Earth'.
  private bool E_FirstTimeJumping;
  // Reference to the game menu
  public Transform E_GameMenu;

  void Start ()
  {
    E_FirstTimeJumping = true;
    E_Player = GameObject.Find("Player");
  }
	
	void Update ()
  {
    FirstJump();
  }

  // Makes the character jump for the first time without accessing to the collision function
  void FirstJump()
  {
    if (!GameManager.GetInstance.GetMainMenuActive)
    {
      if (E_FirstTimeJumping)
      {
        E_Player.GetComponent<Player>().PL_RB.AddForce(transform.up * E_Player.GetComponent<Player>().PL_JumpPower, ForceMode2D.Impulse);
        E_Player.GetComponent<Player>().PL_JumpPower += 2;
        E_FirstTimeJumping = false;
      }

    }
  }

  // Detects if the player's 'Body' or 'Legs' collides with the 'Earth'
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (!E_Player.GetComponent<Player>().PL_IsDead && !GameManager.GetInstance.GetMainMenuActive)
    {
      if (collision.collider.gameObject.CompareTag("Body"))
      {
        E_Player.GetComponent<Player>().PL_IsDead = true;
        E_Player.GetComponent<Player>().PL_RB.velocity = Vector2.zero;
        E_GameMenu.GetComponent<GameMenu>().MM_MenuToAppear.gameObject.SetActive(true);
      }

      if (collision.collider.gameObject.CompareTag("Legs"))
      {
        StartCoroutine(E_GameMenu.GetComponent<GameMenu>().AppearStepFeedback(E_Player.transform.rotation.eulerAngles.z));
        E_Player.GetComponent<Player>().PL_RB.AddForce(transform.up * E_Player.GetComponent<Player>().PL_JumpPower, ForceMode2D.Impulse);
        E_Player.GetComponent<Player>().PL_JumpPower += 2;
        E_Player.GetComponent<Player>().PL_LoopTimes = 0;
      }
    }
  }
}
