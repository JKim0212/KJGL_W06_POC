using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CrewController : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    float _health;
    [SerializeField] float _moveSpeed;
    NavMeshAgent _agent;
    public RoomSystem CurrentRoom { get; set; }
    GameObject highlight;
    Coroutine _moveCo;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateUpAxis = false;
        _agent.updatePosition = false;
        _agent.speed = _moveSpeed;
    }

    private void Start()
    {
        highlight = transform.GetChild(0).gameObject;
        CurrentRoom = transform.parent.parent.GetComponent<RoomSystem>();
        //_health = _maxHealth;
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
        _moveCo = StartCoroutine(MoveCoroutine(new Vector3(targetPos.x, targetPos.y, -0.1f)));
    }

    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        _agent.SetDestination(targetPos);
        Debug.Log("Moving");
        while(Vector2.Distance(transform.position, targetPos) > 0.1f)
        {
            Vector3 diff = _agent.nextPosition - transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.position = _agent.nextPosition;
            transform.rotation = Quaternion.Euler(0f,0f, angle);
            yield return null;
        }
        transform.position = targetPos;
        transform.rotation = CurrentRoom.transform.rotation;
        RoomManager.Instance.DeselectRoom();
    }

    public void Heal(float amount)
    {
        _health += amount;
        if(_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
