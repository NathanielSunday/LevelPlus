// Copyright (c) Bitwiser.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using Terraria.ModLoader;

namespace LevelPlus.Network.Packets;
/// <summary>
/// Place member variables inside of packet and use methods to read, handle, and send
/// </summary>
public abstract class BasePacket
{
  protected int WhoAmI { get; set; }
  public int ToClient { get; set; } = -1;
  public int IgnoreClient { get; set; } = -1;
  
  /// <summary>Read and handle the packet.</summary>
  /// <param name="reader">Mod.HandlePacket's reader</param>
  /// <param name="whoAmI">Mod.HandlePacket's whoAmI</param>
  public void Read(BinaryReader reader, int whoAmI)
  {
    WhoAmI = whoAmI;
    OnRead(reader);
    Handle();
  }
  
  /// <summary>Send the packet once the properties are filled.</summary>
  public void Send()
  {
    ModPacket packet = ModContent.GetInstance<LevelPlus>().GetPacket();
    //I may have to log this, but I've been told this should get the derivative
    packet.Write(GetType().ToString());
    OnSend(ref packet);
    packet.Send(ToClient, IgnoreClient);
  }
  
  /// <summary>Only read values and set properties here.</summary>
  /// <remarks>whoAmI is already handled, just call this.whoAmI to retrieve it</remarks>
  /// <param name="reader">The accessor to the bytestream</param>
  protected abstract void OnRead(BinaryReader reader);
  
  /// <summary>After the packet is read and properties are assigned, handle whatever else it does here.</summary>
  protected abstract void Handle();
  
  /// <summary>Populate the packet on send.<br/>Make sure "toClient" and "ignoreClient" are where you want them.</summary>
  protected abstract void OnSend(ref ModPacket packet);
}