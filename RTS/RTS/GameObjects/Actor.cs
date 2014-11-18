using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RTS.GameObjects
{
    class Actor
    {
        String _name;
        Vector2 _loc;
        bool _selected;
        bool _active;

        public String Name { get { return this._name; } }
        public Vector2 Loc { get { return this._loc; } }

        public Actor()
        {
            _name = String.Empty;
            _loc = Vector2.Zero;
            _selected = false;
            _active = false;
        }
    }
}
