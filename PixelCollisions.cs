using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Binocle
{
    public enum DirectionUnit
    {
        Top,
        Left,
        Right,
        Bottom
    }

    public static class PixelCollisions
    {

        private static Rect rectCheck = new Rect();

        public static bool Collide(float x, float y, float width, float height, DirectionUnit dir, int layerMask, out Collider2D[] colliders)
        {
            Rect r = RectCollision(x, y, width, height, dir);
            colliders = Physics2D.OverlapAreaAll(new Vector2(r.x, r.y), new Vector2(r.x + r.width, r.y + r.height), layerMask);
            if (colliders.Length > 0)
                return true;

            return false;
        }

        public static bool Collide(float x, float y, float width, float height, DirectionUnit dir, int layerMask)
        {
            Rect r = RectCollision(x, y, width, height, dir);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(r.x, r.y), new Vector2(r.x + r.width, r.y + r.height), layerMask);
            if (colliders.Length > 0)
                return true;

            return false;
        }


        public static Rect RectCollision(float x, float y, float width, float height, DirectionUnit dir)
        {
            float halfWidth = width / 2;
            float halfHeight = height / 2;
            float margin = 1.0f;
            float toX = 0;
            float toY = 0;
            float h = 0;
            float w = 0;

            if (dir == DirectionUnit.Top)
            {
                toX = x - halfWidth + margin;
                toY = y + halfHeight;
                h = -1.0f;
                w = width - (margin * 2);
            }

            if (dir == DirectionUnit.Bottom)
            {
                toX = x - halfWidth + margin;
                toY = y - halfHeight;
                h = 1.0f;
                w = width - (margin * 2);
            }

            if (dir == DirectionUnit.Right)
            {
                toX = x + halfWidth;
                toY = y - halfHeight + margin;
                h = height - (margin * 2);
                w = -1.0f;
            }


            if (dir == DirectionUnit.Left)
            {
                toX = x - halfWidth;
                toY = y - halfHeight + margin;
                h = height - (margin * 2);
                w = 1.0f;
            }

            rectCheck.x = (int)toX;
            rectCheck.y = (int)toY;
            rectCheck.width = (int)w;
            rectCheck.height = (int)h;
            DebugX.DrawRect(rectCheck, Color.red);
            return rectCheck;
        }

    }
}