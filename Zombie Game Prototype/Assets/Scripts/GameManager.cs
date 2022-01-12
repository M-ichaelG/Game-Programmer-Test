using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager s_Instance = null;
    public static GameManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (s_Instance == null)
                    Debug.LogWarning("Could not locate a GameManager object. \n You only can have exactly one GameManager object in the scene.");
            }
            return s_Instance;
        }
    }

    public Transform trPlayer;
    [HideInInspector]
    public int countEnemy;
    [HideInInspector]
    public int specialCountEnemy;       //will not be decreased

    public void AddNormalEnemyHitpoints (int addedAmount)
    {

    }

}
