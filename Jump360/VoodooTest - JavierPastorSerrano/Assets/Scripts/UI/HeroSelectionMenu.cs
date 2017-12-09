using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class HeroSelectionMenu : MenuManager
{
  // All the images of the hero
  public Image[] HS_ActualHeros;
  // Reference to Hero name Text
  public TextMeshProUGUI HS_HeroName;
  // Reference to Hero stats Text
  public TextMeshProUGUI HS_HeroStats;
  // Reference to select button
  public Button HS_Select;
  // Reference to the character
  public GameObject HS_Player;
  // Checs the selected hero
  private int HS_SelectedHero;

  void Start ()
  {
    for (int i = 0; i< GameManager.GetInstance.GetAvailableHero.Length; i++)
    {
      switch (i)
      {
        case 0:
          if(GameManager.GetInstance.GetAvailableHero[i])
            HS_ActualHeros[i].sprite = Resources.Load<Sprite>("Sprites/Character/Full Body");
          break;
        case 1:
          if (GameManager.GetInstance.GetAvailableHero[i])
            HS_ActualHeros[i].sprite = Resources.Load<Sprite>("Sprites/Character/Full Body 1");
          break;
        default:
          break;
      }
    }
    HS_SelectedHero = 0;
  }
	
	void Update (){}
   // Calls some functions depending of the selected hero
  public void ChangeHero(int hero)
  {
    switch (hero)
    {
      case 0:
        HeroAlpha(0, 1.0f);
        HeroAlpha(1, 0.5f);
        break;
      case 1:
        HeroAlpha(1, 1.0f);
        HeroAlpha(0, 0.5f);
        break;
      default:
        break;
    }
    CheckAvailability(hero);
    HS_SelectedHero = hero;
  }

  // Modify the alpha of the image
  void HeroAlpha(int hero, float alpha)
  {
    HS_ActualHeros[hero].color = new Color(HS_ActualHeros[hero].color.r, HS_ActualHeros[hero].color.g, HS_ActualHeros[hero].color.b, alpha);
  }
  // Sets the text depending of the availability of the hero
  void CheckAvailability(int hero)
  {
    if (GameManager.GetInstance.GetAvailableHero[hero])
    {
      HS_Select.gameObject.SetActive(true);

      switch (hero)
      {
        case 0:
          SetText("Mister", "Normal");
          break;
        case 1:
          SetText("Elegant", "Normal");
          break;
        default:
          break;
      }
    }
    else
    {
      HS_Select.gameObject.SetActive(false);
      SetText("???", "???");
    }
  }

  // Modify the hero name text and hero stats text
  void SetText(string name, string stats)
  {
    HS_HeroName.text = name;
    HS_HeroStats.text = stats;
  }
  // Go back to main menu
  public void ReturnHeroSelection()
  {
    MM_MenuToAppear.gameObject.SetActive(true);
    MM_MenuToDisappear.gameObject.SetActive(false);
  }

  // Sets the character sprite when it is selected
  public void SelectHero()
  {
    switch (HS_SelectedHero)
    {
      case 0:
        HS_Player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Character/Body 0");
        HS_Player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Character/Shoes 0");
        GameManager.GetInstance.GetSelectedHero = 0;

        break;
      case 1:
        HS_Player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Character/Body 1");
        HS_Player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Character/Shoes 1");
        GameManager.GetInstance.GetSelectedHero = 1;
        break;
      default:
        break;
    }
  }
}
