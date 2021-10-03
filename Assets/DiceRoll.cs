using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;

    public static bool rollOver = false;
    bool facedUp = false;
    float counter = 0;

    DiceType mode;

    // Start is called before the first frame update
    void Start()
    {
    }

    private Vector3 Force()
    {
        Vector3 rollTarget = Vector3.zero + new Vector3(2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
        return Vector3.Lerp(spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
    }

    private Vector2 GuiMousePosition()
    {
        Vector2 mp = Input.mousePosition;
        Vector3 vp = Camera.main.ScreenToViewportPoint(new Vector3(mp.x, mp.y, 0));
        mp = new Vector2(vp.x * Camera.main.pixelWidth, (1 - vp.y) * Camera.main.pixelHeight);
        return mp;
    }

    string randomColor
    {
        get
        {
            string _color = "blue";
            int c = System.Convert.ToInt32(Random.value * 6);
            switch (c)
            {
                case 0: _color = "red"; break;
                case 1: _color = "green"; break;
                case 2: _color = "blue"; break;
                case 3: _color = "yellow"; break;
                case 4: _color = "white"; break;
                case 5: _color = "black"; break;
            }
            return _color;
        }
    }

    public void Roll()
    {
        Dice.Clear();
        facedUp = false;
        rollOver = false;
        counter = 0;

        switch(EntangledDice.mode)
        {
            case DiceType.Classical:
                EntangledDice.Roll("1d6", "d6-white-dots", spawnPoint.transform.position, Force());
                EntangledDice.Roll("1d6", "d6-black-dots", spawnPoint.transform.position, Force()); break;
            case DiceType.Bosonic: EntangledDice.Roll("2d6", "d6-red-dots", spawnPoint.transform.position, Force()); break;
            case DiceType.Fermionic: EntangledDice.Roll("2d6", "d6-blue-dots", spawnPoint.transform.position, Force()); break;
            case DiceType.BosonicEntangled: EntangledDice.Roll("2d6", "d6-red-dots", spawnPoint.transform.position, Force()); break;
            case DiceType.FermionicEntangled: EntangledDice.Roll("2d6", "d6-blue-dots", spawnPoint.transform.position, Force()); break;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!facedUp || counter < 2.0f)
        {
            facedUp = EntangledDice.AssignFaceUp();
            counter += Time.deltaTime;
        }
        
        if(facedUp && counter > 2.0f)
        {
            rollOver = true;
        }
    }
}
