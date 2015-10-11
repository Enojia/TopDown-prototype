using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    BoardSetUp boardScript;
	// Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardSetUp>();
        initGame();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void initGame()
    {
        boardScript.SetUpScene();
    }
}
