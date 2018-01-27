﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transmission.World
{
    public class NodeManager
    {
        private List<INode> mNodes;
        private static NodeManager mInstance = null;
        private Texture2D mWhiteDisk;

        private NodeManager()
        {
            mNodes = new List<INode>();
            mWhiteDisk = Transmission.Instance().CM().Load<Texture2D>("white_disk");
        }

        public static NodeManager Instance()
        {
            if (mInstance == null)
            {
                mInstance = new NodeManager();
            }
            return mInstance;
        }

        public void CheckWave(Wave pWave)
        {
            for(int i= 0; i < mNodes.Count; i++)
            {
                mNodes[i].IntersectCheck(pWave);
            }
        }

        public void AddNode(INode pNode)
        {
            mNodes.Add(pNode);
        }

        public void Draw(SpriteBatch pSpriteBatch, float pSeconds)
        {
            for (int i = 0; i < mNodes.Count; i++)
            {
                pSpriteBatch.Draw(mNodes[i].Texture, mNodes[i].Rect, mNodes[i].Colour);
            }
        }

        public void Update(float pSeconds)
        {
            for (int i = 0; i < mNodes.Count; i++)
            {
                mNodes[i].Update(pSeconds);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPosition"></param>
        /// <returns>If hack was used</returns>
        public bool CheckMouseClick(Point pPosition)
        {
            for (int i = 0; i < mNodes.Count; i++)
            {
                if(mNodes[i].MouseClick(pPosition))
                {
                    return true;
                }
            }
            return false;
        }
    }
}