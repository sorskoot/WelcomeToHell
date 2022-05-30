using UnityEngine;

namespace WelcomeToHell.Controllers
{
    public class SelfDestruct : MonoBehaviour
    {
        public void Boom()
        {
            Destroy(gameObject);
        }
    }
}