using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private PinballController pinballController;

    [SerializeField]
    private float rotationDuration = 1.0f;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;
    private float rotationProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pinballController = FindObjectOfType<PinballController>();

        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRotation()
    {
        if (!isRotating)
        {
            pinballController.customizer.SetActive(false);
            StartCoroutine(RotateCamera());
        }
    }

    private IEnumerator RotateCamera()
    {
        isRotating = true;
        rotationProgress = 0f;

        while (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationProgress);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;

        StartCoroutine(WaitForStart(0.5f));
    }

    private IEnumerator WaitForStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (pinballController != null)
        {
            pinballController.StartGame();
        }
    }
}
