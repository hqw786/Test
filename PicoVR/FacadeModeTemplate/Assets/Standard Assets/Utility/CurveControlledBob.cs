using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    [Serializable]
    public class CurveControlledBob
    {
        public float HorizontalBobRange = 0.33f;
        public float VerticalBobRange = 0.33f;
        /// <summary>
        /// 动画曲线，摆动曲线
        /// </summary>
        public AnimationCurve Bobcurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                            new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                            new Keyframe(2f, 0f)); // sin curve for head bob
        public float VerticaltoHorizontalRatio = 1f;

        private float m_CyclePositionX;
        private float m_CyclePositionY;
        /// <summary>
        /// 摆动基础间隔
        /// </summary>
        private float m_BobBaseInterval;
        /// <summary>
        /// 起始摄像机位置
        /// </summary>
        private Vector3 m_OriginalCameraPosition;
        private float m_Time;

        /// <summary>
        /// 设置摄像头
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="bobBaseInterval">脚步间隔</param>
        public void Setup(Camera camera, float bobBaseInterval)
        {
            m_BobBaseInterval = bobBaseInterval;
            m_OriginalCameraPosition = camera.transform.localPosition;

            // get the length of the curve in time//获得在某段时间曲线的长度 
            m_Time = Bobcurve[Bobcurve.length - 1].time;
        }


        public Vector3 DoHeadBob(float speed)
        {
            float xPos = m_OriginalCameraPosition.x + (Bobcurve.Evaluate(m_CyclePositionX)*HorizontalBobRange);
            float yPos = m_OriginalCameraPosition.y + (Bobcurve.Evaluate(m_CyclePositionY)*VerticalBobRange);

            m_CyclePositionX += (speed*Time.deltaTime)/m_BobBaseInterval;
            m_CyclePositionY += ((speed*Time.deltaTime)/m_BobBaseInterval)*VerticaltoHorizontalRatio;

            if (m_CyclePositionX > m_Time)
            {
                m_CyclePositionX = m_CyclePositionX - m_Time;
            }
            if (m_CyclePositionY > m_Time)
            {
                m_CyclePositionY = m_CyclePositionY - m_Time;
            }

            return new Vector3(xPos, yPos, 0f);
        }
    }
}
