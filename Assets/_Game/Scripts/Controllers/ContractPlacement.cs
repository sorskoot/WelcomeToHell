using System.Collections;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ILogger = Sorskoot.Ioc.ILogger;

namespace WelcomeToHell.Controllers
{
    public class ContractPlacement : MonoBehaviour
    {
        private SnapDependency<IGameState> gameState;
        private XRSocketInteractor contractSocketInteractor;
        
        // Start is called before the first frame update
        void Start()
        {
            contractSocketInteractor = GetComponent<XRSocketInteractor>();
            
            contractSocketInteractor.selectEntered.AddListener(OnSelectEntered);
            contractSocketInteractor.selectExited.AddListener(OnSelectExited);
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            gameState.Value.TakeContractFromTable();
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            gameState.Value.PlaceContractOnTable();
        }
    }
}