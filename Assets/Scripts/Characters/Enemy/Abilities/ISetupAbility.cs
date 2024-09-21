using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetupAbility
{
    void ActivateAbility(Vector3 position = default, Quaternion rotation = default);
    void DeactivateAbility();

}
