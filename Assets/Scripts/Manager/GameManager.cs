using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance { get; private set; }

    public int crewCount;
    public int enemySlotCount;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }




}
