using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FindWayOp : ScriptableObject
{
	public virtual bool StartFindWay(Map map, out WayNode path)
	{
		path = null;
		return false;
	}

	public virtual WayNode this[int2 pos] => null;
}