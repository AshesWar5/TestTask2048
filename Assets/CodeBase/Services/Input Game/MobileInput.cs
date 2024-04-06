using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input_Game
{
    public class MobileInput : IInputService, ITickable
    {
        public event Action<Vector2> Move;

        private const float DEAD_ZONE = 80;
        
        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;
        private bool _isSwiping;

        public void Tick()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Canceled ||
                         touch.phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
            
            CheckSwipe();
        }

        private void CheckSwipe()
        {
            _swipeDelta = Vector2.zero;
            
            if(!_isSwiping) return;
            
            if (Input.touchCount > 0)
            {
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
            }

            if (_swipeDelta.magnitude > DEAD_ZONE)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                {
                    Move?.Invoke(_swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                }
                else
                {
                    Move?.Invoke(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);
                }
                
                ResetSwipe();
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;
            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
        }
    }
}