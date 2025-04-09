using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;
    float speed = 5;

    private void Update()
    {
        transform.position = transform.position + transform.right * Time.deltaTime * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.CompareTag("EnemySlot"))
        {
            collision.GetComponent<EnemySlot>().Damage(damage);
            Destroy(gameObject) ;
        }
    }
}
