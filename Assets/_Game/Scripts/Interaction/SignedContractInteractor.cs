using System;
using Sorskoot.Ioc;
using UniRx;
using UnityEngine.XR.Interaction.Toolkit;
using WelcomeToHell.Controllers;

namespace WelcomeToHell.Interaction
{
    public class SignedContractInteractor : XRSocketInteractor
    {
        private SnapDependency<IGameState> gameState;
        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            PutStampOnPaper putStampOnPaper = interactable.transform.GetComponent<PutStampOnPaper>();
            return base.CanSelect(interactable) && putStampOnPaper != null && putStampOnPaper.StampName != String.Empty;
        }

        public override bool CanHover(IXRHoverInteractable interactable)
        {
            PutStampOnPaper putStampOnPaper = interactable.transform.GetComponent<PutStampOnPaper>();
            return base.CanHover(interactable) && putStampOnPaper != null && putStampOnPaper.StampName != String.Empty;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            PutStampOnPaper putStampOnPaper = args.interactableObject.transform.GetComponent<PutStampOnPaper>();
            gameState.Value.ContractPlaced(putStampOnPaper.StampName);
            base.OnSelectEntered(args);
            Scheduler.MainThread.Schedule(500, () => args.interactableObject.transform.GetComponent<SelfDestruct>().Boom());
        }
    }
}