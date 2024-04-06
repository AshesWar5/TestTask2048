using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Animation_UI
{
    public sealed class AnimationUIService : IAnimationUIService
    {
        private readonly AnimationUIConfig _config;

        public AnimationUIService(AnimationUIConfig config)
        {
            _config = config;
            
            _config.Initialize();
        }

        public void PlayArrival(RectTransform rect, Action finish = null)
        {
            var model = _config.Get(AnimationUIType.DEPARTURE);
            float posX = rect.localPosition.x;
            float width = rect.rect.width;
            float startPos = posX + width;
            rect.localPosition = new Vector3(startPos, rect.localPosition.y, 0);
            rect.DOLocalMoveX(posX, model.Duration).OnComplete(() =>
                finish?.Invoke());
        }
        
        public void PlayDeparture(RectTransform rect, Action finish = null)
        {
            var model = _config.Get(AnimationUIType.DEPARTURE);
            float posX = rect.localPosition.x;
            rect.DOLocalMoveX(-rect.rect.width, model.Duration).OnComplete(() =>
            {
                rect.localPosition = new Vector3(posX, rect.localPosition.y, 0);
                finish?.Invoke();
            });
        }
    }
}