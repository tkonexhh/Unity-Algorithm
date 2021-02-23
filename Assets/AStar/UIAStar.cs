using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Algorithm.AStar
{
    public class UIAStar : MonoBehaviour
    {
        [SerializeField] GridMap m_GridMap;
        [SerializeField] Button m_BtnStart;
        [SerializeField] Button m_BtnClear;
        [SerializeField] Toggle m_ToggleStart;
        [SerializeField] Toggle m_ToggleEnd;
        [SerializeField] Toggle m_ToggleWall;

        private NodeType m_CurType;

        private void Awake()
        {
            m_CurType = NodeType.Wall;
            m_ToggleWall.isOn = true;
            m_ToggleStart.onValueChanged.AddListener((v) => { m_CurType = NodeType.Start; });
            m_ToggleEnd.onValueChanged.AddListener((v) => { m_CurType = NodeType.End; });
            m_ToggleWall.onValueChanged.AddListener((v) => { m_CurType = NodeType.Wall; });
            m_BtnStart.onClick.AddListener(() => { m_GridMap.StartAStar(); });
            m_BtnClear.onClick.AddListener(() => { m_GridMap.Clear(); });
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    var nodeItem = hit.collider.gameObject.GetComponent<NodeItem>();
                    if (nodeItem != null)
                    {

                        if (m_CurType == NodeType.Start)
                        {
                            nodeItem.nodeType = m_CurType;
                            m_GridMap.SetStartNode(nodeItem);
                        }
                        else if (m_CurType == NodeType.End)
                        {
                            nodeItem.nodeType = m_CurType;
                            m_GridMap.SetEndNode(nodeItem);
                        }
                        else// if (m_CurType == NodeType.Wall)
                        {
                            if (nodeItem.nodeType == NodeType.Wall)
                            {
                                nodeItem.nodeType = NodeType.Normal;
                            }
                            else
                                nodeItem.nodeType = m_CurType;
                        }
                    }
                }
            }
        }
    }
}