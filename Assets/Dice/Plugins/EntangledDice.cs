/**
 * Copyright (c) 2010-2015, WyrmTale Games and Game Components
 * All rights reserved.
 * http://www.wyrmtale.com
 *
 * THIS SOFTWARE IS PROVIDED BY WYRMTALE GAMES AND GAME COMPONENTS 'AS IS' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL WYRMTALE GAMES AND GAME COMPONENTS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */ 
using UnityEngine;
using System.Collections;
using Qiskit;
using System.Collections.Generic;

public enum DiceType
{
	Classical = 0,
	Fermionic = 2,
	Bosonic = 1,
	FermionicEntangled = 4,
	BosonicEntangled = 3
}

public class EntangledDice : Dice {

	public static DiceType mode = DiceType.Classical;

	static int[,] classical = new int[36,2] {
		{ 1,1 }, { 1,2 }, { 1,3 }, { 1,4 }, { 1,5 }, { 1,6 },
		{ 2,1 }, { 2,2 }, { 2,3 }, { 2,4 }, { 2,5 }, { 2,6 },
		{ 3,1 }, { 3,2 }, { 3,3 }, { 3,4 }, { 3,5 }, { 3,6 },
		{ 4,1 }, { 4,2 }, { 4,3 }, { 4,4 }, { 4,5 }, { 4,6 },
		{ 5,1 }, { 5,2 }, { 5,3 }, { 5,4 }, { 5,5 }, { 5,6 },
		{ 6,1 }, { 6,2 }, { 6,3 }, { 6,4 }, { 6,5 }, { 6,6 }
	};

	static int[,] bosonic = new int[21, 2] {
		{ 1,1 }, { 1,2 }, { 1,3 }, { 1,4 }, { 1,5 }, { 1,6 },
		         { 2,2 }, { 2,3 }, { 2,4 }, { 2,5 }, { 2,6 },
		                  { 3,3 }, { 3,4 }, { 3,5 }, { 3,6 },
		                           { 4,4 }, { 4,5 }, { 4,6 },
		                                    { 5,5 }, { 5,6 },
		                                             { 6,6 }
	};

	static int[,] fermionic = new int[15, 2] {
		         { 1,2 }, { 1,3 }, { 1,4 }, { 1,5 }, { 1,6 },
				          { 2,3 }, { 2,4 }, { 2,5 }, { 2,6 },
						           { 3,4 }, { 3,5 }, { 3,6 },
								            { 4,5 }, { 4,6 },
											         { 5,6 }
	};

	static int[,] bosonicEnt = new int[6, 2] {
		{ 1,1 }, { 2,2 }, { 3,3 }, { 4,4 }, { 5,5 }, { 6,6 }
	};

	static int[,] fermionicEnt= new int[3, 2] {
		{ 1,6 }, { 2,5 }, { 3,4 }
	};

	public static Dictionary<DiceType, int[,]> distributions = new Dictionary<DiceType, int[,]>()
	{
		{ DiceType.Classical, classical },
		{ DiceType.Bosonic, bosonic },
		{ DiceType.BosonicEntangled, bosonicEnt },
		{ DiceType.Fermionic, fermionic },
		{ DiceType.FermionicEntangled, fermionicEnt }
	};

	public void Start()
    {

	}

	public static bool AssignFaceUp()
    {
		if (mode == DiceType.Classical) return true;

		if (allDice.Count < 2) return false;

		Die d1 = ((RollingDie)allDice[0]).die;
		Die d2 = ((RollingDie)allDice[1]).die;

		var list = distributions[mode];

		var index = Random.Range(0, list.Length/2);

		var die1Result = list[index,0];
		var die2Result = list[index,1];

		var one = d1.SetSide(die1Result);
		var two = d2.SetSide(die2Result);

		if(rolling)
        {
			return false;
        }

		if (d1.rolling || d2.rolling)
		{
			return false;
		}

		d1.GetValue();
		d2.GetValue();

		if(d1.value != die1Result || d2.value != die2Result)
        {
			return false;
        }

		if(one && two)
        {
			return true;
        }

		return false;
	}

	public void SetMode(string d)
    {
		mode = (DiceType)System.Enum.Parse(typeof(DiceType), d);
		Clear();
	}
}

