
/*!
 *  @file       DrawObject3DManager.cs
 *  @brief      3D描画用オブジェクト管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/06
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;

namespace aqua.core
{
    /// <summary>
    /// 3D描画オブジェクト管理クラス
    /// </summary>
    public sealed class CDrawObject3DManager
        : IDisposable
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CDrawObject3DManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CDrawObject3DManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
            Clear( );
        }
        
        /// <summary>
        /// 描画オブジェクトの追加
        /// </summary>
        /// <param name='obj'>
        /// 描画オブジェクト
        /// </param>
        public void Add( IDrawObject3D obj )
        {
            try
            {
                m_DrawObjectList.Add( obj );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
        }
        
        /// <summary>
        /// 描画オブジェクトの削除
        /// </summary>
        /// <param name='obj'>
        /// 描画オブジェクト
        /// </param>
        public void Remove( IDrawObject3D obj )
        {
            try
            {
                m_DrawObjectList.Remove( obj );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
        }
        
        /// <summary>
        /// オブジェクトリストのクリア
        /// </summary>
        public void Clear( )
        {
            m_DrawObjectList.Clear( );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public void Update( )
        {
            foreach( var obj in m_DrawObjectList )
                obj.Update( );
        }
        
        /// <summary>
        /// 描画
        /// </summary>
        public void Draw( )
        {
            foreach( var obj in m_DrawObjectList )
                obj.Draw( );
			
			// 
			Clear( );
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CDrawObject3DManager( )
        {
            m_DrawObjectList = new List<IDrawObject3D>( );
            
            m_DrawObjectList.Clear( );
        }
        
        /// <summary>
        /// プライオリティでソート
        /// </summary>
        private void Sort( )
        {
            if( m_DrawObjectList.Count <= 1 ) return;
            
            // プライオリティから描画順のソート
            m_DrawObjectList.Sort( delegate( IDrawObject3D a, IDrawObject3D b ) { return a.Priority.CompareTo( b.Priority ); } );
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CDrawObject3DManager     m_Instance = null;
        
        /// <summary>
        /// 描画オブジェクトリスト
        /// </summary>
        private List<IDrawObject3D>             m_DrawObjectList;
    }
}
