using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	static class DHCharacter
	{
		public static Stat WS = new Stat(0, 0);
		public static Stat BS = new Stat(0, 0);
		public static Stat S = new Stat(0, 0);
		public static Stat T = new Stat(0, 0);
		public static Stat Ag = new Stat(0, 0);
		public static Stat Int = new Stat(0, 0);
		public static Stat Per = new Stat(0, 0);
		public static Stat Wp = new Stat(0, 0);
		public static Stat Fel = new Stat(0, 0);

		public static int Wounds { get; set; }
		public static int Fate { get; set; }
		public static int Insanity { get; set; }
		public static int Corruption { get; set; }

		public static Origin Origin = new Origin("","");
		public static Career Career = new Career("","");
		public static Background Background = new Background("","");

		public static List<Trait> Traits = new List<Trait>();
		public static List<Skill> Skills = new List<Skill>();
		public static List<Talent> Talents = new List<Talent>();
		public static List<Equipment> Gear = new List<Equipment>();

		public static int Thrones { get; set; }
		public static string Income { get; set; }

		public static string Name { get; set; }
		public static string NameType { get; set; }
		public static string Sex { get; set; }
		public static string Build { get; set; }
		public static string Age { get; set; }
		public static string Skin { get; set; }
		public static string Hair { get; set; }
		public static string Eye { get; set; }
		public static string Quirk { get; set; }
		public static string Bio { get; set; }

		public static void Reset()
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

			Wounds = 0;
			Fate = 0;
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
