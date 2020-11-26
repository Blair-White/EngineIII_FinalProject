using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _PlayerController : MonoBehaviour
{
    public bool inEncounterTiles, lockPlayer;
    private Rigidbody2D mRig;
    public float mSpeed;
    public float EncounterRate;
    private float EncounterRng;
    private int EncounterThrottle;
    private GameObject FadePanel;
    public Animator animator;
    public float mExp;
    public int mLevel;
    private GameObject xpBar, xpLabel;
    public GameObject lvlup;
    private bool leveling;
    private int levelingcount;
    // Start is called before the first frame update
    void Start()
    {
        this.SendMessage("LoadData");
        EncounterRate = 0.998f;
        mRig = GetComponent<Rigidbody2D>();
        GameObject o = GameObject.Find("PersistentManager");
        o.SendMessage("GetExp");
        this.SendMessage("SetExp", mExp);
        if (mExp >= 1.0f)
        {
            
            if (mLevel == 1)
            {
                mLevel = 2;
                LevelUp();
                mExp = 0;
            }

            if (mLevel > 1) mExp = 1.0f;
        }    
        xpBar = GameObject.Find("ExpHolder");
        xpLabel = GameObject.Find("xpLabel");
        xpBar.transform.localScale = new Vector3(mExp, xpBar.transform.localScale.y, 1);
        xpLabel.GetComponent<Text>().text = "Level " + mLevel;
        
    }

    void SetLevel(int lvl)
    {
        mLevel = lvl;
    }
    void LevelUp()
    {
        lvlup.SetActive(true);
        Debug.Log("Leveled up");
        this.SendMessage("SetAbility", 1);
        leveling = true;
    }
    void _LoadExp(float xp)
    {
        mExp = xp;
    }

    // Update is called once per frame
    void Update()
    {
        if(leveling)
        {
            levelingcount++;
            if(levelingcount > 900)
            {
                lvlup.SetActive(false);
                leveling = false;
                levelingcount = 0;
            }
        }
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, vAxis, 0);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (!lockPlayer)
            transform.position = transform.position + movement * mSpeed * Time.deltaTime;

        if (hAxis > 0 || vAxis > 0)
        {
            EncounterThrottle++;
            if(inEncounterTiles&&EncounterThrottle>7)
            {
                EncounterRng = Random.value;
                EncounterThrottle = 0;
                if (EncounterRng > EncounterRate)
                {
                    EncounterRng = 0;
                    FadePanel = GameObject.FindGameObjectWithTag("FadePanel");
                    FadePanel.SendMessage("FadeOutNow");
                    lockPlayer = true;
                    this.SendMessage("SavePosition");
                    this.SendMessage("SavePlayerStats");
                    this.SendMessage("SaveAbilities");
                    GameObject sManager = GameObject.Find("SceneController");
                    sManager.SendMessage("MoveToEncounter");
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EncounterTile")
        {
            inEncounterTiles = false;
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EncounterTile")
        {
            inEncounterTiles = true;
        }
    }
}
