using Terraria.UI;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics;

namespace levelplus.UI
{
	internal class LevelUI : UIState
	{
		public static bool visible;

		const float CIRCLE_RATIO = 310f / 256f;

		private LevelButton Con;
		private LevelButton Str;
		private LevelButton Int;
		private LevelButton Cha;
		private LevelButton Dex;

		private LevelButton Mob;
		private LevelButton Exc;
		private LevelButton Ani;
		private LevelButton Gra;
		private LevelButton Mys;

		private string conText;
		private string strText;
		private string intText;
		private string chaText;
		private string dexText;

		private string mobText;
		private string excText;
		private string aniText;
		private string graText;
		private string mysText;

		private UICircleHollow combat;
		private UIText unspentPoints;

		private Player player;
		private levelplusModPlayer modPlayer;

		public override void OnInitialize()
		{
			visible = false;
			float widthCenter = (float)Main.screenWidth / 2;
			float heightCenter = (float)Main.screenHeight / 2;

			conText = "";
			strText = "";
			intText = "";
			chaText = "";
			dexText = "";

			combat = new UICircleHollow();
			combat.Width.Set(200 * CIRCLE_RATIO, 0f);
			combat.Height.Set(200 * CIRCLE_RATIO, 0f);
			combat.SetPadding(0);
			combat.Left.Set(widthCenter - combat.Width.Pixels / 2f, 0f);
			combat.Top.Set(heightCenter - combat.Height.Pixels / 2f, 0f);

			float m1 = 100f * CIRCLE_RATIO - 25f;
			float m2 = 75f * CIRCLE_RATIO - 19f;
			float c1 = (m1 * (float)Math.Cos(2 * Math.PI / 5));
			float c2 = (m1 * (float)Math.Cos(Math.PI / 5));
			float c3 = c1;
			//float c3 = (m2 * (float)Math.Cos(2 * Math.PI / 5));
			float c4 = (m2 * (float)Math.Cos(Math.PI / 5));
			float s1 = (m1 * (float)Math.Sin(2 * Math.PI / 5));
			float s2 = (m1 * (float)Math.Sin(4 * Math.PI / 5));
			float s3 = s1;
			//float s3 = (m2 * (float)Math.Sin(2 * Math.PI / 5));
			float s4 = (m2 * (float)Math.Sin(4 * Math.PI / 5));

			unspentPoints = new UIText("-1");
			unspentPoints.SetPadding(0);
			unspentPoints.Width.Set(50f, 0f);
			unspentPoints.Height.Set(50f, 0f);
			unspentPoints.Top.Set(m1 + 13f, 0f);
			unspentPoints.Left.Set(m1 - 1f, 0f);
			unspentPoints.OnClick += new MouseEvent(CloseMenu);
			combat.Append(unspentPoints);


			//bottom
			Ani = new LevelButton(ButtonMode.ANI, 38, 38);
			Ani.Left.Set(m1 + 6f, 0f); //adds the diffrence between 25 and 19
			Ani.Top.Set(m1 + m2 + 4f, 0f);
			Ani.OnClick += new MouseEvent(AniSpend);
			combat.Append(Ani);

			Gra = new LevelButton(ButtonMode.GRA, 38, 38);
			Gra.Left.Set(m1 + s1 - 19f, 0f);
			Gra.Top.Set(m1 + c1, 0f);
			Gra.OnClick += new MouseEvent(GraSpend);
			combat.Append(Gra);
			
			Mob = new LevelButton(ButtonMode.MOB, 38, 38);
			Mob.Left.Set(m1 + s2 - 9f, 0f);
			Mob.Top.Set(m1 - c2 + 25f, 0f);
			Mob.OnClick += new MouseEvent(MobSpend);
			combat.Append(Mob);
			
			Exc = new LevelButton(ButtonMode.EXC, 38, 38);
			Exc.Left.Set(m1 - s2 + 20f, 0f);
			Exc.Top.Set(m1 - c2 + 26f, 0f);
			Exc.OnClick += new MouseEvent(ExcSpend);
			combat.Append(Exc);
			
			Mys = new LevelButton(ButtonMode.MYS, 38, 38);
			Mys.Left.Set(m1 - s1 + 29f, 0f);
			Mys.Top.Set(m1 + c1 - 4f, 0f);
			Mys.OnClick += new MouseEvent(MysSpend);
			combat.Append(Mys);
			

			//top
			Con = new LevelButton(ButtonMode.CON, 50, 50);
			Con.Left.Set(m1, 0f);
			Con.Top.Set(0, 0f);
			Con.OnClick += new MouseEvent(ConSpend);
			combat.Append(Con);

			//top left
			Str = new LevelButton(ButtonMode.STR, 50, 50);
			Str.Left.Set(m1 - s1, 0f);
			Str.Top.Set(m1 - c1, 0f);
			Str.OnClick += new MouseEvent(StrSpend);
			combat.Append(Str);

			//bottom left
			Int = new LevelButton(ButtonMode.INT, 50, 50);
			Int.Left.Set(m1 - s2, 0f);
			Int.Top.Set(m1 + c2, 0f);
			Int.OnClick += new MouseEvent(IntSpend);
			combat.Append(Int);

			//bottom right
			Cha = new LevelButton(ButtonMode.CHA, 50, 50);
			Cha.Left.Set(m1 + s2, 0f);
			Cha.Top.Set(m1 + c2, 0f);
			Cha.OnClick += new MouseEvent(ChaSpend);
			combat.Append(Cha);

			//top right
			Dex = new LevelButton(ButtonMode.DEX, 50, 50);
			Dex.Left.Set(m1 + s1, 0f);
			Dex.Top.Set(m1 - c1, 0f);
			Dex.OnClick += new MouseEvent(DexSpend);
			combat.Append(Dex);

			base.Append(combat);

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			
			base.Draw(spriteBatch);

			//spriteBatch.Begin();
			//checking for hover on each of the stats

			if (Main.mouseX >= (combat.Left.Pixels + Con.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Con.Left.Pixels + Con.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Con.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Con.Top.Pixels + Con.Height.Pixels))
			{
				Main.instance.MouseText(conText, 7); //lime 7
			}
			if (Main.mouseX >= (combat.Left.Pixels + Str.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Str.Left.Pixels + Str.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Str.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Str.Top.Pixels + Str.Height.Pixels))
			{
				Main.instance.MouseText(strText, 10); //red 10
			}
			if (Main.mouseX >= (combat.Left.Pixels + Int.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Int.Left.Pixels + Int.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Int.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Int.Top.Pixels + Int.Height.Pixels))
			{
				Main.instance.MouseText(intText, 9); //cyan 9
			}
			if (Main.mouseX >= (combat.Left.Pixels + Cha.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Cha.Left.Pixels + Cha.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Cha.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Cha.Top.Pixels + Cha.Height.Pixels))
			{
				Main.instance.MouseText(chaText, 6); //light purple 6
			}
			if (Main.mouseX >= (combat.Left.Pixels + Dex.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Dex.Left.Pixels + Dex.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Dex.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Dex.Top.Pixels + Dex.Height.Pixels))
			{
				Main.instance.MouseText(dexText, 8); //yellow 8
			}
			if (Main.mouseX >= (combat.Left.Pixels + Mys.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Mys.Left.Pixels + Mys.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Mys.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Mys.Top.Pixels + Mys.Height.Pixels))
			{
				Main.instance.MouseText(mysText, -1);
			}
			if (Main.mouseX >= (combat.Left.Pixels + Mob.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Mob.Left.Pixels + Mob.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Mob.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Mob.Top.Pixels + Mob.Height.Pixels))
			{
				Main.instance.MouseText(mobText, -1);
			}
			if (Main.mouseX >= (combat.Left.Pixels + Ani.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Ani.Left.Pixels + Ani.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Ani.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Ani.Top.Pixels + Ani.Height.Pixels))
			{
				Main.instance.MouseText(aniText, -1);
			}
			if (Main.mouseX >= (combat.Left.Pixels + Exc.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Exc.Left.Pixels + Exc.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Exc.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Exc.Top.Pixels + Exc.Height.Pixels))
			{
				Main.instance.MouseText(excText, -1); 
			}
			if (Main.mouseX >= (combat.Left.Pixels + Gra.Left.Pixels) && Main.mouseX <= (combat.Left.Pixels + Gra.Left.Pixels + Gra.Width.Pixels) && Main.mouseY >= (combat.Top.Pixels + Gra.Top.Pixels) && Main.mouseY <= (combat.Top.Pixels + Gra.Top.Pixels + Gra.Height.Pixels))
			{
				Main.instance.MouseText(graText, -1); 
			}

			//spriteBatch.End();

		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.player[Main.myPlayer];
			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

			//stat buffs (I should really make a player variable for this)

			conText = "Constitution:\n\n"
				+ "  +" + (modPlayer.getCon() * 5) + " life\n"
				+ "  +" + (modPlayer.getCon() / 2) + " defense\n"
				+ "  +" + (modPlayer.getCon() / 25) + " life regen";
			strText = "Strength:\n\n"
				+ "  +" + (modPlayer.getStr() / 2) + "% melee damage\n"
				+ "  +" + (modPlayer.getStr() / 20) + "% melee crit chance\n"
				+ "  +" + (modPlayer.getStr() / 2) + "% throwing velocity";
			intText = "Intelligence:\n\n"
				+ "  +" + (modPlayer.getInt() / 2) + "% magic damage\n"
				+ "  +" + (modPlayer.getInt() / 20) + "% magic crit chance\n"
				+ "  +" + (modPlayer.getInt() * 2) + " max mana\n"
				+ "  +" + (modPlayer.getInt() / 25) + " mana regen";
			chaText = "Charisma:\n\n"
				+ "  +" + (modPlayer.getCha() / 2) + "% minion damage\n"
				+ "  +" + (modPlayer.getCha() / 30) + " minion slots";
			dexText = "Dexterity:\n\n"
				+ "  +" + (modPlayer.getDex() / 2) + "% ranged damage\n"
				+ "  +" + (modPlayer.getDex() / 20) + "% ranged crit chance\n";
			mobText = "Mobility:\n\n";
			excText = "Excavation:\n\n";
			aniText = "Animalia:\n\n";
			graText = "Grace:\n\n";
			mysText = "Mysticism:\n\n";

			unspentPoints.SetText("" + modPlayer.getUnspentPoints());

			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}

			base.Update(gameTime);
		}

		//close the menu when center is clicked
		private void CloseMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			visible = false;
		}

		//spending points for each of the stats

		private void AniSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.ANI);
		}

		private void MysSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.MYS);
		}

		private void GraSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.GRA);
		}

		private void ExcSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.EXC);
		}

		private void MobSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.MOB);
		}

		private void DexSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.DEX);
		}

		private void ChaSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.CHA);
		}

		private void IntSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.INT);
		}

		private void StrSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.STR);
		}

		private void ConSpend(UIMouseEvent evt, UIElement listeningElement)
		{
			player = Main.player[Main.myPlayer];
			modPlayer = player.GetModPlayer<levelplusModPlayer>();
			modPlayer.spend(ButtonMode.CON);
		}
	}
}
