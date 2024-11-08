using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace ShootBottle
{
    public class Bottle : MonoBehaviour, IBottle
    {
        [SerializeField] Path path;
        [SerializeField] GameManager gameManager;
        [SerializeField] ParticleSystem shootVfx;

        float moveSpeed = 2f;
        float newAngle = 0;
        int currentIndex = 0;

        public UnityEvent OnReachingWaypoint;

        private void Awake()
        {
            path ??= FindObjectOfType<Path>();
            gameManager ??= FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            OnReachingWaypoint.AddListener(ChangeDirection);
        }

        public void ChangeDirection()
        {
            newAngle -= 90f;
            newAngle = Mathf.Repeat(newAngle, 360f) - 360f;

            transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, newAngle), .2f);
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, path.wayPoints[currentIndex].position, Time.deltaTime * moveSpeed);

            float distanceFromBottle = Vector2.Distance(transform.position, path.wayPoints[currentIndex].position);
            if (distanceFromBottle < 0.1f)
            {
                currentIndex++;
                if (currentIndex >= path.wayPoints.Length)
                {
                    currentIndex = 0;
                }
                OnReachingWaypoint.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.gameObject.tag == "Bullet")
            {
                gameManager.SendPoolData(gameObject);
                gameManager.OnBottleHit.Invoke();

                Bullet bulletObj = collision.GetComponent<Bullet>();
                if (bulletObj != null)
                {
                    bulletObj.InstantiateVfx(shootVfx, transform);
                }
            }
        }
    }
}
