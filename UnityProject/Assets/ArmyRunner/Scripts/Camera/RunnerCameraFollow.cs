using UnityEngine;

namespace ArmyRunner
{
    public sealed class RunnerCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 8f, -10f);
        [SerializeField, Range(0.1f, 20f)] private float smoothSpeed = 8f;
        public void SetTarget(Transform value) => target = value;

        private void LateUpdate()
        {
            if (target == null) return;
            Vector3 desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
            transform.LookAt(target.position + Vector3.forward * 8f);
        }
    }
}
