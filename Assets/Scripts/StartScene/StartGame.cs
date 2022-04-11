using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void MoveToGamePlayScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
