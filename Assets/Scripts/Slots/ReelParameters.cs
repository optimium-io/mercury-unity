using UnityEngine;

namespace Slots
{
    [CreateAssetMenu(fileName = "ReelParameters", menuName = "ScriptableObjects/CreateReelParameters")]
    public sealed class ReelParameters : ScriptableObject
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _maxVelocity;
        public float Acceleration { get; set; }
        public float MaxVelocity { get; set; }
        
        private void OnEnable()
        {
            Acceleration = _acceleration;
            MaxVelocity = _maxVelocity;
        }
    }
}
