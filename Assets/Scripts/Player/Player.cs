using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerHealth health;
    [SerializeField] public PlayerControls controls;
    [SerializeField] public PlayerStats stats;
    
    
    public static Player Instance;
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
}
