using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class DHCharacter
	{
		
		public Stat WS = new Stat();
		public Stat BS = new Stat();
		public Stat S = new Stat();
		public Stat T = new Stat();
		public Stat Ag = new Stat();
		public Stat Int = new Stat();
		public Stat Per = new Stat();
		public Stat Wp = new Stat();
		public Stat Fel = new Stat();

		public int XP_Spent { get; set; }

		public List<Rank> Ranks = new List<Rank>();

		public Stat Wounds = new Stat();
		public int CurWounds { get; set; }
		public Stat Fate = new Stat();
		public int CurFate { get; set; }
		public Stat Insanity = new Stat();
		public Stat Corruption = new Stat();

		public Origin Origin = new Origin("","");
		public Career Career = new Career("","");
		public Background Background = new Background("",0);

		public List<Trait> Traits = new List<Trait>();
		public List<Skill> Skills = new List<Skill>();
		public List<Talent> Talents = new List<Talent>();
		public List<Equipment> Gear = new List<Equipment>();

		public int Thrones { get; set; }
		public string Income { get; set; }
		
		public string Name { get; set; }
		public string NameType { get; set; }
		public string Sex { get; set; }
		public string Build { get; set; }
		public string Age { get; set; }
		public string Skin { get; set; }
		public string Hair { get; set; }
		public string Eye { get; set; }
		public string Quirk { get; set; }
		public string Bio { get; set; }

		public void Reset()
		{
			WS.Reset();
			BS.Reset();
			S.Reset();
			T.Reset();
			Ag.Reset();
			Int.Reset();
			Per.Reset();
			Wp.Reset();
			Fel.Reset();

			XP_Spent = 0;

			Wounds.Reset();
			CurWounds = 0;
			Fate.Reset();
			CurFate = 0;
			Insanity.Reset();
			Corruption.Reset();

			Career.Reset();
			Background.Reset();

			Traits.Clear();
			Skills.Clear();
			Talents.Clear();
			Gear.Clear();

			Thrones = 0;
			Income = "";

			Name = "";
			Sex = "";
			Age = "";
			Build = "";
			Skin = "";
			Hair = "";
			Eye = "";
			Quirk = "";
			Bio = "";
		}

	}
}
