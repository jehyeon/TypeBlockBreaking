using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource attackSound;
    [SerializeField]
    private AudioSource breakSound;
    [SerializeField]
    private AudioSource guardSound;
    [SerializeField]
    private AudioSource attackedSound;
    [SerializeField]
    private AudioSource moveSound;
    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private AudioSource enchantSound;

    private static SoundManager instance = null;
    public static SoundManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAttackSound()
    {
        attackSound.Play();
    }

    public void PlayBreakSound()
    {
        breakSound.Play();
    }

    public void PlayGuardSound()
    {
        guardSound.Play();
    }

    public void PlayAttackedSound()
    {
        attackedSound.Play();
    }

    public void PlayMoveSound()
    {
        moveSound.Play();
    }

    public void PlayJumpSound()
    {
        jumpSound.Play();
    }

    public void PlayEnchantSound()
    {
        enchantSound.Play();
    }
}
