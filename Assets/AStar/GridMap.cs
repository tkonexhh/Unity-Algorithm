using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algorithm.AStar
{
    public class GridMap : MonoBehaviour
    {
        public GameObject NodeWall;
        public GameObject NodeNormal;

        private NodeItem[,] grid;
        private int w, h;

        private NodeItem m_StartNode;
        private NodeItem m_EndNode;

        private void Awake()
        {
            w = h = 20;
            grid = new NodeItem[w, h];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Vector3 pos = new Vector3(x * 0.5f - 4, y * 0.5f - 4, 1);
                    var obj = GameObject.Instantiate(NodeNormal, pos, Quaternion.identity);
                    obj.transform.localScale = Vector3.one * 3f;
                    obj.transform.SetParent(transform);
                    obj.transform.name = x + "-" + y;
                    var node = obj.GetComponent<NodeItem>();
                    node.x = x;
                    node.y = y;
                    node.title = obj.transform.name;
                    grid[x, y] = node;
                }
            }

            NodeNormal.SetActive(false);
        }

        public void Clear()
        {
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    grid[x, y].visited = false;
                }
            }
        }

        public void SetStartNode(NodeItem node)
        {
            if (m_StartNode != null)
            {
                m_StartNode.nodeType = NodeType.Normal;
            }
            m_StartNode = node;
        }

        public void SetEndNode(NodeItem node)
        {
            if (m_EndNode != null)
            {
                m_EndNode.nodeType = NodeType.Normal;
            }
            m_EndNode = node;
        }

        public void StartAStar()
        {
            if (m_EndNode == null || m_StartNode == null) return;

            List<NodeItem> openList = new List<NodeItem>();
            openList.Add(m_StartNode);
            m_StartNode.gCost = 0;
            m_StartNode.hCost = GetDistance(m_StartNode, m_EndNode);


            StartCoroutine(NextStep(openList));
        }

        IEnumerator NextStep(List<NodeItem> openList)
        {
            yield return new WaitForSeconds(1.0f);

            var curNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].cost <= curNode.cost && openList[i].hCost < curNode.hCost)
                {
                    curNode = openList[i];
                }
            }
            curNode.SetColor(Color.blue);
            openList.Remove(curNode);
            curNode.visited = true;
            // closedList.Add(curNode);

            if (curNode == m_EndNode)
            {
                Debug.LogError("Find");
            }
            else
            {
                foreach (var node in GetNeigbour(curNode))
                {
                    if (!node.walkAble || node.visited) continue;
                    int newGCost = curNode.gCost + 1;//GetDistance(curNode, m_StartNode);


                    if (newGCost < node.gCost || !openList.Contains(node))
                    {
                        node.gCost = newGCost;
                        node.hCost = GetDistance(node, m_EndNode);
                        node.title = node.gCost + "-" + node.hCost;
                        node.visited = true;

                        if (!openList.Contains(node))
                        {
                            node.SetColor(Color.yellow);
                            openList.Add(node);
                        }
                    }
                    Debug.LogError("1111");
                }
                // Debug.LogError(openList.Count);
                StartCoroutine(NextStep(openList));
            }

        }

        public void NextStep()
        {

        }

        public List<NodeItem> GetNeigbour(NodeItem node)
        {
            List<NodeItem> neig = new List<NodeItem>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    if (x == -1 && y == -1) continue;
                    if (x == -1 && y == 1) continue;
                    if (x == 1 && y == -1) continue;
                    if (x == 1 && y == 1) continue;
                    int nodeX = node.x + x;
                    int nodeY = node.y + y;

                    if (nodeX < w && nodeX >= 0 && nodeY < h && nodeY >= 0)
                    {
                        neig.Add(grid[nodeX, nodeY]);
                    }
                }
            }
            return neig;
        }



        private int GetDistance(NodeItem startNode, NodeItem endNode)
        {
            return Manhattan(startNode, endNode);
        }

        //估价方法
        private int Manhattan(NodeItem startNode, NodeItem endNode)
        {
            return Mathf.Abs(startNode.x - endNode.x) + Mathf.Abs(startNode.y - endNode.y);
        }

        private int EulerDistance(NodeItem startNode, NodeItem endNode)
        {
            int x = Mathf.Abs(startNode.x - endNode.x);
            int y = Mathf.Abs(startNode.y - endNode.y);
            return (int)Mathf.Sqrt(x * x + y * y);
        }

    }

}