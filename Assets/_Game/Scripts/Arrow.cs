using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Bot bot;
    [SerializeField] private Image image;
    [SerializeField] private float scalePosition;

    private void Awake()
    {
        transform.SetParent(LevelManager.Instance.hintParent);
    }

    // Pool later
    private void OnEnable() {
        bot.Arrow = gameObject;
    }

    private void LateUpdate()
    {
        // Direction before removing z value;
        // Camera.main.WorldToViewportPoint(bot.transform.position - character.transform.position);

        // Get position of the bot
        Vector3 botViewportPoint = Camera.main.WorldToViewportPoint(bot.transform.position);
        if (botViewportPoint.x > 1 || botViewportPoint.x < 0 || botViewportPoint.y > 1 || botViewportPoint.y < 0)
        {
            // Remove z value to rotate only the z axis
            botViewportPoint -= Vector3.forward * botViewportPoint.z;
            // Rotate arrow
            transform.up = botViewportPoint - new Vector3(0.5f, 0.5f, 0);

            // Set arrow position (set position for UI using screen point not world point)
            if (Mathf.Abs(transform.up.x) > Mathf.Abs(transform.up.y)) {
                transform.position = Camera.main.ViewportToScreenPoint(Vector3.one * 0.5f + transform.up * (transform.up.x > 0 ? 1 : -1) * scalePosition / transform.up.x);
            } else {
                transform.position = Camera.main.ViewportToScreenPoint(Vector3.one * 0.5f + transform.up * (transform.up.y > 0 ? 1 : -1) * scalePosition / transform.up.y);
            }
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}
