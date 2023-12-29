using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LevelPlus.Utility;

public class ClientConfig : ModConfig
{
  public static ClientConfig Instance => ModContent.GetInstance<ClientConfig>();
  
  public override ConfigScope Mode => ConfigScope.ClientSide;
  
  [DefaultValue(true)]
  [Header("xpBar")]
  public bool xpBarLocked;
  
  [DefaultValue(480)]
  public int xpBarLeft;
  
  [DefaultValue(35)]
  public int xpBarTop;
  
  [DefaultValue(true)]
  [Header("textPopups")]
  public bool enablePopups;
}