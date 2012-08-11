﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkyCrane.Screens;

namespace SkyCrane
{
    public class PlayerCharacter : Entity, PhysicsAble
    {

        public static PlayerCharacter createDefaultPlayerCharacter(GameplayScreen g)
        {
            PlayerCharacter pc = new PlayerCharacter(g);
            pc.worldPosition = new Vector2(1280 / 2, 720 / 2);

            return pc;
        }

        public PlayerCharacter(GameplayScreen g) : base(g)
        {
        }

        public void UpdatePhysics(GameTime time, List<PhysicsAble> others)
        {
        }

        public CollisionDirection CheckCollision(Vector2 position, Rectangle bounds)
        {
            return CollisionDirection.NONE;
        }
    }
}
