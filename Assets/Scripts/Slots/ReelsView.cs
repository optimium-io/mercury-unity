using System.Collections;
using System.Linq;
using UnityEngine;

namespace Slots
{
    public sealed class ReelsView : MonoBehaviour
    {
        [SerializeField] private Marks _marks;
        [SerializeField] private ReelView[] _reelViews;

        private static readonly WaitForSeconds WaitForSeconds = new(0.1f);

        public int[] MarkIdSequence
        {
            set
            {
                var marks = new Mark[value.Length];
                for (var i = 0; i < value.Length; i++)
                    marks[i] = _marks.Value[value[i]];
                foreach (var reelView in _reelViews)
                    reelView.MarkSequence = marks;
            }
        }

        public bool IsRolling => _reelViews.Any(x => x.IsRolling);

        public IEnumerator StartRollingAsync()
        {
            foreach (var reelView in _reelViews) reelView.StartRolling();
            yield return WaitForSeconds;
        }

        public void StopRolling(int index)
        {
            _reelViews[index].StopRolling();
        }
    }
}
