using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeItem : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_TMPTitle;

    private bool m_Visited = false;
    public int x, y;

    public int gCost;
    public int hCost;

    public int cost => gCost + hCost;

    private NodeType m_NodeType = NodeType.Normal;

    private SpriteRenderer m_SpriteRenderer;


    public bool visited
    {
        set
        {
            if (value)
            {
                m_SpriteRenderer.color = Color.green;
            }
            else
            {
                SetColor(m_NodeType);
            }
        }
        get => m_Visited;
    }

    public string title
    {
        set
        {
            m_TMPTitle.text = value;//gCost + "+" + hCost;
        }
    }

    public NodeType nodeType
    {
        set
        {
            m_NodeType = value;
            SetColor(m_NodeType);
        }
        get => m_NodeType;
    }

    public bool walkAble
    {
        get { return m_NodeType != NodeType.Wall; }
    }

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        m_SpriteRenderer.color = color;
    }

    public void SetColor(NodeType type)
    {
        switch (type)
        {
            case NodeType.Start:
                m_SpriteRenderer.color = Color.red;
                break;
            case NodeType.End:
                m_SpriteRenderer.color = Color.blue;
                break;
            case NodeType.Wall:
                m_SpriteRenderer.color = Color.gray;
                break;
            case NodeType.Normal:
                m_SpriteRenderer.color = Color.white;
                break;
        }
    }
}


public enum NodeType
{
    Normal,
    Start,
    End,
    Wall,
}