using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    [Serializable]
    public class CurveControlledBob     //���߿��ưڶ�
    {
        public float HorizontalBobRange = 0.33f;//ˮƽ�ڶ�����
        public float VerticalBobRange = 0.33f;//��ֱ�ڶ�����
        public AnimationCurve Bobcurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                            new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                            new Keyframe(2f, 0f)); // sin curve for head bob��ͷ���ڶ����������ߡ�new Keyframe(ʱ�䣬��ֵ��
        public float VerticaltoHorizontalRatio = 1f;//��ֱ��ˮƽ��ת

        private float m_CyclePositionX;//ѭ��λ��X
        private float m_CyclePositionY;//ѭ��λ��Y
        private float m_BobBaseInterval;//�ڶ�һ����
        private Vector3 m_OriginalCameraPosition;//ԭʼ�����λ��
        private float m_Time;//


        public void Setup(Camera camera, float bobBaseInterval)//��������ڶ�һ����
        {
            m_BobBaseInterval = bobBaseInterval;//
            m_OriginalCameraPosition = camera.transform.localPosition;//��������ı���λ��

            // get the length of the curve in time
            m_Time = Bobcurve[Bobcurve.length - 1].time;//
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
