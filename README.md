Game Title: Dreadnauts
_________________________________________________________________________
Gameplay Mechanics

Time-Based Gameplay: A timer constantly counts down from one minute. Players must strategically navigate the dungeon to reset the timer by reaching certain locations, using gold/currency. Resetting the timer becomes progressively more expensive.

Enemy Difficulty Scaling: If the timer hits zero, enemies become significantly stronger, making survival difficult. Players must then escape the dungeon with their loot.

Boss Encounter: Players' main goal is to defeat the boss at the end of the dungeon to halt the timer and obtain substantial loot. After defeating the boss, players can explore the dungeon without time pressure.

_________________________________________________________________________
Camp Mechanics

Camp Hub: After completing a dungeon, players return to camp where they can:

Rest to regain full health.

Upgrade gear at the blacksmith.

Purchase items from a merchant.

Gain extra temporary health by spending time with a prostitute (cost gold).

Gamble gold/currency at blackjack and/or texas hold'em tables (texas hold'em available only in multiplayer).

_________________________________________________________________________
Progression Systems

Leveling System: Players can spend points on attributes such as health, stamina, magic, strength, dexterity, intelligence, and luck, similar to Elden Ring's leveling system.

Weapon Variety: Players can choose from multiple weapons, including basic swords, polearms, axes, greatswords, katanas, firearms (e.g., Glock, AK47, Sniper), and explosive weapons (e.g., RPG, nuke). Weapons can be upgraded at the blacksmith.

_________________________________________________________________________
Dungeon Mechanics

Procedural Generation: Dungeons are procedurally generated with premade rooms, ensuring each playthrough is unique.

Scaling Difficulty and Loot: Enemies and loot scale based on the number of completed dungeons. Subsequent dungeons feature higher-level enemies and rarer loot, incentivizing progression and exploration.

_________________________________________________________________________
Multiplayer Mechanics

Cooperative Play: Multiplayer co-op mode allows up to 4 players to team up and explore dungeons together. Proximity chat will be feature.

Friendly Fire: nuff said.

_________________________________________________________________________
thx to chat for organizing this template <3

_________________________________________________________________________
(March 2, 2024)

Combat Update 1!!!

-Added Katana

-Basic Enemy AI WIP

-Weapon Knockback

-Damage Numbers Popups

![CombatDemo-ezgif com-video-to-gif-converter](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/7df759ee-03dd-4f42-85bd-f6c8f6026f99)

The character teleportation that looks like they are snapping around every now and then is the dash ability. It has no animation yet.

_________________________________________________________________________
(March 10, 2024)

Another Combat Update!1!!!1!

Changes Since Last Update:

-Improved Enemy AI (Now uses context steering)

-Added Health Bar, Stamina Bar, and Mana Bar (When hit, health and stamina will both deplete. Health cannot regen naturally, Stamina regens over time, Mana regens when landing hits with melee weapons)

-Added Parry and Block Mechanic (Parry last for a short time (0.25s). Parry negates all incoming damage and depletes enemy stamina. Block follows right after a parry and can be held indefinitely, but will only halve the damage taken for both health and stamina)

-Added Hit Pause (Screen pauses for a split second when landing a heavy blow/stance break)

-Added Weapon Switch (Only weapons available rn are sword and katana)

-Added Stance Break (When player's or enemy's stamina reaches below zero, they will be forced into a stunned state where they cannot move. While in this state, they take double the damage)

![2024-03-1001-40-39-ezgif com-video-to-gif-converter](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/d8a978e2-7d8b-45b4-a4ac-414693b4ad49)

_________________________________________________________________________
(March 17, 2024)

HORY SHI ENVIRONMENT UPDATE JUST DROPPED!!!!

Changes Since Last Update:

-Updated to Universal Render Pipeline Graphics (basically means we have updated graphics and have the ability to make the game way more beautiful than before)

-Added a ton of new assets (tilesets, sound fx, sprites)

-Overhaul for dungeon tile set. Replaced old tile set with a new tile set that looks better for the envisioned theme of the game. All room layouts have been unchanged and are still temporary

-Added light sources that cast shadows for limited amount of objects (WIP)

-Added some sound fx (rn only footsteps WIP)

![2024-03-1723-31-08-ezgif com-optimize](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/addf894b-0b94-4596-b4c0-cd51c5420d37)

The compression kinda ruined the lighting for the torches, but it looks better in-game, trust

_________________________________________________________________________
(March 29, 2024)

RAAAAAHHHHH MERICA BAAAABY!!!!! THE LORD SAID WE HAVE THE RIGHT TO BEAR ARMS!!!!!!! GOD BLESS AMERICA CUZ THE GUN UPDATE IS HERE!!!!!

Changes Since Last Update:

Front End

-Added AK47 (Guns have been added and are set with these features that can all be customized: bullet speed, bullet damage, stamina damage, friction, ricochet WIP, pierce WIP, raycast WIP, knockback power, knockback delay)

-Added More Feedback Events (This includes new VFX and SFX to charcters and weapons.)

-Added Enemy Death Animation (Enemies will now dissolve into nothingness once they are killed.)

-Tweaked Damage Taken Animation (Sprites will now flash white instead of red when taking damage.)

-Updated Lighting (Made global lighting more visiable and tweaked casted shadows. It was too damn dark last time.)

-Camera Tweaks (Added bloom for post processing effects. Added slight camera shaking to invoke intensity.)

-Changed Text Popup (Changed "Stance Break" popup to "Broken" to reduce text on screen & damage number popup has new popup animation.)

Back End

-Added More Unity Events (Events are more accessable in the inspector and no longer need to be set in scripts.)

-Data Overhaul (Each enemy, character, weapon, etc. will have their own specific data instead of hardcoded stats. Enemies, guns, and bullets all have their data that can be customized within unity.)

-Updated Sprite Materials (Basic charcter sprite now uses shader graphs to update in-game sprites.)

-Added DOTween Library (This lib helps out with animations within scripts.)

![2024-03-2823-56-19-ezgif com-optimize](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/89891a47-91a0-46d7-9567-88f58fe1798b)

_________________________________________________________________________
(April 6, 2024)

BOOOOOOOM!!!!! MOAR GUNS JUST DROPPED!! BRRRRRRRRRRRRR!!!! BLASTING YOU WITH A NEW UPDATE RIGHT HERE!!

Changes Since Last Update:

-Added 3 New Guns!! (Shotgun, Sniper, RPG)

-Added New Gun/Bullet Data Modifiers (Piercing: Shoot through multiple enemies with one bullet, Explosion: On impact, make a customized aoe that damages anything within, Ricochet: Bullets bounce off of walls WIP)

-Added Healing Mechanic (Player can regen health by picking up health packs WIP)

-Enemy Drops (Enemies now have a chance to drop items such as ammo or health WIP)

-Enemy Spawn (Enemies can now have a dedicated spawn location inside a room WIP)

-Tweaked Health and Stamina Handlers (Health and Stamina can now be dynamically changed throughout the game aka no longer hard coded) 

Dev Note: A lot of things this update are WIP, but the main mechanics for these features are working. More progress for the game is needed in order for these features to be "complete".

Shotgun Gameplay

![2024-04-0601-15-08-ezgif com-optimize](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/f715049d-e6d3-4d18-b364-1de20f13c57d)


Sniper Gameplay

![2024-04-0601-00-11-ezgif com-optimize](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/68c4a842-9357-4dd1-a475-2c92f508e7da)


RPG Gameplay

![2024-04-0601-00-56-ezgif com-optimize](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/c57b3e40-1a8f-4ab9-8948-28425f720391)

_________________________________________________________________________
(May 7, 2024)

It's been a while since the last log; that's mainly because I've been working on my AI Boyfriend project. Here's the repo if you wanna check it out (https://github.com/AI-Boyfriend/Full-Stack). I got very addicted to working on that project that I put this game on the side for the time being. But now that I'm done with that project, I can put some more time into this game. But will I finish it? That is a very good question. Here's the new Update!

Added so much stuff that I'm just gonna call this the VARIETY UPDATE!

Changes Since Last Update That Was 5 Billion Years Ago:

-Added Fireball Spell (Player can cast a fireball as spell. Fireball is a projectile and upon impoact will cause an explosion that has an AOE. This will use up some of the player's mana. Players can use mmelee attacks on enemies to replenish lost mana. This should encourage the player to adapt to an aggresive playstyle, by always attacking the enemy.)

-Added Sprinting (Players can now go through the dungeon faster with sprinting. Sprinting uses up stamina overtime, but will give the player a decent speed boost that is worth the stamina loss.)

-Changed Item Drops (Items dropped from enemies now have a limited time to be picked up by the player. The items will start to blink when they are about to despawn. This reduces lags since the world scene will not be cluttered from dropped items that were not picked up by the player.)

-Added Gold (Gold drops after every enemy kill. Picking up the gold will give you a random amount of gold in a set range.)

-Added Clock World Object (Players can find clocks within the dungeon and reset the timer back to 60 seconds for a price. The price has a low base amount and will increase after every reset. This will encourage the player keep the aggresive playstyle by killing and looting more enemies to keep the timer from reaching 0 seconds.)

-Added Basic Main Menu Screen (Works good enough to launch the main game scene, quit the game, and shows title design)

-Added Enemy Spawn Locations In Procedurally Generated Rooms (All of the rooms are now populated with enemies. Each room has a set amount of enemies that will spawn in that room. The spawn rate allows for a steady flow of enemies.)

-Updated HUD (Player health, stamina, and mana bars now have numbers to show the amount of each bar has. The player no longer has to guess their player status.)


Currently Working On:

-Improving Overall World Design (Rooms will no longer have the basic and barebones design. They will be more fleshed out and will be overall nicer to look at, at least I hope that's the case.)

-Hub Area (The area before players set foot into dungeons, the hub is the safe place for players to stay at. They can upgrade their gear, buy items, learn spells, gamble their gold, and visit the local "healer" to gain extra temporary hp.)


Reseting The Timer Using The Clock

![2024-05-0722-49-55-ezgif com-optimize](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/d1629bc7-5d65-4f4f-9239-8516a25d02ae)

Fireball Spell

![2024-05-0722-53-40-ezgif com-video-to-gif-converter](https://github.com/BrandonLeho/2D-Dungeon-Crawler/assets/89223038/313528c5-006f-418b-9048-e6fbbef9e677)
