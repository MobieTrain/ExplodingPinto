using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class UIController : MonoBehaviour
    {
        #region SINGLETON
        public static UIController _instance;
        public static UIController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIController>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("UIController");
                        _instance = container.AddComponent<UIController>();
                    }
                }

                return _instance;
            }
        }
        #endregion

        [Header("UI Elements")]
        [SerializeField]
        Text score;

        public void UpdateScoreText(string newText)
        {
            score.text = "Score: " + newText;
        }
    }
}