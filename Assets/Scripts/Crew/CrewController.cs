using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrewController : MonoBehaviour
{
    float _health;
    [SerializeField] float _moveSpeed;

    public RoomSystem CurrentRoom { get; set; }
    GameObject highlight;
    Coroutine _moveCo;

    private void Start()
    {
        highlight = transform.GetChild(0).gameObject;
        CurrentRoom = transform.parent.parent.GetComponent<RoomSystem>();
    }

    public void ToggleSelect(bool select)
    {
        highlight.SetActive(select);
    }

    public void Move(Vector2 targetPos)
    {
        if(_moveCo != null)
        {
            StopCoroutine(_moveCo);
        }
        _moveCo = StartCoroutine(MoveCoroutine(targetPos));
    }

    IEnumerator MoveCoroutine(Vector2 targetPos)
    {
        Debug.Log("Moving");
        while(Vector2.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        transform.position = new Vector3 (targetPos.x, targetPos.y, -0.1f);
        RoomManager.Instance.DeselectRoom();
    }
}
