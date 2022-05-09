using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    [Header("Scenes")]
    [SerializeField] string mainMenuScene = null;
    [SerializeField] string PVPGameScene = null;
    [SerializeField] string PVEGameScene = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // called from UI (GAME END Mask Canvas)
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync(mainMenuScene);
    }
    // called from UI (Main Menu scene)
    public void LoadPVPGameScene()
    {
        SceneManager.LoadSceneAsync(PVPGameScene);
    }
    // called from UI (Main Menu scene)
    public void LoadPVEGameScene()
    {
        SceneManager.LoadSceneAsync(PVEGameScene);
    }
    // called from UI (Main Menu scene)
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    // called from UI (Main Menu scene)
    public void ExitGame()
    {
        Application.Quit();
    }
}
