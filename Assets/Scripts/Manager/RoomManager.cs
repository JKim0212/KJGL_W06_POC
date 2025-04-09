using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    public RoomSystem SelectedRoom { get; private set; }
    GameObject roomHighlight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        roomHighlight = transform.GetChild(0).gameObject;
        InputManager.Instance.cellSelectAction += SelectRoom;
        SelectedRoom = null;
    }

    void SelectRoom(GameObject cell)
    {
        CrewManager.Instance.SelectedCrew.CurrentRoom.RemoveCrew(CrewManager.Instance.SelectedCrew);
        SelectedRoom = cell.transform.parent.GetComponent<RoomSystem>();
        if (cell.transform.childCount == 0)
        {
            ToggleRoomHighlight(true);
            roomHighlight.transform.position = cell.transform.position;
            CrewManager.Instance.SelectedCrew.Move(cell.transform.position);
            SelectedRoom.AddCrew(CrewManager.Instance.SelectedCrew);
            CrewManager.Instance.SelectedCrew.CurrentRoom = SelectedRoom;
            CrewManager.Instance.SelectedCrew.transform.SetParent(cell.transform);
            CrewManager.Instance.DeselectCrew();
        }
    }

    public void DeselectRoom()
    {
        ToggleRoomHighlight(false);
        SelectedRoom = null;
    }

    public void ToggleRoomHighlight(bool select)
    {
        roomHighlight.SetActive(select);
    }
}
