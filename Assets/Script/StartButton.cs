using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button button;
    public GameObject uiElements;
    public FollowMouse followMouse;
    void Start()
    {
        Time.timeScale = 0f;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        Time.timeScale = 1f;
        uiElements.SetActive(false);
        followMouse.redUnit = null;
        followMouse.blueUnit = null;
        // 查找所有包含 InfantryUnit 脚本的单位，并调用 ManualRefreshTarget
        InfantryUnit[] infantryUnits = FindObjectsOfType<InfantryUnit>();
        foreach (InfantryUnit unit in infantryUnits)
        {
            unit.ManualRefreshTarget();
        }

        // 查找所有包含 TankUnit 脚本的单位，并调用 ManualRefreshTarget
        TankUnit[] tankUnits = FindObjectsOfType<TankUnit>();
        foreach (TankUnit unit in tankUnits)
        {
            unit.ManualRefreshTarget();
        }
    }

}
