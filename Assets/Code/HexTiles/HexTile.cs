﻿using UnityEngine;
using System.Collections;
using System;

namespace HexTiles
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class HexTile : MonoBehaviour
    {
        /// <summary>
        /// Total diameter of the hex.
        /// </summary>
        [HideInInspector]
        private float size = 1f;

        /// <summary>
        /// Total diameter of the hex.
        /// </summary>
        public float Diameter
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        /// <summary>
        /// sqrt(3)/2
        /// The ratio of a flat-topped hexagon's height to its width.
        /// </summary>
        public static readonly float hexHeightToWidth = 0.86602540378f;

        /// <summary>
        /// Create the mesh used to render the hex.
        /// </summary>
        [ContextMenu("Generate mesh")]
        public void GenerateMesh()
        {
            var mesh = GetComponent<MeshFilter>().mesh = new Mesh();
            mesh.name = "Procedural hex tile";

            var vertices = HexMetrics.GetHexVertices(Diameter);
            var uv = new Vector2[vertices.Length];
            var tangents = new Vector4[vertices.Length];

            for (var i = 0; i < vertices.Length; i++)
            {
                tangents[i].Set(1f, 0f, 0f, -1f);
            }

            mesh.vertices = vertices;

            // Calculate triangles.
            mesh.triangles = new int[] {
                0, 1, 5,
                1, 4, 5,
                1, 2, 4,
                2, 3, 4
            };

            mesh.RecalculateNormals();

            GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        /// <summary>
        /// Generates and adds a side piece to the tile.
        /// </summary>
        internal void AddSidePiece(HexCoords side, float height)
        {
            Debug.Log("Adding side piece on side: " + side + " with height " + height);
        }
    }
}
