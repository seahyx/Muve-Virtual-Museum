using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RainbowColour : MonoBehaviour
{
	private enum ColourBlendMode
	{
		Normal,
		Multiply,
		Addition,
		Subtraction
	}

	[SerializeField,
			Tooltip("Colour blending mode between the original colour and the visual effect colour.")]
	private ColourBlendMode colourBlendMode = ColourBlendMode.Normal;

	[SerializeField, Tooltip("(optional) If a renderer is present, this will be used to change the colour.")]
	private Renderer targetRenderer;

	[SerializeField, Tooltip("(optional) If an image is present, this will be used to change the colour.")]
	private Image targetImage;

	[SerializeField, Tooltip("The speed of colour cycling in terms of the full hue cycle per second.")]
	private float fullCycleDuration = 1.0f;

	private Color originalColor = Color.white;

	void Start()
    {
		if (targetRenderer == null && targetImage == null)
		{
			targetRenderer = GetComponent<Renderer>();
			targetImage = GetComponent<Image>();
		}

		if (targetRenderer != null)
		{
			originalColor = targetRenderer.material.color;
		}
		else if (targetImage != null)
		{
			originalColor = targetImage.color;
		}
	}

    void Update()
    {
		SetColour(BlendColours(originalColor, Color.HSVToRGB(Mathf.Repeat(Time.time / fullCycleDuration, 1.0f), 1.0f, 1.0f), colourBlendMode));
	}

	private void OnDisable()
	{
		SetColour(originalColor);
	}

	protected void SetColour(Color color)
	{
		if (targetRenderer != null)
		{
			targetRenderer.material.color = color;
		}
		else if (targetImage != null)
		{
			targetImage.color = color;
		}
	}

	private static Color BlendColours(Color a, Color b, ColourBlendMode blendMode)
	{
		switch (blendMode)
		{
			case ColourBlendMode.Normal:
				return b;

			case ColourBlendMode.Multiply:
				return a * b;

			case ColourBlendMode.Addition:
				return a + b;

			case ColourBlendMode.Subtraction:
				return a - b;

			default: return b;
		}
	}
}
