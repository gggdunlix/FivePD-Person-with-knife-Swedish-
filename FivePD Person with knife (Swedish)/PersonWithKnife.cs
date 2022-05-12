﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace PersonWithKnife
{
    [CalloutProperties("Person med kniv", "GGGDunlix & översatt av Råven/Jackpro144", "0.1.2")]
    public class PersonWithKnife : Callout
    {
        private Ped suspect;

        public PersonWithKnife()
        {
            InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 600f)));
            ShortName = "Person med kniv";
            CalloutDescription = "En misstänkt person sågs med en kniv. Svara i kod 3.";
            ResponseCode = 3;
            StartDistance = 60f;
        }

        public override async Task OnAccept()
        {
            InitBlip();

            UpdateData();
        }

        public async override void OnStart(Ped closest)
        {

            base.OnStart(closest);

            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;

            var weapons = new[]
            {
                WeaponHash.Knife,
                WeaponHash.SwitchBlade,
                WeaponHash.Dagger,
                WeaponHash.Machete,
            };
            suspect.Weapons.Give(weapons[RandomUtils.Random.Next(weapons.Length)], int.MaxValue, true, true);
            suspect.Armor = 0;

            suspect.AttachBlip();
            Tick += TaskKnife;
        }



        private async Task TaskKnife()
        {
            if (suspect.Weapons.HasWeapon(WeaponHash.Knife))
            {
                suspect.Task.FightAgainst(Game.PlayerPed);
            }
            if (suspect.Weapons.HasWeapon(WeaponHash.SwitchBlade))
            {
                suspect.Task.FightAgainst(Game.PlayerPed);
            }
            if (suspect.Weapons.HasWeapon(WeaponHash.Dagger))
            {
                suspect.Task.FightAgainst(Game.PlayerPed);
            }
            if (suspect.Weapons.HasWeapon(WeaponHash.Machete))
            {
                suspect.Task.FightAgainst(Game.PlayerPed);
            }
        }
    }
}





