using UnityEngine;

public class ArrowSelect : MonoBehaviour
{
    public void RevealArrow()
    {
        gameObject.SetActive(true);
    }

    public void HideArrow()
    {
        gameObject.SetActive(false);
    }
}
