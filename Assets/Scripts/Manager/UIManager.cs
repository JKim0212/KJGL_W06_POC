using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    Canvas _crewInfoCanvas;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _crewInfoCanvas = FindAnyObjectByType<UI_CrewInfo>().GetComponent<Canvas>();
    }

    public void ToggleCrewInfoCanvas(bool turnOn, CrewSO crewInfo)
    {
        if (turnOn)
        {
            _crewInfoCanvas.enabled = true;
            _crewInfoCanvas.GetComponent<UI_CrewInfo>().UpdateInfo(crewInfo);
        } else
        {
            _crewInfoCanvas.enabled = false;
        }
    }

}
