using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace XO.Core
{
    public class DataLoader : MonoBehaviour
    {
        public static DataLoader Instance { get; private set; }
        [Header("Basic Settings")]
        [SerializeField] private string streamingAssetsGraphicsPath = ""; // "/MoonActive";
        [Header("Sound Settings")]
        [SerializeField] private AudioClip buttonClickSFX = null;
        [SerializeField] private AudioClip gameButtonClickSFX = null;
        [SerializeField] private AudioClip victorySFX = null;
        [SerializeField] private AudioClip drawSFX = null;
        [Header("Player Customization")]
        [SerializeField] private PlayerTypes[] playerTypesList;
        [SerializeField] private Color[] playerColors = null;
        [SerializeField] private Sprite[] playerIcons = null;
        [SerializeField] private Sprite bgSprite = null;
        [Header("UI Customizations")]
        [SerializeField] private Sprite horizontalStrikeoutSprite = null;
        private bool isAllDataLoaded = false;
        private Image auxImage;
        private Texture2D auxTex;
        private uint playerNo = 2;
        private PlayerController[] playerList = null;
        [SerializeField] private bool hintsActive = false;
        private bool bgLoaded = false;

        public Color[] PlayerColors { get { return playerColors; } }
        public Sprite BGSprite { get { return bgSprite; } }
        public Sprite GetCurrentPlayerIcon(int playerIconIdx) { return playerIcons[playerIconIdx]; }
        public Sprite HorizontalStrikeoutSprite { get { return horizontalStrikeoutSprite; } }
        public AudioClip ButtonClickSFX { get { return buttonClickSFX; } }
        public AudioClip GameButtonClickSFX { get { return gameButtonClickSFX; } }
        public AudioClip VictorySFX { get { return victorySFX; } }
        public AudioClip DrawSFX { get { return drawSFX; } }
        public bool IsDataLoaded { get { return isAllDataLoaded; } }
        public uint PlayerNo { get { return playerNo; } }
        public PlayerController[] PlayerList { get { return playerList; } }
        public PlayerTypes[] PlayerTypesList { get { return playerTypesList; } }
        public bool HintsActive { get { return hintsActive; } }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        private void Start() {
            Init();
        }

        // Start is called before the first frame update
        public void Init()
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
                    playerColors[i] = colorList[i].PlayerColorValue;
                }
            }
            else
            {
                Debug.LogWarning("No Player Colors defined - Color fallback (random colors)");
                // fallback in case of missing data/resources
                for (int i = 0; i < playerNo; i += 1)
                {
                    playerColors[i] = (new Color(UnityEngine.Random.Range(0f, 1f),
                                                UnityEngine.Random.Range(0, 255),
                                                UnityEngine.Random.Range(0f, 1f)));
                }
            }

            // load player icons
            string url;
            for (int i = 0; i < playerNo; i += 1)
            {
                url = $"{Application.streamingAssetsPath}{streamingAssetsGraphicsPath}/PlayerIcon{(i + 1)}.png";
                GetPlayerSpriteIconFromURL(i, url);
                url = $"{Application.streamingAssetsPath}{streamingAssetsGraphicsPath}/EmptyBG.png";
                GetBackgroundSpriteIconFromURL(url);
            }

            // load strikeout image
            url = $"{Application.streamingAssetsPath}{streamingAssetsGraphicsPath}/Line.png";
            GetSpriteStrikeoutImageFromURL(url);
        }

        private IEnumerator LoadAssetBundle(string assetBundleName, string objectNameToLoad) {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
            filePath = System.IO.Path.Combine(filePath, assetBundleName);

            //Load "animals" AssetBundle
            var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
            yield return assetBundleCreateRequest;

            AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

            //Load the "dog" Asset (Use Texture2D since it's a Texture. Use GameObject if prefab)
            AssetBundleRequest asset = asseBundle.LoadAssetAsync<Texture2D>(objectNameToLoad);
            yield return asset;

            //Retrieve the object (Use Texture2D since it's a Texture. Use GameObject if prefab)
            Texture2D loadedAsset = asset.asset as Texture2D;

            // //Do something with the loaded loadedAsset  object (Load to RawImage for example) 
            // image.texture = loadedAsset;
        }

        private void GetBackgroundSpriteIconFromURL(string url)
        {
            UnityEditor.AssetDatabase.Refresh();
            StartCoroutine(ImageRequest(url, (UnityWebRequest req) =>
            {
                // Debug.Log($"url {url}");
                if (req.result == UnityWebRequest.Result.Success)
                {
                    // Get the texture out using a helper downloadhandler
                    auxTex = DownloadHandlerTexture.GetContent(req);
                    // Save image to game cache
                    bgSprite = Sprite.Create(auxTex, new Rect(0, 0, auxTex.width, auxTex.height), new Vector2(0.5f, 0.5f));
                    // Debug.Log($"GetBackgroundSpriteIconFromURL: {bgSprite.name} | isNUll {bgSprite == null}");
                    bgLoaded = true;
                    CheckIsDataLoaded();
                }
                else
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
            }));
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
            }));
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

        private void CheckIsDataLoaded()
        {
            isAllDataLoaded = true;
            // check if strikeout line sprite has been loaded
            if (horizontalStrikeoutSprite == null)
            {
                isAllDataLoaded = false;
            }
            // check if background was loaded
            if (!bgLoaded)
            {
                isAllDataLoaded = false;
            }
            // check if all resources are loaded
            for (int i = 0; isAllDataLoaded && i < playerNo; i += 1)
            {
                if (playerIcons[i] == null)
                {
                    isAllDataLoaded = false;
                }
            }
        }
        private void SetupPlayersList()
        {
            hintsActive = false;

            bool playerActive = false, aiActive = false;
            playerNo = (uint)playerTypesList.Length;
            playerList = new PlayerController[playerNo];

            // setup player list
            for (int i = 0; i < playerTypesList.Length; i += 1)
            {
                switch (playerTypesList[i])
                {
                    case PlayerTypes.Player:
                        playerList[i] = new PlayerController();
                        playerActive = true;
                        break;
                    case PlayerTypes.AI:
                        playerList[i] = new AIController();
                        aiActive = true;
                        break;
                }
            }
            
            if (playerActive == true && aiActive == true) {
                hintsActive = true;
            }
        }
        
        // called from UI
        public void SetGamePlayers(PlayerTypes[] newPlayerTypes) {
            playerTypesList = newPlayerTypes;
            SetupPlayersList();
        }
    }
}