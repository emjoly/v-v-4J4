using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Variables pour le texte et l'image du bouton
    private Image buttonImage;
    private TextMeshProUGUI buttonText;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.red;

    void Start()
    {
        // Récupérer les composants Image et TextMeshProUGUI
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        // Désactiver l'image du bouton
        buttonImage.enabled = false;
    }
    // Fonction pour changer la couleur du texte et activer l'image du bouton lorsqu'on passe la souris dessus
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Activer le composant Image lorsqu'on passe la souris dessus
        buttonImage.enabled = true;
        // Changer la couleur du texte lorsqu'on passe la souris dessus
        buttonText.color = hoverColor;
    }
    // Fonction pour désactiver l'image du bouton et réinitialiser la couleur du texte lorsqu'on ne passe plus la souris dessus
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.enabled = false;
        buttonText.color = normalColor;
    }

    // Désactiver l'image du bouton lorsqu'on désactive le script
    void OnDisable()
    {
        buttonImage.enabled = false;
        buttonText.color = normalColor;
    }
}