using DG.Tweening;
//using save;
using SceneLoading;
using UnityEngine;
using VContainer.Unity;

namespace Boot
{
    public class BootEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        //private SaveProgress _saveProgress;

        public BootEntryPoint(IAsyncSceneLoading sceneLoading/*, SaveProgress saveProgress*/)
        {
            //_saveProgress = saveProgress;
            _sceneLoading = sceneLoading;
        }

        public async void Initialize() //так как не монобех используем такой вариант
        {
            Application.targetFrameRate = 60;//для телефона
            Screen.sleepTimeout = SleepTimeout.NeverSleep;//для телефона
            DOTween.SetTweensCapacity(5000, 100);//как много твин анимаций может быть в 1 время запущено
            //_saveProgress.LoadData();
            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}