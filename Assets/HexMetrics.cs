﻿using UnityEngine;

public static class HexMetrics {
	public const float outerRadius = 10f;
	public const float innerRadius = outerRadius * 0.866025404f;

	//Define hex corners clockwise and oriented top bottom
	static Vector3[] corners = {
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		//todois this a hax ? stops overflow when final tri gets 
		// seventh corner
		new Vector3(0f, 0f, outerRadius)
	};

	public static Vector3 GetFirstCorner (HexDirection direction) {
		return corners[(int)direction];
	}

	public static Vector3 GetSecondCorner (HexDirection direction) {
		return corners[(int)direction + 1];
	}
}
