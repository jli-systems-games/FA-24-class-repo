using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    [Header("Throw Settings")]
    public float minThrowForce = 2f;
    public float maxThrowForce = 15f;
    public float minDragDistance = 0.1f;
    public float spinMultiplier = 100f;
    public float maxSpinSpeed = 100f;

    [Header("Blade Colliders for Each Knife")]
    public GameObject bladeColliderKnife1;
    public GameObject bladeColliderKnife2;
    public GameObject bladeColliderKnife3;

    private Rigidbody2D rb;
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isStopped = false;
    private Target targetScript;

    // Public property for access
    public bool IsStopped => isStopped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        targetScript = FindObjectOfType<Target>();

        bladeColliderKnife1.SetActive(false);
        bladeColliderKnife2.SetActive(false);
        bladeColliderKnife3.SetActive(false);

        int knifeIndex = CustomizationData.instance != null ? CustomizationData.instance.selectedKnifeIndex : 0;
        SetSelectedKnife(knifeIndex);
    }

    public void SetSelectedKnife(int knifeIndex)
    {
        bladeColliderKnife1.SetActive(false);
        bladeColliderKnife2.SetActive(false);
        bladeColliderKnife3.SetActive(false);

        switch (knifeIndex)
        {
            case 0:
                bladeColliderKnife1.SetActive(true);
                break;
            case 1:
                bladeColliderKnife2.SetActive(true);
                break;
            case 2:
                bladeColliderKnife3.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (!isStopped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startDragPosition.z = 0;
            }

            if (Input.GetMouseButtonUp(0))
            {
                endDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endDragPosition.z = 0;
                float dragDistance = Vector2.Distance(startDragPosition, endDragPosition);

                if (dragDistance >= minDragDistance)
                {
                    ThrowKnife(startDragPosition, endDragPosition, dragDistance);
                }
            }
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
        }
    }

    private void ThrowKnife(Vector3 start, Vector3 end, float dragDistance)
    {
        rb.isKinematic = false;

        Vector2 throwDirection = (end - start).normalized;
        float throwForce = Mathf.Clamp(dragDistance, minThrowForce, maxThrowForce);

        rb.velocity = throwDirection * throwForce;

        float baseSpinSpeed = 720f;
        rb.angularVelocity = baseSpinSpeed;
        rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
    }

    public void StopKnife()
    {
        if (isStopped) return;

        isStopped = true;

        Debug.Log("Stopping knife...");

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.isKinematic = true;

        if (targetScript != null)
        {
            targetScript.SetPaused(true);
        }

        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);
        ResetKnife();
    }

    public void ResetKnife()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        isStopped = false;

        Debug.Log("Knife has been reset.");

        if (targetScript != null)
        {
            targetScript.SetPaused(false);
        }
    }
}
