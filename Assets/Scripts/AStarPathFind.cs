using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable; // 是否可通行
    public Vector3 worldPosition; // 世界坐标
    public int gridX; // 节点在网格中的X坐标
    public int gridY; // 节点在网格中的Y坐标

    public int gCost; // 从起点到该节点的移动成本
    public int hCost; // 从该节点到终点的启发式成本
    public Node parent; // 父节点，用于回溯路径

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int FCost
    {
        get { return gCost + hCost; } // 总成本
    }
}

public class AStarPathFind : MonoBehaviour
{
    public Transform player; // 玩家Transform
    public Transform target; // 目标Transform
    public LayerMask unwalkableMask; // 不可通行层
    public Vector2 gridWorldSize; // 网格的世界尺寸
    public float nodeRadius; // 节点半径
    public LineRenderer lineRenderer; // 用于绘制路径的Line Renderer

    private Node[,] grid; // 网格
    private float nodeDiameter; // 节点直径
    private int gridSizeX, gridSizeY; // 网格的X和Y尺寸

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void Update()
    {
        FindPath(player.position, target.position); // 每帧更新路径
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); // 检查节点是否可通行
                grid[x, y] = new Node(walkable, worldPoint, x, y); // 创建节点
            }
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = NodeFromWorldPoint(startPos);
        Node targetNode = NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                // 查找FCost最低的节点
                if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode); // 找到路径
                return;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour); // 将节点加入开放列表
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode); // 回溯路径
            currentNode = currentNode.parent;
        }
        path.Reverse();

        DrawPath(path); // 绘制路径
    }

    void DrawPath(List<Node> path)
    {
        Vector3[] pathPositions = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            // 将路径点的Y坐标设置为1
            pathPositions[i] = new Vector3(path[i].worldPosition.x, 0.1f, path[i].worldPosition.z);
        }
        lineRenderer.positionCount = pathPositions.Length;
        lineRenderer.SetPositions(pathPositions); // 设置Line Renderer的路径点


    }


    Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y]; // 返回世界坐标对应的节点
    }

    List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]); // 添加相邻节点
                }
            }
        }

        return neighbours;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY); // 对角线距离
        return 14 * dstX + 10 * (dstY - dstX); // 直线距离
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        FindPath(player.position, target.position); // 设置新目标后重新计算路径
    }

}
