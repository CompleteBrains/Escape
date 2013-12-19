﻿using System;
using Microsoft.Xna.Framework;
using Tools.Markers;
using Traffic.Cars.Weights;

namespace Traffic.Cars
{
    public class Player : Car
    {
        //------------------------------------------------------------------
        public Player (Lane lane, int id, int position, Weight weight, string textureName)
            : base (lane, id, position, weight, textureName)
        {
            Lives = 99;
            Velocity = 0;
            Acceleration = 1; //0.3f;
            Deceleration = 2; //1.0f;

            Driver = new Drivers.Player (this);
        }

        public override void Update (float elapsed)
        {
            base.Update (elapsed);

            Vector3 velocity = Lane.Road.Fluid.Data.GetData (Position);
            var vel = new Vector2 (velocity.X, velocity.Y);
//            LocalPosition += vel * 2;
//            Angle += velocity.Z / 50;

//            new Line (Position, Position + vel * 10, Color.Orange);
        }
    }
}