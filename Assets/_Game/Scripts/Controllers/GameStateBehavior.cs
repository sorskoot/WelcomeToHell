using Sorskoot.Ioc;
using UnityEngine;

namespace WelcomeToHell.Controllers
{
    public class GameStateBehavior: MonoBehaviour 
    {
        private SnapDependency<IGameState> internalGamestate;
        protected IGameState gameState => internalGamestate.Value;
    }
}