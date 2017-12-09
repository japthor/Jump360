using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingMenu : MonoBehaviour
{

	void Start ()
  {
    StartCoroutine(LoadAsyncLevel(1));
  }
	
	void Update ()
  {
		
	}

  // Calls to the LoadAsyncLevel function
  public void LoadLevel(int scene)
  {
    StartCoroutine(LoadAsyncLevel(scene));
  }

  // Loads the level when it is ready
  IEnumerator LoadAsyncLevel(int scene)
  {
    AsyncOperation op = SceneManager.LoadSceneAsync(scene);

    while (!op.isDone)
    {
      yield return null;
    }
  }
}
