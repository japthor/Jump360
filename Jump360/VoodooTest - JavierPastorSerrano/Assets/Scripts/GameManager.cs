using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

  // Instance to access to the GameManager
  private static GameManager GM_Instance;
  // Total of coins
  private int GM_TotalCoins;
  // High Score
  private int GM_HighScore;
  // Actual Score
  private int GM_ActualScore;
  // Checks if the player is inside the main menu
  private bool GM_MainMenuActive;
  // Checks if the player is touching the screen
  private bool GM_IsTouchingScreen;
  // Checks if the music is muted
  private bool GM_IsBGMMuted;
  // Checks if the sfx are muted
  private bool GM_IsSFXMuted;
  // Checks if the other hero are available to use it
  private bool[] GM_AvailableHero = new bool[2];
  // Checks what hero is selected
  private int GM_SelectedHero;
  
  public static GameManager GetInstance
  {
    get
    {
      if (GM_Instance == null)
        GM_Instance = new GameObject("AudioManager").AddComponent<GameManager>();
      return GM_Instance;
    }
  }

  void Awake()
  {
    if (GM_Instance)
    {
      DestroyImmediate(gameObject);
    }
    else
    {
      GM_Instance = this;
      DontDestroyOnLoad(gameObject);
    }

    LoadData();
  }

  void Start()
  {
    if (GM_IsBGMMuted)
      this.gameObject.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("AmbientSound", -80.0f);

    if(GM_IsSFXMuted)
      this.gameObject.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("SFXSound", -80.0f);

    GM_MainMenuActive = true;
    GM_IsTouchingScreen = false;
    GM_ActualScore = 0;
  }

  void Update()
  {
   
  }

  /// SETTERS AND GETTERS

  public bool GetMainMenuActive
  {
    get { return GM_MainMenuActive; }
    set { GM_MainMenuActive = value; }
  }

  public bool GetIsTouchingScreen
  {
    get { return GM_IsTouchingScreen; }
    set { GM_IsTouchingScreen = value; }
  }

  public bool GetIsBGMMuted
  {
    get { return GM_IsBGMMuted; }
    set { GM_IsBGMMuted = value; }
  }

  public bool GetIsSFXMuted
  {
    get { return GM_IsSFXMuted; }
    set { GM_IsSFXMuted = value; }
  }

  public int GetTotalCoins
  {
    get { return GM_TotalCoins; }
    set { GM_TotalCoins = value; }
  }

  public int GetHighScore
  {
    get { return GM_HighScore; }
    set { GM_HighScore = value; }
  }

  public int GetActualScore
  {
    get { return GM_ActualScore; }
    set { GM_ActualScore = value; }
  }

  public bool[] GetAvailableHero
  {
    get { return GM_AvailableHero; }
    set { GM_AvailableHero = value; }
  }

  public int GetSelectedHero
  {
    get { return GM_SelectedHero; }
    set { GM_SelectedHero = value; }
  }

  // Restart the values that they do not need to save it to their original status
  public void RestartValues()
  {
    GM_MainMenuActive = true;
    GM_IsTouchingScreen = false;
    GM_ActualScore = 0;
    Time.timeScale = 1.0f;
  }
  // Saves some values into a .dat
  public void SaveData()
  {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream fs = File.Create(Application.persistentDataPath + "/GameManagerData.dat");

    GameManagerData gm = new GameManagerData();
    gm.GMD_TotalCoins = GM_TotalCoins;
    gm.GMD_IsBGMMuted = GM_IsBGMMuted;
    gm.GMD_IsSFXMuted = GM_IsSFXMuted;
    gm.GMD_HighScore = GM_HighScore;
    gm.GMD_AvailableHero0 = GM_AvailableHero[0];
    gm.GMD_AvailableHero1 = GM_AvailableHero[1];
    gm.GMD_SelectedHero = GM_SelectedHero;

    bf.Serialize(fs, gm);
    fs.Close();
  }
  // Loads some values from a .dat
  public void LoadData()
  {
    if(File.Exists(Application.persistentDataPath + "/GameManagerData.dat"))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream fs = File.Open(Application.persistentDataPath + "/GameManagerData.dat", FileMode.Open);
      GameManagerData gm = (GameManagerData)bf.Deserialize(fs);
      fs.Close();

      GM_TotalCoins = gm.GMD_TotalCoins;
      GM_IsBGMMuted = gm.GMD_IsBGMMuted;
      GM_IsSFXMuted = gm.GMD_IsSFXMuted;
      GM_HighScore = gm.GMD_HighScore;
      GM_AvailableHero[0] = gm.GMD_AvailableHero0;
      GM_AvailableHero[1] = gm.GMD_AvailableHero1;
      GM_SelectedHero = gm.GMD_SelectedHero;
    }
    else
    {
      GM_TotalCoins = 0;
      GM_IsBGMMuted = false;
      GM_IsSFXMuted = false;
      GM_HighScore = 0;
      GM_AvailableHero[0] = true;
      GM_AvailableHero[1] = false;
      GM_SelectedHero = 0;
    }
  }
  // Saves the game if the application is in pause
  private void OnApplicationPause(bool pause)
  {
    if (pause)
     SaveData();
  }
  // Saves the game if the application is not in focus
  private void OnApplicationFocus(bool focus)
  {
    if (!focus)
      SaveData();
  }
  // Saves the game if the application has ended
  private void OnApplicationQuit()
  {
    SaveData();
  }
}

[System.Serializable]
class GameManagerData
{
  public int GMD_TotalCoins;
  public bool GMD_IsBGMMuted;
  public bool GMD_IsSFXMuted;
  public int GMD_HighScore;
  public bool GMD_AvailableHero0;
  public bool GMD_AvailableHero1;
  public int GMD_SelectedHero;
}
