using System.Collections;
using UnityEngine;

public class WeaponRoom : RoomSystem, IRoomAction
{
    float damage = 10;
    Coroutine shootCo;
    bool canShoot = true;
    float coolDown = 5;

    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.AddRoom(this);
    }
    public void RoomAction()
    {
        Shoot();
    }

    void Shoot()
    {
        if (canShoot && crewList.Count != 0)
        {
            canShoot = false;
            float currentCooldown = coolDown * (1- (0.125f * crewList.Count));
            shootCo = StartCoroutine(ShootCoroutine(currentCooldown));
        }
    }

    IEnumerator ShootCoroutine(float currentCooldown)
    {
        Debug.Log("Shoot");
        yield return new WaitForSeconds(currentCooldown);
        canShoot = true;
    }
}
