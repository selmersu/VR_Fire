using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable; // �Ƿ��ͨ��
    public Vector3 worldPosition; // ��������
    public int gridX; // �ڵ��������е�X����
    public int gridY; // �ڵ��������е�Y����

    public int gCost; // ����㵽�ýڵ���ƶ��ɱ�
    public int hCost; // �Ӹýڵ㵽�յ������ʽ�ɱ�
    public Node parent; // ���ڵ㣬���ڻ���·��

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int FCost
    {
        get { return gCost + hCost; } // �ܳɱ�
    }
}

public class AStarPathFind : MonoBehaviour
{
    public Transform player; // ���Transform
    public Transform target; // Ŀ��Transform
    public LayerMask unwalkableMask; // ����ͨ�в�
    public Vector2 gridWorldSize; // ���������ߴ�
    public float nodeRadius; // �ڵ�뾶
    public LineRenderer lineRenderer; // ���ڻ���·����Line Renderer

    private Node[,] grid; // ����
    private float nodeDiameter; // �ڵ�ֱ��
    private int gridSizeX, gridSizeY; // �����X��Y�ߴ�

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void Update()
    {
        FindPath(player.position, target.position); // ÿ֡����·��
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
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); // ���ڵ��Ƿ��ͨ��
                grid[x, y] = new Node(walkable, worldPoint, x, y); // �����ڵ�
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
                // ����FCost��͵Ľڵ�
                if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode); // �ҵ�·��
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
                        openSet.Add(neighbour); // ���ڵ���뿪���б�
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
            path.Add(currentNode); // ����·��
            currentNode = currentNode.parent;
        }
        path.Reverse();

        DrawPath(path); // ����·��
    }

    void DrawPath(List<Node> path)
    {
        Vector3[] pathPositions = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            // ��·�����Y��������Ϊ1
            pathPositions[i] = new Vector3(path[i].worldPosition.x, 0.1f, path[i].worldPosition.z);
        }
        lineRenderer.positionCount = pathPositions.Length;
        lineRenderer.SetPositions(pathPositions); // ����Line Renderer��·����


    }


    Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y]; // �������������Ӧ�Ľڵ�
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
                    neighbours.Add(grid[checkX, checkY]); // ������ڽڵ�
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
            return 14 * dstY + 10 * (dstX - dstY); // �Խ��߾���
        return 14 * dstX + 10 * (dstY - dstX); // ֱ�߾���
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        FindPath(player.position, target.position); // ������Ŀ������¼���·��
    }

}
