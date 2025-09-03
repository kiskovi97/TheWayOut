using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kiskovi.Core;

using TheWayOut.Gameplay;
using TheWayOut.Input;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    public class Level : IData
    {
        public int number;
        public bool enabled;
    }

    internal class LevelHolder : DataHolder<Level>
    {
        public TMP_Text[] titles;
        public GameObject disabledObj;
        public GameObject enabledObj;
        public Button selectLeveButton;

        private void OnEnable()
        {
            selectLeveButton?.onClick.AddListener(OnSelect);
        }

        private void OnDisable()
        {
            selectLeveButton?.onClick.RemoveListener(OnSelect);
        }

        private void OnSelect()
        {
            if (Data == null) return;

            LevelManager.CurrentLevel = Data.number;
            SceneLoader.LoadScene(SceneLoader.GAMEPLAY);
        }

        public override void SetData(IData itemData)
        {
            base.SetData(itemData);

            if (Data == null) return;

            foreach(var title in titles)
                if (title != null)
                    title.text = Data.number.ToString();

            if (disabledObj != null)
                disabledObj.SetActive(!Data.enabled);

            if (enabledObj != null)
                enabledObj.SetActive(Data.enabled);

            if (selectLeveButton != null)
                selectLeveButton.interactable = Data.enabled;
        }
    }
}
