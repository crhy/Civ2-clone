---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by R-105.
--- DateTime: 2022-03-06 10:20 PM
---
--Nothing
--Palace
local palace = civ.getImprovement(1)
palace.Effects.Add(civ.core.Effects.Unique, 1);
palace.Effects.Add(civ.core.Effects.Capital, 0);

--Barracks
civ.getImprovement(2).Effects.Add(civ.core.Effects.Veteran, civ.core.UnitDomain.Land)

--Granary
civ.getImprovement(3).Effects.Add(civ.core.Effects.FoodStorage, 50)

--Temple,                   4,  1,    Cer,
civ.getImprovement(4).Effects.Add(civ.core.Effects.ContentFace, 2)
--MarketPlace,              8,  1,    Cur,
civ.getImprovement(5).Effects.Add(civ.core.Effects.TaxMultiplier, 50)
civ.getImprovement(5).Effects.Add(civ.core.Effects.LuxMultiplier, 50)
--Library,                  8,  1,    Wri,
civ.getImprovement(6).Effects.Add(civ.core.Effects.ScienceMultiplier, 50)
--Courthouse,               8,  1,    CoL,
--City Walls,               8,  0,    Mas,
civ.getImprovement(8).Effects.Add(civ.core.Effects.Walled, 100);
--Aqueduct,                 8,  2,    Cst,
--Bank,                     12, 3,    Ban,
civ.getImprovement(10).Effects.Add(civ.core.Effects.TaxMultiplier, 50)
civ.getImprovement(10).Effects.Add(civ.core.Effects.LuxMultiplier, 50)
--Cathedral,                12, 3,    MT,
--University,               16, 3,    Uni,
civ.getImprovement(12).Effects.Add(civ.core.Effects.ScienceMultiplier, 50)
--Mass Transit,             16, 4,    MP,
civ.getImprovement(13).Effects.Add(civ.core.Effects.EliminatePopulationPollution, 1)
--Colosseum,                10, 4,    Cst,
--Factory,                  20, 4,    Ind,
--Manufacturing Plant,      32, 6,    Rob,
--SDI Defense,              20, 4,    Las,
--Recycling Center,         20, 2,    Rec,
--Power Plant,              16, 4,    Ref,
--Hydro Plant,              24, 4,    E2,
--Nuclear Plant,            16, 2,    NP,
--Stock Exchange,           16, 4,    Eco,
civ.getImprovement(22).Effects.Add(civ.core.Effects.TaxMultiplier, 50)
civ.getImprovement(22).Effects.Add(civ.core.Effects.LuxMultiplier, 50)
--Sewer System,             12, 2,    San,
--Supermarket,               8, 3,    Rfg,
--Superhighways,            20, 5,    Aut,
--Research Lab,             16, 3,    Cmp,
civ.getImprovement(26).Effects.Add(civ.core.Effects.ScienceMultiplier, 50)
--SAM Missile Battery,      10, 2,    Roc,
--Coastal Fortress,          8, 1,    Met,
--Solar Plant,              32, 4,    Env,
civ.getImprovement(29).Effects.Add(civ.core.Effects.PopulationPollutionModifier, -1)
civ.getImprovement(29).Effects.Add(civ.core.Effects.EliminateIndustrialPollution, 1)
--Harbor,                   6,  1,    Sea,
--Offshore Platform,        16, 3,    Min,
--Airport,                  16, 3,    Rad,
civ.getImprovement(32).Effects.Add(civ.core.Effects.Veteran, civ.core.UnitDomain.Air)
--Police Station,           6,  2,    Cmn,
--Port Facility,            8,  3,    Amp,
civ.getImprovement(34).Effects.Add(civ.core.Effects.Veteran, civ.core.UnitDomain.Sea)