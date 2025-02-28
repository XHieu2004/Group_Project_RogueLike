using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndDetector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "Level1";

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer not found on this GameObject!");
            return;
        }
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp){
        LoadNextLevel(2);
    }
     void OnDestroy(){
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
    public void LoadNextLevel(int levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

}