using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Tentacle : MonoBehaviour {
    public Sprite tetherBlob;
	private bool canGrab;
    public float grabDistance;
    public float tetherWidth;
    private LineRenderer lineRenderer;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public float alpha = 1.0f;
    GameObject sprGameObj;
    GameObject player;
    public static Vector2 TetherLocation;
    public static float TetherDistance;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        TetherLocation = Vector2.zero;
        canGrab = true;
        SetLineRendererProperties();
        player.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
        GameObject player = GameObject.Find("Player");
        if (TetherLocation != null && TetherLocation != Vector2.zero)
        {
            DrawTether(player);
        }

		if(player != null) {
			if((Input.GetMouseButtonDown(0)) && canGrab) {
                Debug.Log("In mouse phase");
                Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 heading = target - new Vector2(player.transform.position.x, player.transform.position.y);
				var distance = heading.magnitude;
				var dir = heading / distance;
				RaycastHit2D hit = Physics2D.Raycast(player.transform.position, dir, grabDistance, (1 << 9));
                if ((hit != null) && (hit.transform != null))
                {
                    canGrab = false;
                    TetherLocation = new Vector2(hit.transform.position.x, hit.transform.position.y);
                    TetherDistance = Vector2.Distance(hit.transform.position, player.transform.position);
                    PlaceBlobOnTetherPoint();
                    DrawTether(player);
                    ConfigureDistanceJoint();
                    Movement.CanMoveVertical = true;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("In mouse phase");
                player.gameObject.GetComponent<DistanceJoint2D>().connectedAnchor = Vector2.zero;
                player.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
                canGrab = true;
                TetherLocation = Vector2.zero;
                // Destroy placed blob
                DestroyBlobOnTetherPoint();
                // Clear the line renderer
                lineRenderer.numPositions = 0;
                Movement.CanMoveVertical = false;
            }
            /*else if ((Input.GetTouch(0).position != null) && canGrab)
            {
                Debug.Log("In touch phase");
                Vector2 target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
                Vector2 heading = target - playerPos;
                var distance = heading.magnitude;
                var dir = heading / distance;
                RaycastHit2D hit = Physics2D.Raycast(player.transform.position, dir, grabDistance, (1 << 9));
                if ((hit != null) && (hit.transform != null))
                {
                    canGrab = false;
                    TetherLocation = new Vector2(hit.transform.position.x, hit.transform.position.y);
                    PlaceBlobOnTetherPoint(TetherLocation);
                    player.GetComponent<Rigidbody2D>().gravityScale = 0;
                    DrawTether(player);
                    Movement.CanMoveVertical = true;
                }
            }
            else if (Input.GetTouch(0).position != null)
            {
                Debug.Log("In touch phase");
                canGrab = true;
                TetherLocation = Vector2.zero;
                // Destroy placed blob
                DestroyBlobOnTetherPoint(TetherLocation);
                player.GetComponent<Rigidbody2D>().gravityScale = gravity;
                // Clear the line renderer
                lineRenderer.numPositions = 0;
                Movement.CanMoveVertical = false;
            }*/
		}
	}

    private void ConfigureDistanceJoint()
    {
        var joint = player.gameObject.GetComponent<DistanceJoint2D>();
        joint.anchor = player.transform.position;
        joint.connectedAnchor = TetherLocation;
        joint.enabled = false;
    }

    private void PlaceBlobOnTetherPoint()
    {
        sprGameObj = new GameObject();
        sprGameObj.name = "tetherBlob";
        sprGameObj.AddComponent<SpriteRenderer>();
        SpriteRenderer sprRenderer = sprGameObj.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = tetherBlob;
        sprRenderer.transform.localScale = new Vector3(5, 5);
        sprRenderer.transform.position = TetherLocation;
    }

    private void DestroyBlobOnTetherPoint()
    {
        var renderer = sprGameObj.GetComponent<SpriteRenderer>();
        renderer.sprite = null;
        sprGameObj = null;
    }

    private void DrawTether(GameObject slime)
    {
        Vector3[] lineEnds = new Vector3[2];

        lineEnds[0] = new Vector3(slime.transform.position.x, slime.transform.position.y);
        lineEnds[1] = new Vector3(TetherLocation.x, TetherLocation.y);
        lineRenderer.numPositions = 2;
        lineRenderer.SetPositions(lineEnds);
    }

    private void SetLineRendererProperties()
    {
        if(gameObject.GetComponent<LineRenderer>() != null)
        {
            lineRenderer = new LineRenderer();
        }
        else
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = tetherWidth;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }
}
