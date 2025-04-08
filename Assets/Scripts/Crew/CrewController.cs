using UnityEngine;
using UnityEngine.EventSystems;

public class CrewController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    float health;
    float moveSpeed;
    [SerializeField] GameObject clone;

    public static CrewController crewToMove = null;
    public static GameObject draggedObject = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedObject = Instantiate(clone, transform.position, Quaternion.identity);
        crewToMove = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        draggedObject.transform.position = currentPos;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggedObject);
        draggedObject=null;
        crewToMove=null;
    }
}
