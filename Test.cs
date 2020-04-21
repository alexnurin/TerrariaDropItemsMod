using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.UI;
using Terraria.ObjectData;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Test
{   
    public class MOD : Mod
    {
        public MOD()
        {
        }
    }
    public class Test : ModPlayer
	{
        const int CHANCE = 15;
        const int MAX_CHANCE = 100;
       
		public Test()
		{

        }

        public bool DropThis(int a, int b)
        {
            return Main.rand.Next(0, b+1) <= a;
        }

        public void DropI() 
        {
            //IList<Item> items = PlayerHooks.SetupStartInventory(this.player, true);
            IList<Item> items = (IList<Item>)new List<Item>();
            IDictionary<int, int> dictionary = (IDictionary<int, int>)new Dictionary<int, int>();
            foreach (Item obj in (IEnumerable<Item>)items)
            {
                if (!dictionary.ContainsKey(obj.netID))
                    dictionary[obj.netID] = 0;
                dictionary[obj.netID] += obj.stack;
            }
            //dictionary[ModContent.ItemType<StartBag>()] = 1;
            for (int index = 0; index < 59; ++index)
            {
                if (DropThis(CHANCE, MAX_CHANCE))
                {
                    Item obj = this.player.inventory[index];
                    if (obj.stack > 0 && (!dictionary.ContainsKey(obj.netID) || dictionary[obj.netID] < obj.stack))
                    {

                        int stack = obj.stack;
                        if (dictionary.ContainsKey(obj.netID))
                        {
                            stack -= dictionary[obj.netID];
                            dictionary[obj.netID] = 0;
                        }
                        int number = Item.NewItem((int)this.player.position.X, (int)this.player.position.Y, this.player.width, this.player.height, this.player.inventory[index].type, 1, false, 0, false, false);
                        Main.item[number].netDefaults(this.player.inventory[index].netID);
                        Main.item[number].Prefix((int)this.player.inventory[index].prefix);
                        Main.item[number].stack = stack;
                        Main.item[number].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[number].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].newAndShiny = false;
                        //Main.item[number].modItem = this.player.inventory[index].modItem;
                        //Main.item[number].globalItems = this.player.inventory[index].globalItems;
                        if (Main.netMode == 1)
                            NetMessage.SendData(21, -1, -1, (NetworkText)null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    
                    }
                    else if (obj.stack > 0 && dictionary.ContainsKey(obj.netID))
                        dictionary[obj.netID] -= obj.stack;
                    this.player.inventory[index] = new Item();
                }
                if (DropThis(CHANCE, MAX_CHANCE) && index < this.player.armor.Length)
                {
                    if (this.player.armor[index].stack > 0)
                    {
                        int number = Item.NewItem((int)this.player.position.X, (int)this.player.position.Y, this.player.width, this.player.height, this.player.armor[index].type, 1, false, 0, false, false);
                        Main.item[number].netDefaults(this.player.armor[index].netID);
                        Main.item[number].Prefix((int)this.player.armor[index].prefix);
                        Main.item[number].stack = this.player.armor[index].stack;
                        Main.item[number].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[number].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].newAndShiny = false;
                        //Main.item[number].modItem = this.player.armor[index].modItem;
                        //Main.item[number].globalItems = this.player.armor[index].globalItems;
                        if (Main.netMode == 1)
                            NetMessage.SendData(21, -1, -1, (NetworkText)null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                    this.player.armor[index] = new Item();
                }
                if (DropThis(CHANCE, MAX_CHANCE) && index < this.player.dye.Length)
                {
                    if (this.player.dye[index].stack > 0)
                    {
                        int number = Item.NewItem((int)this.player.position.X, (int)this.player.position.Y, this.player.width, this.player.height, this.player.dye[index].type, 1, false, 0, false, false);
                        Main.item[number].netDefaults(this.player.dye[index].netID);
                        Main.item[number].Prefix((int)this.player.dye[index].prefix);
                        Main.item[number].stack = this.player.dye[index].stack;
                        Main.item[number].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[number].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].newAndShiny = false;
                        //Main.item[number].modItem = this.player.dye[index].modItem;
                       // Main.item[number].globalItems = this.player.dye[index].globalItems;
                        if (Main.netMode == 1)
                            NetMessage.SendData(21, -1, -1, (NetworkText)null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                    this.player.dye[index] = new Item();
                }
                if (DropThis(CHANCE, MAX_CHANCE) && index < this.player.miscEquips.Length)
                {
                    if (this.player.miscEquips[index].stack > 0)
                    {
                        int number = Item.NewItem((int)this.player.position.X, (int)this.player.position.Y, this.player.width, this.player.height, this.player.miscEquips[index].type, 1, false, 0, false, false);
                        Main.item[number].netDefaults(this.player.miscEquips[index].netID);
                        Main.item[number].Prefix((int)this.player.miscEquips[index].prefix);
                        Main.item[number].stack = this.player.miscEquips[index].stack;
                        Main.item[number].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[number].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].newAndShiny = false;
                       // Main.item[number].modItem = this.player.miscEquips[index].modItem;
                        //Main.item[number].globalItems = this.player.miscEquips[index].globalItems;
                        if (Main.netMode == 1)
                            NetMessage.SendData(21, -1, -1, (NetworkText)null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                    this.player.miscEquips[index] = new Item();
                }
                if (DropThis(CHANCE, MAX_CHANCE) && index < this.player.miscDyes.Length)
                {
                    if (this.player.miscDyes[index].stack > 0)
                    {
                        int number = Item.NewItem((int)this.player.position.X, (int)this.player.position.Y, this.player.width, this.player.height, this.player.miscDyes[index].type, 1, false, 0, false, false);
                        Main.item[number].netDefaults(this.player.miscDyes[index].netID);
                        Main.item[number].Prefix((int)this.player.miscDyes[index].prefix);
                        Main.item[number].stack = this.player.miscDyes[index].stack;
                        Main.item[number].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[number].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].newAndShiny = false;
                       // Main.item[number].modItem = this.player.miscDyes[index].modItem;
                       // Main.item[number].globalItems = this.player.miscDyes[index].globalItems;
                        if (Main.netMode == 1)
                            NetMessage.SendData(21, -1, -1, (NetworkText)null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                    this.player.miscDyes[index] = new Item();
                }
            }
            Main.mouseItem = new Item();
        }

        public override bool PreKill(
            double damage,
            int hitDirection,
            bool pvp,
            ref bool playSound,
            ref bool genGore,
            ref PlayerDeathReason damageSource)
        {
            DropI();
           
            return true;
        }

    }
}
