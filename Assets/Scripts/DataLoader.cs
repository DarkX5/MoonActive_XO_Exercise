using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace XO.Core {
    public class DataLoader : MonoBehaviour
    {
        public static DataLoader Instance { get; private set; }
        [Header("Sound Settings")]
        [SerializeField] private AudioClip buttonClickSFX = null;
        [SerializeField] private AudioClip gameButtonClickSFX = null;
        [SerializeField] private AudioClip victorySFX = null;
        [SerializeField] private AudioClip drawSFX = null;
        [Header("Player Customization")]
        [SerializeField][Range(2, 10)] private uint playerNo = 2;
        [SerializeField] private Color[] playerColors = null;
        [SerializeField] private Sprite[] playerIcons = null;
        [Header("UI Customizations")]
        [SerializeField] private Sprite horizontalStrikeoutSprite = null;
        private bool isAllDataLoaded = false;
        private Image auxImage;
        private Texture2D auxTex;
        public Color[] PlayerColors { get { return playerColors; } }
        public Sprite GetCurrentPlayerIcon(int playerIconIdx) { return playerIcons[playerIconIdx]; }
        public Sprite HorizontalStrikeoutSprite { get { return horizontalStrikeoutSprite; } }
        public AudioClip ButtonClickSFX { get { return buttonClickSFX; } }
        public AudioClip GameButtonClickSFX { get { return gameButtonClickSFX; } }
        public AudioClip VictorySFX { get { return victorySFX; } }
        public AudioClip DrawSFX { get { return drawSFX; } }
        public bool IsDataLoaded { get { return isAllDataLoaded; } }
        public uint PlayerNo { get { return playerNo; } }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // create icon array
            playerIcons = new Sprite[playerNo];
            // create player color array
            playerColors = new Color[playerNo];

            // load player colors
            var colorList = Resources.LoadAll<PlayerColor>("PlayerColors");
            if (colorList?.Length > 0)
            {
                for (int i = 0; i < playerNo; i += 1)
                {
                    playerColors[i] = colorList[i].playerColor;
                }
            }
            else
            {
                Debug.Log("fallback");
                // fallback in case of missing data/resources
                for (int i = 0; i < playerNo; i += 1)
                {
                    playerColors[i] =(new Color(UnityEngine.Random.Range(0f, 1f),
                                                UnityEngine.Random.Range(0, 255),
                                                UnityEngine.Random.Range(0f, 1f)));
                }
            }

            // load player icons
            string url;
            for (int i = 0; i < playerNo; i += 1) {
                url = $"{Application.streamingAssetsPath}/MoonActive/PlayerIcon{(i + 1)}.png";
                GetPlayerSpriteIconFromURL(i, url);
            }
            
            // load strikeout image
            url = $"{Application.streamingAssetsPath}/MoonActive/Line.png";
            GetSpriteStrikeoutImageFromURL(url);
        }
        private void GetPlayerSpriteIconFromURL(int idx, string url)
        {
            StartCoroutine(ImageRequest(url, (UnityWebRequest req) =>
            {
                if (req.result == UnityWebRequest.Result.Success)
                {
                    // Get the texture out using a helper downloadhandler
                    auxTex = DownloadHandlerTexture.GetContent(req);
                    // Save image to game cache
                    playerIcons[idx] = Sprite.Create(auxTex, new Rect(0, 0, auxTex.width, auxTex.height), new Vector2(0.5f, 0.5f));
                    CheckIsDataLoaded();
                }
                else
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
            } ));
        }
        private void GetSpriteStrikeoutImageFromURL(string url)
        {
            StartCoroutine(ImageRequest(url, (UnityWebRequest req) =>
            {
                if (req.result == UnityWebRequest.Result.Success)
                {
                    // Get the texture out using a helper downloadhandler
                    auxTex = DownloadHandlerTexture.GetContent(req);
                    // Save image to game cache
                    horizontalStrikeoutSprite = Sprite.Create(auxTex, new Rect(0, 0, auxTex.width, auxTex.height), new Vector2(0.5f, 0.5f));
                    CheckIsDataLoaded();
                }
                else
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
            }));
        }
        IEnumerator ImageRequest(string url, Action<UnityWebRequest> callback)
        {
            using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(url))
            {
                yield return req.SendWebRequest();
                callback(req);
            }
        }

        private void CheckIsDataLoaded() {
            isAllDataLoaded = true;
            // check if strikeout line sprite has been loaded
            if (horizontalStrikeoutSprite == null) {
                isAllDataLoaded = false;
            }
            // check if all resources are loaded
            for (int i = 0; isAllDataLoaded && i < playerNo; i += 1) {
                if (playerIcons[i] == null) {
                    isAllDataLoaded = false;
                }
            }
            // // check if all colors are loaded
            // for (int i = 0; allDataLoaded && i < playerNo; i += 1)
            // {
            //     if (playerColors[i] == null)
            //     {
            //         allDataLoaded = false;
            //     }
            // }
        }
    }
}