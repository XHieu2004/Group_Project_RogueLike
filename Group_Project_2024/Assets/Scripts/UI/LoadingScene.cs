using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public void LoadScene(int sceneID){
        StartCoroutine(LoadScenAsync(sceneID));
    }
    IEnumerator LoadScenAsync(int scneID){
        AsyncOperation operation = SceneManager. LoadSceneAsync(scneID);
        LoadingScreen.SetActive(true);
        while (!operation.isDone){
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
