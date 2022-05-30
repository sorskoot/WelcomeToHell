using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            Sprite randomSprite = photos[Random.Range(0, photos.Length)];
            image.sprite = randomSprite;
            SinDescriptor sin = sins[Random.Range(0, sins.Length)];
            text.text = sin.message;
        }
    }
}
