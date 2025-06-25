using UnityEngine;
using UnityEngine.UI;

public class HideParentImage : MonoBehaviour
{
    public void HideImage()
    {
        Image parentImage = GetComponentInParent<Image>();
        if (parentImage != null)
        {
            parentImage.enabled = false;
        }
    }
}