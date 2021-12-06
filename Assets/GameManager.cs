using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] public List<GameObject> Levels = new List<GameObject>();
    
    [ShowInInspector,ReadOnly]  public int CurrentLevelRings;
    [ShowInInspector,ReadOnly]  public int ReamaingTube;
    private int SolvedRing;

    public int currentLevel 
    {
        get {  return SolvedRing; }
        set {  SolvedRing = value; }
    }
    [ShowInInspector,ReadOnly]
    public int Level
    {
        get
        {
            return PlayerPrefs.GetInt("Level", -1);
        }
        set
        {
            PlayerPrefs.SetInt("Level", value);
        }
    }
    void Awake()=> instance = this;

    void Start()
    {
        LoadLevel();
    }
    [Button(30)]
    public void LoadLevel()
    {
        Level++;
        if (Level > 3) Level = 0;
        SolvedRing = 0;
        var levls = GameObject.FindObjectsOfType<Level>();
        if(levls.Length > 0)
        {
            for (int i = 0; i < levls.Length; i++)
            {
                Destroy(levls[i].gameObject);
            }
        }
        var currntlevel =  Instantiate(Levels[Level]);
        CurrentLevelRings = currntlevel.GetComponent<Level>().Difficuly;
        ReamaingTube = currntlevel.GetComponent<Level>().ReamaingTube;
    }
   public void function()
   {
        if (SolvedRing >= CurrentLevelRings) 
        { 
           Invoke(nameof(LoadLevel),2f);
        }
    } 
}
