///<summary>
/// Add Recoil to Gun.
/// </summary>


using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace ShootBottle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected GameObject bulletSpawnPoint;
        [SerializeField] protected Animator fireAnim;

        [SerializeField] protected float reloadTime;
        [SerializeField] protected float shootInterval;
        [SerializeField] protected float rotationSpeed;
        [SerializeField] protected float bulletSpeed;

        protected Rigidbody2D rb;
        protected bool isGiveBoost = false;
        protected void RotateGun()
        {
            float recoilAmount = isGiveBoost ? 2f : 1;
            transform.Rotate(new Vector3(0f, 0f, -rotationSpeed * recoilAmount));
        }

        public void Shoot(GameObject bullet)
        {
            StartCoroutine(RotateCounterclockwiseForSeconds(.5f));
            fireAnim.gameObject.SetActive(true);
            fireAnim.Play("BulletShoot");

            bullet.transform.position = bulletSpawnPoint.transform.position;
            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, 90f));

            bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletSpeed, ForceMode2D.Impulse);
        }

        protected void OnAnimationComplete()
        {
            fireAnim.gameObject.SetActive(false);
        }

        private IEnumerator RotateCounterclockwiseForSeconds(float duration)
        {
            isGiveBoost = true;
            yield return new WaitForSeconds(duration);
            isGiveBoost = false;
        }
    }


    public class Gun : BaseGun
    {

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            fireAnim.gameObject.GetComponent<AnimationEvent>().OnCompleteEvent.AddListener(OnAnimationComplete);
        }

        private void FixedUpdate()
        {
            RotateGun();
        }
    }
}
