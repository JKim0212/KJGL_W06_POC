using System.Collections;
using UnityEngine;

public class HealingRoom : RoomSystem, IRoomAction
{
    [SerializeField] float healAmount = 10;
    [SerializeField] float healRate = 1;
    bool canHeal = true;
    Coroutine healCo;
    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.AddRoom(this);
    }

    public void RoomAction()
    {
        if (canHeal && crewList.Count != 0)
        {
            canHeal = false;
            HealCrews();
        }
    }

    public void HealCrews()
    {
        healCo = StartCoroutine(HealCoroutine());
    }

    IEnumerator HealCoroutine()
    {
        foreach (CrewController crew in crewList) { 
            crew.Heal(healAmount);
        }
        yield return new WaitForSeconds(healRate);
        canHeal = true;
    }
}
