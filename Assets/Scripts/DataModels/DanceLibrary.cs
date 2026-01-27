using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DanceEntry
{
    public string danceName;
    public AnimationClip clip;
}

[CreateAssetMenu (fileName = "DanceLibrary", menuName = "ScriptableObject/DanceLibrary")]
public class DanceLibrary : ScriptableObject
{
    public List<DanceEntry> dances;
}
