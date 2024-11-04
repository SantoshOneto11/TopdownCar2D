using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace GraplingHook
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject ropeHingerAnchor;
        [SerializeField] private DistanceJoint2D distanceJoint;
        [SerializeField] private Transform crossHair;

        [SerializeField] private SpriteRenderer crossHeirSprite;
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private bool isRopeAttached = false;
        [SerializeField] private Vector2 playerPos;
        [SerializeField] private Rigidbody2D ropHighAcnhorRb;

        [SerializeField] private SpriteRenderer ropeHingeAnchorSprite;
        [SerializeField] private LineRenderer ropeRenderer;

        [SerializeField] private LayerMask ropeLayerMask;
        [SerializeField] private float ropeMaxCastDistance = 20f;
        [SerializeField] private List<Vector2> ropePosition = new List<Vector2>();

        private void Awake()
        {
            distanceJoint.enabled = false;
            playerPos = transform.position;
            ropHighAcnhorRb = ropeHingerAnchor.GetComponent<Rigidbody2D>();
            ropeHingeAnchorSprite = ropeHingerAnchor.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, 0f));
            var facingDirection = worldMousePosition - transform.position;
            var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);

            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
            playerPos = transform.position;

            if (!isRopeAttached)
            {
                SetCrossHeirPosition(aimAngle);
            }
            else
            {
                crossHeirSprite.enabled = false;
            }
        }

        void SetCrossHeirPosition(float aimAngle)
        {
            if (!crossHeirSprite.enabled)
            {
                crossHeirSprite.enabled = true;
            }

            var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
            var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

            var crossHeirPos = new Vector3(x, y, 0f);
            crossHair.transform.position = crossHeirPos;
        }

        private void HandleInput(Vector2 aimDirection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isRopeAttached) return;

                ropeRenderer.enabled = false;
                var hit = Physics2D.Raycast(playerPos, aimDirection,
                    ropeMaxCastDistance, ropeLayerMask);

                Debug.DrawRay(playerPos, aimDirection * ropeMaxCastDistance, Color.red);

                if (hit.collider != null)
                {
                    isRopeAttached = true;

                    if (!ropePosition.Contains(hit.point))
                    {
                        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                        ropePosition.Add(hit.point);
                        distanceJoint.distance = Vector2.Distance(playerPos, hit.point);
                        distanceJoint.enabled = true;
                        ropeHingeAnchorSprite.enabled = true;
                    }
                }
                else
                {
                    ropeRenderer.enabled = false;
                    isRopeAttached = false;
                    distanceJoint.enabled = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                ResetRope();
            }
        }

        private void ResetRope()
        {
            distanceJoint.enabled = false;
            isRopeAttached = false;
            //playerMovement.isSwinging = false;
            ropeRenderer.positionCount = 2;
            ropeRenderer.SetPosition(0, transform.position);
            ropeRenderer.SetPosition(1, transform.position);

            ropePosition.Clear();
            ropeHingeAnchorSprite.enabled = false;
        }
    }
}
