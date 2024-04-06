using System;
using UnityEngine;

namespace CodeBase.Logic.Animation_UI
{
    [Serializable]
    public class AnimationUIModel
    {
        [SerializeField] private AnimationUIType _type;
        [SerializeField] private float _duration;

        public AnimationUIType Type => _type;
        public float Duration => _duration;
    }
}