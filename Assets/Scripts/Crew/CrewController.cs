using UnityEngine;
using UnityEngine.EventSystems;

public class CrewController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    float health;
    float moveSpeed;
    [SerializeField] GameObject clone;

    public static CrewController crewToMove = null;
    public static GameObject draggedObject = null;

    public Transform currentParent;
    Transform previousParent;

    private void Start()
    {
        currentParent = transform.parent;
    }

    //선원을 클릭시 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        currentParent.parent.GetComponent<RoomSystem>().RemoveCrew(this);
        previousParent = currentParent;
        draggedObject = Instantiate(clone, transform.position, Quaternion.identity); //선원의 옮기는 위치를 표시해줄 분신 소환
        crewToMove = this;
    }

    //드래그 중 분신의 위치 마우스 포인터 따라오기
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        draggedObject.transform.position = currentPos;

    }
    //마우스 놓을 시 만약 방에 들어가지 않았다면 리셋
    public void OnEndDrag(PointerEventData eventData)
    {
        if (previousParent == currentParent) //원래 부모와 지금 부모가 같은지체크
        {
            Debug.Log("prev parent add");
            previousParent = null;
            currentParent.parent.GetComponent<RoomSystem>().AddCrew(this);
            Destroy(draggedObject);
            draggedObject = null;
            crewToMove = null;
        }

    }
}
