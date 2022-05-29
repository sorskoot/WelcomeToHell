using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

namespace WelcomeToHell.Controllers
{
    public class PutStampOnPaper : GameStateBehavior
    {
        [SerializeField] private GameObject paperWithoutStamp;
        [SerializeField] private GameObject paperWithStamp;
        
        public string StampName = string.Empty;
        private bool hasStamp = false;
        
        private void Start()
        {
            MessageBroker.Default.Receive<StampMessage>().Subscribe(OnStampMessage).AddTo(this);
            paperWithStamp.SetActive(false);
            paperWithoutStamp.SetActive(true);
        }

        private void OnStampMessage(StampMessage stampMessage)
        {
            if (hasStamp)
            {
                return;
            }
            
            hasStamp = true;
            paperWithStamp.SetActive(true);
            paperWithoutStamp.SetActive(false);
            StampName = stampMessage.stampName;
        }
    }
}