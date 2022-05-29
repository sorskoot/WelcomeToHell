using Sorskoot.Ioc;
using UnityEngine;

namespace WelcomeToHell.Controllers
{
    public class PrintButton : MonoBehaviour
    {
        private SnapDependency<IGameState> gameState;

        public void ButtonPressed()
        {
            gameState.Value.AddPrintToQueue();
        }
        
    }
}