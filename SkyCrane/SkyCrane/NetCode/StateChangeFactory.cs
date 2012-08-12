﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyCrane.NetCode
{
    class StateChangeFactory
    {
        public static StateChange createEntityStateChange(int entity_id, int posX, int posY, int frameWidth, String textureName, float scale, int drawPriority)
        {
            StateChange sc = new StateChange();
            sc.type = StateChangeType.CREATE_ENTITY;
            sc.intProperties.Add(StateProperties.ENTITY_ID, entity_id);
            sc.intProperties.Add(StateProperties.POSITION_X, posX);
            sc.intProperties.Add(StateProperties.POSITION_Y, posY);
            sc.intProperties.Add(StateProperties.DRAW_PRIORITY, drawPriority);
            sc.intProperties.Add(StateProperties.FRAME_WIDTH, frameWidth);
            sc.stringProperties.Add(StateProperties.SPRITE_NAME, textureName);
            sc.doubleProperties.Add(StateProperties.SCALE, scale);

            return sc;
        }

        public static StateChange createChangeSpriteStateChange(int id, int frameWidth, String texture, int frameTime)
        {
            StateChange sc = new StateChange();
            sc.type = StateChangeType.CHANGE_SPRITE;
            sc.intProperties.Add(StateProperties.ENTITY_ID, id);
            sc.intProperties.Add(StateProperties.FRAME_WIDTH, frameWidth);
            sc.intProperties.Add(StateProperties.FRAME_TIME, frameTime);
            sc.stringProperties.Add(StateProperties.SPRITE_NAME, texture);

            return sc;
        }
    }
}
