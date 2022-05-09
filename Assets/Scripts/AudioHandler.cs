using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XO.UI;

namespace XO.Core
{
    public class AudioHandler : MonoBehaviour
    {
        private AudioSource audioSource = null;

        private void Start()
        {

            audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;

            UIButtonsHandler.onUIButtonClick += PlayButtonClickSFX;
            GameHandler.onGameButtonMove += PlayGameButtonClickSFX;
            GameHandler.onGameEnd += PlayVictorySFX;
            GameHandler.onGameDraw += PlayDrawSFX;
        }
        private void OnDestroy()
        {
            UIButtonsHandler.onUIButtonClick -= PlayButtonClickSFX;
            GameHandler.onGameButtonMove -= PlayGameButtonClickSFX;
            GameHandler.onGameEnd -= PlayVictorySFX;
            GameHandler.onGameDraw -= PlayDrawSFX;
        }
        private void PlayAudioClip(AudioClip newClip)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }

        public void PlayButtonClickSFX()
        {
            PlayAudioClip(DataLoader.Instance.ButtonClickSFX);
        }
        public void PlayGameButtonClickSFX(uint buttonID)
        {
            PlayAudioClip(DataLoader.Instance.ButtonClickSFX);
        }
        public void PlayVictorySFX(uint buttonID)
        {
            PlayAudioClip(DataLoader.Instance.VictorySFX);
        }
        public void PlayDrawSFX()
        {
            PlayAudioClip(DataLoader.Instance.DrawSFX);
        }
    }
}