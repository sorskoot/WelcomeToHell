using Sorskoot.Ioc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
            var psop = args.interactableObject.transform.GetComponent<PutStampOnPaper>();
            if (psop)
            {
                psop.PlacedDown(false);
            }
            gameState.Value.TakeContractFromTable();
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            var psop = args.interactableObject.transform.GetComponent<PutStampOnPaper>();
            if (psop)
            {
                psop.PlacedDown(true);
            }
            gameState.Value.PlaceContractOnTable();
        }
    }
}