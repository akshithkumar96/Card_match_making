
using System;
using UnityEngine;

public interface ITransitionEffect
{
    void Initialize(Sprite frontImage, Sprite backImage);
    void ShowTransition(float duration, Action OnComplete);
}
