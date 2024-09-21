using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    
    [field: SerializeField] public float LerpSpeed = 0.05f;

    [field: SerializeField] public Transform Canvas;
    [field: SerializeField] public Slider HealthSlider;
    [field: SerializeField] public Slider EaseHealthSlider;
    [field: SerializeField] public TextMeshProUGUI LevelText;

    [field: SerializeField] public int RedThreshold = 10;
    [field: SerializeField] public int GreenThreshold = 3;

    private Enemy enemy;
    private Camera cameraMain;

    #region Unity Methods

    private void Awake()
    {

        enemy = GetComponent<Enemy>();
        
        cameraMain = Camera.main;

    }

    private void Start()
    {

        InitializeHealthbar();
        InitializeLevelText();
        
    }

    private void Update()
    {

        if (HealthSlider.value != EaseHealthSlider.value)
        {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, enemy.Stats.RuntimeData.Health, LerpSpeed);
        }

    }

    private void LateUpdate()
    {
        Canvas.LookAt(cameraMain.transform);
        Canvas.rotation = Quaternion.LookRotation(cameraMain.transform.forward);
    }

    #endregion

    #region Main Methods

    private void InitializeHealthbar()
    {
        HealthSlider.maxValue = enemy.Stats.DefaultData.MaxHealth;
        HealthSlider.value = enemy.Stats.RuntimeData.Health;
        EaseHealthSlider.maxValue = enemy.Stats.RuntimeData.Health;
    }

    private void InitializeLevelText()
    {

        LevelText.SetText("Lv. " + enemy.Stats.DefaultData.Level);

        int levelDifference = Mathf.Abs(Player.Instance.Stats.RuntimeData.Level - enemy.Stats.DefaultData.Level);

        if (levelDifference >= RedThreshold)
        {
           LevelText.color = Color.red;
        }
        else if (levelDifference > GreenThreshold)
        {
            LevelText.color = Color.yellow;
        }
        else if (levelDifference <= GreenThreshold)
        {
            LevelText.color = Color.green;
        }
        else
        {
            LevelText.color = Color.white;
        }
    }

    public void UpdateHealth()
    {
        HealthSlider.value = enemy.Stats.RuntimeData.Health;

        if (enemy.Stats.RuntimeData.Health <= 0) GetComponentInChildren<Canvas>().enabled = false;

    }

     #endregion
}
