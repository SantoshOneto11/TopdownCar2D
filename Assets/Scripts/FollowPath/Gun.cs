using DG.Tweening;
using UnityEngine;

namespace ShootBottle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected GameObject bulletSpawnPoint;
        [SerializeField] protected float reloadTime;
        [SerializeField] protected float shootInterval;
        [SerializeField] protected float rotationSpeed;
        [SerializeField] protected float bulletSpeed;

        protected Rigidbody2D rb;
        protected void RotateGun()
        {
            transform.Rotate(new Vector3(0f, 0f, -rotationSpeed));
        }

        //Get pooled Bullet 
        //Add Force to Bullet
        public void Shoot(GameObject bullet)
        {
            bullet.transform.position = bulletSpawnPoint.transform.position;
            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, 90f));

            bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }


    public class Gun : BaseGun
    {
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            RotateGun();
        }
    }
}
