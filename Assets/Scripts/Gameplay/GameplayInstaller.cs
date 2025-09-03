using Zenject;

namespace TheWayOut.Gameplay
{
    public class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<PeacePlacedSignal>();
        }
    }
}
