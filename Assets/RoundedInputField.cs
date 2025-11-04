using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RoundedInputField : MonoBehaviour
{
    [Range(0, 50)]
    public float cornerRadius = 20f;

    private Image backgroundImage;
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        SetupRoundedCorners();
    }
    void SetupRoundedCorners()
    {
        // Убедитесь что используется спрайт поддерживающий 9-slicing
        if (backgroundImage.sprite != null)
        {
            backgroundImage.type = Image.Type.Sliced;
            backgroundImage.pixelsPerUnitMultiplier = 100f;
        }
    }
    // Update is called once per frame
    void OnValidate()
    {
        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();

        SetupRoundedCorners();
    }
}
