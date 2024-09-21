using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = transform.GetComponent<Slider>();
    }

    private void OnEnable()
    {
        SceneLoader.DisableDontDestroyObjects();
    }

    private void OnDisable()
    {
        SceneLoader.EnableDontDestroyObjects();
    }

    private void Update()
    {
        slider.value = SceneLoader.GetLoadingProgress();
    }
}
