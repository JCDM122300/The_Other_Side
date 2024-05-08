using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

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

    //This canvas
    private Canvas CanvasComponent;
    private int BaseSortingLayer;

    //Minumum length of a transition
    [SerializeField] private float TransitionDuration = 1.5f; //Default 1.5 

    public static event EventHandler OnEnableScreen;
    public static event EventHandler OnDisableScreen;
    public static event EventHandler OnWipeFinished;

    private void Start()
    {
        CanvasComponent = GetComponent<Canvas>();
        BaseSortingLayer = CanvasComponent.sortingOrder;

        if (BattleTransitionImage == null)
        {
            BattleTransitionImage = GetComponentInChildren<Image>();
        }

        if (SceneTransitionImage != null)
        {
            Color c = SceneTransitionImage.color;
            c.a = 0.0f;
            SceneTransitionImage.color = c;
        }

        BattleManager.OnBattleFlee += FleeBattleTransition;
    }


    private void FleeBattleTransition(object sender, EventArgs e)
    {
        LeaveBattleTransition(true);
    }

    public void EnterBattleTransition(bool clockwise)
    {
        if (BattleTransitionImage != null)
        {
            BattleTransitionImage.fillClockwise = clockwise;

            StartCoroutine(EnterBattleScreen(clockwise));
        }
    }
    
    public void LeaveBattleTransition(bool clockwise)
    {
        if (BattleTransitionImage != null)
        {
            BattleTransitionImage.fillClockwise = clockwise;

            StartCoroutine(ExitBattleScreen(clockwise));
        }
    }

    public void MoveToScene(string sceneName)
    {
        StartCoroutine(SceneTransition(TransitionDuration, sceneName));
    }

    #region Battle Transitions
    private IEnumerator EnterBattleScreen(bool clockwise)
    {
        CanvasComponent.sortingOrder = 20;

        Color bc = BattleTransitionImage.color;
        bc.a = 255;
        BattleTransitionImage.color = bc;

        float t = 0.0f;
        while (t < TransitionDuration)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        BattleTransitionImage.fillClockwise = !clockwise;
        t = TransitionDuration;

        OnEnableScreen?.Invoke(this, EventArgs.Empty);

        /*
        GameObject canvas = GameObject.Find(canvasName);
        if (canvas != null)
        {
            canvas.GetComponent<ScreenActivate>().ToggleBattleScreen(true);
        }
        */

        while (t > 0.0f)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;

            t -= Time.unscaledDeltaTime;
            yield return null;
        }

        BattleTransitionImage.fillAmount = 0.0f;

        CanvasComponent.sortingOrder = BaseSortingLayer;

        yield return null;
    }

    private IEnumerator ExitBattleScreen(bool clockwise)
    {
        CanvasComponent.sortingOrder = 20;

        Color bc = BattleTransitionImage.color;
        bc.a = 255;
        BattleTransitionImage.color = bc;

        float t = 0.0f;
        while (t < TransitionDuration)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        BattleTransitionImage.fillClockwise = !clockwise;
        t = TransitionDuration;

        OnDisableScreen?.Invoke(this, EventArgs.Empty);

        /*
        GameObject canvas = GameObject.Find(canvasName);
        if (canvas != null)
        {
            canvas.GetComponent<ScreenActivate>().ToggleBattleScreen(true);
        }
        */

        while (t > 0.0f)
        {
            BattleTransitionImage.fillAmount = t / TransitionDuration;

            t -= Time.unscaledDeltaTime;
            yield return null;
        }

        BattleTransitionImage.fillAmount = 0.0f;

        CanvasComponent.sortingOrder = BaseSortingLayer;

        //Sets world time to 1
        OnWipeFinished?.Invoke(this, EventArgs.Empty);
        yield return null;
    }
    #endregion

    #region Scene Transitions

    private IEnumerator SceneTransition(float transitionTime, string sceneName)
    {
        CanvasComponent.sortingOrder = 20;

        Color c = SceneTransitionImage.color;
        c.a = 0.0f;
        SceneTransitionImage.color = c;
        
        float t = 0.0f;

        while (t < transitionTime)
        {
            c.a = Mathf.Lerp(c.a, 1, t);
            SceneTransitionImage.color = c;

            Debug.Log(c.a);

            t += Time.unscaledDeltaTime;
            yield return null;
        }

        c.a = 1;
        SceneTransitionImage.color = c;

        
        AsyncOperation s = SceneManager.LoadSceneAsync(sceneName);

        StartCoroutine(FadeIn(transitionTime));
        yield return null;
    }

    private IEnumerator FadeIn(float transitionTime)
    {
        Color c = SceneTransitionImage.color;
        float t = 0.0f;

        while (t < transitionTime)
        {
            c.a = Mathf.Lerp(c.a, 0, t);
            SceneTransitionImage.color = c;

            Debug.Log(c.a);

            t += Time.unscaledDeltaTime;
            yield return null;
        }

        c.a = 0.0f;
        SceneTransitionImage.color = c;

        yield return null;

    }
    #endregion

    private void OnDestroy()
    {
        BattleManager.OnBattleFlee -= FleeBattleTransition;
    }
}
