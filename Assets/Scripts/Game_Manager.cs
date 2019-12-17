using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameEvent StartGame;
    public GameObject RestartGameObj;

    private void Awake()
    {
        RestartGameObj.SetActive(false);
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
        RestartGameObj.SetActive(true);
    }

    public void EnableGameStart()
    {
        RestartGameObj.SetActive(false);
    }
}
