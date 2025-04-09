using UnityEngine;

public class Cell : MonoBehaviour
{
    private void Start()
    {
        RoomManager.Instance.Cells.Add(transform);
    }
}
