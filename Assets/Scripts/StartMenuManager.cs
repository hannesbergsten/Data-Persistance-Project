using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public TMP_InputField inputName;
    public Button saveButton;
    
    public static StartMenuManager Instance;
    public string Name;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        saveButton.onClick.AddListener(SavePlayerName);
    }

    void SavePlayerName()
    {
        if (Instance != null)
        {
            Instance.Name = inputName.text;
            inputName.text = string.Empty;
            SceneManager.LoadScene((int)SceneName.Main);
        }
    }
}
