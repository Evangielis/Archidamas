using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;
using Microsoft.Xna.Framework.Input;

namespace ActionRPG
{
    enum EnumMapDirection { North, East, South, West };

    interface IMapService
    {
        void PlaceAvatar(IActor mob);
        void PlaceActor(IActor actor, Point Loc);
        IMapTile GetFeatureAt(int x, int y);
        IMapTile GetSurfaceAt(int x, int y);
        bool IsActorAt(int x, int y);
        IActor GetActorAt(int x, int y);
        float GridSize { get; }
        Point GetPointFrom(Point origin, EnumMapDirection direction, int distance);
    }

    /*class Grid
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
}*/

    class AreaMapComponent : GameComponent, IMapService
    {
        const float MOVE_SPEED = 2.0F;
        const int GRID_MOVE_SPEED = 1;
        const int DEFAULT_SIZE = 100;
        const int DEFAULT_GRID_SIZE = 32; //Size of one square on the grid.

        float GridSize { get { return DEFAULT_GRID_SIZE; } }

        IObjectService ObjService { get; set; }

        //Setup map grids
        Point EntryPoint { get; set; }
        int Size { get { return DEFAULT_SIZE; } }
        IMapTile[,] _surfaceGrid;
        IMapTile[,] _featureGrid;
        int[,] _mobGrid;

        public AreaMapComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IMapService), this);
        }

        public override void Initialize()
        {
            this.ObjService = (IObjectService)this.Game.Services.GetService(typeof(IObjectService));

            this._featureGrid = new IMapTile[DEFAULT_SIZE, DEFAULT_SIZE];
            this._surfaceGrid = new IMapTile[DEFAULT_SIZE, DEFAULT_SIZE];
            this._mobGrid = new int[DEFAULT_SIZE, DEFAULT_SIZE];

            //Initialize grids
            for (int i = 0; i < DEFAULT_SIZE; i++)
            {
                for (int j = 0; j < DEFAULT_SIZE; j++)
                {

                    this._surfaceGrid[i, j] = new GrassTile(i, j);
                    this._featureGrid[i, j] = new NullTile(i, j);
                    this._mobGrid[i, j] = -1;
                }
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Checks for collision with objects on the map
        /// </summary>
        /// <param name="collider">The object being checked</param>
        /// <returns>True if a collision has occured with a non-passable object</returns>
        public bool CheckCollision(Point proposed)
        {
            return this.CheckCollision(proposed.X, proposed.Y);
        }
        bool CheckCollision(int x, int y)
        {
            return (_featureGrid[x, y].Impassable);
        }

        public void LoadMap(IMapTile[] objs)
        {
            foreach (IMapTile t in objs)
            {
                this._featureGrid[t.Loc.X, t.Loc.Y] = t;
                if (t.Name == "Entry Point")
                    this.EntryPoint = t.Loc;
            }
        }

        void PlaceActor(IActor actor, Point Loc)
        {
            if (CheckCollision(Loc))
                throw new InvalidOperationException();
            else
            {
                this._mobGrid[actor.Loc.X, actor.Loc.Y] = -1;
                this._mobGrid[Loc.X, Loc.Y] = actor.MobID;
                actor.Loc = Loc;
            }
        }

        void PlaceAvatar(IActor mob)
        {
            this.PlaceActor(mob, this.EntryPoint);
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
        float IMapService.GridSize
        {
            get { return this.GridSize; }
        }
        void IMapService.PlaceActor(IActor mob, Point Loc)
        {
            this.PlaceActor(mob, Loc);
        }
        void IMapService.PlaceAvatar(IActor mob)
        {
            this.PlaceAvatar(mob);
        }
        bool IMapService.IsActorAt(int x, int y)
        {
            return (this._mobGrid[x, y] < 0) ? false : true;
        }

        IActor IMapService.GetActorAt(int x, int y)
        {
            return (this.ObjService.FetchActor(this._mobGrid[x,y]));
        }


        Point IMapService.GetPointFrom(Point origin, EnumMapDirection direction, int distance)
        {
            int x = origin.X;
            int y = origin.Y;

            switch (direction)
            {
                case EnumMapDirection.North: y -= distance; break;
                case EnumMapDirection.East: x += distance; break;
                case EnumMapDirection.South: y += distance; break;
                case EnumMapDirection.West: x -= distance; break;
            }

            //Clamp values to map size
            x = (x < 0) ? 0 : (x >= this.Size) ? this.Size - 1 : x;
            y = (y < 0) ? 0 : (y >= this.Size) ? this.Size - 1 : y;

            return new Point(x, y);
        }
    }
}
