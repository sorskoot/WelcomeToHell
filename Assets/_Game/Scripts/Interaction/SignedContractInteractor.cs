using System;
using UnityEngine.XR.Interaction.Toolkit;
using WelcomeToHell.Controllers;

namespace WelcomeToHell.Interaction
{
    public class SignedContractInteractor : XRSocketInteractor
    {
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
    }
}