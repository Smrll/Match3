using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Tiles;
using UnityEngine;

namespace Animations
{
    public class AnimationManager : IAnimation, IDisposable
    {
        private CancellationTokenSource _cts;//написать что такое корутины(?) чем они отличаются от асинхронных задач,
        
        public async UniTask Reveal(GameObject target, float delay) //метод возвращающий юнитаск 
        {
            _cts = new CancellationTokenSource();//возможность отменить задачу
            target.transform.localScale = Vector3.one * 0.1f; //единичный вектор 1 1 1 
            await target.transform.DOScale(Vector3.one, delay).SetEase(Ease.OutBounce);
            _cts.Cancel();
        }

        public async UniTask HideTile(GameObject target)
        {
            _cts = new CancellationTokenSource();//возможность отменить задачу
            target.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.OutBounce);
            target.SetActive(false);
            target.transform.localScale = Vector3.one;
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }

        public void DoPunchAnimate(GameObject target, Vector3 scale, float duration)
        {
            target.transform.DOPunchScale(scale, duration, vibrato: 1, 0.5f);
            
        }

        public void MoveUI(RectTransform target, Vector3 position, float duration, Ease ease)
        {
            target.DOAnchorPos(position, duration).SetEase(ease);
        }

        public void AnimateTile(Tile tile, float value)
        {
            tile.transform.DOScale(value, duration:0.3f).SetEase(Ease.OutCubic);
        }

        public void MoveTile(Tile tile, Vector3 position, Ease ease)
        {
            tile.transform.DOLocalMove(position, 0.2f).SetEase(ease);
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }
    }
}