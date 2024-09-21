using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class PlayerAbilityUI : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> AbilityGameObjects { get; set; }

    public event Action<PlayerAbilityGameObject> AbilityClicked;

    public PlayerAbilityGameObject SelectedAbilityToChange { get; private set; }

    public void Initialize()
    {
        foreach (GameObject abilityGameObject in AbilityGameObjects)
        {
            PlayerAbilityGameObject abilityObject = abilityGameObject.GetComponent<PlayerAbilityGameObject>();

            EventTrigger eventTrigger = abilityGameObject.GetComponent<EventTrigger>();

            if (eventTrigger == null)
            {
                eventTrigger = abilityGameObject.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnAbilityClick(abilityObject); });

            eventTrigger.triggers.Add(entry);
        }

    }
    
    public void UpdateAbilityCooldownUI(PlayerAbilitiesNameEnum name)
    {
        GetAbilityGameObjectByAbility(name).ActivateAbility();
    }

    public void UpdateAbilityUI(PlayerActiveAbilitiesControlEnum control, PlayerActiveAbilitySO activeAbility, string key)
    {

        GetAbilityGameObjectByControl(control).Initialize(control, activeAbility, key);

    }

    public void UpdateAbilityKey(PlayerActiveAbilitiesControlEnum control, string key)
    {
        GetAbilityGameObjectByControl(control).UpdateAbilityKey(key);
    }

    public List<GameObject> GetAbilityGameObjects()
    {
        return AbilityGameObjects;
    }

    #region Main Methods

    public PlayerAbilityGameObject GetAbilityGameObjectByAbility(PlayerAbilitiesNameEnum name)
    {
        foreach (GameObject gameObject in AbilityGameObjects)
        {
            if (gameObject.TryGetComponent(out PlayerAbilityGameObject obj))
            {
                if (obj.Data.Name.Equals(name))
                {
                    return obj;
                }
            }
        }

        return null;
    }

    public PlayerAbilityGameObject GetAbilityGameObjectByControl(PlayerActiveAbilitiesControlEnum control)
    {
        foreach (GameObject gameObject in AbilityGameObjects)
        {
            if (gameObject.TryGetComponent(out PlayerAbilityGameObject obj))
            {
                if (obj.ControlEnum.Equals(control))
                {
                    return obj;
                }
            }
        }

        return null;
    }

    private void OnAbilityClick(PlayerAbilityGameObject abilityObject)
    {
        SelectedAbilityToChange = abilityObject;

        AbilityClicked?.Invoke(abilityObject);

    }


    #endregion

}
