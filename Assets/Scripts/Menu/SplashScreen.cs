using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float transitionDelay;
    [SerializeField] private string transitionTo;

    private void Start()
    {
        Invoke("Transition", transitionDelay);
    }

    private void Transition()
    {
        SceneManagement.Load(transitionTo);
    }
}
