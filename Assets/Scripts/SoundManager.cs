﻿using UnityEngine;
using System.Collections;

public enum SoundType { HitContact, HitBlock, HitCat, HitDog, HitWhoosh, DeadDog, DeadCat, CharacterSelect, TimerTick, MenuTick,
    Shuffle, MenuMusic, GameMusic }

public class SoundManager : MonoBehaviour {

    private static SoundManager _instance;
    static public SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (_instance == null)
                {
                    Debug.LogError("[ No Instance was found for SoundManager check for errors ]");
                    GameObject go = new GameObject("SoundManager");
                    DontDestroyOnLoad(go);

                    _instance = go.AddComponent<SoundManager>();
                    Debug.Log("[ Creating A New SoundManager ]");
                }
            }
            return _instance;
        }
    }

    public AudioClip HitContact;
    public AudioClip HitBlock;
    public AudioClip HitCat;
    public AudioClip HitDog;
    public AudioClip HitWhoosh;
    public AudioClip DeadDog;
    public AudioClip DeadCat;
    public AudioClip CharacterSelect;
    public AudioClip TimerTick;
    public AudioClip MenuTick;
    public AudioClip Shuffle1;
    public AudioClip Shuffle2;
    public AudioClip Shuffle3;
    public AudioClip Shuffle4;
    public AudioClip MenuMusic;
    public AudioClip GameMusic;
    public AudioSource audioSource;
    private int shuffleRand = 0;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("[SoundManager] Start");
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Play(SoundType sound)
    {
        switch(sound)
        {
            case SoundType.HitContact:
                audioSource.clip = HitContact;
                audioSource.Play();
                break;

            case SoundType.HitBlock:
                audioSource.clip = HitBlock;
                audioSource.Play();
                break;

            case SoundType.HitCat:
                audioSource.clip = HitCat;
                audioSource.Play();
                break;

            case SoundType.HitWhoosh:
                audioSource.clip = HitWhoosh;
                audioSource.Play();
                break;

            case SoundType.DeadDog:
                audioSource.clip = DeadDog;
                audioSource.Play();
                break;

            case SoundType.DeadCat:
                audioSource.clip = DeadCat;
                audioSource.Play();
                break;

            case SoundType.CharacterSelect:
                audioSource.clip = CharacterSelect;
                audioSource.Play();
                break;

            case SoundType.TimerTick:
                audioSource.clip = TimerTick;
                audioSource.Play();
                break;

            case SoundType.MenuTick:
                audioSource.clip = MenuTick;
                audioSource.Play();
                break;

            case SoundType.MenuMusic:
                audioSource.clip = MenuMusic;
                audioSource.Play();
                break;

            case SoundType.GameMusic:
                audioSource.clip = GameMusic;
                audioSource.Play();
                break;

            case SoundType.Shuffle:
                shuffleRand = Random.Range(1, 5);

                switch (shuffleRand)
                {
                    case 1:
                        audioSource.clip = Shuffle1;
                        audioSource.Play();
                        break;

                    case 2:
                        audioSource.clip = Shuffle2;
                        audioSource.Play();
                        break;

                    case 3:
                        audioSource.clip = Shuffle3;
                        audioSource.Play();
                        break;

                    case 4:
                        audioSource.clip = Shuffle4;
                        audioSource.Play();
                        break;
                }
                break;
        }
    }
}