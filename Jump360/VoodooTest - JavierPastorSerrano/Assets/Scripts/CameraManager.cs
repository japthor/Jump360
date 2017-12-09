using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

  // Reference to the player game object
  public GameObject CM_Player;
  // Offset
  private Vector3 CM_Offset; 

  void Start()
  {
    CM_Offset = transform.position - CM_Player.transform.position;
  }

  void Update()
  {
    // Moves the camera
    transform.position = CM_Player.transform.position + CM_Offset;
  }
}
