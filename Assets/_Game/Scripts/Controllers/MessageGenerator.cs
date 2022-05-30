using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WelcomeToHell.Controllers;
using Random = UnityEngine.Random;


namespace WelcomeToHell.Controllers
{
    [Serializable]
    public class SinDescriptor
    {
        [SerializeField]public Sins sin;
        [SerializeField][TextArea(3, 10)]public string message;
    }

    public class MessageGenerator : GameStateBehavior
    {
        [SerializeField] private Sprite[] photos;
        [SerializeField] private SinDescriptor[] sins;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image image;

        private void Start()
        {
            MessageBroker.Default.Receive<NewDeceased>().Subscribe(OnNewDeceased).AddTo(this);
        }

        private void OnNewDeceased(NewDeceased newDeceased)
        {
            Sprite randomSprite = photos[Random.Range(0, photos.Length)];
            image.sprite = randomSprite;
            var possibleSins = sins.ToList().Where(s => s.sin == newDeceased.sin).ToArray();
            SinDescriptor sin = possibleSins[Random.Range(0, possibleSins.Length)];
            text.text = sin.message;
        }
    }
}
