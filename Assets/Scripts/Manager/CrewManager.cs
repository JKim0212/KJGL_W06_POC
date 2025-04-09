using UnityEngine;

public class CrewManager : MonoBehaviour
{
    public static CrewManager Instance { get; private set; }
    public CrewController SelectedCrew => _selectedCrew;

    CrewController _selectedCrew;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InputManager.Instance.crewSelectAction += SelectCrew;
        InputManager.Instance.CrewDeselectAction += DeselectCrew;
    }
    public void SelectCrew(GameObject crewToSelect)
    {
        Debug.Log($"Clicked on {crewToSelect.name}");
        if (_selectedCrew == null) {
            _selectedCrew = crewToSelect.GetComponent<CrewController>();
            _selectedCrew.ToggleSelect(true);
        } else
        {
            DeselectCrew();
            _selectedCrew = crewToSelect.GetComponent<CrewController>();
            _selectedCrew.ToggleSelect(true);
        }
    }

    public void DeselectCrew()
    {
        _selectedCrew.ToggleSelect(false);
        _selectedCrew = null;
    }
}
