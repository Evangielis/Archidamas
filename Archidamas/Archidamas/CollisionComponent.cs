using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Archidamas
{
    /// <summary>
    /// This interface provides services for collision detection
    /// </summary>
    public interface ICollisionService
    {
        /// <summary>
        /// Registers a new collider with the service
        /// </summary>
        /// <param name="collider">The collider to register</param>
        void RegisterCollider(ICollider collider);

        /// <summary>
        /// Removes a collider from the list of registered colliders
        /// </summary>
        /// <param name="collider"></param>
        void DeregisterCollider(ICollider collider);

        /// <summary>
        /// Checks two ICollider objects to see if a collision has occured
        /// </summary>
        /// <param name="obj1">First ICollider object</param>
        /// <param name="obj2">Second ICollider object</param>
        /// <returns>True if a collision has occured, false otherwise</returns>
        bool CheckCollision(ICollider obj1, ICollider obj2);
    }

    /// <summary>
    /// This class is the component which underlies collision detection services.
    /// </summary>
    public class CollisionComponent : GameComponent, ICollisionService
    {
        /// <summary>
        /// The maximum number of colliders which can be on the screen at a time (whether active or not)
        /// Currently not being used
        /// </summary>
        private int _maxColliders = 1000;
        private List<ICollider> _allColliders;

        public CollisionComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(ICollisionService), this);
        }

        public override void Initialize()
        {
            _allColliders = new List<ICollider>();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateCollisions(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Updates collisions for all objects in collider list
        /// </summary>
        /// <param name="gameTime">Elapsed game time</param>
        private void UpdateCollisions(GameTime gameTime)
        {
            //If there are less than two registered colliders just exit
            if (this._allColliders.Count < 2) return;

            //If there are at least two then we iterate across all objects
            for (int i = 0; i < this._allColliders.Count - 1; i++)
            {
                //Skip this object if not collidable
                if (!(this._allColliders[i].IsColliable))
                    continue;

                //Iterate across everything after i and check once per (i,j) tuple
                for (int j = (i + 1); j < this._allColliders.Count; j++)
                {
                    if (this.CheckCollision(this._allColliders[i], this._allColliders[j]))
                        //Register collision update
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Checks two objects to see if a collision has occured
        /// </summary>
        /// <param name="obj1">First ICollider object</param>
        /// <param name="obj2">Second ICollider object</param>
        /// <returns>True if a collision has occured, false otherwise</returns>
        public bool CheckCollision(ICollider obj1, ICollider obj2)
        {
            //If either object is not collidable return false
            if (!(obj1.IsColliable && obj2.IsColliable))
                return false;

            if (obj1.CollideType == ColliderType.Circle)
            {
                if (obj2.CollideType == ColliderType.Circle)
                {
                    //A collision occurs if the distance between the two centers is less than
                    //the sum of their radii
                    return (Vector2.Distance(obj1.Center, obj2.Center) < (obj1.Norm + obj2.Norm));
                }
            }

            //Default value
            return false;
        }

        /// <summary>
        /// Checks two objects to see if a collision has occured
        /// </summary>
        /// <param name="obj1">First ICollider object</param>
        /// <param name="obj2">Second ICollider object</param>
        /// <returns>True if a collision has occured, false otherwise</returns>
        bool ICollisionService.CheckCollision(ICollider obj1, ICollider obj2)
        {
            return this.CheckCollision(obj1, obj2);
        }

        /// <summary>
        /// Registers a new collider with the component.  Will check to be
        /// sure the collider isn't already on the list.
        /// </summary>
        /// <param name="collider">The collider to add</param>
        void ICollisionService.RegisterCollider(ICollider collider)
        {
            //Check if the collider has been previously registered
            if (!(_allColliders.Contains(collider)))
                _allColliders.Add(collider);
        }

        /// <summary>
        /// Removes a collider from the list of registered colliders.
        /// </summary>
        /// <param name="collider">The collider to remove</param>
        void ICollisionService.DeregisterCollider(ICollider collider)
        {
            _allColliders.Remove(collider);
        }
    }

    /// <summary>
    /// Types of colliders available for use
    /// </summary>
    public enum ColliderType
    {
        Circle,
        //Rectangle
    }

    /// <summary>
    /// Objects wishing to use the collision service need to implement this interface
    /// </summary>
    public interface ICollider
    {
        /// <summary>
        /// This should be true if the object is a collider
        /// </summary>
        bool IsColliable { get; set; }

        /// <summary>
        /// This is the center of mass for the collider
        /// </summary>
        Vector2 Center { get; set; }

        /// <summary>
        /// This should return the collider type for the implementing object
        /// </summary>
        ColliderType CollideType { get; set; }

        /// <summary>
        /// Like a vector norm this is used to indicate the 'size' of the collider
        /// For circle colliders this is a radius
        /// For rectangle colliders this would be half a diagonal
        /// </summary>
        float Norm { get; set;}
    }
}
