using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    
    [SerializeField] private Image experienceBar;
    [SerializeField] private float levelThresholdMultiplier = 1.1f;
    
    private int currentExperience = 0;
    private int levelThreshold = 10;

    private WeaponManager weaponManager;
    
    [HideInInspector] public List<Mine> spawnedMines = new List<Mine>();

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
            Debug.Log("UpgradesManager destroyed");
        }
    }
    
    private void Start()
    {
        currentExperience = 0;
        experienceBar.fillAmount = (float)currentExperience / (float)levelThreshold;
        weaponManager = WeaponManager.Instance;
    }

    public void IncreaseExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= levelThreshold)
        {
            currentExperience = 0;
            IncreaseLevelThreshold(levelThresholdMultiplier);
            weaponManager.TurnOnUpgradesScreen();
        }
        experienceBar.fillAmount = (float)currentExperience / (float)levelThreshold;
    }

    private void IncreaseLevelThreshold(float multiplier)
    {
        float multipliedLevelThreshold = (float)levelThreshold * multiplier;
        levelThreshold = Mathf.RoundToInt(multipliedLevelThreshold);
    }
}
