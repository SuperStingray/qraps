using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfo : MonoBehaviour
{
    Text t;

    static int point;
    bool resetRoll;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        point = -1;
    }

    public void ResetRoll()
    {
        resetRoll = true;
    }

    // Update is called once per frame
    void Update()
    {
        int total = 0;
        int i = 0;
        if (!EntangledDice.rolling && EntangledDice.GetAllDice().Count > 0 && DiceRoll.rollOver == true && resetRoll == true)
        {
            resetRoll = false;
            t.text = "";
            foreach (var d in EntangledDice.GetAllDice())
            {
                var rd = (RollingDie)d;
                if (rd.value == 0) continue;

                t.text += rd.value;
                total += rd.value;
                ++i;

                if (i < EntangledDice.GetAllDice().Count)
                {
                    t.text += " + ";
                }
            }

            t.text += " = " + total;

            //Craps rules
            if(point > -1)
            {
                if(total == point)
                {
                    t.text += " You win!";
                    point = -1;
                }
                else if(total == 7)
                {
                    t.text += " You lose!";
                    point = -1;
                }    
            }
            else if (total == 7 || total == 11)
            {
                t.text += " You win!";
                point = -1;
            }
            else if (total == 2 || total == 3 || total == 12)
            {
                t.text += " You Lose!";
                point = -1;
            }
            else
            {
                point = total;
            }
        }
    }
}
