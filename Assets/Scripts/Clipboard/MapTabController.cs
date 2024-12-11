using UnityEngine;

public class MapTabController : MonoBehaviour
{
    [SerializeField]
    private RectTransform player;

    private float pixelsPerUnit => .004f / (0.001f * 0.04f);

	private Vector2 originOffset;

	private void Start()
	{
		originOffset = player.anchoredPosition;
	}

	private void Update()
	{
		Transform playerTransform = Camera.main.transform;

		// Set player position
		Vector3 playerPosition = playerTransform.position;
		Vector2 playerPosition2D = new Vector2(playerPosition.x, playerPosition.z);
		Vector2 playerOffset = playerPosition2D * pixelsPerUnit;
		player.anchoredPosition = originOffset + playerOffset;

		// Set player rotation
		player.localEulerAngles = new Vector3(0, 0, -playerTransform.eulerAngles.y);
	}
}
