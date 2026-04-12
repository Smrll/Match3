using Animations;
using Boot;
using SceneLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class RootScope: LifetimeScope //базовый корневой скоуп от которого все идет
    {
        [SerializeField] private LoadingView _loadingView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BootEntryPoint>();//не спрашивает лайфтайм,так как это регистрация точки входа
            builder.Register<IAsyncSceneLoading, AsyncSceneLoading>(Lifetime.Singleton);
            builder.Register<IAnimation, AnimationManager>(Lifetime.Singleton);//регистрирцем здесь так как анимации будут и в руте в том числе а не только в игре
            builder.RegisterInstance(_loadingView); 
        }
    }
}