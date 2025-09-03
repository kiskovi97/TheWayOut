using UnityEngine;

using TheWayOut.Gameplay;

using Zenject;

namespace TheWayOut.Main
{
    [CreateAssetMenu(fileName = "Settings", menuName = "TheWayOut/Settings")]
    internal class TheWayOutInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            GameplayInstaller.Install(Container);
        }
    }
}
