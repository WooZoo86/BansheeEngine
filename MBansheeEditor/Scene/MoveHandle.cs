﻿using BansheeEngine;

namespace BansheeEditor
{
    public sealed class MoveHandle : DefaultHandle
    {
        private const float CONE_HEIGHT = 0.05f;
        private const float CONE_RADIUS = 0.05f;

        private Vector3 delta;

        private HandleSliderLine xAxis;
        private HandleSliderLine yAxis;
        private HandleSliderLine zAxis;

        public Vector3 Delta
        {
            get { return delta; }
        }

        public MoveHandle()
        {
            xAxis = new HandleSliderLine(this, Vector3.xAxis, 1.0f);
            yAxis = new HandleSliderLine(this, Vector3.yAxis, 1.0f);
            zAxis = new HandleSliderLine(this, Vector3.zAxis, 1.0f);
        }

        protected override void PreInput()
        {
            xAxis.Position = position;
            yAxis.Position = position;
            zAxis.Position = position;

            xAxis.Rotation = rotation;
            yAxis.Rotation = rotation;
            zAxis.Rotation = rotation;
        }

        protected override void PostInput()
        {
            delta = Vector3.zero;
            delta += xAxis.Delta * GetXDir();
            delta += yAxis.Delta * GetYDir();
            delta += zAxis.Delta * GetZDir();
        }

        protected override void Draw()
        {
            Vector3 center = position;
            Vector3 xEnd = center + GetXDir();
            Vector3 yEnd = center + GetYDir();
            Vector3 zEnd = center + GetZDir();

            if (xAxis.State == HandleSlider.StateType.Active)
                HandleDrawing.SetColor(Color.white);
            else if(xAxis.State == HandleSlider.StateType.Hover)
                HandleDrawing.SetColor(Color.red * 0.8f);
            else
                HandleDrawing.SetColor(Color.red);
            
            HandleDrawing.DrawLine(center, xEnd);
            HandleDrawing.DrawCone(xEnd - GetXDir()*CONE_HEIGHT, GetXDir(), CONE_HEIGHT, CONE_RADIUS);

            if (yAxis.State == HandleSlider.StateType.Active)
                HandleDrawing.SetColor(Color.white);
            else if (yAxis.State == HandleSlider.StateType.Hover)
                HandleDrawing.SetColor(Color.green * 0.8f);
            else
                HandleDrawing.SetColor(Color.green);

            HandleDrawing.DrawLine(center, yEnd);
            HandleDrawing.DrawCone(yEnd - GetYDir() * CONE_HEIGHT, GetYDir(), CONE_HEIGHT, CONE_RADIUS);

            if (zAxis.State == HandleSlider.StateType.Active)
                HandleDrawing.SetColor(Color.white);
            else if (zAxis.State == HandleSlider.StateType.Hover)
                HandleDrawing.SetColor(Color.blue * 0.8f);
            else
                HandleDrawing.SetColor(Color.blue);

            HandleDrawing.DrawLine(center, zEnd);
            HandleDrawing.DrawCone(zEnd - GetZDir() * CONE_HEIGHT, GetZDir(), CONE_HEIGHT, CONE_RADIUS);
        }

        private Vector3 GetXDir()
        {
             return rotation.Rotate(Vector3.xAxis);
        }

        private Vector3 GetYDir()
        {
            return rotation.Rotate(Vector3.yAxis);
        }

        private Vector3 GetZDir()
        {
            return rotation.Rotate(Vector3.zAxis);
        }
    }
}