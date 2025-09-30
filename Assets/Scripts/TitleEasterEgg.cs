using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class TitleEasterEgg : MonoBehaviour
    {
        private TextMeshProUGUI titleText;

        void Start()
        {
            titleText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Vector3 mousePos = Input.mousePosition;
                RectTransform rectTransform = titleText.GetComponent<RectTransform>();

                if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos))
                {
                    TriggerEasterEgg();
                }
            }
        }

        void TriggerEasterEgg()
        {
            // Random color
            Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            titleText.color = randomColor;
        }
    }
}
