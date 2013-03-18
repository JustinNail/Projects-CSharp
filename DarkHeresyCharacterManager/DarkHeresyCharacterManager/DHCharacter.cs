using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class DHCharacter
	{
		public Stat WS = new Stat(0, 0);
		public Stat BS = new Stat(0, 0);
		public Stat S = new Stat(0, 0);
		public Stat T = new Stat(0, 0);
		public Stat Ag = new Stat(0, 0);
		public Stat Int = new Stat(0, 0);
		public Stat Per = new Stat(0, 0);
		public Stat Wp = new Stat(0, 0);
		public Stat Fel = new Stat(0, 0);

		public int XP_Spent { get; set; }

		public List<Rank> Ranks = new List<Rank>();

		public int Wounds { get; set; }
		public int CurWounds { get; set; }
		public int Fate { get; set; }
		public int CurFate { get; set; }
		public int Insanity { get; set; }
		public int Corruption { get; set; }

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

			Wounds = 0;
			CurWounds = 0;
			Fate = 0;
			CurFate = 0;
			Insanity = 0;
			Corruption = 0;

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
