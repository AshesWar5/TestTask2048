using System;
using System.Collections.Generic;
using CodeBase.Infrastructures;
using UnityEngine;

namespace CodeBase.Logic.Animation_UI
{
    [CreateAssetMenu(fileName = "Animation UI", menuName = "Animation/UI")]
    public class AnimationUIConfig : ScriptableObject, IInitialize
    {
        [SerializeField] private AnimationUIModel[] _animations;

        private Dictionary<AnimationUIType, AnimationUIModel> _animationsModels;

        public void Initialize()
        {
            _animationsModels = new Dictionary<AnimationUIType, AnimationUIModel>();
            foreach (var animation in _animations)
            {
                _animationsModels.Add(animation.Type, animation);
            }
        }

        public AnimationUIModel Get(AnimationUIType type)
        {
            if(!_animationsModels.TryGetValue(type, out var model))
                throw new NullReferenceException($"No animation was found for your {type}.");

            return model;
        }
    }
}