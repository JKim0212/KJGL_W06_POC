using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CrewInfo : MonoBehaviour
{
    Image crewImage;
    TextMeshProUGUI crewName;
    TextMeshProUGUI crewDescription;

    private void Start()
    {
        crewImage = transform.GetChild(0).GetChild(0). GetComponent<Image>();
        crewName = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        crewDescription = transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateInfo(CrewSO crewInfo)
    {
        crewImage.sprite = crewInfo.crewImage;
        crewName.text = $"Name: {crewInfo.name}";
        crewDescription.text = crewInfo.description;
    }
}
