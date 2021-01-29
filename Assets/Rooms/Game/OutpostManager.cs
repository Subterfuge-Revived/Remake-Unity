using UnityEngine;
using SubterfugeCore.Core;
 using SubterfugeCore.Core.Entities.Positions;
 using TMPro;

 public class OutpostManager : MonoBehaviour
{

    private Animator OutpostAnimator;
    public int ID;
    private float downtime;
    private bool expanded = false;
    private TextMeshPro textMesh;

    public Outpost outpost;

    public readonly Color subOlive = new Color(0.439f, 0.463f, 0.290f);
    public readonly Color subRed = new Color(0.667f, 0.122f, 0.137f);
    public readonly Color subSky = new Color(0.482f, 0.659f, 0.851f);
    public readonly Color subOrange = new Color(0.824f, 0.553f, 0.161f);
    public readonly Color subTeal = new Color(0.353f, 0.6f, 0.541f);
    public readonly Color subBiege = new Color(0.608f, 0.565f, 0.482f);
    public readonly Color subPink = new Color(0.784f, 0.467f, 0.694f);
    public readonly Color subPurple = new Color(0.42f, 0.333f, 0.557f);
    public readonly Color subNavy = new Color(0.227f, 0.294f, 0.639f);
    public readonly Color subBrown = new Color(0.545f, 0.369f, 0.235f);

    // Start is called before the first frame update
    void Start()
    {
        OutpostAnimator = gameObject.GetComponent<Animator>();
        textMesh = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set color based on the owner
        textMesh.text = outpost.GetDrillerCount().ToString();

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int playerId = 0;
        if (outpost.GetOwner() != null)
        {
            playerId = Game.TimeMachine.GetState().GetPlayers().IndexOf(outpost.GetOwner()) + 1;
        }
        switch (playerId)
        {
            case 0:
                renderer.color = subSky;
                break;
            case 1:
                renderer.color = subRed;
                break;
            case 2:
                renderer.color = subOlive;
                break;
            case 3:
                renderer.color = subOrange;
                break;
            case 4:
                renderer.color = subPink;
                break;
            case 5:
                renderer.color = subBrown;
                break;
            case 6:
                renderer.color = subPurple;
                break;
            case 7:
                renderer.color = subBiege;
                break;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            downtime = Time.time;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            downtime = Time.time - downtime;
        }
        
        if (Input.GetMouseButton(0))
        {
            return;
        }

        if (downtime < 0.25 && downtime > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject && expanded == false)
                {
                    hit.collider.gameObject.GetComponent<OutpostManager>().Expand();
                    //gameManager.SelectedOutpost = hit.transform.GetComponent<OutpostManager>().ID;
                }
                else
                {
                    gameObject.GetComponent<OutpostManager>().Contract();
                }
            }
            else
            {
                gameObject.GetComponent<OutpostManager>().Contract();
            }
            
            downtime = 0;
        }
    }

    public void Expand()
    {
        expanded = true;
        OutpostAnimator.ResetTrigger("Contract");
        OutpostAnimator.SetTrigger("Expand");
    }
    
    public void Contract()
    {
        expanded = false;
        OutpostAnimator.ResetTrigger("Expand");
        OutpostAnimator.SetTrigger("Contract");
    }
}
