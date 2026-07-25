using System.Linq;
using LevelPlus.UI;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace LevelPlus.Systems;

public class ExperienceSortSystem : ModSystem
{
    private static ExperienceSortStep sort;

    // // COULD NOT get bestiary to add the entry here, so I ended up having to use the Merge hook
    // public override void PostSetupContent()
    // {
    //     //     var sortStep = new ExperienceSortStep();
    //     Main.BestiaryDB?.Register(sort);
    //     Main.BestiaryDB?.SortSteps.Add(sort);
    // }

    public override void Load()
    {
        sort = new ExperienceSortStep();
        On_BestiaryDatabase.Merge += InjectSortStep;
    }
    
    public override void Unload()
    {
        On_BestiaryDatabase.Merge -= InjectSortStep;
        sort = null;
    }
    
    private void InjectSortStep(On_BestiaryDatabase.orig_Merge orig, BestiaryDatabase self,
        ItemDropDatabase dropsDatabase)
    {
        if (self.SortSteps.Contains(sort)) return;
        self.Register(sort);
    }
}

internal class ExperienceSortStep : IBestiarySortStep
{
    public bool HiddenFromSortOptions => false;
    public string GetDisplayNameKey() => ModContent.GetInstance<LevelPlus>().GetLocalizationKey("Bestiary.Sort");

    public int Compare(BestiaryEntry x, BestiaryEntry y)
    {
        var xVal = (x?.Info.FirstOrDefault(e => e is BestiaryExperienceElement) as BestiaryExperienceElement)
            ?.Experience ?? 0;
        var yVal = (y?.Info.FirstOrDefault(e => e is BestiaryExperienceElement) as BestiaryExperienceElement)
            ?.Experience ?? 0;
        return yVal - xVal;
    }
}