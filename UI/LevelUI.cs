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

		private const int SHIFT = 0;


		private LevelButton Con;
		private LevelButton Str;
		private LevelButton Int;
		private LevelButton Cha;
		private LevelButton Dex;
		private string conText;
		private string strText;
		private string intText;
		private string chaText;
		private string dexText;

		private UICircleHollow circle;
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

			circle = new UICircleHollow();
			circle.Width.Set(200 * (310f / 256f), 0f);
			circle.Height.Set(200 * (310f / 256f), 0f);
			circle.SetPadding(0);
			circle.Left.Set(widthCenter - circle.Width.Pixels / 2, 0f);
			circle.Top.Set(heightCenter - circle.Height.Pixels / 2, 0f);

			float multiplier = 100 * (310f / 256f) - 25;
			float c1 = (multiplier * (float)Math.Cos(2 * Math.PI / 5));
			float c2 = (multiplier * (float)Math.Cos(Math.PI / 5));
			float s1 = (multiplier * (float)Math.Sin(2 * Math.PI / 5));
			float s2 = (multiplier * (float)Math.Sin(4 * Math.PI / 5));

			unspentPoints = new UIText("-1");
			unspentPoints.SetPadding(0);
			unspentPoints.Width.Set(48f, 0f);
			unspentPoints.Height.Set(50f, 0f);
			unspentPoints.Top.Set(multiplier + (10 * (310f / 256f)) + SHIFT, 0f);
			unspentPoints.Left.Set(multiplier + SHIFT, 0f);
			unspentPoints.OnClick += new MouseEvent(CloseMenu);
			circle.Append(unspentPoints);

			Con = new LevelButton(ButtonMode.CON, 50, 50);
			Con.Left.Set(multiplier + SHIFT, 0f);
			Con.Top.Set(0 + SHIFT, 0f);
			Con.OnClick += new MouseEvent(ConSpend);
			circle.Append(Con);

			Str = new LevelButton(ButtonMode.STR, 50, 50);
			Str.Left.Set(multiplier - s1 + SHIFT, 0f);
			Str.Top.Set(multiplier - c1 + SHIFT, 0f);
			Str.OnClick += new MouseEvent(StrSpend);
			circle.Append(Str);

			Int = new LevelButton(ButtonMode.INT, 50, 50);
			Int.Left.Set(multiplier - s2 + SHIFT, 0f);
			Int.Top.Set(multiplier + c2 + SHIFT, 0f);
			Int.OnClick += new MouseEvent(IntSpend);
			circle.Append(Int);

			Cha = new LevelButton(ButtonMode.CHA, 50, 50);
			Cha.Left.Set(multiplier + s2 + SHIFT, 0f);
			Cha.Top.Set(multiplier + c2 + SHIFT, 0f);
			Cha.OnClick += new MouseEvent(ChaSpend);
			circle.Append(Cha);

			Dex = new LevelButton(ButtonMode.DEX, 50, 50);
			Dex.Left.Set(multiplier + s1 + SHIFT, 0f);
			Dex.Top.Set(multiplier - c1 + SHIFT, 0f);
			Dex.OnClick += new MouseEvent(DexSpend);
			circle.Append(Dex);

			base.Append(circle);

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			//checking for hover on each of the stats

			if (Main.mouseX >= (circle.Left.Pixels + Con.Left.Pixels) && Main.mouseX <= (circle.Left.Pixels + Con.Left.Pixels + Con.Width.Pixels) && Main.mouseY >= (circle.Top.Pixels + Con.Top.Pixels) && Main.mouseY <= (circle.Top.Pixels + Con.Top.Pixels + Con.Height.Pixels))
			{
				Main.instance.MouseText(conText);
			}
			if (Main.mouseX >= (circle.Left.Pixels + Str.Left.Pixels) && Main.mouseX <= (circle.Left.Pixels + Str.Left.Pixels + Str.Width.Pixels) && Main.mouseY >= (circle.Top.Pixels + Str.Top.Pixels) && Main.mouseY <= (circle.Top.Pixels + Str.Top.Pixels + Str.Height.Pixels))
			{
				Main.instance.MouseText(strText);
			}
			if (Main.mouseX >= (circle.Left.Pixels + Int.Left.Pixels) && Main.mouseX <= (circle.Left.Pixels + Int.Left.Pixels + Int.Width.Pixels) && Main.mouseY >= (circle.Top.Pixels + Int.Top.Pixels) && Main.mouseY <= (circle.Top.Pixels + Int.Top.Pixels + Int.Height.Pixels))
			{
				Main.instance.MouseText(intText);
			}
			if (Main.mouseX >= (circle.Left.Pixels + Cha.Left.Pixels) && Main.mouseX <= (circle.Left.Pixels + Cha.Left.Pixels + Cha.Width.Pixels) && Main.mouseY >= (circle.Top.Pixels + Cha.Top.Pixels) && Main.mouseY <= (circle.Top.Pixels + Cha.Top.Pixels + Cha.Height.Pixels))
			{
				Main.instance.MouseText(chaText);
			}
			if (Main.mouseX >= (circle.Left.Pixels + Dex.Left.Pixels) && Main.mouseX <= (circle.Left.Pixels + Dex.Left.Pixels + Dex.Width.Pixels) && Main.mouseY >= (circle.Top.Pixels + Dex.Top.Pixels) && Main.mouseY <= (circle.Top.Pixels + Dex.Top.Pixels + Dex.Height.Pixels))
			{
				Main.instance.MouseText(dexText);
			}
		}

		public override void Update(GameTime gameTime)
		{
			Player player = Main.player[Main.myPlayer];
			levelplusModPlayer modPlayer = player.GetModPlayer<levelplusModPlayer>();

			//stat buffs (I should really make a player variable for this)

			conText = "Constitution:\n\n"
				+ "  +" + (modPlayer.getCon() * 2) + " life\n"
				+ "  +" + modPlayer.getCon() + " defense\n"
				+ "  +" + (modPlayer.getCon() / 20) + " life regen";
			strText = "Strength:\n\n"
				+ "  +" + modPlayer.getStr() + "% melee damage\n"
				+ "  +" + (modPlayer.getStr() / 10) + "% melee crit chance\n"
				+ "  +" + modPlayer.getStr() + "% throwing damage\n"
				+ "  +" + (modPlayer.getStr() / 10) + "% throwing crit chance";
			intText = "Intelligence:\n\n"
				+ "  +" + modPlayer.getInt() + "% magic damage\n"
				+ "  +" + (modPlayer.getInt() / 10) + "% magic crit chance\n"
				+ "  +" + modPlayer.getInt() + " max mana\n"
				+ "  +" + (modPlayer.getInt() / 20) + " mana regen";
			chaText = "Charisma:\n\n"
				+ "  +" + modPlayer.getCha() + "% minion damage\n"
				+ "  +" + (modPlayer.getCha() / 10) + " minion slots\n"
				+ "  +" + modPlayer.getCha() + "% flight time";
			dexText = "Dexterity:\n\n"
				+ "  +" + modPlayer.getDex() + "% ranged damage\n"
				+ "  +" + (modPlayer.getDex() / 10) + "% ranged crit chance\n"
				+ "  +" + modPlayer.getDex() + "% move speed";

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
