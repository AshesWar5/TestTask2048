using System;
using UnityEngine;

namespace CodeBase.Logic.Animation_UI
{
    public interface IAnimationUIService
    {
        void PlayArrival(RectTransform rect, Action finish = null);
        void PlayDeparture(RectTransform rect, Action finish = null);
    }
}