using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 해당 프로젝트는 초창기 제작된 프로젝트로, 코드에서 미숙한 부분이 보일 수 있습니다. **/

public class Level_Index : MonoBehaviour
{

    public GameObject[] level1_lock;
    public GameObject[] level2_lock;
    public GameObject[] level3_lock;


    private void Update()
    {
        #region stage1

        if (PlayerPrefs.GetInt("1-1") == 1)
        {
            level1_lock[0].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("1-2") == 1)
        {
            level1_lock[1].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("1-3") == 1)
        {
            level1_lock[2].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("1-4") == 1)
        {
            level1_lock[3].gameObject.SetActive(false);
        }
        #endregion

        #region stage2
        if (PlayerPrefs.GetInt("1-5") == 1)
        {
            level2_lock[0].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("2-1") == 1)
        {
            level2_lock[1].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("2-2") == 1)
        {
            level2_lock[2].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("2-3") == 1)
        {
            level2_lock[3].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("2-4") == 1)
        {
            level2_lock[4].gameObject.SetActive(false);
        }
        #endregion

        #region stage3
        if (PlayerPrefs.GetInt("2-5") == 1)
        {
            level3_lock[0].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("3-1") == 1)
        {
            level3_lock[1].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("3-2") == 1)
        {
            level3_lock[2].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("3-3") == 1)
        {
            level3_lock[3].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("3-4") == 1)
        {
            level3_lock[4].gameObject.SetActive(false);
        }
        #endregion
    }


    #region stage1-function
    public void Level1_1()
    {
        SceneManager.LoadScene("Level1_1");
    }

    public void Level1_2()
    {
        if(PlayerPrefs.GetInt("1-1") == 1)
        {
            SceneManager.LoadScene("Level1_2");
        }        
    }

    public void Level1_3()
    {
        if (PlayerPrefs.GetInt("1-2") == 1)
        {
            SceneManager.LoadScene("Level1_3");
        }
    }

    public void Level1_4()
    {
        if (PlayerPrefs.GetInt("1-3") == 1)
        {
            SceneManager.LoadScene("Level1_4");
        }
    }

    public void Level1_5()
    {
        if (PlayerPrefs.GetInt("1-4") == 1)
        {
            SceneManager.LoadScene("Level1_5");
        }
    }
    #endregion

    #region stage2-function
    public void Level2_1()
    {
        if (PlayerPrefs.GetInt("1-5") == 1)
        {
            SceneManager.LoadScene("Level2_1");
        }
    }

    public void Level2_2()
    {
        if (PlayerPrefs.GetInt("2-1") == 1)
        {
            SceneManager.LoadScene("Level2_2");
        }
    }

    public void Level2_3()
    {
        if (PlayerPrefs.GetInt("2-2") == 1)
        {
            SceneManager.LoadScene("Level2_3");
        }
    }

    public void Level2_4()
    {
        if (PlayerPrefs.GetInt("2-3") == 1)
        {
            SceneManager.LoadScene("Level2_4");
        }
    }

    public void Level2_5()
    {
        if (PlayerPrefs.GetInt("2-4") == 1)
        {
            SceneManager.LoadScene("Level2_5");
        }
    }
    #endregion

    #region stage3-function
    public void Level3_1()
    {
        if (PlayerPrefs.GetInt("2-5") == 1)
        {
            SceneManager.LoadScene("Level3_1");
        }
    }

    public void Level3_2()
    {
        if (PlayerPrefs.GetInt("3-1") == 1)
        {
            SceneManager.LoadScene("Level3_2");
        }
    }

    public void Level3_3()
    {
        if (PlayerPrefs.GetInt("3-2") == 1)
        {
            SceneManager.LoadScene("Level3_3");
        }
    }

    public void Level3_4()
    {
        if (PlayerPrefs.GetInt("3-3") == 1)
        {
            SceneManager.LoadScene("Level3_4");
        }
    }

    public void Level3_5()
    {
        if (PlayerPrefs.GetInt("3-4") == 1)
        {
            SceneManager.LoadScene("Level3_5");
        }
    }
    #endregion

}
