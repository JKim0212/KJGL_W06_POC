using System.Collections;
using UnityEngine;

public class EnemySlot : MonoBehaviour
{
    float health = 100;

    [SerializeField] float coolDown;
    bool canShoot = true;
    [SerializeField] GameObject _projectile;
    Transform target;

    Coroutine shootCo;

    private void Update()
    {
        if (canShoot)
        {
            int random = Random.Range(0, RoomManager.Instance.Cells.Count);
            target = RoomManager.Instance.Cells[random];
            Vector2 diff = target.transform.position - transform.position;
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,angle);
            canShoot = false;
            Shoot();
        }
    }

    void Shoot()
    {
        canShoot = false;
        shootCo = StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        Debug.Log("Shoot");
        Instantiate(_projectile, new Vector3(transform.position.x, transform.position.y, -0.1f), transform.rotation);
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

}
