using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMouseArrow : MonoBehaviour
{
    public GameObject arrowHeadPfb;
    public GameObject arrowNodPfb;
    public int arrowNodNumber;
    public float scaleFactor = 1f;

    public RectTransform origin;
    private List<RectTransform> arrowNodes = new List<RectTransform>();
    private List<Vector2> controlPoints = new List<Vector2>();
    private readonly List<Vector2> controlPointFactors = new List<Vector2> { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };

    private void Awake()
    {
        origin = GetComponent<RectTransform>();
        for (int i = 0; i < arrowNodNumber; i++)
        {
            arrowNodes.Add(Instantiate(arrowNodPfb, transform).GetComponent<RectTransform>());
        }
        arrowNodes.Add(Instantiate(arrowHeadPfb, transform).GetComponent<RectTransform>());
        arrowNodes.ForEach(a => a.GetComponent<RectTransform>().position = new Vector2(-1000, -1000));

        for (int i = 0; i < 4; i++)
        {
            controlPoints.Add(Vector2.zero);
        }
    }

    private void Update()
    {
        controlPoints[0] = new Vector2(origin.position.x, origin.position.y);
        controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        controlPoints[1] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[0];
        controlPoints[2] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[1];

        for (int i = 0; i < arrowNodes.Count; i++)
        {
            var _t = Mathf.Log(1f * i / (arrowNodes.Count - 1) + 1f, 2f);
            arrowNodes[i].position =
                Mathf.Pow(1 - _t, 3) * controlPoints[0] +
                3 * Mathf.Pow(1 - _t, 2) * _t * controlPoints[1] +
                3 * (1 - _t) * Mathf.Pow(_t, 2) * controlPoints[2] +
                Mathf.Pow(_t, 3) * controlPoints[3];
            if (i > 0)
            {
                var eular = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, arrowNodes[i].position - arrowNodes[i - 1].position));
                arrowNodes[i].rotation = Quaternion.Euler(eular);
            }

            var scale = scaleFactor * (1f - 0.03f * (arrowNodes.Count - i - 1));
            arrowNodes[i].transform.localScale = new Vector3(scale, scale, 1f);
        }
        arrowNodes[0].transform.rotation = arrowNodes[1].transform.rotation;
    }

}
