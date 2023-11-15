using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // 메인 메뉴
    public AudioSource bgmMainMenu;

    // 인 게임
    public AudioSource bgmMainGame;
    public AudioSource bgmAttack;
    public AudioSource bgmButton;
    public AudioSource bgmDie;
    public AudioSource bgmHit;
    public AudioSource bgmItemGraw;
    public AudioSource bgmStageClear;
    public AudioSource bgmUpgrade;
    public AudioSource bgmUseItem;
    public AudioSource bgmFailMoney;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgmMainMenu.Play();
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            bgmMainGame.Play();

            Invoke(nameof(UnmuteUseItemSound), 1f);
            Invoke(nameof(UnmuteDieSound), 2f);
        }
    }

    void UnmuteUseItemSound()
    {
        bgmUseItem.mute = false;
    }

    void UnmuteDieSound()
    {
        bgmDie.mute = false;
    }


    public void PlayAttackSound()
    {
        bgmAttack.Play();
    }

    public void PlayButtonSound()
    {
        bgmButton.Play();
    }

    public void PlayDieSound()
    {
        bgmDie.Play();
    }

    public void PlayHitSound()
    {
        bgmHit.Play();
    }

    public void PlayItemGrawSound()
    {
        bgmItemGraw.Play();
    }

    public void PlayStageClearSound()
    {
        bgmStageClear.Play();
    }

    public void PlayUpgradeSound()
    {
        bgmUpgrade.Play();
    }

    public void PlayUseItemSound()
    {
        bgmUseItem.Play();
    }

    public void PlayFailMoneySound()
    {
        bgmFailMoney.Play();
    }
}
