﻿using System.Collections.Generic;
using Civ2engine.IO;
using Civ2engine.Units;

namespace Civ2engine
{
    public partial class Game : BaseInstance
    {
        public static void LoadGame(Ruleset ruleset, string SAVname)
        {
            // Read SAV file & RULES.txt
            var rules = RulesParser.ParseRules(ruleset);
            GameData gameData = Read.ReadSAVFile(ruleset.FolderPath, SAVname);

            // Make an instance of a new game & map
            _instance = new Game(rules, gameData);
            Map.PopulateTitleData(gameData, rules);
            Map.MapRevealed = gameData.MapRevealed;
            Map.WhichCivsMapShown = gameData.WhichCivsMapShown;
            Map.Zoom = gameData.Zoom;
            Map.StartingClickedXY = gameData.ClickedXY;
            Map.ActiveXY = gameData.ActiveCursorXY;
        }

        private Game(Rules rules, GameData gameData)
        {
            _rules = rules;

            //_civsInPlay = SAVgameData.CivsInPlay;
            _gameVersion = gameData.GameVersion;

            _options = new Options();
            _options.Set(gameData.Options);

            _turnNumber = gameData.TurnNumber;
            TurnNumberForGameYear = gameData.TurnNumberForGameYear;
            _difficultyLevel = gameData.DifficultyLevel;
            _barbarianActivity = gameData.BarbarianActivity;
            PollutionAmount = gameData.PollutionAmount;
            GlobalTempRiseOccured = gameData.GlobalTempRiseOccured;
            NoOfTurnsOfPeace = gameData.NoOfTurnsOfPeace;
            NumberOfUnits = gameData.NumberOfUnits;
            NumberOfCities = gameData.NumberOfCities;

            // Create all 8 civs (tribes)
            for (var i = 0; i < 8; i++)
            {
                CreateCiv(i, gameData.PlayersCivIndex, gameData.CivsInPlay[i], gameData.CivCityStyle[i], gameData.CivLeaderName[i], gameData.CivTribeName[i], gameData.CivAdjective[i], gameData.RulerGender[i], gameData.CivMoney[i], gameData.CivNumber[i], gameData.CivResearchProgress[i], gameData.CivResearchingTech[i], gameData.CivSciRate[i], gameData.CivTaxRate[i], gameData.CivGovernment[i], gameData.CivReputation[i], gameData.CivTechs);
            }

            // Create cities
            for (var i = 0; i < gameData.NumberOfCities; i++)
            {
                CreateCity(gameData.CityXloc[i], gameData.CityYloc[i], gameData.CityCanBuildCoastal[i],
                    gameData.CityAutobuildMilitaryRule[i], gameData.CityStolenTech[i],
                    gameData.CityImprovementSold[i], gameData.CityWeLoveKingDay[i],
                    gameData.CityCivilDisorder[i], gameData.CityCanBuildShips[i], gameData.CityObjectivex3[i],
                    gameData.CityObjectivex1[i], gameData.CityOwner[i], gameData.CitySize[i],
                    gameData.CityWhoBuiltIt[i], gameData.CityFoodInStorage[i], gameData.CityShieldsProgress[i],
                    gameData.CityNetTrade[i], gameData.CityName[i], gameData.CityDistributionWorkers[i],
                    gameData.CityNoOfSpecialistsx4[i], gameData.CityImprovements[i],
                    gameData.CityItemInProduction[i], gameData.CityActiveTradeRoutes[i],
                    gameData.CityCommoditySupplied[i], gameData.CityCommodityDemanded[i],
                    gameData.CityCommodityInRoute[i], gameData.CityTradeRoutePartnerCity[i],
                    gameData.CityScience[i], gameData.CityTax[i], gameData.CityNoOfTradeIcons[i],
                    gameData.CityTotalFoodProduction[i], gameData.CityTotalShieldProduction[i],
                    gameData.CityHappyCitizens[i], gameData.CityUnhappyCitizens[i]);
            }

            // Create units
            for (int i = 0; i < gameData.NumberOfUnits; i++)
            {
                CreateUnit(gameData.UnitType[i], gameData.UnitXloc[i], gameData.UnitYloc[i],
                    gameData.UnitDead[i], gameData.UnitFirstMove[i], gameData.UnitGreyStarShield[i],
                    gameData.UnitVeteran[i], gameData.UnitCiv[i], gameData.UnitMovePointsLost[i],
                    gameData.UnitHitPointsLost[i], gameData.UnitPrevXloc[i], gameData.UnitPrevYloc[i],
                    gameData.UnitCaravanCommodity[i], gameData.UnitOrders[i], gameData.UnitHomeCity[i],
                    gameData.UnitGotoX[i], gameData.UnitGotoY[i], gameData.UnitLinkOtherUnitsOnTop[i],
                    gameData.UnitLinkOtherUnitsUnder[i]);
            }

            //_activeXY = SAVgameData.ActiveCursorXY; // Active unit or view piece coords (if it's active unit, you really don't need this)

            _activeUnit = gameData.SelectedUnitIndex == -1 ? null : AllUnits.Find(unit => unit.Id == gameData.SelectedUnitIndex);    // null means all units have ended turn
            _playerCiv = GetCivs[gameData.PlayersCivIndex];
            _activeCiv = _playerCiv;
        }
    }
}
