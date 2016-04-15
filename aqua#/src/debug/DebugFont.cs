
/*!
 *  @file       DebugFont.cs
 *  @brief      デバッグ管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using aqua.core;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using System.Text;
using System.Collections.Generic;

namespace aqua
{
    /// <summary>
    /// デバッグフォント用データ
    /// </summary>
    internal sealed class CDebugFontData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CDebugFontData( )
        {
            text        = "";
            position    = Vector2.Zero;
        }
        
        public string      text;
        public Vector2     position;
    }
    
    /// <summary>
    /// デバッグフォントクラス
    /// </summary>
    public sealed class CDebugFont
        : IDisposable
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CDebugFont GetInstance( )
        {
#if DEBUG
            if( m_Instatnce == null )
                m_Instatnce = new CDebugFont( );
            
            return m_Instatnce;
#else
            return null;
#endif
        }
        
        /// <summary>
        /// 描画テキスト設定
        /// </summary>
        /// <param name='text'>
        /// テキスト
        /// </param>
        /// <param name='position'>
        /// 位置
        /// </param>
        public static void WriteLine( string text, Vector2 position )
        {
            GetInstance( ).Add( text, position );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
#if DEBUG
            // 頂点バッファ解放
            m_VertexBuffer.Dispose( );
            
            // シェーダ解放
            m_SpriteShader.Dispose( );
            
            // テクスチャ解放
            m_Texture.Dispose( );
#endif
        }
        
        /// <summary>
        /// 文字列描画
        /// 末尾で改行
        /// </summary>
        /// <param name='text'>
        /// 描画するテキスト
        /// </param>
        public void Add( string text )
        {
#if DEBUG
            Add( text, Vector2.Zero );
#endif
        }
        
        /// <summary>
        /// 文字列描画
        /// 末尾で改行
        /// </summary>
        /// <param name='text'>
        /// 描画するテキスト
        /// </param>
        /// <param name='position'>
        /// 位置
        /// </param>
        public void Add( string text, Vector2 position )
        {
#if DEBUG
            CDebugFontData d = new CDebugFontData( );
            
            d.text      = text + "\n";
            d.position  = position;
            
            m_DebugFontDataList.Add( d );
#endif
        }
        
        /// <summary>
        /// 描画処理
        /// </summary>
        internal void Draw( )
        {
#if DEBUG
            // 描画文字数カウンタ
            int draw_count      = 0;
            int vertex_count    = 0;
            int index_count     = 0;
            int icount = 0;
            
            // フォントサイズ
            float font_w = m_font_size.width;
            float font_h = m_font_size.height;
            
            // テクスチャサイズ
            float tex_w = font_w / (float)m_Texture.Width;
            float tex_h = font_h / (float)m_Texture.Height;
            
            foreach( var data in m_DebugFontDataList )
            {
                // 現在位置
                float offsetX = data.position.X;
                float offsetY = data.position.Y;
                
                int count = 0;
                
                for( int i = 0 ; i < data.text.Length; ++i )
                {
                    // 頂点情報設定
                    m_Vertices[vertex_count + 0] = new Vector2( offsetX + 0.0f,   offsetY + 0.0f   );
                    m_Vertices[vertex_count + 1] = new Vector2( offsetX + font_w, offsetY + 0.0f   );
                    m_Vertices[vertex_count + 2] = new Vector2( offsetX + 0.0f,   offsetY + font_h );
                    m_Vertices[vertex_count + 3] = new Vector2( offsetX + font_w, offsetY + font_h );
                    
                    // 右にずらす
                    offsetX += font_w;
                    
                    int code = data.text[count];
                    
                    // 改行コードがあったら字下げして先頭へ
                    if( code == '\n' )
                    {
                        offsetX =  data.position.X;
                        offsetY += font_h;
                    }
                    
                    int val = 0;
                    
                    // 文字列から読み込み範囲算出
                    if( code >= ' ' && code <= '~' )
                        val = code - ' ';
                    else if( code == '\n' )
                        val = 0;
                    else
                        val = '?' - ' ';
                    
                    int u = val % 16;
                    int v = val / 16;
                    
                    // テクスチャ座標設定
                    m_Texcoords[vertex_count + 0] = new Vector2( tex_w * u,         tex_h * v );
                    m_Texcoords[vertex_count + 1] = new Vector2( tex_w * ( u + 1 ), tex_h * v );
                    m_Texcoords[vertex_count + 2] = new Vector2( tex_w * u,         tex_h * ( v + 1 ) );
                    m_Texcoords[vertex_count + 3] = new Vector2( tex_w * ( u + 1 ), tex_h * ( v + 1 ) );
                    
                    ++count;
                    
                    vertex_count += m_vertices_num;
                    
                    ++draw_count;
                }
                
                count = 0;
                
                // インデックスデータ設定
                for( int i = 0; i < data.text.Length; ++i )
                {
                    m_Indices[index_count + 0] = (ushort)( icount + 0 );
                    m_Indices[index_count + 1] = (ushort)( icount + 1 );
                    m_Indices[index_count + 2] = (ushort)( icount + 2 );
                    m_Indices[index_count + 3] = (ushort)( icount + 1 );
                    m_Indices[index_count + 4] = (ushort)( icount + 2 );
                    m_Indices[index_count + 5] = (ushort)( icount + 3 );
                    
                    index_count += m_indices_num;
                    
                    icount += m_vertices_num;
                }
            }
            
            // グラフィックスコンテキスト取得
            CGraphicsContext context = CGraphicsContext.GetInstance( );
            
            // カメラ行列取得
            Matrix4 view        = m_Camera.ViewMatrix;
            Matrix4 projection  = m_Camera.ProjectionMatrix;
            Matrix4 transform   = Matrix4.Identity;
            
            // シェーダプログラムにビュー行列設定
            m_SpriteShader.SetUniformValue( 0, ref view );
            m_SpriteShader.SetUniformValue( 1, ref projection );
            
            // トランスフォーム行列設定
            m_SpriteShader.SetUniformValue( 2, ref transform );
            
            // 色設定
            Vector4 color = CColor.White;
            
            m_SpriteShader.SetUniformValue( 3, ref color );
            
            // シェーダプログラム設定
            context.GraphicsContext.SetShaderProgram( m_SpriteShader );
            
            // 頂点バッファに設定
            m_VertexBuffer.SetVertices( 0, m_Vertices );
            m_VertexBuffer.SetVertices( 1, m_Texcoords );

            // インデックスバッファ設定
            m_VertexBuffer.SetIndices( m_Indices );
            
            // 頂点バッファ設定
            context.GraphicsContext.SetVertexBuffer( 0, m_VertexBuffer );
            
            // テクスチャ設定
            context.GraphicsContext.SetTexture( 0, m_Texture );
            
            // アルファブレンド設定
            context.SetAlphaBlend( ALPHABLEND_TYPE.TRANS );
            
            // 頂点描画
            context.GraphicsContext.DrawArrays( DrawMode.Triangles, 0, draw_count * m_indices_num );
            
            // テクスチャ解除
            context.GraphicsContext.SetTexture( 0, null );
            
            // リストクリア
            m_DebugFontDataList.Clear( );
#endif
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CDebugFont( )
        {
#if DEBUG
            // テクスチャ読み込み
            if( m_Texture == null )
                m_Texture = core.CTextureManager.GetInstance( ).Load( m_texture_name, true );
            
            // 頂点バッファ生成
            if( m_VertexBuffer == null )
                m_VertexBuffer = new VertexBuffer( m_max_text_length * m_vertices_num, m_max_text_length * m_indices_num, VertexFormat.Float2, VertexFormat.Float2 );
            
            // 頂点座標用バッファ作成
            if( m_Vertices == null )
                m_Vertices = new Vector2[m_max_text_length * m_vertices_num];
            
            // テクスチャ座標用バッファ作成
            if( m_Texcoords == null )
                m_Texcoords = new Vector2[m_max_text_length * m_vertices_num];
            
            // インデックスバッファ作成
            if( m_Indices == null )
                m_Indices = new ushort[m_max_text_length * m_indices_num];
            
            // カメラ生成
            if( m_Camera == null )
            {
                m_Camera = new CCamera2D( );
            
                // 更新
                m_Camera.Update( );
            }
            
            // シェーダプログラム読み込み
            if( m_SpriteShader == null )
            {
                // 組み込みのスプライトシェーダ読み込み
                byte[] shader = core.CEmbeddedFileLoader.Load( m_sprite_shader_name );
                
                // シェーダプログラム生成
                m_SpriteShader = new ShaderProgram( shader );
                
                // アトリビュート変数設定
                m_SpriteShader.SetAttributeBinding( 0, "a_Position" );
                m_SpriteShader.SetAttributeBinding( 1, "a_Texcoord0" );
                
                // ユニフォーム変数設定
                m_SpriteShader.SetUniformBinding( 0, "u_ViewMatrix" );
                m_SpriteShader.SetUniformBinding( 1, "u_ProjectionMatrix" );
                m_SpriteShader.SetUniformBinding( 2, "u_TransformMatrix" );
                m_SpriteShader.SetUniformBinding( 3, "u_DiffuseColor" );
            }
            
            // リスト生成
            m_DebugFontDataList = new List<CDebugFontData>( );
#endif
        }
        
#if DEBUG
        /// <summary>
        /// フォントサイズ
        /// </summary>
        private readonly CSize          m_font_size         = new CSize( 10.0f, 20.0f );
        
        /// <summary>
        /// テクスチャ名
        /// </summary>
        private const string            m_texture_name      = "aqua.resources.debug_font.png";
        
        
        /// <summary>
        /// スプライト用シェーダプログラム名
        /// </summary>
        private const string            m_sprite_shader_name = "aqua.shaders.sprite.cgx";
        
        /// <summary>
        /// 文字列の長さの最大値
        /// </summary>
        private const int               m_max_text_length   = 512;
        
        /// <summary>
        /// 1文字あたりの頂点数
        /// </summary>
        private const int               m_vertices_num      = 4;
        
        /// <summary>
        /// 1文字あたりのインデックス数
        /// </summary>
        private const int               m_indices_num       = 6;
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CDebugFont       m_Instatnce         = null;
        
        /// <summary>
        /// デバッグフォントテクスチャ
        /// </summary>
        private Texture2D               m_Texture           = null;
        
        /// <summary>
        /// 頂点バッファ
        /// </summary>
        private VertexBuffer            m_VertexBuffer      = null;
        
        /// <summary>
        /// 頂点データ
        /// </summary>
        private Vector2[]               m_Vertices          = null;
        
        /// <summary>
        /// テクスチャ座標
        /// </summary>
        private Vector2[]               m_Texcoords         = null;
        
        /// <summary>
        /// インデックスデータ
        /// </summary>
        private ushort[]                m_Indices           = null;
                
        /// <summary>
        /// 2Dカメラ
        /// </summary>
        private CCamera2D               m_Camera            = null;
        
        /// <summary>
        /// スプライト用シェーダプログラム
        /// </summary>
        private ShaderProgram           m_SpriteShader      = null;
        
        /// <summary>
        /// 描画データリスト
        /// </summary>
        private List<CDebugFontData>    m_DebugFontDataList;
#endif
    }
}
