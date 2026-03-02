using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddressableCacheLoader : MonoBehaviour
{
    private AsyncOperationHandle handle;

    public TextMeshProUGUI mainText;
    public TextMeshProUGUI percentText;
    public LobbyBootStrapper strapper;


    public void Start()
    {
        Caching.ClearCache();
        StartCoroutine(StartFunc());
    }

    IEnumerator StartFunc()
    {
        AsyncOperationHandle<long> downloadSize = Addressables.GetDownloadSizeAsync("Test");
        yield return downloadSize;

        if (downloadSize.Result > 0)
        {
            DownBtn();
        }
        else
        {
            NextShow();
        }
        Addressables.Release(downloadSize);
    }

    public void DownBtn()
    {
        StartCoroutine(DownFunc());
    }

    IEnumerator DownFunc()
    {
        handle = Addressables.DownloadDependenciesAsync("Test");

        StartCoroutine(Show()); // ÁøÇà·ü UI
        yield return handle;

        NextShow();
        Addressables.Release(handle);
    }
    IEnumerator Show()
    {
        yield return new WaitUntil(() => handle.IsValid());
        while (handle.PercentComplete < 1)
        {
            percentText.text = $"{handle.PercentComplete * 100:F2}%";
            yield return null;
        }
        percentText.text = "100%";
    }
    public void NextShow()
    {
        StartCoroutine(StartLoading());
    }
    IEnumerator StartLoading()
    {
        mainText.GetComponent<LoadingDot>().baseText = "Loding";
        yield return new WaitForSeconds(1f);
        strapper.Init();
        percentText.gameObject.SetActive(false);
    }
    IEnumerator GoNextMap()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("Game");
        yield return ao;
    }
}