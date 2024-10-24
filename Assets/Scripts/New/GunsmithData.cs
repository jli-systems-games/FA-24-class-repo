using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsmithData : MonoBehaviour
{
    public Color color;
    public ScopeType scopeType = ScopeType.IronSights;
    public BarrelType barrelType = BarrelType.ShortM;
    public GripType gripType = GripType.None;
    public MuzzleType muzzleType = MuzzleType.None;
    public MegType megType = MegType.Standard;
    public StockType stockType = StockType.Standard;
    public HandguardType handguardType = HandguardType.Short;
    public TriggerType triggerType = TriggerType.None;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public enum ScopeType
    {
        IronSights,
        Holographic,
        MidRange,
        LongRange,
    }
    public enum BarrelType
    {
        ShortS,
        ShortM,
        ShortL,
        LongS,
        LongM,
        LongL,
    }
    public enum GripType
    {
        TGrip,
        ThumbGrip,
        AngledGrip,
        LightGrip,
        Grenade,
        None,
    }
    public enum MuzzleType
    {
        Silencer,
        Compensator,
        None,
    }
    public enum MegType
    {
        VeryLight,
        Light,
        Standard,
        StandardEx,
        Drum,
        DoubleDrum,
    }
    public enum StockType
    {
        Standard,
        Heavy,
        Light,
        Sniper,
        None,
    }
    public enum HandguardType
    {
        Long,
        Short,
        None,
    }
    public enum TriggerType
    {
        Sports,
        Upgrade,
        Illegal,
        None,
    }
}
