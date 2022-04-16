using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    private int levelIndex;
    // Start is called before the first frame update
    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OnClick(){
        LevelManager.Instance.playerHp = GameObject.Find("Player").GetComponent<Unit>().hp;
        LevelManager.Instance.switchIndex = 10;
        SceneManager.LoadScene(levelIndex + 1);
    }
}
