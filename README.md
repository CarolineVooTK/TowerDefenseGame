
**The University of Melbourne**

# COMP30019 – Graphics and Interaction

## Teamwork plan/summary

<!-- [[StartTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

<!-- Fill this section by Milestone 1 (see specification for details) -->

#### Members responsibilities

| Name            | Responsibility                       |
| :-------------- | :----------------------------------- |
| Caroline Voo    | Procedural Generation Map            |
|                 | Procedural General Path              |
|                 | Procedural Generation Objects        |
|                 | Creating and Designing Maps          |
|                 | Evaluation                           |
| Livya Riany     | Heads Up Display (HUD)               |
|                 | Model Chefs, Citizen, Foods          |
|                 | Audio and Sound Effect               |
|                 | Implement Citizens                   |
|                 | Particle System Collision Effects    |
|                 | Shaders                              |
| Yuji Nojima     | Implementing Chefs and Food Logic    |
|                 | Implement Shop Gacha Logic           |
|                 | Manage Bullet Effects (slow,aoe)     |
|                 | Balancing of Chef and Citizen        |
|                 | Procedural Generation Citizen        |
|                 | Upgrading and Selling Logic with HUD |

#### Meetings conducted

| Date            | Responsibility                  |
| :-------------- | :------------------------------ |
| 13 September    | Initial Planning                |
| 15 September    | Role Assignments                |
| 13 October      | Video Planning + Progress update|
| 15 October      | Video Editing                   |
| 20 October      | Progress update                 |
| 27 October      | Progress update                 |
| 31 October      | Final preperation               |

<!-- [[EndTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

## Final report

### Table of contents

* [Game Summary](#game-summary)
* [Technologies](#technologies)
* [How To Play The Game](#how-to-play-the-game)
* [Gameplay Related Design](#gameplay-related-design)
* [Graphics Pipeline](#graphics-pipeline)
* [Procedural Generation](#procedural-generation)
* [Particle System](#particle-system)
* [Evaluation](#evaluation)
* [References](#references)

### Game Summary

With the high demand of Laksa in the modern world, people are all craving to have their hunger sated, some with different needs and hangry tolerance. With enemies coming in all shapes and sizes, eg Sumos, Food Critiques, 'That' Korean MukBang Influencer, or the average Joe. Tower defense but with the emergence of gacha, you must wish for your chefs, will you risk it all in the premium gacha or the standard gacha to summon your chefs on the field. Upon Choosing a map, you will have a choice to spend your starting coins on 2 standard pulls, or 1 premium to protect your kitchen from the waves of hungry citizens. You can place your chefs on the map and upgrade them to take them closer to their potentials. Save the kitchen for as long as you can as the waves won't stop until they are sated! Be careful for the Rich tourists, or even the rush hour... See you in the laksa kitchen!

### Technologies

Project is created with:

* Unity 2022.1.9f1
* Blender version 3.3.0
* Canva

### How To Play The Game

#### 1. Select map

Theres a selection between 3 different themed maps that you could pick from; Castle, Desert, and Shore.

<p align="center">
    <img src="Gifs/start_scene.gif" width="500">
</p>

#### 2. Start the game and select a scroll (Standard Scroll or Premium Scroll)

At the start of the game you are given 500 Laksa Coins which could be used to purchase either 2 Standard Scroll or 1 Premium Scroll; a Standard Scroll costs 200 Laksa Coins and a Premium Scroll costs for 400 Laksa Coins. With a higher price, Premium Scroll would have higher odds on recruiting a more professional chef. Each chef is unique as they would feed citizens their speciality cuisine.

<p align="center">
    <img src="Gifs/select_scroll.gif" width="500">
</p>

Here are the gacha chances you would be faced when purchasing a scroll:

<p align="center">
    <img src="Assets/Images/2.jpg" width="500">
    <img src="Assets/Images/4.jpg" width="500">
</p>
* Chefs shown are not using toon shader

#### 3. Purchase scroll

Place the scroll beside the path that has different color than the base map. Usually 1 tile beside the paths of the citizen. See what chef you have recruited!

<p align="center">
    <img src="Gifs/correct_place.jpg" width="500">
</p>
<p align="center">
    <img src="Gifs/purchase_scroll.gif" width="500">
</p>

#### 4. Enjoy the chefs feeding the citizens

Let the chef feed the citizens their specialised foods. Once the citizens are full, they would exlode and give us coins.

<p align="center">
    <img src="Gifs/chef_feed.gif" width="500">
</p>

#### 5. Upgrade and sell Chefs

If you feel your chefs are too weak against the citizens, you can opt for an upgrade to increase it's stats with a price.

<p align="center">
    <img src="Gifs/upgrade_chef.gif" width="500">
</p>

Or if you are feeling unlucky, you can sell the chef and retry the gacha to test your luck.

<p align="center">
    <img src="Gifs/sell_chef.gif" width="500">
</p>

#### 6. Defend Kitchen

Use coins to purchase more scroll and test your luck to defend the kitchen! You start off with 30 lives, if a number of enemies reach the end, they will take a number of your lives. Once your life reach 0, the kitchen has already been fully invaded by the citizens.

<p align="center">
    <img src="Gifs/end_game.gif" width="500">
</p>

\* Protip: If you place the scroll on the correct tile and have enough money, you can spam and purchase multiple scrolls!

### Gameplay Related Design

#### Balancing

Many factors need to be taken into consideration to keep the game challenging and not too simple. Factors include coins dropped, damage outputted and damage tanked by the enemies. At some points the enemies got too easy, and the chefs only had one fixed dmg output. Therefore both chef and enemy scaling had to be introduced.
##### Bullet Effects
###### Favourite Food
If hit with favourite food, the enemy will slow down and turn to face the chef that gave them their favourite food. The slow effects are according to the chefs meals.
###### Explosion
If hit with an explosive food (pizza, donuts and laksa), the enemies around the radius of the enemy hit will take the same amount of damage.

##### Enemy
| Enemy  | Starting Hunger  | Speed   | Tokend dropped   | Damage to lives   | favourite food (infatuation)
| :----  | :----------- |:-------------|:-------------------------|:-------------------------|:-------------------------|
| Average Joe     | 50   | 5 | 5 | 1 | farmer, legendary
|  Marathon Cindy | 70   | 7.5 | 7 | 2 | coffee, legendary
| Sumo            | 4500  | 1.25 | 50|  5 | pizza, korean, doughnuts, legendary
|  Mukbang Kim Jong Duos     | 10500   |3 | 70|  7| doughnuts, korean chicken, sandwiches, boba
|  Critique Anton Ego      | 20000  | 4  | 100|  15 | legendary
|  Aristocrat Bill Fences   | 50000   |2.5 | 250|  39 | coffee, sandwiches, legendary
##### Wave Scaling
| Enemy  | Hunger  | Multiplier   | Every N Levels  | 
| :----  | :----------- |:------------- |:-- |
| Average Joe     | 50   | 200% | 2 |
|  Marathon Cindy | 70   | 200%| 2 |
| Sumo            | 4500  | 200%| 2
|  Mukbang Kim Jong Duos     | 6500   |250% | (wave-5) / 2|
|  Critique Anton Ego      | 10000  | 300% | (wave-15) / 5
|  Aristocrat Bill Fences   | 25000   |200% | (wave-23) / 10

| Enemy  | Starting Hunger  | Speed   | Tokend dropped   | Damage to lives   | favourite food (infatuation)|
| :----  | :----------- |:-------------|:-------------------------|:-------------------------|:-------------------------|
| Average Joe     | 50   | 5 | 5 | 1 | farmer, legendary|
|  Marathon Cindy | 70   | 7.5 | 7 | 2 | coffee, legendary|
| Sumo            | 4500  | 1.25 | 50|  5 | pizza, korean, doughnuts, legendary|
|  Mukbang Kim Jong Duos     | 6500   |3 | 70|  7| doughnuts, korean chicken, sandwiches, boba|
|  Critique Anton Ego      | 10000  | 4  | 100|  15 | legendary|
|  Aristocrat Bill Fences   | 25000   |2.5 | 250|  39 | coffee, sandwiches, legendary|

##### Wave Scaling

| Enemy  | Hunger  | Multiplier   | Every N Levels  |
| :----  | :----------- |:------------- |:-- |
| Average Joe     | 50   | 200% | 3 ||
|  Marathon Cindy | 70   | 200%| 3 | 3|
| Sumo            | 4500  | 200%| 5|
|  Mukbang Kim Jong Duos     | 6500   |170% | (wave-5) / 10|
|  Critique Anton Ego      | 10000  | 150% | (wave-15) / 10|
|  Aristocrat Bill Fences   | 25000   |150% | (wave-23) / 10|

##### Chef
There are four types of chefs, basic, fast shooter, explosion and normal. They can slow enemies if they are given their favourite foods
| Chef  |Type| Rarity | Range   | Initial Damage  | Fire Rate  | Infatuation (slow effect)  | Explosion Radius  |  Food Served  | Sell price  | 
| :----  | :----------- |:-------------|:-------------------------|:-------------------------| :----  | :----------- |:-------------|:-------------------------|:-------------------------|
| Farmer     | Basic|⭐   | 12 | 17| 3| 50% | 0| carrots, eggs, raw meat| 50 + (level * 25)|
|  Coffee| Fast| ⭐⭐⭐   | 15 | 30| 1.5| 20% | 0| coffee |100 + (level * 25)|
| Boba   |Fast |⭐⭐⭐⭐ | 23 | 35| 2 | 20%|0| boba|125 + (level * 25)|
|  Indomie|  Fast| ⭐⭐⭐⭐⭐ | 25 | 45| 3 | 60% |0| indomie| 200 + (level * 25)|
| Doughnut    |Explosion       | ⭐⭐⭐  | 15| 50|  0.9 | 70% | 3| doughnut|100 + (level * 25)|
|        Pizza|Explosion | ⭐⭐⭐⭐        |20 | 85|  0.6 | 70%|6| pizza|125 + (level * 25)|
| Laksa   | Explosion        | ⭐⭐⭐⭐⭐ | 35 | 200|  0.4 | 60%|8| laksa|200 + (level * 25)|
|         Sandwich  |Normal| ⭐⭐⭐  |15 | 55|  1.2 | 40%| 0| sandwich|100 + (level * 25)|
|        Korean     |Normal| ⭐⭐⭐⭐       |25  | 70|  1.4 | 40% |0| korean chicken |125 + (level * 25)|
|         Sushi |Normal   | ⭐⭐⭐⭐⭐  |30 | 175|  1.6 |60%| 0| sushi|200 + (level * 25)|

*note level for sell bonus isnt included if level 1

##### Chef Upgrades

| Upgrade level  | ⭐ Cost  |⭐⭐⭐ Cost  |⭐⭐⭐⭐ Cost  |⭐⭐⭐⭐⭐ Cost  | Damage   | Radius   | Explosion Radius (applicable to explosion objects only)   | Fire Rate   |  
| :----  | :----------- |:-------------|:-------------------------| :----  | :----------- |:-------------|:-------------------------|:-------------------------|
| 1   | --   | -- | --| --| --| --| --| --|
| 2 | 75   | 125 | 150| 225| 130% |--|130%|130%|
| 3 | 200  | 250 | 275|   350| 130% |130%|130%|--|
| 4 | 275   |325 | 350|   375| 130% |--|130%|130%|
| 5 | 350        |400  | 425|  450| 130% |130%|130%|130% |

upgrade formula = sellAmount+baseUpgradeAmount;
from lvl2 onwards upgrade formula = sellAmount+baseUpgradeAmount*(level*3);

#### Maps

The assets used for the maps consists of assets downloaded from the Unity Store and the chefs, citizens and food were customly modelled ourselves. The downloaded assets included buildings, trees, stones, grass, flowerd, fountains , boats etc. These are all placed in the prefab. We created 3 maps for the game, inspired by castles, beaches, the desert. The objects and paths of each map were originally fixed, however procedural generation for the maps was added later on to generate the map's path randomly. We positioned the point of view of the player in a slightly angled down bird's eye view so players could see the objects in the map clearly and also visualize what their strategy would be when playing the game.

#### Citizen, Chef, Food

The citizens, chefs, and foods are customly modeled and rendered through Blender version 3.3.0. Initially we wanted to create a 3D cartoon game which has an overhead camera. Therefore, we took heavy inspiration on the models based on the game Overcooked 2. It has the foods, people, and chefs that is very similar to our concept. Additionally, it has a overhead camera point of view so when designing we knew already  roughly how our model would look in Unity. The baseframe of the people are similar to Overcooked 2, round head and a cone with half sphere as a base body. These models only have solid colors without any texture. We wanted to keep it clean and simple.

<p align="center">
    <img src="Assets/Images/5.jpg" width="500">
    <img src="Assets/Images/4.jpg" width="500">
</p>
* Characters shown are not using toon shader

Design  Inspirations:

* Citizen
  * Average Joe: Overcooked 2 chefs without chef attributes
  * Marathon Cindy: Sinead Diver, an Australian long distance runner
  * Sumo Miyama: Wakatakakage Atsushi, a Japanese professional sumo wrestler
  * Mukbanger Kim Jong Duos: Teenagers with baseball hat
  * Food Critic Anton Ego: Anton Ego from movie Ratatouille
  * Rich Man Bill Fences: Aristocrat with a top hat
* Chef
  * Sushiherro Yuji: Sushi chefs with traditional headband
  * Laksa Mania Caroline: Normal chef from Overcooked 2
  * Indomie Ivy: Indonesian with traditional ricefield hat
  * Coco's Rosie: Basic person
  * Pizza Izza: Normal chef from Overcooked 2
  * Kami's Chungen: Sushi chefs with traditional headband
  * Dunkin Dougie: Dunkin Doughnut staff
  * Sunbucks Bucky: Starbucks staff
  * Trainway Bucky: Subway staff
  * Farmer Dan: American farmer
* Food
  * Sushi: 6 set of sushi with salmon, cucumber, and carrot
  * Laksa: Stir-fried pork food in Overcooked 2 as the soup and a simple bowl
  * Indomie: A pack  of indomie
  * Boba: Gongcha's boba drink
  * Pizza: Pepperoni pizza
  * Korean Chicken: 2 drumstick fried chicken
  * Doughnut: Doughnut with pink icing
  * Coffee: Starbucks coffee drink with sleve
  * Sandwich: Grilled cheese sandwhich
  * Egg: An egg
  * Meat: Raw porterhouse meat
  * Carrot: A carrot

### Graphics Pipeline

#### Toon Shader

As our concept is a 3D cartoonish game, we decided to create Toon Shader. This shader deliberately create 3D objects to appear toonish. Final color of each pixel is calculated using Blinn-Phong, with threshold value creating a cartoon effect.

For the light of the pixel, we calculate if the pixel is lit or its a shadow by doing dot product on the light source and normal of the item. Additionally, we also need to take into account other items, if light source is covered by them. Lastly, using "smoothstep", it would intensify the color of light and dark.

For specular reflection of the pixel, we implemented Blinn-Phong reflection model. We use a specular reflection as we want the character to appear to have a glossy reflection eeffect. Calculating the half vector between the light vector and the view vector, using the formula $H = {L + V \over || L + V ||}$ of Blinn-Phong. Then dot product the result with the normal of the item to calculate intensity of specular reflection. After calculate the size of the specular by multipy the result with light intensity of the pixel and power it to the glossiness variable. Lastly utilize "smoothstep" again to intensify the difference between pixel colors.

For rim lighting, it gives an illumination effect at the edges of the item. We use this as it shows the depth of the object in a cartoon manner. Firstly calculate surfaces that are facing away from the camera. Then multiply it with the normal of light intensity power a certain threshold (how far rim extends). Lastly, using "smoothstep" to give the cartoon effect.

 \* Path to Toon Shader: <https://github.com/COMP30019/project-2-laksa-novona/blob/main/Assets/Shader/toon.shader>

#### Water Shader

When analysing which shader we should create, we noticed that all 3 maps have one component in common, water. Before implementing this shader, we just applied a water texture without any animation. By adding a shader, it enhances the game's visuals so it looks like we are playing with water.

To implement this we used textures from Perlin Noise and red-green distortion textures. The textures' color would then be added to the water color to add a wave like effect. Perlin Noise was chosen as the base texture as it gives a gradient noise increasing the realism of water waves.

For the animation, we shift the texture by a certain x-axis and y-axis in respect to time. This will then loop as the game runs creating a movement effect. The axis' value is chosen based on how fast and in which direction the water would stream.

Lastly, to create a cartoon effect also on the water to fit the game theme, we implement smoothstep for a distinct difference between colors.

\* Path to Water Shader: <https://github.com/COMP30019/project-2-laksa-novona/blob/main/Assets/Shader/water.shader>

### Procedural Generation

#### Map

Procedural generation for the path, attack tiles and some objects are implemented in the maps. For the path, the ground is firstly generated then the path. Since the map originally had a fixed design, the start and end points of the path are fixed and thus cannot be changed. Only the path connecting the endpoints is randomly generated and is restricted to a specific area. The algorithm for the procedural generation is as follows. Starting at the Start Tile, check to see which direction it is possible to move in while staying within the boundaries. Add this to a list, then randomly select one of these options to add a tile. For map 3, the first one-third of the path is set to generate moving only right to ensure the path does not end too fast. The attack tiles of the path are generated by choosing randomly from a set of possible configurations. The decorations for the map are randomly added after the path and attack tiles are generated in the empty spaces left on the map within the preset boundary.

#### Enemy Waves

Procedural generation was managed by the enemy manager, other than some precoded rounds, the enemy manager would have a given cost of enemies to spend on depending on round number and the cost of the previous wave, using the formula totalcost = previouswavecost + wave \* 200. The enemy manager would then rotate through the available enemies (note that difficult enemies are added into the pool depending on the level) and put down a number a cycle accordingly to the wave number and random spacing. Wave 10 costing 4000, meaning that wave 11 = 4000 + (11 \* 200) = 6200.
| Enemy  | Cost  | Wave introduction   | Amount spawned per cycle   |
| :----  | :----------- |:-------------|:-------------------------|
| Average Joe     | 10   | 0 | 10 + waveNum / 2|
|  Marathon Cindy | 20   | 3 | 7 + waveNum / 3|
| Sumo            | 200  | 5 | waveNum / 6|  
|          Mukbang Kim Jong Duos     | 250   |10 | waveNum / 8|  
|        Critique Anton Ego      | 500        |15  | waveNum / 15|  
|         Aristocrat Bill Fences   | 1000          |20 | waveNum / 20|  

### Particle System

We have implemented 5 particle system in Laksa Mania. Both particle system are implemented when the chefs throws food on the citizens. 4 of which are effects of crumbs falling off when the citizen consumes the food (when food collides with citizens). These effects range from normal crumbs falling, burnt crisps from pizza, colorful donut explosion, and exploding laksa. Effects are implemented based on what food is consumed by the citizens.

The other one which we would love to highlight is when the  citizen is full, it will explode into coin particles. These coin particles would represent that when citizens are full, player would gain coins from them. It can also be seen when selling off the chefs, it would also explode and give out coins.

<p align="center">
    <img src="Gifs/particle_system_map.gif" width="400">
</p>

The implementation file can be located in <https://github.com/COMP30019/project-2-laksa-novona/blob/main/Assets/Prefabs/Particle%20System/particle_system_enemy.prefab>.

<p align="center">
    <img src="Gifs/particle_system_trial.gif" width="400">
</p>

Below are the particle systems that we varied:

| Name            | Option          | Values                               |
| :-------------- | :-------------- | :----------------------------------- |
| Particle System | Duration        | 1                                    |
|                 | Start Lifetime  | 4                                    |
|                 | Start Speed     | [0,5]                                |
|                 | Start Size      | [0.5,1.5]                            |
| Emission        | Burst           | 2 Count in 5 Cycles, 0.01 Interval   |
| Shape           | Shape           | Sphere                               |
|                 | Radius          | 0.08                                 |
| Renderer        | Render Mode     | Mesh                                 |
|                 | Meshes          | Coin Mesh                            |
|                 | Material        | Gold with Toon Shader                |

We utilized randomisation using the feature "Random Between Two Constants" on the start speed and most importantly the start size. The start size is decided in ratio with the citizens so it won't be too big covering other actions but not too small making it unseen. Aside from that we also used burst emissions so theres more randomization by implementing 5 cycles with 0.01 interval and 2 particle each.

### Evaluation

#### Query and Observational Methods Used

##### Method

We used Cooperative Evaluation for the Observation and Interviews for the Query technique. We split the 5 players into two different types, ones that had played Tower Defence Games before and ones that had not. They are all in their twenties and we had them all play 15 rounds of the game. We recorded the interview questions, answers and observations on paper. All of the interviews and query were conducted face to face.

##### Observation

It was observed that some players thought the colour for the Path and Attack Tiles in Map2 is too similar to the colour of the map ground. Players also thought the first 10 waves of the game were too easy and required not much effort. They thought the "enemies" in the games were too weak since there were too many "average joes", which is the weakest enemy in the game. There was also one player who thought there were too many "attack tiles", thus it should be decreased to make the game more challenging or decrease the amount of coins earned when killing the "enemies". All of the players felt that the game needed to be more challenging. All of the players were confused at the start of the game and had to be taught that they had to press the shop to buy the "chefs". Players were also unclear what chef they got from the gacha. Most players found that the aesthetic was good and complimented how the path changed everytime. They also noted how detailed the characters were.

##### Query

For the query part, we generated some questions for the interview. The first questions was "Do they like the overall aesthetic of the game?". Most players liked the colours and design of the game except for one who thought the colour for the path in Map2 was too similar to the colour of the ground. However, he said that it did not effect his gameplay. The second question was "Were they aware that everytime they start the game, the path and design of the map were different?". The results from this was a 50/50 response. Some did not realize, but others did and liked it a lot. The third question was "Was the controls of the game easy to understand?". All players did not find the controls confusing and the instructions in the how to play guide was clear. The third question was "Were there moments in the game that you did not know what to do?". Some players did not have any issues while some were confused where to put the "chefs". They did not understand where the attack tiles were and that they could only place it on those certain tiles. All players did not know they should press on the shop to buy the "chefs" and felt that it would be better if this was explained. One player that had never played Tower Defense Games thought the how to play instructions were confusing. The final question for the interview was "Do you find the game engaging". All players liked the game but felt that it needed to be more challenging.

#### Changes Made

After the query and observation, we made some changes to the game to make it more challenging. We added in a functionality that increase the health of the weaker enemies every 3 rounds during the game so that they get harder to eliminate. We also added procedural generation for the "attack tiles" so that it is different everytime players start the game and only certain areas can be placed instead of generating it beside the path everytime. We also changed the colour of the map 2 "attack tiles" and "path" so that it does not blend with the ground. We also added a notification popup at the bottom left to make it clear what chef was bought to make it clearer. We also added text on the screen when players start the game so that they are clear on how to use gatcha to buy the chef and to place onto the path.

### References

#### Audio

* Main Menu Theme Song: <https://assetstore.unity.com/packages/audio/music/casual-game-bgm-5-135943>
* Desert Theme Song: <https://assetstore.unity.com/packages/audio/music/ancient-egyptian-desert-music-free-pack-131425>
* Shore Theme Song: <https://assetstore.unity.com/packages/audio/music/pirate-music-album-050418-118488>
* Castle Theme Song: <https://assetstore.unity.com/packages/audio/music/casual-kingdom-world-sounds-free-136406>
* Shooting Sound Effects: <https://assetstore.unity.com/packages/audio/sound-fx/shooting-sound-177096>
* Other Sound Effects: <https://assetstore.unity.com/packages/audio/sound-fx/score-and-time-59255>

#### Castle Map Assets

* Trees Prefab : <https://assetstore.unity.com/packages/3d/vegetation/trees/free-trees-103208>
* Castle Prefab : <https://assetstore.unity.com/packages/3d/environments/forest-low-poly-toon-battle-arena-tower-defense-pack-100080>
* Village Prefab :<https://assetstore.unity.com/packages/3d/polygon-city-pack-environment-and-interior-free-101685>
* Grass Material: <https://assetstore.unity.com/packages/2d/textures-materials/nature/nature-materials-vol-1-21113>
* Flower and Stones Prefab : <https://assetstore.unity.com/packages/3d/environments/landscapes/free-low-poly-nature-forest-205742>

#### Desert Map Assets

* Pyramids Prefab: <https://assetstore.unity.com/packages/3d/environments/landscapes/desert-kits-64-sample-86482>
* Dessert Houses Prefab: <https://assetstore.unity.com/packages/3d/environments/desert-village-houses-lowpoly-200247>
* Rocks Prefab: <https://assetstore.unity.com/packages/3d/environments/landscapes/polydesert-107196>
* Sand Material: <https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-pbr-textures-pack-111778>
* Water Textures : <https://assetstore.unity.com/?q=water%20texture&orderBy=1>

#### Shore Map Assets

* Pirate Ship Prefab: <https://assetstore.unity.com/packages/3d/vehicles/sea/stylized-pirate-ship-200192>
* Boats Prefab: <https://assetstore.unity.com/packages/3d/vehicles/sea/boats-polypack-189866>
* Beach Houses Prefab: <https://assetstore.unity.com/packages/3d/environments/historic/3d-pirates-lowpoly-pack-233903>
* Wood Plank Prefab: <https://assetstore.unity.com/packages/2d/textures-materials/wood/plank-textures-pbr-72318>
* Water Material: <https://assetstore.unity.com/packages/2d/textures-materials/floors/five-seamless-tileable-ground-textures-57060>
* Sand Material: <https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-pbr-textures-pack-111778>
* Palm Tree Prefab: <https://assetstore.unity.com/packages/3d/vegetation/trees/free-trees-103208>

#### Toon Shader Resources

* Blinn-Phong Reflection: <https://en.wikipedia.org/wiki/Blinn%E2%80%93Phong_reflection_model>
* Cel Shading: <https://www.youtube.com/watch?v=mnxs6CR6Zrk>
* Basic Shader Information: <https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html>
* Toon Shading Steps:  <https://roystan.net/articles/toon-shader/>
* Toon Shading Information: <https://en.wikibooks.org/wiki/Cg_Programming/Unity/Toon_Shading>
* Toon Shader in HLSL Language: <https://gist.github.com/JSandusky/4f9a4f00110691eb45104f69abd32f75>
* Inspiration: <https://docs.unity3d.com/Packages/com.unity.toonshader@0.6/manual/index.html>
* Analysis on Toon Shader: <https://www.youtube.com/watch?v=HD85S4TYVjg>

#### Water Shader Resources

* Minecraft Water Block Inspiration: <https://minecraft.fandom.com/wiki/Water>
* Perlin Noise Texture: <https://en.wikipedia.org/wiki/Perlin_noise>
* Water Shader Steps: <https://roystan.net/articles/toon-water/>
* Water In Unity: <https://docs.unity3d.com/560/Documentation/Manual/HOWTO-Water.html>
* Water Shader Implementation: <https://github.com/lindenreid/Unity-Shader-Tutorials/blob/master/Assets/Materials/Shaders/water.shader>
* Inspiration: <https://www.artstation.com/artwork/QzX90E>

#### Others

* Buttons Images: <https://assetstore.unity.com/packages/2d/gui/buttons-set-211824>
* Steps on Creating Tower Defense Game: <https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0>
* Canva "How To Play" Template: <https://www.canva.com/design/DAFP7Gh3YFU/4xQJ5-E4pOfaxq9WTIvf8A/view?utm_content=DAFP7Gh3YFU&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton>