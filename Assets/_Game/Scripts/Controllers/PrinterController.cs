using System.Collections;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace WelcomeToHell.Controllers
{
    public class PrinterController : MonoBehaviour
    {
        [SerializeField] private UnityEvent startPrinting;
        [SerializeField] private UnityEvent stopPrinting;
        
        private SnapDependency<IGameState> gameState;
        private bool wasPrinting;

        // Start is called before the first frame update
        void Start()
        {
            gameState.Value.IsPrinting.Subscribe(IsPrinting).AddTo(this);
        }

        private void IsPrinting(bool isPrinting)
        {
            if (!wasPrinting && isPrinting)
            {
                startPrinting.Invoke();
                wasPrinting = true;
            }

            if (wasPrinting && !isPrinting)
            {
                stopPrinting.Invoke();
                wasPrinting = false;
            }
        }
    }
}