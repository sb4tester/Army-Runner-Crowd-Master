using System;
using UnityEngine;

namespace ArmyRunner
{
    public sealed class CrowdCount : MonoBehaviour
    {
        [SerializeField, Min(0)] private int count = 10;
        [SerializeField, Min(1)] private int maximumCount = GateMath.DefaultMaximumCrowd;
        public int Count => count;

        public void SetCount(int value) => count = Mathf.Clamp(value, 0, maximumCount);

        public void Add(int amount)
        {
            long next = (long)count + amount;
            SetCount((int)Math.Max(0L, Math.Min(maximumCount, next)));
        }

        public bool ApplyGate(string gateValue)
        {
            if (!GateMath.TryApply(count, gateValue, out int next, maximumCount)) return false;
            SetCount(next);
            return true;
        }
    }
}
