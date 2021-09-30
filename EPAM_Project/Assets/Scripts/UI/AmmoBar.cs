using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AmmoBar : UIBar
{
    private readonly Color reloadColor = Color.black;
    private readonly Color defaultColor = new Color32(219, 164, 99, 255);

    [SerializeField]
    private Weapon weapon;

    protected override void SetupBar()
    {
        base.SetupBar();
        maxValue = weapon.ClipSize;
        weapon.BULLET_COUNT_CHANGED += UpdateBar;
        weapon.RELOADING += ReloadIndication;
    }

    private void ReloadIndication(bool reloading)
    {
        image.color = reloading ? reloadColor : defaultColor;
    }
}
