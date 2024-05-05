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
    [SerializeField] private Image BattleTransitionImage;
    [SerializeField] private Image SceneTransitionImage;

    //Minumum length of a transition
    [SerializeField] private float TransitionDuration = 1.5f; //Default 1.5 

    private void Start()
    {
        if (BattleTransitionImage == null)
        {
            BattleTransitionImage = GetComponentInChildren<Image>();
        }

        if (SceneTransitionImage != null)
        {
            SceneTransitionImage = GetComponentInChildren<Image>();
            Color c = SceneTransitionImage.color;
            c.a = 0.0f;
            SceneTransitionImage.color = c;
        }
    }

    public void BattleTransition(bool clockwise, string name, Sprite enenySprite)
    {
        if (BattleTransitionImage != null)
        {
            BattleTransitionImage.fillClockwise = clockwise;

            StartCoroutine(TransitionAndLoadScreen(clockwise, name, enenySprite));
        }
    }

    public void MoveToScene(string sceneName)
    {
        StartCoroutine(SceneTransition(1.5f, sceneName));
    }

    #region Battle Transitions
    private IEnumerator TransitionAndLoadScreen(bool clockwise, string canvasName, Sprite enemySprite)
    {
        float t = 0.0f;
        while (t < TransitionDuration)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;
            t += Time.deltaTime;
            yield return null;
        }

        BattleTransitionImage.fillClockwise = !clockwise;
        t = TransitionDuration;

        GameObject canvas = GameObject.Find(canvasName);
        if (canvas != null)
        {
            canvas.GetComponent<ScreenActivate>().ToggleBattleScreen(true);
            Enemy n = new Enemy();
            canvas.GetComponentInChildren<BattleDataPasser>().PassEnemyData(n, enemySprite);
        }

        while (t > 0.0f)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;

            t -= Time.deltaTime;
            yield return null;
        }

        BattleTransitionImage.fillAmount = 0.0f;

        yield return null;
    }
    /*
    private IEnumerator TransitionAndLoadScene(bool clockwise, string SceneName)
    {
        float t = 0.0f;
        while (t < TransitionDuration)
        {
            BattleTransitionImage.fillAmount = t/TransitionDuration;
            Debug.Log("Transitionign");
            t += Time.deltaTime;
            yield return null;
        }

        ///
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
        ///

        BattleTransitionImage.fillClockwise = !clockwise;
        t = TransitionDuration;

        var lof = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        lof.allowSceneActivation = false;
        while (t > 0.0f)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;
            Debug.Log("Unwiping");

            t -= Time.deltaTime;
            yield return null;
        }

        BattleTransitionImage.fillAmount = 0.0f;
        lof.allowSceneActivation = true;

        yield return null;
    }
    */
    #endregion

    #region Scene Transitions

    private IEnumerator SceneTransition(float transitionTime, string sceneName)
    {
        Color c = SceneTransitionImage.color;
        c.a = 0.0f;
        SceneTransitionImage.color = c;
        
        float t = 0.0f;

        while (t < transitionTime)
        {
            c.a = Mathf.Lerp(c.a, 255, t);
            SceneTransitionImage.color = c;

            t += Time.deltaTime;
            yield return null;
        }

        AsyncOperation s = SceneManager.LoadSceneAsync(sceneName);

        while (!s.isDone)
        {
            yield return null;
        }

        

        t = 0.0f;
        while (t < transitionTime)
        {
            c.a = Mathf.Lerp(c.a, 0, t);
            SceneTransitionImage.color = c;

            t += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

    #endregion
}
