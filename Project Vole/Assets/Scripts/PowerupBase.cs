using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBase : MonoBehaviour {
    public virtual void OnStart(Powerup p) {
        PowerupManager.instance.currentPowerup = p;
    }

    public void OnFinishBase() {
        PowerupManager.instance.currentPowerup = Powerup.NONE;
    }
}
