using UnityEngine;

namespace ArmyRunner
{
    [RequireComponent(typeof(Collider))]
    public sealed class GateChoice : MonoBehaviour
    {
        [SerializeField] private string gateValue = "+2";
        private bool consumed;
        public string GateValue { get => gateValue; set => gateValue = value; }

        private void Reset() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (consumed) return;
            CrowdCount crowd = other.GetComponentInParent<CrowdCount>();
            if (crowd == null) return;
            if (crowd.ApplyGate(gateValue))
            {
                consumed = true;
                gameObject.SetActive(false);
            }
            else Debug.LogWarning($"Invalid gate value '{gateValue}' on {name}.", this);
        }
    }
}
