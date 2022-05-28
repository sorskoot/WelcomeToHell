using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractorTagCheck : XRSocketInteractor
{
    [SerializeField] private string tag = string.Empty;
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(tag);
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.CompareTag(tag);
    }
}
