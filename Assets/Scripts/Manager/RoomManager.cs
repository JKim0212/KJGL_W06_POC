using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    public RoomSystem SelectedRoom { get; private set; }
    public float FireDamage => _fireDamage;
    public float FireDamageTime => _fireDamageTime;

    GameObject roomHighlight;

    List<IRoomAction> roomList = new List<IRoomAction>();
    public List<Transform> Cells = new List<Transform>();

    [SerializeField] float _fireDamage = 5;
    [SerializeField] float _fireDamageTime = 1f;
    [SerializeField] GameObject fireEffect;
    public GameObject FireEffect => fireEffect;


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

    private void Update()
    {
        foreach (IRoomAction room in roomList) {
            room.RoomAction();
        }
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

    public void AddRoom(IRoomAction room)
    {
        roomList.Add(room);
    }

}
