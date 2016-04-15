
/*!
 *  @file       Sprite.cs
 *  @brief      スプライト
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using aqua.core;

namespace aqua
{
    /// <summary>
    /// スプライトクラス
    /// </summary>
    public class CSprite
        : core.IDrawObject2D, IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CSprite( )
            : base( )
        {
            // 描画オブジェクトID設定
            m_DrawObjectID = DRAW_OBJECT_ID.SPRITE;
            
            m_Texture   = null;
            Rect        = CRect.Zero;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='texture'>
        /// テクスチャクラス
        /// </param>
        public CSprite( CTexture texture )
            : base( )
        {
            Create( texture, 0 );
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='texture'>
        /// テクスチャクラス
        /// </param>
        /// <param name='priority'>
        /// プライオリティ
        /// </param>
        public CSprite( CTexture texture, uint priority )
            : base( )
        {
            Create( texture, priority );
        }
        
        /// <summary>
        /// スプライト生成
        /// </summary>
        /// <param name='texture'>
        /// テクスチャクラス
        /// </param>
        /// <param name='priority'>
        /// プライオリティ
        /// </param>
        public void Create( CTexture texture, uint priority )
        {
            // テクスチャ設定
            m_Texture       = texture.Texture;
            
            // 読み込み範囲設定
            Rect            = new CRect( 0.0f, 0.0f, m_Texture.Width, m_Texture.Height );
            
            // 回転拡大中心設定
            Center.X        = (float)m_Texture.Width  * 0.5f;
            Center.Y        = (float)m_Texture.Height * 0.5f;
            
            // 描画
            Priority        = priority;
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
            m_Texture   = null;
            Visible     = true;
            Position    = Vector2.Zero;
            Color       = Vector4.One;
            Scaling     = Vector2.One;
            Center      = Vector2.Zero;
            Angle       = 0.0f;
            Rect        = CRect.Zero;
			Priority    = 99999;
        }
        
        /// <summary>
        /// テクスチャ
        /// </summary>
        /// <value>
        /// テクスチャクラス
        /// </value>
        public Texture2D Texture
        {
            get { return m_Texture;  }
            set { m_Texture = value; }
        }
        
        /// <summary>
        /// テクスチャの横幅取得
        /// </summary>
        /// <value>
        /// テクスチャの横幅
        /// </value>
        public int TextureWidth
        {
            get { return m_Texture.Width; }
        }
        
        /// <summary>
        /// テクスチャの縦幅取得
        /// </summary>
        /// <value>
        /// テクスチャの縦幅
        /// </value>
        public int TextureHeight
        {
            get { return m_Texture.Height; }
        }
        
        /// <summary>
        /// 読み込み範囲
        /// </summary>
        public CRect                    Rect;
        
        /// <summary>
        /// テクスチャ
        /// </summary>
        protected Texture2D             m_Texture       = null;
    }
}
