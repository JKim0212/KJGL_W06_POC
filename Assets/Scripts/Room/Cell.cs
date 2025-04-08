using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        
        CrewController.crewToMove.transform.position = transform.position;
        CrewController.crewToMove = null;
        Destroy(CrewController.draggedObject);
        CrewController.draggedObject = null;
        
    }
}
