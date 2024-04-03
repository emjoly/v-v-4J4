using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private TextMeshProUGUI buttonText;

    // You can adjust these colors according to your preference
    public Color normalColor = Color.white;
    public Color hoverColor = Color.red;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonImage.enabled = false; // Disable the Image component at the beginning
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.enabled = true; // Enable the Image component on hover
        buttonText.color = hoverColor; // Change text color on hover
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.enabled = false; // Disable the Image component when not hovering
        buttonText.color = normalColor; // Revert text color when not hovering
    }

    // Reset button appearance when the GameObject is disabled
    void OnDisable()
    {
        buttonImage.enabled = false;
        buttonText.color = normalColor;
    }
}