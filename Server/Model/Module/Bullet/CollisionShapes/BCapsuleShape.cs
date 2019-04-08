﻿using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using ETModel;

namespace BulletUnity {


    public class BCapsuleShape : BCollisionShape {
        public enum CapsuleAxis
        {
            x,
            y,
            z
        }

        protected float radius = 1f;
        public float Radius
        {
            get { return radius; }
            set
            {
                if (collisionShapePtr != null && value != radius)
                {
                    Log.Error("Cannot change the radius after the bullet shape has been created. Radius is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    radius = value;
                }
            }
        }

        protected float height = 2f;
        public float Height
        {
            get { return height; }
            set
            {
                if (collisionShapePtr != null && value != height)
                {
                    Log.Error("Cannot change the height after the bullet shape has been created. Height is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    height = value;
                }
            }
        }


        protected CapsuleAxis upAxis = CapsuleAxis.y;
        public CapsuleAxis UpAxis
        {
            get { return upAxis; }
            set
            {
                if (collisionShapePtr != null && value != upAxis)
                {
                    Log.Error("Cannot change the upAxis after the bullet shape has been created. upAxis is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    upAxis = value;
                }
            }
        }

        protected Vector3 m_localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get { return m_localScaling; }
            set
            {
                m_localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((CapsuleShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        CapsuleShape _CreateCapsuleShape()
        {
            CapsuleShape cs = null;
            if (upAxis == CapsuleAxis.x)
            {
                cs = new CapsuleShapeX(radius, height);
            }
            else if (upAxis == CapsuleAxis.y)
            {
                cs = new CapsuleShape(radius, height);
            }
            else if (upAxis == CapsuleAxis.z)
            {
                cs = new CapsuleShapeZ(radius, height);
            }
            else
            {
                Log.Error("invalid axis value");
            }
            cs.LocalScaling = m_localScaling.ToBullet();
            return cs;
        }

        public override CollisionShape CopyCollisionShape()
        {
            return _CreateCapsuleShape();
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr == null) {
                collisionShapePtr = _CreateCapsuleShape();
            }
            return collisionShapePtr;
        }
    }
}
