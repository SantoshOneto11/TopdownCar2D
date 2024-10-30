using UnityEngine;

namespace DirtVally
{
    public class CarController : MonoBehaviour
    {
        //[SerializeField]
        //private float accelerationFactor = 30f;

        //[SerializeField]
        //private float turnFactor = 5f;

        //private float accelerationInput = 0.0f;
        //private float steeringInput = 0.0f;

        //private float rotationAngle = 0.0f;

        //Rigidbody2D rigidbody2d;

        //private void Awake()
        //{
        //    rigidbody2d = GetComponent<Rigidbody2D>();
        //}

        public float acceleration;
        public float steering;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            float h = -Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector2 speed = transform.up * (v * acceleration);
            rb.AddForce(speed);

            float direction = Vector2.Dot(rb.linearVelocity, rb.GetRelativeVector(Vector2.up));
            if (direction >= 0.0f)
            {
                rb.rotation += h * steering * (rb.linearVelocity.magnitude / 5.0f);
                //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
            }
            else
            {
                rb.rotation -= h * steering * (rb.linearVelocity.magnitude / 5.0f);
                //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
            }

            Vector2 forward = new Vector2(0.0f, 0.5f);
            float steeringRightAngle;
            if (rb.angularVelocity > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }

            Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
            Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);

            float driftForce = Vector2.Dot(rb.linearVelocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

            Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);


            Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);

            rb.AddForce(rb.GetRelativeVector(relativeForce));
        }

    }

}
