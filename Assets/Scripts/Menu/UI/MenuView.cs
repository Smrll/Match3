using System;
using System.Collections.Generic;
using System.Threading;
using Animations;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Menu.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _leftTower;
        [SerializeField] private RectTransform _rightTower;
        [SerializeField] private RectTransform _wall;
        [SerializeField] private RectTransform _logo;
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();
        private CancellationTokenSource _cts;
        //audio
        private IAnimation _animation;

        public async UniTask StartAnimation()
        {
            _cts  = new CancellationTokenSource();
            //audio
            _animation.MoveUI(_leftTower, new Vector3(
                -71f, -50f, 0), 2f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), _cts.IsCancellationRequested);
            _animation.MoveUI(_rightTower, new Vector3(
                -392f, -50f, 0), 2f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), _cts.IsCancellationRequested);
            _animation.MoveUI(_wall, new Vector3(
                0, -30f, 0), 2f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), _cts.IsCancellationRequested);
            _animation.MoveUI(_logo, new Vector3(
                -134f, 117f, 0), 3f, Ease.OutBounce);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), _cts.IsCancellationRequested);

            foreach (var button in _levelButtons)
            {
                // play sound
                button.SetActive(true);
                await _animation.Reveal(button, 0.5f);
            }
        }

        [Inject]
        private void Construct(IAnimation animation)
        {
            _animation = animation;
        }
    }
}