// Copyright (c) BitWiser.
// Licensed under the Apache License, Version 2.0.

using System;
using LevelPlus.Core;

namespace LevelPlus.Commands
{
    internal class CommandHelper {
    //return the index of a stat name as a string
    //returns -1 if not a valid stat
    public static int GetStatIndex(string name) {
      int index = -1;
      SystemHelper.Stat stat;
      if (Enum.TryParse(name, out stat)) {
        index = (int)stat;
      }
      return index;
    }
  }
}
