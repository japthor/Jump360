using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
  // Coin Life
  private float C_Life;

	void Start ()
  {
    C_Life = 10.0f;
  }
	
	void Update ()
  {
    LifeCycle();
  }
  // The lifecycle of the Coin. If it ends, the coin will disappear
  void LifeCycle()
  {
    C_Life -= Time.deltaTime;

    if (C_Life <= 0.0f)
      Destroy(this.gameObject);
  }
}
