using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VinciRoom.UI
{
    public class LoadingScenePanel : MonoBehaviour
    {
        private AsyncOperation operation;
        private int strProgressValue = 0, endProgressValue = 100;

        private void Start()
        {
            StartCoroutine(AsyncLoading(transform.parent.GetComponent<LSM>().nextSceneName));
        }

        IEnumerator AsyncLoading(string sceneName)
        {
            operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            yield return operation;
        }

        void Update()
        {
            System.GC.Collect();

            if (strProgressValue < endProgressValue)
            {
                strProgressValue++;
            }

            if (strProgressValue == 100)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}