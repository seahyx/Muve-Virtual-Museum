using UnityEngine;
using UnityEngine.UI;

public class FixLayoutReload : MonoBehaviour
{
	private void OnEnable()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
	}
}
