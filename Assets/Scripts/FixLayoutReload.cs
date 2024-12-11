using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutGroup))]
public class FixLayoutReload : MonoBehaviour
{
	private LayoutGroup layoutGroup;

	private void Start()
	{
		layoutGroup = GetComponent<LayoutGroup>();
		
	}
}
