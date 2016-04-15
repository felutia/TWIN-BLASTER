
/*!
 *  @file       SpriteManager.cs
 *  @brief      スプライト管理
 *  @author     Kazuya Maruyama
 *  @date       2014/11/16
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace aqua.core
{
    /// <summary>
    /// スプライト管理クラス
    /// </summary>
    public sealed class CSpriteManager
        : IDisposable
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CSpriteManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CSpriteManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize( )
        {
            // 描画用スプライトリスト生成
            m_Sprites = new CSprite[m_max_sprite_num];
            
            for( int i = 0; i < m_max_sprite_num; ++i )
                m_Sprites[i] = new CSprite( );
            
            // スプライト頂点データ生成
            m_SpriteVertex = new CSpriteVertex[m_max_sprite_num];
            
            // 頂点バッファを作成
            m_VertexBuffer1 = new VertexBuffer( m_max_sprite_num, 0, 1, m_sprite_vertex_format );
            
            float[] points = { 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 1.0f };
            
            // インスタンス描画用頂点バッファ
            m_VertexBuffer2 = new VertexBuffer( 4, VertexFormat.Float2 );
            
            // 頂点データ設定
            m_VertexBuffer2.SetVertices( 0, points );
            
            // 組み込みのスプライトシェーダ読み込み
            byte[] shader = core.CEmbeddedFileLoader.Load( m_sprite_shader_name );
            
            // シェーダプログラム生成
            m_SpriteShader = new ShaderProgram( shader );
            
            // ユニフォーム変数設定
            m_SpriteShader.SetUniformBinding( 0, "u_ViewMatrix" );
            m_SpriteShader.SetUniformBinding( 1, "u_ProjectionMatrix" );
            
            // 2Dカメラ生成
            m_Camera = new CCamera2D( );
            
            // 更新
            m_Camera.Update( );
            
            // カメラ行列取得
            Matrix4 view        = m_Camera.ViewMatrix;
            Matrix4 projection  = m_Camera.ProjectionMatrix;
            
            // シェーダプログラムにビュー行列設定
            m_SpriteShader.SetUniformValue( 0, ref view );
            m_SpriteShader.SetUniformValue( 1, ref projection );
            
            // スプライトカウントリスト
            m_CountList = new List<DrawTexData>( );
            
            // ソートフラグ
            m_SortFlag = true;
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public void Update( )
        {
            // スプライトカウントリストのクリア
            m_CountList.Clear( );
            
            // ソート処理の実行
            if( m_SortFlag == true )
            {
                Sort( );
                
                m_SortFlag = false;
            }
            
            CRect   rect  = null;
            float   inv_w = 0.0f;
            float   inv_h = 0.0f;
            Vector2 size;
            
            int count = 0;
            
            // 先頭のテクスチャ取得
            Texture2D texture = m_Sprites[0].Texture;
            
            // 描画カウントリセット
            m_DrawSpriteCount = 0;
            
            // 描画用頂点バッファの作成
            for( int i = 0; i < m_Sprites.Length; ++i )
            {
                if( m_Sprites[i].Texture == null  ) continue;
                if( m_Sprites[i].Visible == false ) continue;
                
                // テクスチャ読み込み範囲取得
                rect = m_Sprites[i].Rect;
                
                // 読み込みサイズ算出
                size.X = rect.right  - rect.left;
                size.Y = rect.bottom - rect.top;
                
                // 表示位置設定
                m_SpriteVertex[m_DrawSpriteCount].position      = m_Sprites[i].Position;
                
                // 回転値設定
                m_SpriteVertex[m_DrawSpriteCount].direction     = m_Sprites[i].Angle;
                
                // 読み込み範囲から表示サイズ算出
                m_SpriteVertex[m_DrawSpriteCount].size          = size;
                
                // スケーリング
                m_SpriteVertex[m_DrawSpriteCount].size.X        *= m_Sprites[i].Scaling.X;
                m_SpriteVertex[m_DrawSpriteCount].size.Y        *= m_Sprites[i].Scaling.Y;
                
                // テクスチャ座標レート
                inv_w = 1.0f / (float)m_Sprites[i].TextureWidth;
                inv_h = 1.0f / (float)m_Sprites[i].TextureHeight;
                
                // 回転拡大中心設定
                m_SpriteVertex[m_DrawSpriteCount].center.X      = m_Sprites[i].Center.X * inv_w;
                m_SpriteVertex[m_DrawSpriteCount].center.Y      = m_Sprites[i].Center.Y * inv_h;
                
                // テクスチャ座標設定
                m_SpriteVertex[m_DrawSpriteCount].uv_offset.X   = rect.left   * inv_w;
                m_SpriteVertex[m_DrawSpriteCount].uv_offset.Y   = rect.top    * inv_h;
                m_SpriteVertex[m_DrawSpriteCount].uv_size.X     = size.X      * inv_w;
                m_SpriteVertex[m_DrawSpriteCount].uv_size.Y     = size.Y      * inv_h;
                
                // 色設定
                m_SpriteVertex[m_DrawSpriteCount].color         = m_Sprites[i].Color;
                
                // テクスチャサイズ設定
                m_SpriteVertex[m_DrawSpriteCount].tex_size      = size;
                
                // 頂点データ数をカウント
                ++m_DrawSpriteCount;
                
                // テクスチャ毎にスプライトの枚数をカウント
                if( m_Sprites[i].Texture != texture )
                {
                    DrawTexData d;
                    
                    d.texture       = texture;
                    d.draw_count    = count;
                    
                    m_CountList.Add( d );
                    
                    count = 1;
                    
                    texture = m_Sprites[i].Texture;
                }
                else
                {
                    ++count;
                }
            }
            
            if( count > 0 )
            {
                DrawTexData t;
                        
                t.texture       = texture;
                t.draw_count    = count;
                        
                m_CountList.Add( t );
            }
        }
        
        /// <summary>
        /// 描画
        /// </summary>
        public void Draw( )
        {
            if( m_CountList.Count <= 0 ) return;
            
            // グラフィックスコンテキスト取得
            CGraphicsContext context = CGraphicsContext.GetInstance( );
            
            // 深度テストOFF
            context.GraphicsContext.Disable( EnableMode.DepthTest );
            
            // アルファブレンド設定
            context.SetAlphaBlend( ALPHABLEND_TYPE.TRANS );
            
            // 頂点バッファに設定
            m_VertexBuffer1.SetVertices( m_SpriteVertex, 0, 0, m_DrawSpriteCount );
            
            // 頂点バッファ設定
            context.GraphicsContext.SetVertexBuffer( 0, m_VertexBuffer1 );
            context.GraphicsContext.SetVertexBuffer( 1, m_VertexBuffer2 );
            
            // シェーダプログラム設定
            context.GraphicsContext.SetShaderProgram( m_SpriteShader );
            
            int count = 0;
            
            // テクスチャの回数分だけ回す
            for( int i = 0; i < m_CountList.Count; ++i )
            {
                // テクスチャ設定
                context.GraphicsContext.SetTexture( 0, m_CountList[i].texture );
                
                // 頂点描画
                // 同じテクスチャだったらまとめて描画する
                context.GraphicsContext.DrawArraysInstanced( DrawMode.TriangleStrip, 0, 4, count, m_CountList[i].draw_count );
                
                // 描画した頂点数をカウント
                count += m_CountList[i].draw_count;
            }
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )        {
            if( m_SpriteShader != null )
                m_SpriteShader.Dispose( );
            
            if( m_VertexBuffer2 != null )
                m_VertexBuffer2.Dispose( );
            
            if( m_VertexBuffer1 != null )
                m_VertexBuffer1.Dispose( );
        }
        
        /// <summary>
        /// スプライトの生成
        /// </summary>
        public CSprite Create( CTexture texture, uint priority )
        {
            for( int i = m_max_sprite_num - 1; i >= 0; --i )
            {
                // テクスチャが設定されていないスプライトを検索
                if( m_Sprites[i].Texture == null )
                {
                    m_Sprites[i].Create( texture, priority );
                    
                    // ソートする
                    //m_SortFlag = true;
                    
                    return m_Sprites[i];
                }
            }
            
            return null;
        }
		
		public void SpriteReset( )
		{
			if( m_Sprites != null )
			{
				foreach( var s in m_Sprites )
					s.Dispose( );
			}
		}
        
        /// <summary>
        /// ソートフラグの取得と設定
        /// </summary>
        /// <value>
        /// ソートフラグ
        /// </value>
        public bool SortFlag
        {
            get { return m_SortFlag;  }
            set { m_SortFlag = value; }
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CSpriteManager( )
        {
        }
        
        /// <summary>
        /// プライオリティでソート
        /// </summary>
        private void Sort( )
        {
            if( m_Sprites == null ) return;
            
            // プライオリティでソート
            Array.Sort( m_Sprites, delegate( CSprite a, CSprite b ) { return a.Priority.CompareTo( b.Priority ); } );
        }
        
        /// <summary>
        /// スプライト頂点データ
        /// </summary>
        [StructLayout( LayoutKind.Explicit, Size = 68 )]
        internal struct CSpriteVertex
        {
            /// <summary>
            /// 位置
            /// </summary>
            [FieldOffset( 0 )]
            public Vector2 position;
            
            /// <summary>
            /// 回転値
            /// </summary>
            [FieldOffset( 8 )]
            public float   direction;
            
            /// <summary>
            /// サイズ
            /// </summary>
            [FieldOffset( 12 )]
            public Vector2 size;
            
            /// <summary>
            /// 回転拡大中心
            /// </summary>
            [FieldOffset( 20 )]
            public Vector2 center;
            
            /// <summary>
            /// テクスチャ座標
            /// </summary>
            [FieldOffset( 28 )]
            public Vector2 uv_offset;
            
            /// <summary>
            /// テクスチャ読み込みサイズ
            /// </summary>
            [FieldOffset( 36 )]
            public Vector2 uv_size;
            
            /// <summary>
            /// 色
            /// </summary>
            [FieldOffset( 44 )]
            public Vector4 color;
            
            /// <summary>
            /// テクスチャサイズ
            /// </summary>
            [FieldOffset( 60 )]
            public Vector2 tex_size;
         }
        
        /// <summary>
        /// 頂点データフォーマット
        /// </summary>
        internal static readonly VertexFormat[] m_sprite_vertex_format =
        {
            VertexFormat.Float3,    // Position Direction
            VertexFormat.Float4,    // Size     Center
            VertexFormat.Float4,    // UVOffset UVSize
            VertexFormat.Float4,    // Color
            VertexFormat.Float2,    // TextureSize
        };
        
        /// <summary>
        /// 描画テクスチャ情報
        /// </summary>
        internal struct DrawTexData
        {
            /// <summary>
            /// テクスチャ情報
            /// </summary>
            public Texture2D    texture;
            
            /// <summary>
            /// 描画カウント数
            /// </summary>
            public int          draw_count;
        }
        
        /// <summary>
        /// スプライト用シェーダプログラム名
        /// </summary>
        private const string                m_sprite_shader_name = "aqua.shaders.sprite_batch.cgx";
        
        /// <summary>
        /// 最大スプライト数
        /// </summary>
        private const int                   m_max_sprite_num    = 2000;
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CSpriteManager       m_Instance          = null;
        
        /// <summary>
        /// 描画オブジェクトリスト
        /// </summary>
        private CSprite[]                   m_Sprites           = null;
        
        /// <summary>
        /// 描画スプライト数
        /// </summary>
        private int                         m_DrawSpriteCount   = 0;
        
        /// <summary>
        /// スプライト頂点データ
        /// </summary>
        private CSpriteVertex[]             m_SpriteVertex      = null;
        
        /// <summary>
        /// 頂点バッファ
        /// </summary>
        private VertexBuffer                m_VertexBuffer1     = null;
        
        /// <summary>
        /// 頂点バッファ
        /// </summary>
        private VertexBuffer                m_VertexBuffer2     = null;
        
        /// <summary>
        /// 2Dカメラ
        /// </summary>
        private static CCamera2D            m_Camera            = null;
        
        /// <summary>
        /// スプライト用シェーダプログラム
        /// </summary>
        private static ShaderProgram        m_SpriteShader      = null;
        
        /// <summary>
        /// スプライト描画カウントリスト
        /// </summary>
        private List<DrawTexData>           m_CountList         = null;
        
        /// <summary>
        /// ソートの実行フラグ
        /// </summary>
        private bool                        m_SortFlag          = false;
    }
}
