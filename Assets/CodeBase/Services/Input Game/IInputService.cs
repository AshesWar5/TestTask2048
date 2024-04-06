using System;
using UnityEngine;

namespace CodeBase.Services.Input_Game
{
    public interface IInputService
    {
        event Action<Vector2> Move;
    }
}