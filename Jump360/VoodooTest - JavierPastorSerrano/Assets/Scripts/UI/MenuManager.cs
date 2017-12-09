using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

  // Active menu reference
  public Transform MM_MenuToAppear;
  // Desactive Menu reference
  public Transform MM_MenuToDisappear;

  void Start (){}
	
	void Update (){}

  // Swap Menu
  protected void ChangeMenu()
  {
    MM_MenuToAppear.gameObject.SetActive(true);
    MM_MenuToDisappear.gameObject.SetActive(false);
  }
}
