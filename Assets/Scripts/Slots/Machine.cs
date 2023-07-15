using UnityEngine;

namespace Slots
{
    public class Machine : MonoBehaviour
    {
        [SerializeField] private ReelsView _reelsView;

        private void Awake()
        {
            Application.targetFrameRate = 30;
            var ids = new[]
            {
                0, 1, 0, 2, 3, 4, 5
            };
            _reelsView.MarkIdSequence = ids;
        }

        public void Play()
        {
            if (_reelsView.IsRolling) return;

            StartCoroutine(_reelsView.StartRollingAsync());
        }

        public void StopRolling(int index)
        {
            _reelsView.StopRolling(index);
        }
    }
}
