using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitButtons : MonoBehaviour
{
    public string unitName;
    public TextMeshProUGUI textMeshProUGUI;
    private Button button;
    public FollowMouse followMouse;
    public GameObject blueUnit;
    public GameObject redUnit;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        textMeshProUGUI.text = unitName;
        followMouse.redUnit = redUnit;
        followMouse.blueUnit = blueUnit;
    }
}
