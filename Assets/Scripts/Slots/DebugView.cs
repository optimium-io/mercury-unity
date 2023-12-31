﻿using UnityEngine;

namespace Slots
{
    public sealed class DebugView : MonoBehaviour
    {
        [SerializeField] private float m_updateInterval = 0.5f;
        [SerializeField] private ReelParameters _reelParameters;

        private float m_accum;
        private int m_frames;
        private float m_timeleft;
        private float m_fps;

        private void Update()
        {
            m_timeleft -= Time.deltaTime;
            m_accum += Time.timeScale / Time.deltaTime;
            m_frames++;

            if (0 < m_timeleft) return;

            m_fps = m_accum / m_frames;
            m_timeleft = m_updateInterval;
            m_accum = 0;
            m_frames = 0;
        }

        private bool _toggleSlow;

        private void OnGUI()
        {
            GUILayout.Label("target FPS: " + Application.targetFrameRate);
            GUILayout.Label("FPS: " + m_fps.ToString("f2"));
            _toggleSlow = GUILayout.Toggle(_toggleSlow, "Slow");
            _reelParameters.MaxVelocity = _toggleSlow ? 1 : 40;
        }
    }
}
