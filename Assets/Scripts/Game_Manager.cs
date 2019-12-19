using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameEvent StartGame;
    public GameObject GameOverInputs;

    public List<VarColor> bulletColors;

    public static Game_Manager instance;

    private void Awake()
    {
        GameOverInputs.SetActive(false);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void EnableGameOver()
    {
        GameOverInputs.SetActive(true);
    }

    public void EnableGameStart()
    {
        GameOverInputs.SetActive(false);
    }

    public Color GetBulletColor(int index)
    {
        if (index >= 0 && index < bulletColors.Count) return bulletColors[index].value;
        else
        {
            Debug.Log("Index out of range of bullet colors :: Game_Manager ERROR");
            return Color.red;
        }
    }
}
