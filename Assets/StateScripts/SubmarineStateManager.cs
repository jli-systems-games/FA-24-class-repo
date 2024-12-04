using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineStateManager : MonoBehaviour
{
    SubmarineBaseState currentState;
    public NormalState normalState = new NormalState();
    public DepthControlState depthControlState = new DepthControlState();
    public PressureCalibrationState pressureCalibrationState = new PressureCalibrationState();
    public SilentRunningState silentRunningState = new SilentRunningState();
    public LeakDetectionState leakDetectionState = new LeakDetectionState();
    public NavigationHazardState navigationHazardState = new NavigationHazardState();
    public MissileLaunchState  misileLaunchState = new MissileLaunchState();
    public EngineRestartState engineRestartState = new EngineRestartState();
    public WaterChangesState waterChangesState = new WaterChangesState();

    public TextMeshProUGUI IssueText;

    //central control panel
    public Slider WaterSalinity;
    public Slider WaterTemperature;
    public Slider PressureSensor;
    public Slider WaterDensity;
    public Slider ElectricitySwitchPrefab;

    //driving panel
    public Slider FuelSensor;
    public Slider PropulsionShaft;
    public Slider Thrusters;
    public Slider DepthGauge;
    public Toggle Anchor;
    public Toggle Sonar;
    public GameObject PeriscopeControls;
    public GameObject SteeringWheel;
    public GameObject PeriscopeView;
    public GameObject SonarArray;

    //ballast panel
    public Slider BuoyantSensor;
    public Slider TrimTank;
    public Slider BallastTank;
    public Toggle VentPrefab;

    //comms
    public Toggle AntennaStatus;
    public GameObject RadioDial;

    //emergency
    public Toggle BlowSystemPrefab;

    //weapons
    public Button TorpedoFwdPrefab;
    public Button TorpedoVLSPrefab;
    public Button LoadMissilePrefab;
    public Button LaunchMissilePrefab;


    

    //ballast panel
    public Transform TrimBallastSensorContainer;
    public Transform VentGridContainer;

    //weapons panel
    public Transform LoadMissileGridContainer;
    public Transform LaunchMissileGridContainer;
    public Transform FwdTubesGridContainer;
    public Transform VLSGridContainer;

    //central control panel
    public Transform ElectricityBoxContainer;
    public Transform WaterSensorGridContainer;

    //emergency panel
    public Transform BlowGridContainer;

    //radio container
    public Transform RadioCommsContainer;

    //driving panel
    public Transform ThrustersAndShaftContainer;
    public Transform FuelSensorContainer;




    // Start is called before the first frame update
    // create 8 states that will display submarine issues one at a time, player must complete an issue to recieve the next

    void Start()
    {
        currentState = normalState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(SubmarineBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    //state 1: depth control
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 2: pressure calibration
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 3: silent running
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 4: leak detection
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 5: navigation hazzard
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 6: missile launch
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 7: engine restart
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

    //state 8: water changes
    //player checks sensors, player relays sensor info to tech, tech relays into to player, player executes with controls

}
