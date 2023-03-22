using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Line", menuName = "SO/Upgrade Line")]
public class UpgradeLineSO : ScriptableObject
{
	public int LineId;
	[HideInInspector] public int Index = 0;
	public List<int> ItemIds;
}
