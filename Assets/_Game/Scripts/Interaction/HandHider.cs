using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WelcomeToHell.Interaction
{
    public class HandHider : MonoBehaviour
    {
        private SkinnedMeshRenderer meshRenderer = null;
        private XRDirectInteractor interactor = null;

        private void Awake()
        {
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            interactor = GetComponentInChildren<XRDirectInteractor>();
            
            interactor.hoverEntered.AddListener(OnHoverEntered);
            interactor.hoverExited.AddListener(OnHoverExit);
        }

        private void OnHoverEntered(HoverEnterEventArgs args)
        {
            meshRenderer.enabled = false;
        }
        
        private void OnHoverExit(HoverExitEventArgs args)
        {
            meshRenderer.enabled = true;
        }
    }
}