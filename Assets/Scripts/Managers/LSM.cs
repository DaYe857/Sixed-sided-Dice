using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 加载场景管理器
/// </summary>
public class LSM : MonoBehaviour
{
    public static LSM instance;
    [SerializeField] private GameObject loadingPanel;//加载场景界面
    public string nextSceneName;//跳转场景名称

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void LoadNextScene(string sceneName)
    {
        Time.timeScale = 1f;
        nextSceneName = sceneName;
        loadingPanel.SetActive(true);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        nextSceneName = SceneManager.GetActiveScene().name;
        loadingPanel.SetActive(true);
    }

    public string[] GetAllSceneNames()
    {
        int count = SceneManager.sceneCountInBuildSettings;
        string[] scene_names = new string[count];
        for (int i = 0; i < count; i++)
        {
            scene_names[i] = SceneUtility.GetScenePathByBuildIndex(i);
            string[] strs = scene_names[i].Split('/');
            string str = strs[strs.Length - 1];
            strs = str.Split('.');
            str = strs[0];
            scene_names[i] = str;
        }
        return scene_names;
    }

    public int GetThemeSceneCount(string sceneName)
    {
        int cout = 0;
        string[] scene_names = GetAllSceneNames();
        foreach (var name in scene_names)
        {
            if(name.Contains(sceneName)){cout++;}
        }
        return cout;
    }
}