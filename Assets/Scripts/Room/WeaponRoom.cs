using System.Collections;
using UnityEngine;

public class WeaponRoom : RoomSystem, IRoomAction
{
    float damage = 10;
    Coroutine shootCo;
    bool canShoot = true;
    float coolDown = 5;

    GameObject _gun;

    EnemySlot targetSlot;
    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.AddRoom(this);
        InputManager.Instance.enemySlotSelectAction += TargetEnemy;
        _gun = FindAnyObjectByType<Gun>().gameObject;
    }
    public void RoomAction()
    {
        if(targetSlot != null)
        {
            Vector2 diff = targetSlot.transform.position - _gun.transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _gun.transform.rotation = Quaternion.Euler(0f,0f, angle);
            Shoot();
        }
    }

    void TargetEnemy(GameObject target)
    {
        RoomManager.Instance.DeselectRoom();
        targetSlot = target.GetComponent<EnemySlot>();
    }

    void Shoot()
    {
        if (canShoot && crewList.Count != 0)
        {
            canShoot = false;
            float currentCooldown = coolDown * (1 - (0.125f * crewList.Count));
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
