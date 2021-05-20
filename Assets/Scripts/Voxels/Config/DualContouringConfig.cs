﻿using System;
using UnityEngine;

namespace Tuntenfisch.Voxels.Config
{
    [CreateAssetMenu(fileName = "Dual Contouring Config", menuName = "Voxels/Dual Contouring Config", order = 2)]
    public class DualContouringConfig : ScriptableObject
    {
        public event Action OnDirtied;

        public static readonly int[] CellStrides = { 1, 2, 3, 4, 6, 8, 12, 24 };

        // Dual contouring properties.
        public ComputeShader Compute => m_compute;
        public int SchmitzParticleIterations => m_schmitzParticleIterations;
        public float SchmitzParticleStepSize => m_schmitzParticleStepSize;
        public float SharpFeatureAngle => m_sharpFeatureAngle;

        [SerializeField]
        private ComputeShader m_compute;
        [Range(0, 50)]
        [SerializeField]
        private int m_schmitzParticleIterations = 20;
        [Range(0.0f, 0.4f)]
        [SerializeField]
        private float m_schmitzParticleStepSize = 0.2f;
        [Range(0.1f, 180.0f)]
        [SerializeField]
        private float m_sharpFeatureAngle = 50.0f;

        private void OnValidate() => OnDirtied?.Invoke();

        public int GetCellStride(int lod) => CellStrides[lod];
    }
}