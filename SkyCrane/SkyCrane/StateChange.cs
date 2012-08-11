﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkyCrane
{
    public enum StateChangeType { MOVED, CREATE_PLAYER_CHARACTER, SET_PLAYER, CREATE_ENTITY, DELETE_ENTITY, CHANGE_SPRITE}
    public enum StateProperties { ENTITY_ID, POSITION_X, POSITION_Y, SPRITE_NAME, ANIMATION_NAME, DRAW_PRIORITY, FRAME_WIDTH }

    public class StateChange
    {
        public StateChangeType type;
        public Dictionary<StateProperties, int> intProperties = new Dictionary<StateProperties, int>();
        public Dictionary<StateProperties, String> stringProperties = new Dictionary<StateProperties, String>();
    }
}
