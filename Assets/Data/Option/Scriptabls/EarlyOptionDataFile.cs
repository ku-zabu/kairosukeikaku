using UnityEngine;

/// VolumeOption
public struct VolumeOption
{
    public float MVolume;
    public float BGMVolume;
    public float SEVolume;
}

// CameraOption
public struct CameraOption
{
    public float SpeedY;
    public float SpeedX;
    public bool MoveY;
    public bool MoveX;
}

// AllOption
public struct AllOption
{
    public VolumeOption volumeOption;
    public CameraOption cameraOption;
}

[CreateAssetMenu(fileName = "EarlyOptionDataFile", menuName = "Scriptable Objects/EarlyOptionDataFile")]

public class EarlyOptionDataFile : ScriptableObject
{
    [Header("VolumeOption")]
    [SerializeField] private float MVolume;
    [SerializeField] private float BGMVolume;
    [SerializeField] private float SEVolume;

    [Header("CameraOption")]
    [SerializeField] private float SpeedY;
    [SerializeField] private float SpeedX;
    [SerializeField] private bool MoveY;
    [SerializeField] private bool MoveX;

    public AllOption GetAllOption()
    {
        VolumeOption volumeOption = new VolumeOption
        {
            MVolume = MVolume,
            BGMVolume = BGMVolume,
            SEVolume = SEVolume
        };
        CameraOption cameraOption = new CameraOption
        {
            SpeedY = SpeedY,
            SpeedX = SpeedX,
            MoveY = MoveY,
            MoveX = MoveX
        };
        AllOption allOption = new AllOption
        {
            volumeOption = volumeOption,
            cameraOption = cameraOption
        };
        return allOption;
    }
}

/// <summary>
/// OptionSeter
/// </summary>
public interface OptionTemp
{
    /// <summary>
    /// OptionSeter
    /// </summary>
    /// <param name="optionDataSet"></param>
    void SetData(AllOption all);

    /// <summary>
    /// OptionGeter
    /// </summary>
    /// <param name="all"></param>
    AllOption GetData();
}