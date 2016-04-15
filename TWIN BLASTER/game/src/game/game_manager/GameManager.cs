
/*!
 *  @file       GameManager.cs
 *  @brief      ゲーム管理
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Input;

namespace game
{
    /// <summary>
    /// ゲーム管理クラス
    /// </summary>
    public class CGameManager
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CGameManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CGameManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize( )
        {
			// ステージ
			CStage stage = new CStage();
			stage.Initialize();
			
			// グリッド
			CGrid grid = new CGrid();
			grid.Initialize();
			
			// ユニット
			CUnit.GetInstance().Initialize();
			
			// プレイヤー
			CPlayer1 player1 = new CPlayer1();
			CPlayer2 player2 = new CPlayer2();
			player1.Initialize();
			player2.Initialize();
			
			// UI
			CUI ui = new CUI( );
			ui.Initialize( );
			
			// エネミー
			CEnemyManager.GetInstance().Initialize();
			
			// ボス
//			CBossManager.GetInstance().Initialize();
			
			// ポーズ画面
			CPause.GetInstance( ).Initialize( );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public void Update( )
		{
			
			if( m_PauseFlag == true )
			{
				CPause.GetInstance( ).Update( );
				
				return;
			}
			
			if( aqua.CGamePad.Trigger( GamePadButtons.Start ) && m_PauseFlag == false )
			{
				m_PauseFlag = true;
				
				CPause.GetInstance( ).PauseStart( );
				
				// ソート
				CSpriteManager.Sort( );
			}
			
			if( m_BreakFlag == true )
			{
				if( aqua.CGamePad.Released( GamePadButtons.Left ) || aqua.CGamePad.Released( GamePadButtons.Circle ) )
				{
					return;
				}
			}
			else if( m_BreakFlag == false )
			{
				// 敵管理更新
				CEnemyManager.GetInstance( ).Update( );
				
				// ボス管理更新
//				CBossManager.GetInstance( ).Update( );
			}
			
			// ユニット更新
			CUnit.GetInstance().Update();
			
			if( m_ObjectList.Count <= 0 )
				return;
			
            for( int i = 0; i < m_ObjectList.Count; ++i )
            {
                // 更新
                m_ObjectList[i].Update( );
                
                // オブジェクトがアクティブでない
                if( m_ObjectList[i].Active == false )
                {
                    // 描画リストから削除
                    m_ObjectList[i].Dispose( );
                    
                    // オブジェクトリストから削除
                    RemoveObject( m_ObjectList[i--] );
                }
            }
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
			CPause.GetInstance( ).Dispose( );
			
            for( int i = 0; i < m_ObjectList.Count; ++i )
            {
                // オブジェクト毎に解放
                m_ObjectList[i].Dispose( );
            }
			
            // リストをクリアする
            m_ObjectList.Clear( );
        }
        
        /// <summary>
        /// オブジェクトの追加
        /// </summary>
        /// <param name='obj'>
        /// オブジェクト
        /// </param>
        public void AddObject( IObject obj )
        {
            if( m_ObjectList == null ) return;
            
            m_ObjectList.Add( obj );
        }
        
        /// <summary>
        /// オブジェクトの削除
        /// </summary>
        /// <param name='obj'>
        /// オブジェクト
        /// </param>
        public void RemoveObject( IObject obj )
        {
            if( m_ObjectList == null ) return;
            
            if( m_ObjectList.Count <= 0 ) return;
            
            m_ObjectList.Remove( obj );
        }
        
        /// <summary>
        /// オブジェクトリスト取得
        /// </summary>
        /// <value>
        /// オブジェクトリスト
        /// </value>
        public List<IObject> ObjectList
        {
            get{ return m_ObjectList; }
        }
		
		public IObject FindObject( OBJECT_ID id )
		{
			return m_ObjectList.Find( x => x.ObjectID == id );
		}
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CGameManager( )
        {
            // オブジェクトリスト生成
            m_ObjectList = new List<IObject>( );
        }
        
		/// <summary>
		/// ポーズ画面フラグアクセッサ
		/// </summary>
		/// <value>
		/// ポーズフラグ
		/// </value>
		public bool PauseFlag
		{
			get { return m_PauseFlag; }
			set { m_PauseFlag = value; }
		}
		
		/// <summary>
		/// 休憩フラグアクセッサ
		/// </summary>
		/// <value>
		/// 休憩フラグ
		/// </value>
		public bool BreakFlag
		{
			get { return m_BreakFlag; }
			set { m_BreakFlag = value; }
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CGameManager     m_Instance;
		
        /// <summary>
        /// オブジェクトリスト
        /// </summary>
        private List<IObject>           m_ObjectList;
		
		/// <summary>
		/// ポーズ画面フラグ
		/// </summary>
		private bool					m_PauseFlag = false;
		
		/// <summary>
		/// 休憩フラグ
		/// </summary>
		private bool					m_BreakFlag = false;
    }
}
