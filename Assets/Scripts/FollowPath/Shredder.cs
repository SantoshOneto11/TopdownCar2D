using UnityEngine;

namespace ShootBottle
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Shredder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.tag == "Bullet")
            {
                //collision.GetComponent<Bullet>().SendDataToPool(collision.gameObject);
                Debug.Log("Bullet Met shredder");
            }
        }
    }
}
