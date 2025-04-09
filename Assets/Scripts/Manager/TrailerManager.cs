using UnityEngine;

public class TrailerManager : MonoBehaviour
{
    public static TrailerManager Instance { get; private set; }
    float missChance = 10;
    float trailerHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetMissChance(float missChance)
    {
        this.missChance = missChance;
    }
}
