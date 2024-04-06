using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input_Game
{
    public class StandaloneInput : IInputService, ITickable
    {
        public event Action<Vector2> Move;
        
        private const float DEAD_ZONE = 80;
        
        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;
        private bool _isSwiping;

        public void Tick()
        {
            if(Input.GetKeyDown((KeyCode.A)))
                Move?.Invoke(Vector2.left);
            
            if(Input.GetKeyDown((KeyCode.D)))
                Move?.Invoke(Vector2.right);
            
            if(Input.GetKeyDown((KeyCode.W)))
                Move?.Invoke(Vector2.up);
            
            if(Input.GetKeyDown((KeyCode.S)))
                Move?.Invoke(Vector2.down);

            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _tapPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
            
            CheckSwipe();
        }
        
        private void CheckSwipe()
        {
            _swipeDelta = Vector2.zero;

            if(!_isSwiping) return;
            
            if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
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