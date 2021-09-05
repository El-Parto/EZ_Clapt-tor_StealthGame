using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEditor.Experimental.GraphView;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour
{
    private PlayerControl pCntrl;

    public bool lostGame;
    public AIBehaviour aIBehaviour;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI lostText;
    public TextMeshProUGUI detectionText;

    public Button restartButton;
    public Button QuitButt;

    [SerializeField]
    private GameObject buttonLayout;
    
    // Start is called before the first frame update
    void Start()
    {
        pCntrl = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pCntrl.wonGame)
        {
            pCntrl.game.SetActive(false);
            winText.gameObject.SetActive(true);
            buttonLayout.SetActive(true);
        }

        if(aIBehaviour.DetectionCaught())
        {
            pCntrl.game.SetActive(false);
            lostText.gameObject.SetActive(true);
            buttonLayout.SetActive(true);
        }
    }

    public void RestartGame() => SceneManager.LoadScene("GameScene");

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
