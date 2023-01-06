using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Group
{
    [SerializeField] private string _name;
    [SerializeField] [Range(0f, 1f)] private float _volume = 1f;
    [SerializeField] private List<Group> Members;

    private float predecessorMult;
    private float volumeMult;

    public string name { get => _name; set => _name = value; }
    public float volume { get => _volume; set { _volume = value; UpdateVolume(predecessorMult); } }

    public Group(string name, float volume)
    {
        _name = name;
        _volume = volume;
    }

    protected void UpdateVolume(float mult)
    {
        predecessorMult = mult;
        volumeMult = predecessorMult * _volume;

        foreach (var group in Members)
        {
            group.UpdateVolume(volumeMult);
        }
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public void GetGroupName(List<string> list)
    {
        list.Add(_name);
        foreach (var group in Members)
        {
            group.GetGroupName(list);
        }
    }

    public void BuildGroupDictionary(Dictionary<string, Group> groups)
    {
        groups.Add(_name, this);
        foreach (var group in Members)
        {
            group.BuildGroupDictionary(groups);
        }
    }
}
