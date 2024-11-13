using UnityEngine;
using System.Collections.Generic;

public class VehicleManager : MonoBehaviour
{
    public static VehicleManager Instance { get; private set; }
    public List<GameObject> components = new List<GameObject>();
    private JointGenerator jointGenerator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        jointGenerator = GetComponent<JointGenerator>();
    }

   
    public void AddComponent(GameObject component)
    {
        components.Add(component);
    }

   
    public void SaveVehicleData()
    {
        foreach (var component in components)
        {
           
        }
    }

   
    public void GenerateAllJoints()
    {
        jointGenerator.components = components;
        jointGenerator.GenerateJoints();
    }
}
