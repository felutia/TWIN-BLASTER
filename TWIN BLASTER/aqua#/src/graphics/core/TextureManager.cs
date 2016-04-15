
/*!
 *  @file       TextureManager.cs
 *  @brief      テクスチャ管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;

namespace aqua.core
{
    /// <summary>
    /// テクスチャデータクラス
    /// テクスチャ管理テーブルで使用
    /// </summary>
    public class CTextureData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CTextureData( )
        {
            texture     = null;
            reference   = 0;
        }
        
        /// <summary>
        /// テクスチャ
        /// </summary>
        public Texture2D    texture     = null;
        
        /// <summary>
        /// 参照カウンタ
        /// </summary>
        public int          reference   = 0;
    }
    
    /// <summary>
    /// テクスチャ管理クラス
    /// </summary>
    public sealed class CTextureManager
        : IDisposable
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CTextureManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CTextureManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CTextureManager( )
        {
            // テクスチャテーブル生成
            m_TextureTable = new Dictionary<string, CTextureData>( );
            
            // テーブルクリア
            m_TextureTable.Clear( );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
            // すべてのテクスチャ解放
            foreach( var tex in m_TextureTable )
                tex.Value.texture.Dispose( );
            
            // テーブルクリア
            m_TextureTable.Clear( );
        }
        
        /// <summary>
        /// テクスチャの検索と読み込み
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルパス
        /// </param>
        public Texture2D Load( string file_path )
        {
            return Load( file_path, false );
        }
        
        /// <summary>
        /// テクスチャの検索と読み込み
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルパス
        /// </param>
        /// <param name='embedded_file'>
        /// 組み込みファイルフラグ
        /// </param>
        public Texture2D Load( string file_path, bool embedded_file )
        {
            // テーブル内にあればテクスチャデータを返す
            if( m_TextureTable.ContainsKey( file_path ) == true )
            {
                // 参照カウンタ加算
                ++m_TextureTable[file_path].reference;
                
                return m_TextureTable[file_path].texture;
            }
            
            // テーブルにテクスチャがない
            // テクスチャデータ生成
            CTextureData data = new CTextureData( );
            
            try
            {
                // 組み込みファイル読み込み
                if( embedded_file == true )
                {
                    byte[] buffer = core.CEmbeddedFileLoader.Load( file_path );
                    
                    data.texture = new Texture2D( buffer, false );
                }
                else
                {
                    data.texture = new Texture2D( "/Application/" + file_path, false );
                }
            }
            catch( Exception e )
            {
                aqua.CDebug.Error( e.Message );
            }
            
            // 参照カウンタ加算
            ++data.reference;
            
            // テーブルに追加
            m_TextureTable.Add( file_path, data );
            
            return data.texture;
        }
        
        /// <summary>
        /// テクスチャ解放
        /// </summary>
        /// <param name='file_path'>
        /// テクスチャファイルパス
        /// </param>
        public void Unload( string file_path )
        {
            // テーブル内を検索
            if( m_TextureTable.ContainsKey( file_path ) == true )
            {
                // 参照カウンタ減算
                if( m_TextureTable[file_path].reference > 0 )
                    --m_TextureTable[file_path].reference;
                
                // 参照がなくなった
                if( m_TextureTable[file_path].reference <= 0 )
                {
                    // テクスチャ解放
                    m_TextureTable[file_path].texture.Dispose( );
                    
                    // テクスチャテーブルから削除
                    m_TextureTable.Remove( file_path );
                }
                
                return;
            }
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CTextureManager              m_Instance      = null;
        
        /// <summary>
        /// テクスチャテーブル
        /// </summary>
        internal Dictionary<string, CTextureData>   m_TextureTable  = null;
    }
}
