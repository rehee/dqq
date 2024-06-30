using DQQ.Combats;
using DQQ.Components.Stages.Actors.Characters;
using DQQ.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DQQ.Helper
{
	public static class CharacterHelper
	{
		public static void ResetCharacterCombatStatus(this Character character)
		{
			character.CombatPanel = new CombatPanel();
			character.CombatPanel.StaticPanel.MaximunLife = DQQGeneral.CharacterBasicHP + (DQQGeneral.HPPerLevel * (character.Level - 1));
		}
	}
}
