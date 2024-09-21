using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerLockSystem: MonoBehaviour
{
    [field: SerializeField] public PlayerLockData Data { get; private set; }
    [field: SerializeField] public LockSystemInfo Info { get; private set; }

    private Transform player;
    private Enemy lockedEnemy;
    private GameObject currentCrosshair;
    private Camera cameraMain;
    private Image crosshairImage;

    private bool isEnable = false;

    public void Initialize(Transform player)
    {
        this.player = player;

        cameraMain = Camera.main;

        Info.gameObject.SetActive(isEnable);
    }

    private void Update()
    {
        if (isEnable && player)
        {
            CheckForEnemies();
            UpdateCrosshair();
            UpdateInfo();
        }

    }

    private void LateUpdate()
    {
        if (currentCrosshair)
        {
            currentCrosshair.transform.LookAt(cameraMain.transform);
            currentCrosshair.transform.rotation = Quaternion.LookRotation(cameraMain.transform.forward);
        }

    }

    public void EnableLock()
    {
        isEnable = true;
        Info.gameObject.SetActive(isEnable);
    }
    public void DisableLock()
    {
        isEnable = false;
        Info.gameObject.SetActive(isEnable);
        Destroy(currentCrosshair);
    }

    #region Main Methods

    private void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemyTransform = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);

            if (distance < Data.FarLockDistance && distance < closestDistance && !enemy.CompareTag("Dead"))
            {
                closestEnemyTransform = enemy.transform;
                closestDistance = distance;
            }
        }

        if (closestEnemyTransform != null)
        {
            Enemy closestEnemy = closestEnemyTransform.GetComponent<Enemy>();

            lockedEnemy = closestEnemy;

            if (!currentCrosshair)
            {
                currentCrosshair = Instantiate(GameAssets.Instance.LockCrosshair, lockedEnemy.transform.position, Quaternion.identity, lockedEnemy.transform);
                currentCrosshair.transform.localPosition += new Vector3(0, Data.HeightOffset, 0);
                crosshairImage = currentCrosshair.GetComponentInChildren<Image>();

            }

            Info.Initialize(lockedEnemy.name, lockedEnemy.Stats.DefaultData.Level, lockedEnemy.Stats.RuntimeData.Health, lockedEnemy.Stats.DefaultData.MaxHealth);
            Info.gameObject.SetActive(true);
        }
        else
        {

            lockedEnemy = null;
            Destroy(currentCrosshair);
            Info.gameObject.SetActive(false);

        }
    }


    private void UpdateCrosshair()
    {
        if (lockedEnemy != null)
        {
            float distance = Vector3.Distance(player.position, lockedEnemy.transform.position);

            if (distance > Data.FarLockDistance)
            {

                lockedEnemy = null;
                Destroy(currentCrosshair);
                Info.gameObject.SetActive(false);
                
            }
            else
            {
                if (currentCrosshair != null)
                {
                    currentCrosshair.transform.position = lockedEnemy.transform.position; 

                    if (distance < Data.NearLockDistance)
                    {
                        crosshairImage.color = Color.Lerp(crosshairImage.color, Color.red, Time.deltaTime * Data.ColorChangeSmoothness);
                    }
                    else
                    {
                        crosshairImage.color = Color.Lerp(crosshairImage.color, Color.white, Time.deltaTime * Data.ColorChangeSmoothness);
                    }
                }
            }
        }
        else
        {

            Destroy(currentCrosshair);
            Info.gameObject.SetActive(false);

        }
    }

    private void UpdateInfo()
    {
        if (lockedEnemy) {
            Info.SetHealthText(lockedEnemy.Stats.RuntimeData.Health, lockedEnemy.Stats.DefaultData.MaxHealth);
        }

    }




    #endregion

}
