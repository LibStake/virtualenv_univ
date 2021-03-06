﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeMeshGenerator
{
    public readonly float Width;
    public readonly float Height;

    public MazeMeshGenerator()
    {
        Width = 3.75f;
        Height = 3.5f;
    }
    public MazeMeshGenerator(float width, float height)
    {
        // Default maze scale
        Width = width;
        Height = height;
    }

    public Mesh FromData(int[,] data)
    {
        // Gen Walls iterating all over matrix
        Mesh maze = new Mesh();
        
        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();

        maze.subMeshCount = 2;
        List<int> floorTriangles = new List<int>();
        List<int> wallTriangles = new List<int>();

        int rMax = data.GetUpperBound(0);
        int cMax = data.GetUpperBound(1);
        float halfH = Height * .5f;
        
        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (data[i, j] != 0)
                {
                    // floor
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * Width, 0, i * Width),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(Width, Width, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);

                    // walls on sides next to blocked grid cells
                    if (i - 1 < 0 || data[i-1, j] == 0)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * Width, halfH, (i-.5f) * Width),
                            Quaternion.LookRotation(Vector3.forward),
                            new Vector3(Width, Height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (j + 1 > cMax || data[i, j+1] == 0)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j+.5f) * Width, halfH, i * Width),
                            Quaternion.LookRotation(Vector3.left),
                            new Vector3(Width, Height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (j - 1 < 0 || data[i, j-1] == 0)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j-.5f) * Width, halfH, i * Width),
                            Quaternion.LookRotation(Vector3.right),
                            new Vector3(Width, Height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (i + 1 > rMax || data[i+1, j] == 0)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * Width, halfH, (i+.5f) * Width),
                            Quaternion.LookRotation(Vector3.back),
                            new Vector3(Width, Height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }
                    
                }
                else
                {
                    // Wall ceiling
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * Width, Height, i * Width),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(Width, Width, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);
                }
            }
        }

        maze.vertices = newVertices.ToArray();
        maze.uv = newUVs.ToArray();
        
        maze.SetTriangles(floorTriangles.ToArray(), 0);
        maze.SetTriangles(wallTriangles.ToArray(), 1);
        
        maze.RecalculateNormals();

        return maze;
    }

    private void AddQuad(Matrix4x4 matrix, ref List<Vector3> newVertices, ref List<Vector2> newUVs,
        ref List<int> newTriangles)
    {
        // Gen Vertices info
        int index = newVertices.Count;

        Vector3 vert1 = new Vector3(-.5f, -.5f, 0);
        Vector3 vert2 = new Vector3(-.5f, .5f, 0);
        Vector3 vert3 = new Vector3(.5f, .5f, 0);
        Vector3 vert4 = new Vector3(.5f, -.5f, 0);
        
        newVertices.Add(matrix.MultiplyPoint3x4(vert1));
        newVertices.Add(matrix.MultiplyPoint3x4(vert2));
        newVertices.Add(matrix.MultiplyPoint3x4(vert3));
        newVertices.Add(matrix.MultiplyPoint3x4(vert4));
        
        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));
        
        newTriangles.Add(index+2);
        newTriangles.Add(index+1);
        newTriangles.Add(index);

        newTriangles.Add(index+3);
        newTriangles.Add(index+2);
        newTriangles.Add(index);
    }

    private void AsEntrance()
    {
        
    }

    private void AsSpot()
    {
        
    }
}
