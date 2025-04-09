using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Cell"))
        {
            int rand = Random.Range(1, 101);
            if(rand > TrailerManager.Instance.MissChance)
            {
                collision.transform.parent.GetComponent<RoomSystem>().HitByProjectile(damage);
                Destroy(gameObject);
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
