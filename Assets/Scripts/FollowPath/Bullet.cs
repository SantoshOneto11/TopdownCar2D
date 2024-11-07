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
    }
}
