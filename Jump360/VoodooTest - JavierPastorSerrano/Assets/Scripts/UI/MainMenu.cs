using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MainMenu : MenuManager
{
  // Reference to Tap to start Text
  public TextMeshProUGUI MM_TapToStartText;
  // Reference to Hero  selection menu
  public Transform MM_HeroSelection;
  // Reference to all the buttons
  public Button[] MMM_Buttons;
  // Checks if the alpha from the Tap to start hero is on
  private bool MM_IsAlpha;

  void Start ()
  {
    for (int i = 0; i < MMM_Buttons.Length; i++)
      MMM_Buttons[i].interactable = false;

    MMM_Buttons[0].interactable = true;
    MM_IsAlpha = true;
  }
	
	void Update ()
  {
    if (GameManager.GetInstance.GetMainMenuActive)
    {
      Alpha(0.5f);
      TouchInput();
    }
  }
  // Checks if the user has touched the screen
  void TouchInput()
  {
    if (Input.touchCount > 0 && EventSystem.current.currentSelectedGameObject == null)
    {
      MM_TapToStartText.color = new Color(MM_TapToStartText.color.r, MM_TapToStartText.color.g, MM_TapToStartText.color.b, 1.0f);
      ChangeMenu();
    }
  }
  // Modify the alpha
  void Alpha(float time)
  {
    if (MM_IsAlpha)
    {
      if (MM_TapToStartText.color.a < 1.0f)
        MM_TapToStartText.color = new Color(MM_TapToStartText.color.r, MM_TapToStartText.color.g, MM_TapToStartText.color.b, MM_TapToStartText.color.a + (Time.deltaTime / time));
      else
        MM_IsAlpha = false;
    }
    else
    {
      if (MM_TapToStartText.color.a > 0.0f)
        MM_TapToStartText.color = new Color(MM_TapToStartText.color.r, MM_TapToStartText.color.g, MM_TapToStartText.color.b, MM_TapToStartText.color.a - (Time.deltaTime / time));
      else
        MM_IsAlpha = true;
    }
  }
  // Go to hero selection menu
  public void ActiveHeroSelectionMenu()
  {
    MM_MenuToDisappear.gameObject.SetActive(false);
    MM_HeroSelection.gameObject.SetActive(true);
  }
}
