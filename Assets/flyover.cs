using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialCameraFlyOver : MonoBehaviour
{
    public Transform[] waypoints;
    public string[] waypointTexts;
    public float speed = 5f;
    public float freezeTime = 2f;
    public TMP_Text tutorialText;

    private int index = 0;
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    void Start()
    {
        tutorialText.text = "";

        // Safety check:
        if (waypointTexts.Length < waypoints.Length)
        {
            // Auto-expand text array
            string[] newTexts = new string[waypoints.Length];
            for (int i = 0; i < newTexts.Length; i++)
            {
                if (i < waypointTexts.Length)
                    newTexts[i] = waypointTexts[i];
                else
                    newTexts[i] = "";  // empty text for missing entries
            }
            waypointTexts = newTexts;
        }
    }

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Handle freeze state
        if (isFrozen)
        {
            freezeTimer += Time.deltaTime;

            if (freezeTimer >= freezeTime)
            {
                freezeTimer = 0f;
                isFrozen = false;
                tutorialText.text = "";

                index++;
                if (index >= waypoints.Length)
                {
                    SceneManager.LoadScene("start");
                    return;
                }
            }

            return;
        }

        // Move camera toward next waypoint
        Transform target = waypoints[index];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, speed * Time.deltaTime);

        // Check arrival
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            ActivateFreezeFrame();
        }
    }

    void ActivateFreezeFrame()
    {
        isFrozen = true;
        freezeTimer = 0f;

        tutorialText.text = waypointTexts[index];
    }
}
