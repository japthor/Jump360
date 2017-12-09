using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsMenu : MenuManager
{
  // Reference to Coins Text
  public TextMeshProUGUI CM_Coin;

  void Start (){}
	
	// Update is called once per frame
	void Update ()
  {
    SetCoinsText();
  }

  // Sets the coins text
  void SetCoinsText()
  {
    CM_Coin.text = "<SPRITE=0> " + GameManager.GetInstance.GetTotalCoins.ToString();
  }
}
