using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DamageVisual {SHAKE, CRUSH}
public enum StatusFX {BUFF, DEUBUFF}
public enum LockMovement {NONE, RIGHT, LEFT }

[RequireComponent(typeof(ReusableAudioController))]
public class BattleEffectsManager : MonoBehaviour
{
    private bool AttackPlaying;

    private ReusableAudioController audioPlayer;

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

        audioPlayer = GetComponent<ReusableAudioController>();
    }

    /* Unused
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
                    //StartCoroutine(DefaultCharacterShake(target, 0.3f, 0.4f, Color.blue, 1));

                    //StartCoroutine(CharacterShaderAttackingShake(target, .6f, 1f, 3, LockMovement.NONE));
                    //StartCoroutine(CharacterShaderFlash(target, 0.4f));
                    //StartCoroutine(CharacterShaderColorHit(target, 0.8f, Color.red));
                    //StartCoroutine(CharacterShaderStatusFX(target, 3, 1, 1.3f, 0.8f, Color.red));
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
    */

    /// <summary>
    /// Apply a transparent scroll effect over a period of time
    /// </summary>
    /// <param name="target">Who to apply effect to</param>
    /// <param name="scrollSpeed">How fast texture scrolls</param>
    /// <param name="scrollDir">1 = down; -1 = up</param>
    /// <param name="scrollTime">Duration of effect</param>
    /// <param name="maxAlpha">0 = Transparent; 1 = Fully Opaque</param>
    /// <param name="color">Color of the effect (eg. Color.red, Color.green )</param>
    public void ScrollEffect(GameObject target, float scrollSpeed, int scrollDir, float scrollTime, float maxAlpha, Color color)
    {
        StartCoroutine(CharacterShaderStatusFX(target, scrollSpeed, scrollDir, scrollTime, maxAlpha, color));
    }
    public void ScrollEffect(GameObject target, float scrollSpeed, int scrollDir, float scrollTime, float maxAlpha, Color color, string audioClip)
    {
        StartCoroutine(CharacterShaderStatusFX(target, scrollSpeed, scrollDir, scrollTime, maxAlpha, color, audioClip));
    }

    /// <summary>
    /// Apply a color 'hit' effect to a target sprite. Hard-in to Ease-out
    /// </summary>
    /// <param name="target">Who to apply effect to</param>
    /// <param name="easeOutTime">How long it takes to return to original color</param>
    /// <param name="color">Color of the hit effect</param>
    public void CharacterHitColorEffect(GameObject target, float easeOutTime, Color color)
    {
        StartCoroutine(CharacterShaderColorHit(target, easeOutTime, color));
    }
    public void CharacterHitColorEffect(GameObject target, float easeOutTime, Color color, string audioClip)
    {
        StartCoroutine(CharacterShaderColorHit(target, easeOutTime, color, audioClip));
    }

    /// <summary>
    /// Flashes a target sprite, alternates between Opaque and Transparent
    /// </summary>
    /// <param name="target">Who to apply effect to</param>
    /// <param name="flashTime">How long effect lasts for</param>
    public void CharcterFlash(GameObject target, float flashTime)
    {
        StartCoroutine(CharacterShaderFlash(target, flashTime));
    }
    public void CharcterFlash(GameObject target, float flashTime, string audioClip)
    {
        StartCoroutine(CharacterShaderFlash(target, flashTime, audioClip));
    }
    /// <summary>
    /// Squishes the target Sprite by rotating using Euler
    /// </summary>
    /// <param name="target">Who to apply effect to</param>
    /// <param name="squishRotation">Euler vector. Rotation of the sprite</param>
    /// <param name="timeToSquish">How to long it takes to reach full squish</param>
    /// <param name="SquishHoldTIme">How long the squish holds before returning to normal</param>
    public void CharacterSquishEffect(GameObject target, Vector3 squishRotation, float timeToSquish, float SquishHoldTIme)
    {
        StartCoroutine(CharacterSquish(target, squishRotation, timeToSquish, SquishHoldTIme));
    }
    public void CharacterSquishEffect(GameObject target, Vector3 squishRotation, float timeToSquish, float SquishHoldTIme, Color color)
    {
        StartCoroutine(CharacterSquish(target, squishRotation, timeToSquish, SquishHoldTIme, color));
    }
    public void CharacterSquishEffect(GameObject target, Vector3 squishRotation, float timeToSquish, float SquishHoldTIme, Color color, string audioClip)
    {
        StartCoroutine(CharacterSquish(target, squishRotation, timeToSquish, SquishHoldTIme, color, audioClip));
    }

    /// <summary>
    /// Applies a horizontal shake to the sprite. Can lock to Right, Left, or have no lock
    /// </summary>
    /// <param name="target">Who to apply effect to</param>
    /// <param name="distanceFromOrigin"> 0 to 1. 1 is approximately the sprites-width distance away from origin</param>
    /// <param name="shakeTime">How long the sprite will shake for</param>
    /// <param name="shakeSpeed">How fast the sprite shakes</param>
    /// <param name="moveType">Clamp movement from RIGHT[Origin<->Right], LEFT[Left<->Origin], or NONE[Left<->Right]</param>
    public void CharacterShake(GameObject target, float distanceFromOrigin, float shakeTime, float shakeSpeed, LockMovement moveType)
    {
        StartCoroutine(CharacterShaderAttackingShake(target, distanceFromOrigin, shakeTime, shakeSpeed, moveType));
    }
    public void CharacterShake(GameObject target, float distanceFromOrigin, float shakeTime, float shakeSpeed, LockMovement moveType, string audioClip)
    {
        StartCoroutine(CharacterShaderAttackingShake(target, distanceFromOrigin, shakeTime, shakeSpeed, moveType, audioClip));
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

    //---------------------- Coroutines

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

        SpriteRenderer Debuff = character.transform.Find("StatusFX").GetComponent<SpriteRenderer>();
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

    private IEnumerator CharacterSquish(GameObject character, Vector3 squishRotation, float timeToSquish, float SquishHoldTime, Color color, string audioClip)
    {
        Quaternion StartingRotation = character.transform.rotation;
        Quaternion EndRotation = Quaternion.Euler(squishRotation);

        audioPlayer.PlaySound(audioClip);

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

    private IEnumerator CharacterShaderAttackingShake(GameObject character, float distanceFromOrigin, float shakeTime, float shakeSpeed, LockMovement moveType)
    {
        SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();
        distanceFromOrigin = Mathf.Abs(distanceFromOrigin);


        float t = 0.0f;
        float dist = 0.0f;
        while (t < shakeTime)
        {
            dist = Mathf.Sin((t * shakeSpeed)/distanceFromOrigin);

            //Clamps movement based on lock-type
            switch (moveType)
            {
                case LockMovement.NONE:
                    dist = Mathf.Clamp(dist, -distanceFromOrigin, distanceFromOrigin);

                    break;
                case LockMovement.RIGHT:

                    dist = Mathf.Clamp(dist, 0, distanceFromOrigin);

                    break;
                case LockMovement.LEFT:
                    dist = Mathf.Clamp(dist, -distanceFromOrigin, 0);

                    break;
                default:
                    break;
            }

            renderer.material.SetFloat("_VertexX", dist);
            Debug.Log(dist);

            t += Time.deltaTime;
            yield return null;
        }

        renderer.material.SetFloat("_VertexX", 0.0f);

        AttackPlaying = false;
        yield return null;
    }

    private IEnumerator CharacterShaderAttackingShake(GameObject character, float distanceFromOrigin, float shakeTime, float shakeSpeed, LockMovement moveType, string audioClip)
    {
        SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();
        distanceFromOrigin = Mathf.Abs(distanceFromOrigin);

        audioPlayer.PlaySound(audioClip);

        float t = 0.0f;
        float dist = 0.0f;
        while (t < shakeTime)
        {
            dist = Mathf.Sin((t * shakeSpeed) / distanceFromOrigin);

            //Clamps movement based on lock-type
            switch (moveType)
            {
                case LockMovement.NONE:
                    dist = Mathf.Clamp(dist, -distanceFromOrigin, distanceFromOrigin);

                    break;
                case LockMovement.RIGHT:

                    dist = Mathf.Clamp(dist, 0, distanceFromOrigin);

                    break;
                case LockMovement.LEFT:
                    dist = Mathf.Clamp(dist, -distanceFromOrigin, 0);

                    break;
                default:
                    break;
            }

            renderer.material.SetFloat("_VertexX", dist);
            Debug.Log(dist);

            t += Time.deltaTime;
            yield return null;
        }

        renderer.material.SetFloat("_VertexX", 0.0f);

        AttackPlaying = false;
        yield return null;
    }


    private IEnumerator CharacterShaderFlash(GameObject character, float flashTime)
    {
        SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();

        float t = 0.0f;
        bool flashed = false;

        while (t < flashTime)
        {
            flashed = !flashed;
            if (flashed)
            {
                renderer.material.SetFloat("_Transparency", 1);
            }
            else
            {
                renderer.material.SetFloat("_Transparency", 0);
            }

            t += Time.deltaTime;
            yield return null;
        }

        renderer.material.SetFloat("_Transparency", 1);

        AttackPlaying = false;
        yield return null;
    }

    private IEnumerator CharacterShaderFlash(GameObject character, float flashTime, string audioClip)
    {
        SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();

        audioPlayer.PlaySound(audioClip);

        float t = 0.0f;
        bool flashed = false;

        while (t < flashTime)
        {
            flashed = !flashed;
            if (flashed)
            {
                renderer.material.SetFloat("_Transparency", 1);
            }
            else
            {
                renderer.material.SetFloat("_Transparency", 0);
            }

            t += Time.deltaTime;
            yield return null;
        }

        renderer.material.SetFloat("_Transparency", 1);

        AttackPlaying = false;
        yield return null;
    }


    private IEnumerator CharacterShaderColorHit(GameObject character, float easeOutTime, Color changeColor)
    {
        //SpriteRenderer renderer = character.transform.Find("StatusFX").GetComponent<SpriteRenderer>();
        SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            Color baseColor = renderer.material.GetColor("_TintColor");
            renderer.material.SetColor("_TintColor", changeColor);

            float t = 0.0f;

            Color finalColor;

            while (t < easeOutTime)
            {
                finalColor = Color.Lerp(changeColor, baseColor, t/easeOutTime);
                renderer.material.SetColor("_TintColor", finalColor);

                Debug.Log(t/easeOutTime);

                t += Time.deltaTime;
                yield return null;
            }
        }

        AttackPlaying = false;
        yield return null;
    }

    private IEnumerator CharacterShaderColorHit(GameObject character, float easeOutTime, Color changeColor, string audioClip)
    {
        //SpriteRenderer renderer = character.transform.Find("StatusFX").GetComponent<SpriteRenderer>();
        SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();

        audioPlayer.PlaySound(audioClip);

        if (renderer != null)
        {
            Color baseColor = renderer.material.GetColor("_TintColor");
            renderer.material.SetColor("_TintColor", changeColor);

            float t = 0.0f;

            Color finalColor;

            while (t < easeOutTime)
            {
                finalColor = Color.Lerp(changeColor, baseColor, t / easeOutTime);
                renderer.material.SetColor("_TintColor", finalColor);

                Debug.Log(t / easeOutTime);

                t += Time.deltaTime;
                yield return null;
            }
        }

        AttackPlaying = false;
        yield return null;
    }

    private IEnumerator CharacterShaderStatusFX(GameObject character, float scrollSpeed, int scrollDir, float scrollTime, float maxAlpha, Color color)
    {
        SpriteRenderer renderer = character.transform.Find("StatusFX").GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            float t = 0.0f;

            scrollTime = Mathf.Abs(scrollTime);
            if (maxAlpha > 1.0f)
            {
                maxAlpha = 1.0f;
            }

            renderer.material.SetColor("_TintColor", color);
            renderer.material.SetFloat("_ScrollSpeed", scrollSpeed);
            renderer.material.SetFloat("_ScrollDir", scrollDir);

            float finalAlpha = 0.0f;
            float progress = 0.0f;

            while (t < scrollTime)
            {
                progress = t / scrollTime;
                if (progress < 0.5f)
                {
                    finalAlpha = progress;
                }
                else
                {
                    finalAlpha = Mathf.Sin(1-progress);
                    Debug.Log("AFTER 0.5 " + finalAlpha);
                }

                finalAlpha = Mathf.Clamp(finalAlpha, 0, maxAlpha);

                renderer.material.SetFloat("_Transparency", finalAlpha);

                t += Time.deltaTime;
                yield return null;
            }

            renderer.material.SetFloat("_Transparency", 0);
        }

        AttackPlaying = false;

        yield return null;
    }

    private IEnumerator CharacterShaderStatusFX(GameObject character, float scrollSpeed, int scrollDir, float scrollTime, float maxAlpha, Color color, string audioClip)
    {
        SpriteRenderer renderer = character.transform.Find("StatusFX").GetComponent<SpriteRenderer>();

        audioPlayer.PlaySound(audioClip);

        if (renderer != null)
        {
            float t = 0.0f;

            scrollTime = Mathf.Abs(scrollTime);
            if (maxAlpha > 1.0f)
            {
                maxAlpha = 1.0f;
            }

            renderer.material.SetColor("_TintColor", color);
            renderer.material.SetFloat("_ScrollSpeed", scrollSpeed);
            renderer.material.SetFloat("_ScrollDir", scrollDir);

            float finalAlpha = 0.0f;
            float progress = 0.0f;

            while (t < scrollTime)
            {
                progress = t / scrollTime;
                if (progress < 0.5f)
                {
                    finalAlpha = progress;
                }
                else
                {
                    finalAlpha = Mathf.Sin(1 - progress);
                    Debug.Log("AFTER 0.5 " + finalAlpha);
                }

                finalAlpha = Mathf.Clamp(finalAlpha, 0, maxAlpha);

                renderer.material.SetFloat("_Transparency", finalAlpha);

                t += Time.deltaTime;
                yield return null;
            }

            renderer.material.SetFloat("_Transparency", 0);
        }

        AttackPlaying = false;

        yield return null;
    }
    #endregion
}
