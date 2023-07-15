using System.Collections;
using UnityEngine;

namespace Slots
{
    public sealed class ReelView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _markSpriteRenderers;
        [SerializeField] private ReelParameters _parameters;

        private const float MarkSizeUnit = 2.6f;

        private int _markSequenceIndex;
        private Mark[] _markSequence;

        public Mark[] MarkSequence
        {
            private get => _markSequence;
            set
            {
                _markSequence = value;
                for (var i = 0; i < _markSpriteRenderers.Length; i++)
                    _markSpriteRenderers[i].sprite = value[i].Sprite;
            }
        }

        private Coroutine _rollCoroutine;

        public bool IsRolling { get; private set; }

        public void StartRolling()
        {
            IsRolling = true;
            _rollCoroutine = StartCoroutine(StartRollingAsync());
        }

        private IEnumerator StartRollingAsync()
        {
            // TODO deltaTimeが変わることを考慮する
            var deltaTime = 1f / Application.targetFrameRate;
            var velocity = 0f;
            while (gameObject.activeSelf)
            {
                if (velocity < _parameters.MaxVelocity)
                    velocity += _parameters.Acceleration * deltaTime;
                if (velocity > _parameters.MaxVelocity)
                    velocity = _parameters.MaxVelocity;
                MoveMark(velocity);
                yield return null;
            }
        }

        public void StopRolling()
        {
            if (_rollCoroutine == null) return;

            StopCoroutine(_rollCoroutine);
            _rollCoroutine = null;

            StartCoroutine(StopRollingAsync());
        }

        private IEnumerator StopRollingAsync()
        {
            var velocity = _parameters.MaxVelocity;
            while (true)
            {
                var beforeMarkSequenceIndex = _markSequenceIndex;
                MoveMark(velocity);
                if (_markSequenceIndex != beforeMarkSequenceIndex)
                {
                    foreach (var markSpriteRenderer in _markSpriteRenderers)
                    {
                        var t = markSpriteRenderer.transform;
                        var index = (int)((t.localPosition.y + MarkSizeUnit) / MarkSizeUnit);
                        t.localPosition = new Vector3(0, MarkSizeUnit * index);
                    }

                    break;
                }
                yield return null;
            }

            IsRolling = false;
        }

        private void MoveMark(float velocity)
        {
            // TODO deltaTimeが変わることを考慮する
            var deltaTime = 1f / Application.targetFrameRate;

            foreach (var markSpriteRenderer in _markSpriteRenderers)
            {
                var t = markSpriteRenderer.transform;
                t.localPosition += 
                    Vector3.down * (velocity * deltaTime);

                if (t.localPosition.y < -MarkSizeUnit)
                {
                    _markSequenceIndex++;
                    if (_markSequenceIndex >= _markSequence.Length)
                        _markSequenceIndex = 0;
                    markSpriteRenderer.transform.localPosition -=
                        Vector3.down * (MarkSizeUnit * 4);

                    var targetIndex = 
                        (_markSequenceIndex + 3) % _markSequence.Length;
                    markSpriteRenderer.sprite = 
                        _markSequence[targetIndex].Sprite;
                }
            }
        }
    }
}
