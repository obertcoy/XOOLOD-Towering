using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheatManager : MonoBehaviour
{
    private Player player;
    private List<string> keyStrokeHistory;

    public void Initialize(Player player)
    {
        this.player = player;

        keyStrokeHistory = new List<string>();
    }

    private void Update()
    {
        if(player != null)
        {
            AddKeyStrokeToHistory(DetectKeyPressed().ToString());

            DetectCheat();
        }
        
    }

    private void DetectCheat()
    {
        if (GetKeyStrokeHistory().Contains("D,E,P,D,E,P,D,E,P"))
        {
            keyStrokeHistory.Clear();

            CheatPopup.Create("DEPDEPDEP");

            player.Stats.CheatData.SpeedModifier = 2.5f;

        }

        if (GetKeyStrokeHistory().Contains("O,O,B,A,C,A,C,H,A,T"))
        {
            keyStrokeHistory.Clear();

            CheatPopup.Create("OOBACACHAT");

            player.GetDropLoot(231231231, 0);

        }

        if (GetKeyStrokeHistory().Contains("N,J,U,S"))
        {
            keyStrokeHistory.Clear();

            CheatPopup.Create("NJUS");

            player.GetDropLoot(0, 231231231);

        }

        if (GetKeyStrokeHistory().Contains("M,A,H,A,S,I,S,W,A,S,A,Y,A,Alpha1,Alpha5,M,E,N,I,T"))
        {
            keyStrokeHistory.Clear();

            CheatPopup.Create("MAHASISWASAYA15MENIT");

            player.Stats.CheatData.CooldownModifier = 0;

        }

        if (GetKeyStrokeHistory().Contains("L,O,R,D,P,I,B,U,S"))
        {
            keyStrokeHistory.Clear();

            CheatPopup.Create("LORDPIBUS");

            player.Stats.CheatData.TakeDamageModifier = 0;

        }

        if (GetKeyStrokeHistory().Contains("Alpha2,Alpha3,Minus,Alpha1"))
        {
            keyStrokeHistory.Clear();

            player.SpawnManager.SpawnAtMainScene(player);
        }
    }

    private KeyCode DetectKeyPressed()
    {
        foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key)) return key;
        }

        return KeyCode.None;
    }

    private void AddKeyStrokeToHistory(string keyStroke)
    {
        if (!keyStroke.Equals("None"))
        {
            keyStrokeHistory.Add(keyStroke);


            if (keyStrokeHistory.Count > 25) keyStrokeHistory.RemoveAt(0);
        }
    }

    private string GetKeyStrokeHistory()
    {
        return String.Join(",", keyStrokeHistory.ToArray());
    }

}
