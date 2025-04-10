using System.Collections;
using UnityEngine;

public class WeaponRoom : RoomSystem, IRoomAction
{
    Coroutine shootCo;
    bool canShoot = true;
    float coolDown = 5;

    GameObject _gun;
    GameObject _shootPos;

    [SerializeField] GameObject _projectile;

    EnemySlot targetSlot;
    protected override void Init()
    {
        base.Init();
        RoomManager.Instance.AddRoom(this);
        InputManager.Instance.enemySlotSelectAction += TargetEnemy;
        _gun = FindAnyObjectByType<Gun>().gameObject;
        _shootPos = _gun.transform.GetChild(0).GetChild(0).gameObject;
    }
    public void RoomAction()
    {
        if (targetSlot != null && crewList.Count != 0 && !onFire)
        {
            Vector2 diff = targetSlot.transform.position - _gun.transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
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
        if (canShoot)
        {
            canShoot = false;
            float currentCooldown = coolDown * (1 - (0.125f * crewList.Count));
            shootCo = StartCoroutine(ShootCoroutine(currentCooldown));
        }
    }

    IEnumerator ShootCoroutine(float currentCooldown)
    {
        Debug.Log("Shoot");
        Instantiate(_projectile, new Vector3(_shootPos.transform.position.x, _shootPos.transform.position.y, -0.1f), _gun.transform.rotation);
        yield return new WaitForSeconds(currentCooldown);
        canShoot = true;
    }


}
