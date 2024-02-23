using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkoPoloUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    [SerializeField]
    private RectTransform bloodPanelParent;
    [SerializeField]
    private RectTransform bloodPanel;
    [SerializeField]
    private Button solveButton;
    [SerializeField]
    private Image bloodPrefab;
    [SerializeField]
    private AudioClip bloodDropSound;
    [SerializeField]
    private AudioClip bloodDropFullSound;
    private AudioSource audioSource;
    private int bloodValue = 0;
    [SerializeField]
    private int sacrificeRequirement = 10;

    private void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned!");
            return;
        }
        bloodPanelParent.gameObject.SetActive(true);
        solveButton.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    private void MarkoPoloSolution()
    {
        textMeshPro.text = "";

        for (int i = 1; i <= 100; i++)
        {
            string output = "";

            if (i % 3 == 0) output += "Marco";
            if (i % 5 == 0) output += "Polo";

            if (output == "") output = i.ToString();

            textMeshPro.text += output + "\n";
        }
    }
    public void SolveMarkoPolo()
    {
        MarkoPoloSolution();
    }
    private void OnEnable()
    {
        BloodSacrifice.OnClick += HandleClickEvent;
    }
    private void OnDisable()
    {
        BloodSacrifice.OnClick -= HandleClickEvent;
    }
    private void HandleClickEvent()
    {
        bloodValue++;
        if (bloodValue > 10) 
        {
            audioSource.PlayOneShot(bloodDropSound);
            return;
        }
        if (bloodValue == 10)
        {
            Instantiate(bloodPrefab, bloodPanel);
            audioSource.PlayOneShot(bloodDropFullSound);
            bloodPanelParent.gameObject.SetActive(false);
            solveButton.gameObject.SetActive(true);
        }
        else
        {
            audioSource.PlayOneShot(bloodDropSound);
            Instantiate(bloodPrefab, bloodPanel);            
        }
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
