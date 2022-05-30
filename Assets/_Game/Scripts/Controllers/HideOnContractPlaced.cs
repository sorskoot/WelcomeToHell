using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace WelcomeToHell.Controllers
{
    public class HideOnContractPlaced : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            MessageBroker.Default.Receive<ContractPlacedMessage>().Subscribe(OnContractPlaced).AddTo(this);
        }
        
        private void OnContractPlaced(ContractPlacedMessage _)
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}