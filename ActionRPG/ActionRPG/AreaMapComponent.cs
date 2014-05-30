using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Archidamas;
using Archidamas.Extensions;
using Microsoft.Xna.Framework.Input;

namespace ActionRPG
{
    interface IMapService
    {
        Vector2 PlayerLoc { get; }
        IMapTile GetFeatureAt(int x, int y);
        IMapTile GetSurfaceAt(int x, int y);
    }

    class Grid
    {
        const int DEFAULT_SIZE = 32;

        public Rectangle Selector { get; private set; }
        private Vector2 _gridRef;
        public Vector2 GridRef { get { return this._gridRef; } }
        int Size { get; set; }

        public Grid()
        {
            this.Selector = new Rectangle(0,0,32,32);
            this._gridRef = new Vector2(0, 0);
            this.Size = DEFAULT_SIZE;
        }

        public Rectangle SetGrid(int x, int y)
        {
            this.Selector.Offset(x, y);
            this._gridRef.X = x*this.Size;
            this._gridRef.Y = y*this.Size;
            return this.Selector;
        }
    }

    class AreaMapComponent : GameComponent, IMapService
    {
        const float MOVE_SPEED = 2.0F;
        const int DEFAULT_SIZE = 100;

        Vector2 _playerLoc;
        Vector2 PlayerLoc { get { return _playerLoc; } }
        IKeyService KeyService { get; set; }

        //Setup map grids
        IMapTile[,] _surfaceGrid;
        IMapTile[,] _featureGrid;

        public AreaMapComponent(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IMapService),this);
        }

        public override void Initialize()
        {
            KeyService = (IKeyService)Game.Services.GetService(typeof(IKeyService));
            this._featureGrid = new IMapTile[DEFAULT_SIZE, DEFAULT_SIZE];
            this._surfaceGrid = new IMapTile[DEFAULT_SIZE, DEFAULT_SIZE];

            //Initialize grids
            for (int i = 0; i < DEFAULT_SIZE; i++)
            {
                for (int j = 0; j < DEFAULT_SIZE; j++)
                {

                    this._surfaceGrid[i,j] = new GrassTile(i, j);
                    this._featureGrid[i, j] = new NullTile(i, j);
                }
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.CheckPlayerMovement();
            base.Update(gameTime);
        }

        public void CheckPlayerMovement()
        {
            Vector2 _proposedLoc = PlayerLoc.Clone();

            foreach (Keys k in this.KeyService.PressedKeys)
            {
                switch (k)
                {
                    case Keys.W: _proposedLoc.Y -= MOVE_SPEED; break;
                    case Keys.A: _proposedLoc.X -= MOVE_SPEED; break;
                    case Keys.S: _proposedLoc.Y += MOVE_SPEED; break;
                    case Keys.D: _proposedLoc.X += MOVE_SPEED; break;
                    default: break;
                }
            }

            Rectangle player = new Rectangle((int)_proposedLoc.X,(int)_proposedLoc.Y,32,32);
            _playerLoc = (this.CheckCollision(player)) ? _playerLoc : _proposedLoc;
        }

        /// <summary>
        /// Checks for collision with objects on the map
        /// </summary>
        /// <param name="collider">The object being checked</param>
        /// <returns>True if a collision has occured with a non-passable object</returns>
        public bool CheckCollision(Rectangle collider)
        {
            int x = collider.X/32;
            int y = collider.Y/32;
            return (_featureGrid[x, y].Impassable);
        }

        public void LoadMap(IMapTile[] objs)
        {
            foreach (IMapTile t in objs)
            {
                switch (t.Name)
                {
                    case "Entry Point":
                        this._playerLoc = new Vector2(t.Loc.X*32, t.Loc.Y*32);
                        break;

                    default:
                        this._featureGrid[t.Loc.X, t.Loc.Y] = t;
                        break;
                }
            }     
        }

        //IMap implementation
        IMapTile IMapService.GetFeatureAt(int x, int y)
        {
            return this._featureGrid[x, y];
        }
        IMapTile IMapService.GetSurfaceAt(int x, int y)
        {
            return this._surfaceGrid[x, y];
        }

        Vector2 IMapService.PlayerLoc
        {
            get { return this.PlayerLoc; }
        }

    }
}
