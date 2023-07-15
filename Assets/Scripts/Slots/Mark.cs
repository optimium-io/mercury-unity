using System;
using UnityEngine;

namespace Slots
{
    [Serializable]
    public sealed class Mark
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;

        public int Id => _id;
        public Sprite Sprite => _sprite;
    }
}
