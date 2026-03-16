using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance { get; private set; }

    [HideInInspector] public float lastScore;

    public List<float> topScores;
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple game managers present in scene! Destroying...");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}