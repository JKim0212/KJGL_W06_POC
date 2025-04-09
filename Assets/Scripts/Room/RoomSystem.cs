using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    protected List<CrewController> crewList = new List<CrewController>();
    bool onFire = false;
    bool canTakeFireDamage = true;
    Coroutine FireDamageCo;

    GameObject fireEffect;
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        foreach (CrewController crew in GetComponentsInChildren<CrewController>())
        {
            crewList.Add(crew);
        }
    }

    private void Update()
    {
        if (onFire && canTakeFireDamage)
        {

        }
    }

    public void AddCrew(CrewController crewToAdd)
    {
        crewList.Add(crewToAdd);
    }

    public void RemoveCrew(CrewController crewToRemove)
    {
        crewList.Remove(crewToRemove);
    }

    
    public void HitByProjectile(float amount)
    {
        TakeDamage(amount / 4);
        int poss = Random.Range(1, 101);
        if(poss <= 25)
        {
            StartFire();
        }
    }
    public void TakeDamage(float damage)
    {
        foreach (CrewController crew in crewList)
        {
            crew.Damage(damage);
        }
    }

    public void StartFire()
    {
        onFire = true;
        fireEffect = Instantiate(fireEffect, transform.position, Quaternion.identity);
    }

    public void ExtinguishFire()
    {
        onFire = false;
        Destroy(fireEffect);
        fireEffect = null;
    }

    public void TakeFireDamage()
    {
        FireDamageCo = StartCoroutine(FireDamageCoroutine());
    }

    IEnumerator FireDamageCoroutine()
    {
        TakeDamage(RoomManager.Instance.FireDamage);
        yield return new WaitForSeconds(RoomManager.Instance.FireDamageTime);
        canTakeFireDamage = true;
    }
}
