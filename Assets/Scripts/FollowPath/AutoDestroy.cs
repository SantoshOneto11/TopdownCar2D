using UnityEngine;

namespace ShootBottle
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float destroyAfter = 1f;

        private void OnEnable()
        {
            Destroy(gameObject, destroyAfter);
        }
    }
}
