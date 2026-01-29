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

    public int GetIndexByClip(AnimationClip clip)
    {
        if (clip == null) return 0;

        for (int i = 0; i < dances.Count; i++)
        {
            if (dances[i].clip == clip)
            {
                return i;
            }
        }
        return 0;
    }
}
