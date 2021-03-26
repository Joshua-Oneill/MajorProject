using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator
{
   
    public static void CreateCube(GameObject gameObject, Vector3 offset, Material textureAtlas, Vector2 textureOffset)
    {
        //https://docs.unity3d.com/Manual/Example-CreatingaBillboardPlane.html

        Mesh frontMesh = new Mesh();
        Mesh topMesh = new Mesh();
        Mesh bottomMesh = new Mesh();
        Mesh backMesh = new Mesh();
        Mesh leftMesh = new Mesh();
        Mesh rightMesh = new Mesh();

        GameObject frontFace = new GameObject("frontFace");
        frontFace.transform.SetParent(gameObject.transform);

        GameObject bottomFace = new GameObject("bottomFace");
        bottomFace.transform.SetParent(gameObject.transform);

        GameObject backFace = new GameObject("backFace");
        backFace.transform.SetParent(gameObject.transform);

        GameObject topFace = new GameObject("topFace");
        topFace.transform.SetParent(gameObject.transform);

        GameObject leftFace = new GameObject("leftFace");
        leftFace.transform.SetParent(gameObject.transform);

        GameObject rightFace = new GameObject("rightFace");
        rightFace.transform.SetParent(gameObject.transform);


        MeshFilter frontMeshFilter = frontFace.AddComponent<MeshFilter>();
        MeshFilter bottomMeshFilter = bottomFace.AddComponent<MeshFilter>();
        MeshFilter backMeshFilter = backFace.AddComponent<MeshFilter>();
        MeshFilter topMeshFilter = topFace.AddComponent<MeshFilter>();
        MeshFilter leftMeshFilter = leftFace.AddComponent<MeshFilter>();
        MeshFilter rightMeshFilter = rightFace.AddComponent<MeshFilter>();

        MeshRenderer frontMeshRenderer = frontFace.AddComponent<MeshRenderer>();
      
        frontMeshRenderer.material = textureAtlas;

        MeshRenderer bottomMeshRenderer = bottomFace.AddComponent<MeshRenderer>();
        bottomMeshRenderer.material = textureAtlas;

        MeshRenderer backMeshRenderer = backFace.AddComponent<MeshRenderer>();
        backMeshRenderer.material = textureAtlas;

        MeshRenderer topMeshRenderer = topFace.AddComponent<MeshRenderer>();
        topMeshRenderer.material = textureAtlas;

        MeshRenderer leftMeshRenderer = leftFace.AddComponent<MeshRenderer>();
        leftMeshRenderer.material = textureAtlas;

        MeshRenderer rightMeshRenderer = rightFace.AddComponent<MeshRenderer>();
        rightMeshRenderer.material = textureAtlas;


        Vector3[] vertices = new Vector3[8] //initialize a vector array
        {
            new Vector3(0, 0, 0) + offset, //0
            new Vector3(1, 0, 0) + offset, //1
            new Vector3(0, 1, 0) + offset, //2
            new Vector3(1, 1, 0) + offset, //3
            new Vector3(1, 0, 1) + offset, //4
            new Vector3(1, 1, 1) + offset, //5
            new Vector3(0, 0, 1) + offset, //6
            new Vector3(0, 1, 1) + offset, //7
        };

        frontMesh.vertices = new Vector3[] { vertices[0], vertices[1], vertices[2], vertices[3] };
        bottomMesh.vertices = new Vector3[] { vertices[0], vertices[1], vertices[4], vertices[6] };
        backMesh.vertices = new Vector3[] { vertices[4], vertices[5], vertices[6], vertices[7] };
        topMesh.vertices = new Vector3[] { vertices[2], vertices[3], vertices[5], vertices[7] };
        leftMesh.vertices = new Vector3[] { vertices[0], vertices[2], vertices[6], vertices[7] };
        rightMesh.vertices = new Vector3[] { vertices[1], vertices[3], vertices[4], vertices[5] };

       // bottomMesh.RecalculateBounds();

        frontMesh.triangles = new int[] {0,2,1, 2,3,1};
        bottomMesh.triangles = new int[] { 3, 0, 2, 0, 1, 2 };
        backMesh.triangles = new int[] { 0, 3, 2, 0, 1, 3 };
        topMesh.triangles = new int[] { 3, 2, 1, 0, 3, 1 };
        leftMesh.triangles = new int[] { 0, 2, 3, 3, 1, 0 };
        rightMesh.triangles = new int[] { 1, 2, 0, 1, 3, 2 };

        //topMesh.triangles = new int[] {0, 3, 1, 3, 2, 1 };

        
        Vector2[] calcUVs(float x, float y)
        {
            float offsetX = 0.0625f * x;
            float offsetY = 0.0625f * y;
  
            return new Vector2[4] 
            {   
                new Vector2(offsetX - 0.0625f, offsetY - 0.0625f),  //left bottom corner           
                new Vector2(offsetX, offsetY - 0.0625f),            //right bottom corner
                new Vector2(offsetX, offsetY),                      //right top corner              
                new Vector2(offsetX - 0.0625f, offsetY)             //left top corner 
            
            };
        }

        Vector2[] calcSideUVs(float x, float y)
        {
            float offsetX = 0.0625f * x;
            float offsetY = 0.0625f * y;

            return new Vector2[4]
            {
                new Vector2(offsetX - 0.0625f, offsetY - 0.0625f), //left bottom corner 
                new Vector2(offsetX - 0.0625f, offsetY),       //left top corner 
                new Vector2(offsetX, offsetY - 0.0625f), //right bottom corner
                new Vector2(offsetX, offsetY)       //right top corner 
            };
                //new Vector2(0.375f, 0.4375f), //left bottom corner 
                //new Vector2(0.375f, 0.5f),       //left top corner 
                //new Vector2(0.4375f, 0.4375f), //right bottom corner
                //new Vector2(0.4375f, 0.5f)       //right top corner 


        }


        Vector3[] normals = new Vector3[] 
        {   -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        };
        
        frontMesh.normals = normals;
        bottomMesh.normals = normals;
        backMesh.normals = normals;
        topMesh.normals = normals;
        leftMesh.normals = normals;
        rightMesh.normals = normals;

        Vector2[] uv = calcUVs(textureOffset.x, textureOffset.y); //the paramteres here corrospond to the row and column on the texture atlas, example dirt would be row 3 column 16

        Vector2[] uv1 = calcSideUVs(textureOffset.x, textureOffset.y);

        
        frontMesh.uv = uv1;
        bottomMesh.uv = uv;
        backMesh.uv = uv1;
        topMesh.uv = uv;
        leftMesh.uv = uv1;
        rightMesh.uv = uv1;

        frontMeshFilter.mesh = frontMesh;
        bottomMeshFilter.mesh = bottomMesh;
        backMeshFilter.mesh = backMesh;
        topMeshFilter.mesh = topMesh;
        leftMeshFilter.mesh = leftMesh;
        rightMeshFilter.mesh = rightMesh;
    }

}
