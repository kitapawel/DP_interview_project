using TMPro;
using UnityEngine;
public class SpriteSelector : MonoBehaviour
{
    private static SpriteSelector _instance;
    public static SpriteSelector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpriteSelector>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("SpriteSelector");
                    _instance = singletonObject.AddComponent<SpriteSelector>();
                }
            }
            return _instance;
        }
    }
    private Unit selectedUnit = null;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI HPtext;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        if (selectedUnit == null) { return; }
        if (Input.GetMouseButtonDown(1)) 
        {
            DeselectPreviousUnit();
        }
    }
    public void SelectUnit(Unit u)
    {
        DeselectPreviousUnit();

        selectedUnit = u;
        u.Select(true);
        u.OnHit += UpdateText;
        UpdateText();
    }
    private void DeselectPreviousUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.Select(false);
            selectedUnit.OnHit -= UpdateText;
            selectedUnit = null;
            nameText.text = "";
            HPtext.text = "";
 
        }
    }
    private void UpdateText()
    {
        nameText.text = selectedUnit.GetName();
        HPtext.text = selectedUnit.GetHP();
    }
}