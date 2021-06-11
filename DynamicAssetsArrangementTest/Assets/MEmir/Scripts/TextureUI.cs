using MEmir.Managers.DataManagement;
using MEmir.Managers.Download;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MEmir.UI
{
    public class TextureUI : MonoBehaviour
    {
        [SerializeField] private RectTransform GridPanel, ImagePanel;
        private RawImage ImagePreview;

        private void Start()
        {
            AssetData data = DataManager.Instance.GetData();
            DownloadManager.Instance.DownloadTextures(data.data.URL.ToArray(), (Callback) => { InstantiateTextures(Callback); });
        }

        private void InstantiateTextures(Texture2D[] textures)
        {
            for (int i = 0; i < textures.Length; i++)
            {
                RawImage image = new GameObject(string.Format("Texture [{0}]", i), typeof(RawImage), typeof(Button)).GetComponent<RawImage>();
                image.rectTransform.SetParent(GridPanel, false);
                image.texture = textures[i];
                Button btn = image.GetComponent<Button>();
                btn.onClick.AddListener(() => DrawImage(image));
            }
            ImagePreview = new GameObject("ImagePreview", typeof(RawImage)).GetComponent<RawImage>();
            ImagePreview.rectTransform.SetParent(ImagePanel);
        }

        public void DrawImage(RawImage img) { ImagePreview.texture = img.texture; UIManager.Instance.SwitchPanel(1); }

    }
}