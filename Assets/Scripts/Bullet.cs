using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float damage = 5;
   
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Enemy"))
      {
         Enemy enemy = collision.gameObject.GetComponent<Enemy>();
         enemy.TakeDamage(damage);
      }
      Destroy(gameObject);
   }
}
