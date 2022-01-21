using TMPro;
using UnityEngine;

public class GravityObject : InputCallback
{
    private TrailRenderer _trailRenderer;

    [SerializeField] 
    private float _outOfBounds;

    // Start is called before the first frame update
    void Start()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        var localScale = transform.localScale;
        _trailRenderer.startWidth = localScale.x /4f;
        _trailRenderer.endWidth = localScale.x / 4f;
    }

    private void Update()
    {
        if(Vector3.Distance(Vector3.zero, transform.localPosition) >_outOfBounds)
            Destroy(gameObject);
    }

    public override void ObjectChanged()
    {
        var localScale = transform.localScale;
        _trailRenderer.startWidth = localScale.x /4f;
        _trailRenderer.endWidth = localScale.x /4f;
        transform.GetChild(0).GetComponent<TextMeshPro>().text = GetComponent<Rigidbody>().mass.ToString("0.##");
    }
}