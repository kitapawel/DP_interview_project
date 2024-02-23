using UnityEngine;

public class BloodSacrifice : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClick;
    private void OnMouseDown()
    {
        OnClick?.Invoke();
        Destroy(gameObject);
    }
}
