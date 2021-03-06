using UnityEngine;
using UnityEngine.SceneManagement;
using XO.Core;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    [Header("Scenes")]
    [SerializeField] string mainMenuScene = null;
    [SerializeField] string gameScene = null;

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
        DataLoader.Instance.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.Player });
        SceneManager.LoadSceneAsync(gameScene);
    }
    // called from UI (Main Menu scene)
    public void LoadPVEGameScene()
    {
        DataLoader.Instance.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.AI });
        SceneManager.LoadSceneAsync(gameScene);
    }
    // called from UI (Main Menu scene)
    public void LoadEVEGameScene()
    {
        DataLoader.Instance.SetGamePlayers(new PlayerTypes[] { PlayerTypes.AI, PlayerTypes.AI });
        SceneManager.LoadSceneAsync(gameScene);
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
