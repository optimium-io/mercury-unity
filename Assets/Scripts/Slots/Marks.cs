using UnityEngine;

namespace Slots
{
    [CreateAssetMenu(fileName = "Marks", menuName = "ScriptableObjects/CreateMarks")]
    public sealed class Marks : ScriptableObject
    {
        [SerializeField] private Mark[] _value;

        public Mark[] Value => _value;
    }
}
