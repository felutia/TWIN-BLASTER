
/*!
 *  @file       Texture.cs
 *  @brief      テクスチャ管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core.Graphics;
using aqua.core;

namespace aqua
{
    /// <summary>
    /// テクスチャクラス
    /// </summary>
    public class CTexture
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CTexture( )
        {
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルへのパス
        /// </param>
        public CTexture( string file_path )
        {
            Load( file_path );
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルへのパス
        /// </param>
        /// <param name='embedded_file'>
        /// 組み込みファイル
        /// </param>
        public CTexture( string file_path, bool embedded_file )
        {
            Load( file_path, embedded_file );
        }
        
        /// <summary>
        /// テクスチャ読み込み
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルへのパス
        /// </param>
        public void Load( string file_path )
        {
            Load( file_path, false );
        }
        
        /// <summary>
        /// テクスチャ読み込み
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルへのパス
        /// </param>
        /// <param name='embedded_file'>
        /// 組み込みファイル
        /// </param>
        public void Load( string file_path, bool embedded_file )
        {
            // ファイルパスの保存
            m_FilePath = file_path;
            
            // テクスチャ読み込み
            m_Texture = CTextureManager.GetInstance( ).Load( m_FilePath, embedded_file );
        }
        
        /// <summary>
        /// テクスチャ解放
        /// </summary> 
        public void Unload( )
        {
            // テクスチャ解放
            CTextureManager.GetInstance( ).Unload( m_FilePath );
        }
        
        /// <summary>
        /// テクスチャの横幅
        /// </summary>
        /// <value>
        /// 横幅
        /// </value>
        public int Width
        {
            get { return ( m_Texture != null ? m_Texture.Width : 0 ); }
        }
        
        /// <summary>
        /// テクスチャの縦幅
        /// </summary>
        /// <value>
        /// 縦幅
        /// </value>
        public int Height
        {
            get { return ( m_Texture != null ? m_Texture.Height : 0 ); }
        }
        
        /// <summary>
        /// テクスチャの取得
        /// </summary>
        /// <value>
        /// テクスチャ
        /// </value>
        public Texture2D Texture
        {
            get { return m_Texture; }
        }
        
        /// <summary>
        /// テクスチャへのファイルパス取得
        /// </summary>
        /// <value>
        /// テクスチャへのファイルパス
        /// </value>
        public string FilePath
        {
            get { return m_FilePath; }
        }
        
        /// <summary>
        /// テクスチャ
        /// </summary>
        private Texture2D   m_Texture   = null;
        
        /// <summary>
        /// テクスチャへのパス
        /// </summary>
        private string      m_FilePath  = null;
    }
}

