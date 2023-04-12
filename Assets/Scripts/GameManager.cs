using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int World { get; private set; }
    public int Stage { get; private set; }
    public int Lives { get; private set; }

    #region Public Methods

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        Lives--;

        if (Lives > 0)
        {
            LoadLevel(World, Stage);
        }
        else
        {
            GameOver();
        }
    }

    public void NextLevel()
    {
        // Eventually, check if we've reached the end of the stage for world
        // e.g. 1-1, 1-2, 2-1, 2-2, etc

        LoadLevel(World, Stage + 1);
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null)
        {
            // There should only ever be one instance (Singleton)
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            // We don't want to destroy ourself, but destroy an exsting instance
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (this == Instance)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    #endregion

    #region Private Methods

    private void NewGame()
    {
        Lives = 3;
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.World = world;
        this.Stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    private void GameOver()
    {
        Debug.Log("You ded.");
        NewGame();
    }

    #endregion
}