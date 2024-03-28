using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DamageVisual {SHAKE, CRUSH}
public enum StatusFX {BUFF, DEUBUFF}
public class BattleEffectsManager : MonoBehaviour
{
    private bool AttackPlaying;

    private static BattleEffectsManager instance;
    public static BattleEffectsManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        gameObject.SetActive(true);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void ApplyAttackFX(GameObject user, GameObject target, DamageVisual damageVisual, Color damagedColor)
    {
        //Gonna be used for when implementing the ranged aspect of the visuals
        Vector2 userPosition = user.gameObject.transform.position;
        Vector2 targetPosition = target.gameObject.transform.position;

        switch (damageVisual)
        {
            case DamageVisual.SHAKE:
                //target shake attack code
                if (!AttackPlaying)
                {
                    AttackPlaying = true;
                    //StartCoroutine(DefaultCharacterShake(target, 0.3f, 3.1f, 0.3f));
                    StartCoroutine(DefaultCharacterShake(target, 0.3f, 0.4f, Color.blue, 1));
                }

                break;
            case DamageVisual.CRUSH:
                //target crush attack code
                if (!AttackPlaying)
                {
                    AttackPlaying = true;
                    StartCoroutine(CharacterSquish(target, new Vector3(-60, 0, 0), 0.2f, 2f));
                }

                break;
            default:
                break;
        }
    }

    //Unused
    /*
    public void ApplyAttackFX(GameObject user, GameObject target, DamageVisual damageVisual, ParticleSystem rangedVisual)
    {
        Vector2 userPosition = user.gameObject.transform.position;
        Vector2 targetPosition = target.gameObject.transform.position;

        //User shake attack code

        //ParticleSystem visual code
        if (true)
        {

        }

        switch (damageVisual)
        {
            case DamageVisual.SHAKE:
                //target shake attack code

                break;
            case DamageVisual.CRUSH:
                //target shake attack code

                break;
            default:
                break;
        }
    }

    public void ApplyStatusFX() 
    {

    }
    */

    #region Damaging Status Show
    /// <summary>
    /// Method to apply 'damage' shake after playing an attack visual
    /// </summary>
    /// <param name="character">Reference to the Character this will apply to</param>
    /// <param name="distanceFromOrigin">How far from the starting point that the shake will move the character on X-axis</param>
    /// <param name="shakeTime">How long in seconds that the shake position will hold</param>
    /// <param name="damagedAlpha">The alpha change within the current color during the shake</param>
    /// <returns></returns>
    private IEnumerator DefaultCharacterShake(GameObject character, float distanceFromOrigin,float shakeTime, float damagedAlpha)
    {
        Vector2 StartingPosition = character.transform.position;
        Color fromColor = character.GetComponent<SpriteRenderer>().color;
        Color color = fromColor;
        color.a = damagedAlpha;

        distanceFromOrigin = Mathf.Clamp(distanceFromOrigin, -1.0f, 1.0f);

        SpriteRenderer c = character.GetComponent<SpriteRenderer>();
        c.color = color;
        c.material.SetFloat("_VertexX", distanceFromOrigin);
        //character.transform.position += new Vector3(distanceFromOrigin, 0, 0);

        float t = 0.0f;
        while (t < shakeTime)
        {            
            t += Time.deltaTime;
            yield return null;
        }

        c.material.SetFloat("_VertexX", 0);
        c.color = fromColor;
        //character.transform.position = StartingPosition;
        //character.GetComponent<SpriteRenderer>().color = fromColor;

        AttackPlaying = false;
        yield return null;
    }

    /// <summary>
    /// Method to apply 'damage' shake after playing an attack visual, applying a full color change
    /// </summary>
    /// <param name="character">Reference to the Character this will apply to</param>
    /// <param name="distanceFromOrigin">How far from the starting point that the shake will move the character on X-axis. From -1 to 1</param>
    /// <param name="shakeTime">How long in seconds that the shake position will hold</param>
    /// <param name="color">The color that will temporarily override during the shake</param>
    /// <param name="damagedAlpha">The alpha change within the current color during the shake</param>
    /// <returns></returns>
    private IEnumerator DefaultCharacterShake(GameObject character, float distanceFromOrigin, float shakeTime, Color color, float damagedAlpha)
    {
        Vector2 StartingPosition = character.transform.position;
        Color fromColor = character.GetComponent<SpriteRenderer>().color;
        color.a = damagedAlpha;

        distanceFromOrigin = Mathf.Clamp(distanceFromOrigin, -1.0f, 1.0f);

        SpriteRenderer c = character.GetComponent<SpriteRenderer>();
        c.color = color;
        c.material.SetFloat("_VertexX", distanceFromOrigin);

        SpriteRenderer Debuff = character.transform.Find("Debuff").GetComponent<SpriteRenderer>();
        Debuff.material.SetFloat("_Transparency", 1);
        Debuff.material.SetFloat("_VertexX", distanceFromOrigin);


        //character.GetComponent<SpriteRenderer>().color = color;
        //character.transform.position += new Vector3(distanceFromOrigin, 0, 0);

        float t = 0.0f;
        while (t < shakeTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        Debuff.material.SetFloat("_Transparency", 0);
        Debuff.material.SetFloat("_VertexX", 0);
        c.material.SetFloat("_VertexX", 0);
        c.color = fromColor;
        //character.transform.position = StartingPosition;
        //character.GetComponent<SpriteRenderer>().color = fromColor;

        AttackPlaying = false;
        yield return null;
    }

    /// <summary>
    /// Method to apply a 'squish' effect to the targeted Character.
    /// </summary>
    /// <param name="character">Reference to the Character this will apply to</param>
    /// <param name="squishRotation">Euler rotation that the Character will rotate</param>
    /// <param name="timeToSquish">Time it takes to reach full squish</param>
    /// <param name="SquishHoldTime">Time the squish effect will hold</param>
    /// <param name="color">Color override during the squish</param>
    /// <returns></returns>
    private IEnumerator CharacterSquish(GameObject character, Vector3 squishRotation, float timeToSquish, float SquishHoldTime, Color color)
    {
        Quaternion StartingRotation = character.transform.rotation;
        Quaternion EndRotation = Quaternion.Euler(squishRotation);

        SpriteRenderer c = character.GetComponent<SpriteRenderer>();
        Color fromColor = c.color;
        c.color = color;

        float t = 0.0f;
        while (t < timeToSquish)
        {
            character.transform.rotation = Quaternion.Slerp(StartingRotation, EndRotation, t);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0.0f;
        while (t < SquishHoldTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        t = 0.0f;
        while (t < timeToSquish)
        {
            character.transform.rotation = Quaternion.Slerp(EndRotation, StartingRotation, t);
            t += Time.deltaTime;
            yield return null;
        }

        character.GetComponent<SpriteRenderer>().color = fromColor;

        AttackPlaying = false;
        yield return null;
    }

    /// <summary>
    /// Method to apply a 'squish' effect to the targeted Character.
    /// </summary>
    /// <param name="character">Reference to the Character this will apply to</param>
    /// <param name="squishRotation">Euler rotation that the Character will rotate</param>
    /// <param name="timeToSquish">Time it takes to reach full squish</param>
    /// <param name="SquishHoldTime">Time the squish effect will hold</param>
    /// <returns></returns>
    private IEnumerator CharacterSquish(GameObject character, Vector3 squishRotation, float timeToSquish, float SquishHoldTime)
    {
        Quaternion StartingRotation = character.transform.rotation;
        Quaternion EndRotation = Quaternion.Euler(squishRotation);

        float t = 0.0f;
        while (t < timeToSquish)
        {
            character.transform.rotation = Quaternion.Slerp(StartingRotation, EndRotation, t/timeToSquish);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;

        
        t = 0.0f;
        while (t < SquishHoldTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        t = 0.0f;
        while (t < timeToSquish)
        {
            character.transform.rotation = Quaternion.Slerp(EndRotation, StartingRotation, t/timeToSquish);
            t += Time.deltaTime;
            yield return null;
        }
        

        AttackPlaying = false;
        yield return null;
    }

    #endregion
}
