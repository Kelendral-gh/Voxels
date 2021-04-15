﻿using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace CubicalMarchingSquares
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public static VertexAttributeDescriptor[] Attributes
        {
            get
            {
                return s_attributes;
            }
        }

        public static int SizeInBytes 
        {
            get
            {
                return s_sizeInBytes;
            }
        }

        private static readonly VertexAttributeDescriptor[] s_attributes =
        {
            new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3),
            new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3)
        };

        private static readonly int s_sizeInBytes = 6 * sizeof(float);

        private Vector3 m_position;
        private Vector3 m_normal;
    }
}
