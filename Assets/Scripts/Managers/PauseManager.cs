using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    
    public bool IsPaused {get; private set;}
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            Debug.Log("Player destroyed");
        }
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }
}
