using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private Button enterGameButton;

    private void Start()
    {
        enterGameButton.onClick.AddListener(EnterGame);
    }

    public void EnterGame()
    {
        // Assuming SceneLoader.Load is a static method
        SceneLoader.Load(SceneEnum.MainScene);
    }
}
