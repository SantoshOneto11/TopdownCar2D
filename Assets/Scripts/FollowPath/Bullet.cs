using System;
using UnityEngine;
using UnityEngine.Events;

namespace ShootBottle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision?.tag == "Bottle" || collision?.tag == "Shredder")
            {
                gameManager.SendPoolData(gameObject);
            }
        }

        public void InstantiateVfx(ParticleSystem vfx, Transform transform)
        {
            ParticleSystem particle = Instantiate(vfx, transform.position, Quaternion.identity);
            particle.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, 90f));
        }
    }
}
