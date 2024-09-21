using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCanvas : GameAssetsObject
{

    public static WinCanvas Create(Player player)
    {
        GameObject gameObject = Instantiate(GameAssets.Instance.WinCanvas);

        WinCanvas winCanvas = gameObject.GetComponent<WinCanvas>();

        winCanvas.StartCoroutine(winCanvas.DisableCanvasCoroutine(player));

        return winCanvas;
    }

    protected override void Start()
    {
       
    }


    private IEnumerator DisableCanvasCoroutine(Player player)
    {
        yield return new WaitForSeconds(DestroyTime);

        enabled = false;

        player.SpawnManager.SpawnAtMainScene(player);
    }
 

}
