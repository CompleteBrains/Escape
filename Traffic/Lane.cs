using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tools.Markers;
using Traffic.Cars;

namespace Traffic
{
    public class Lane : Object
    {
        private int height;
        private readonly List <Car> carsToAdd;
        private static int carsCounter;

        //------------------------------------------------------------------
        public const int MinimumCars = 5;
        public const int MaximumCars = 15;

        //------------------------------------------------------------------
        public readonly int ID;
        private int border;
        public List <Car> Cars { get; private set; }
        public int Velocity { get; set; }
        public Lane Left { get; set; }
        public Lane Right { get; set; }
        public static Random Random { get; set; }
        public Road Road { get; private set; }
        public int CarsQuantity { get; set; }

        #region Creation

        //------------------------------------------------------------------
        static Lane ()
        {
            Random = new Random ();
        }

        //------------------------------------------------------------------
        public Lane (Road road, int id) : base (road)
        {
            ID = id;
            Road = road;
            Anchored = true;

            CalculatePosition (ID);
            CalculateVelocity (ID);

            Cars = new List <Car> ();
            carsToAdd = new List <Car> ();
        }

        //------------------------------------------------------------------
        private void CalculateVelocity (int id)
        {
            const int maximumVelocity = 240;
            const int step = 20;
            Velocity = maximumVelocity - id * step;
        }

        //------------------------------------------------------------------
        private void CalculatePosition (int id)
        {
            const int laneWidth = 40;
            int position = id * laneWidth + laneWidth / 2;

            Position = new Vector2 (position, 0);
        }

        //------------------------------------------------------------------
        public override void Setup ()
        {
            height = Road.Game.GraphicsDevice.Viewport.Height;
            border = 3000;

            base.Setup ();
        }

        //------------------------------------------------------------------
        protected virtual Car CreateCar ()
        {
            var car = new Car (this, GetInsertionPosition ()) {ID = carsCounter};
            car.Setup ();

            Cars.Add (car);
            OwnCar (car);

            carsCounter++;

            return car;
        }

        //------------------------------------------------------------------
        public Player CreatePlayer (Game game)
        {
            var player = new Player (this, 400) {ID = carsCounter};
            player.Setup ();

            Cars.Add (player);
            OwnCar (player);

            carsCounter++;

            return player;
        }

        //------------------------------------------------------------------
        public void CreatePolice (Game game)
        {
            var police = new Police (this, -100) {ID = carsCounter};
            police.Setup ();

            Cars.Add (police);
            OwnCar (police);

            carsCounter++;
        }

        //------------------------------------------------------------------
        // Return point outside the screen
        private int GetInsertionPosition ()
        {
            // Determine where place car: above Player or bottom
            float playerVelocity = (Road.Player != null) ? Road.Player.Velocity : 0;

            int sign = (Velocity > playerVelocity) ? 1 : -1;

            return GetFreePosition (sign);
        }

        //------------------------------------------------------------------
        private int GetFreePosition (int sign)
        {
            int position = 0;
            int lower = 1000 * sign;
            int upper = border * sign;

            // Swap borders if needed
            if (lower > upper)
            {
                var temp = lower;
                lower = upper;
                upper = temp;
            }

            // Get free position
            foreach (var index in Enumerable.Range (0, 20))
            {
                position = Random.Next (lower, upper);

                if (!Cars.Any ()) break;

                float minimum = Cars.Min (car => Math.Abs (car.GlobalPosition.Y - position));

                if (minimum > 400) break;
            }

            return position;
        }

        #endregion

        //------------------------------------------------------------------
        public override void Update (float elapsed)
        {
            base.Update (elapsed);

            AddQueuedCars ();

            CleanUp ();
            AppendCars ();

            Components.Clear ();
            Components.AddRange (Cars);

            Debug ();
        }

        #region Cars Management

        //------------------------------------------------------------------
        private void AppendCars ()
        {
            if (Cars.Count < CarsQuantity)
                CreateCar ();
        }

        //------------------------------------------------------------------
        private void CleanUp ()
        {
            // Remove Cars outside the screen
            Cars.RemoveAll (car =>
            {
                int position = (int) car.GlobalPosition.Y;
                return position < -border || position > border;
            });

            // Remove all dead Cars
            Cars.RemoveAll (car => car.Deleted);
        }

        //------------------------------------------------------------------
        private void AddQueuedCars ()
        {
            Cars.AddRange (carsToAdd);
            carsToAdd.ForEach (OwnCar);
            carsToAdd.Clear ();
        }

        //------------------------------------------------------------------
        public void Add (Car car)
        {
            carsToAdd.Add (car);
        }

        //------------------------------------------------------------------
        private void OwnCar (Car car)
        {
            if (car.Lane != this)
                car.Lane.Cars.Remove (car);

            car.Lane = this;
            car.Position = new Vector2 (0, car.GlobalPosition.Y);
        }

        //------------------------------------------------------------------
        private void FreeCar (Car car)
        {
//            car.Lane = null;
        }

        #endregion

        //------------------------------------------------------------------
        public override string ToString ()
        {
            return string.Format ("{0}", ID);
        }

        //------------------------------------------------------------------
        private void Debug ()
        {
//            new Text (ToString () + ":" + Cars.Count, Position);
//            new Text (Velocity.ToString ("F0"), Position);
//            new Text (CarsQuantity.ToString (), Position);

//            // Particular Type counter
//            int number = Cars.OfType <Player> ().Count ();
//            if (number != 0) 
//                new Text (number.ToString (""), Position);
        }
    }
}