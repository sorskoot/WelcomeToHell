using System.Collections;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UniRx;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using WelcomeToHell;

public class ContractSpawner : MonoBehaviour
{
    [SerializeField] private GameObject contractPrefab;
    [SerializeField] private XRInteractionManager interactionManager;
    
    private SnapDependency<IGameState> gameState;
    private bool wasPrinting;

    private void Start()
    {
        gameState.Value.IsPrinting.Subscribe(IsPrintingChanged).AddTo(this);
    }

    private void IsPrintingChanged(bool isPrinting)
    {
        if (isPrinting && !wasPrinting)
        {
            wasPrinting = true;
        }

        if (!isPrinting && wasPrinting)
        {
            var newContract = Instantiate(contractPrefab);
            newContract.transform.position = gameObject.transform.position;
            newContract.transform.rotation = gameObject.transform.rotation;
            newContract.GetComponent<XRGrabInteractable>().interactionManager = interactionManager;
            newContract.GetComponent<Rigidbody>().useGravity = false;
            wasPrinting = false;
        }
    }
}
