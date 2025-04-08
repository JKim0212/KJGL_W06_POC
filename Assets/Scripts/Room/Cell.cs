using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    //드랍했을 시 만약 같은 방의 셀이면 변경 없음
    public void OnDrop(PointerEventData eventData)
    {
        if (CrewController.crewToMove.currentParent.parent != transform.parent)
        {
            transform.parent.GetComponent<RoomSystem>().AddCrew(CrewController.crewToMove);
            CrewController.crewToMove = null;
            Destroy(CrewController.draggedObject);
            CrewController.draggedObject = null;
        }
    }
}
