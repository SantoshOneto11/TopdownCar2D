using UnityEngine;
using UnityEngine.Events;

namespace ShootBottle
{
    public class AnimationEvent : MonoBehaviour
    {
        public UnityEvent OnCompleteEvent;
        public void OnComplete()
        {
            Debug.Log("Animation Completed!");
            OnCompleteEvent.Invoke();
        }
    }
}
