using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    #region Singleton
    public static TransitionManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //Holds the Image to fade over for transition
    [SerializeField] private Image TransitionImage;

    //Minumum length of a transition
    [SerializeField] private float TransitionDuration = 1.5f; //Default 1.5 

    private void Start()
    {
        if (TransitionImage == null)
        {
            TransitionImage = GetComponentInChildren<Image>();
        }
    }

    public void Transition(bool clockwise, string SceneName)
    {
        if (TransitionImage != null)
        {
            TransitionImage.fillClockwise = clockwise;

            StartCoroutine(TransitionAndLoad(clockwise, SceneName));
        }
    }

    private IEnumerator TransitionAndLoad(bool clockwise, string SceneName)
    {
        float t = 0.0f;
        while (t < TransitionDuration)
        {
            TransitionImage.fillAmount = t/TransitionDuration;
            Debug.Log("Transitionign");
            t += Time.deltaTime;
            yield return null;
        }

        /*
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        Debug.Log("Loading Scene");


        //Wait for scene to finish loading before Screen Wipe
        while (!asyncLoad.isDone)
        {
             Debug.Log("Waiting for Scene");
            yield return null;
        }
        Debug.Log("Fisnihed Scene");
        */

        TransitionImage.fillClockwise = !clockwise;
        t = TransitionDuration;

        var lof = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        lof.allowSceneActivation = false;
        while (t > 0.0f)
        {
            TransitionImage.fillAmount = t / TransitionDuration;
            Debug.Log("Unwiping");

            t -= Time.deltaTime;
            yield return null;
        }

        TransitionImage.fillAmount = 0.0f;
        lof.allowSceneActivation = true;

        yield return null;
    }
}
