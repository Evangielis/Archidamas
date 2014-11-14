using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Archidamas
{
    /// <summary>
    /// This interface provides basic camera services for use in drawing.
    /// </summary>
    public interface ICameraService
    {
        float Zoom { get; set; }
        float Rotation { get; set; }
        Vector2 Center { get; set; }
        Matrix TranslationMatrix { get; }
        void SetCenter(int x, int y);
        void SetCenter(Vector2 v);
    }

    /// <summary>
    /// The camera component underlying the ICameraService service.
    /// </summary>
    public class CameraComponent : GameComponent, ICameraService
    {
        float ZoomLevel { get; set; }
        Vector2 Center { get; set; }
        float Rotation { get; set; }
        
        Matrix TranslationMatrix 
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-1 * Center.X, -1 * this.Center.Y, 0))
                    * Matrix.CreateRotationZ(this.Rotation)
                    * Matrix.CreateScale(new Vector3(this.ZoomLevel, this.ZoomLevel, 1F))
                    * Matrix.CreateTranslation(new Vector3(Game.GraphicsDevice.Viewport.Width * 0.5F, Game.GraphicsDevice.Viewport.Height * 0.5F, 0));
            }
        }

        public CameraComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(ICameraService), this);
        }

        public override void Initialize()
        {
            this.ZoomLevel = 1.0F;
            this.Rotation = 0.0F;
            this.Center = Vector2.Zero;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        void SetCenter(int x, int y)
        {
            this.Center = new Vector2(x, y);
        }

        float ICameraService.Zoom
        {
            get
            {
                return this.ZoomLevel;
            }
            set
            {
                this.ZoomLevel = value;
            }
        }

        float ICameraService.Rotation
        {
            get
            {
                return this.Rotation;
            }
            set
            {
                this.Rotation = value;
            }
        }

        Vector2 ICameraService.Center
        {
            get
            {
                return this.Center;
            }
            set
            {
                this.Center = value;
            }
        }

        Matrix ICameraService.TranslationMatrix
        {
            get { return this.TranslationMatrix; }
        }

        void ICameraService.SetCenter(int x, int y)
        {
            this.SetCenter(x, y);
        }
        void ICameraService.SetCenter(Vector2 v)
        {
            this.Center = v;
        }
    }
}
