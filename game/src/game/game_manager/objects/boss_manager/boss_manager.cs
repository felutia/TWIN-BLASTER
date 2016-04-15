/*!
 *  @file       boss_manager.cs
 *  @brief      ボスマネージャー
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CBossManager
	{
		/// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CBossManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CBossManager( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CBossManager ()
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize ()
		{
			// ボス生成タイマー
			m_create_time = 30;

			m_CreateTimer = new aqua.CFrameTimer( (uint)m_create_time );
		}
		
		/// <summary>
		/// 更新
		/// </summary>/
		public void Update( )
		{
//			IObject Enemy = CGameManager.GetInstance( ).FindObject( OBJECT_ID.ENEMY );
//			
//			IBoss boss = (IBoss)CGameManager.GetInstance( ).FindObject( OBJECT_ID.BOSS );
//
//			CPlayer1 p1 = (CPlayer1)CGameManager.GetInstance( ).FindObject(OBJECT_ID.PLAYER1);
//			CPlayer2 p2 = (CPlayer2)CGameManager.GetInstance( ).FindObject(OBJECT_ID.PLAYER2);
//
//			if( p1 != null )
//			{
//				if( p1.P1State == CPlayer1.STATE.GAMEOVER )
//					return;
//			}
//			
//			if( p2 != null )
//			{
//				if( p2.P2State == CPlayer2.STATE.GAMEOVER )
//					return;
//			}
//			
//			if( Enemy == null && CEnemyManager.GetInstance( ).GetCountDown >= 3 )
//			{
//				CEnemyManager.GetInstance( ).GetCountDown = 0;
//				
//				// ボス生成タイマー更新
//				if( boss == null )
//				{
//					int Lv = CStageLv.GetInstance( ).StageLv;
//					
//					if( Lv <=2)
//					{
//					CreateBoss(BOSS_ID.BOSS_Lv0, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 10 * Lv ) ;
//		
//						
//					}
//					else if(Lv>=3&&Lv <=5)
//					{
//					CreateBoss(BOSS_ID.BOSS_Lv1, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 30 * Lv ) ;
//	
//					}
//						
//					else if(Lv>=6&&Lv <=8)
//					{	
//						CreateBoss(BOSS_ID.BOSS_Lv2, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 50 * Lv );
//					}
//					
//					else if(Lv>=9&&Lv <=10)
//					{
//						CreateBoss(BOSS_ID.BOSS_Lv3, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 100 * Lv );
//					}	
//					// 生成時間再設定
//					//m_CreateTimer = new aqua.CFrameTimer( (uint)m_create_time );
//					m_CreateTimer.Frame = 0;
//				}
//			}
		}
		
		/// <summary>
		/// 生成
		/// </summary>
		public bool  Create( BOSS_ID boss_id,　OBJECT_ID object_id, Vector2 pos, float speed, int power, int life )
		{
			IBoss boss = null;
			
			switch (boss_id)
			{
				case BOSS_ID.BOSS_Lv0:	boss = new CBossLv0( pos ,speed , life );		break;
				case BOSS_ID.BOSS_Lv1:	boss = new CBossLv1( pos, speed,  life );		break;
				case BOSS_ID.BOSS_Lv2:	boss = new CBossLv2( pos, speed,  life );		break;
				case BOSS_ID.BOSS_Lv3:	boss = new CBossLv3( pos, speed,  life );		break;
			}
			
			if(boss == null)
				return	false;
			
			// 初期化処理
			boss.Initialize( );
			
			// オブジェクトID設定
			boss.ObjectID = object_id;
			
			return true;
		}
		
		
		public void CreateBS()
		{
			int Lv = CStageLv.GetInstance( ).StageLv;
			
			if( Lv <=2)
			{
			CreateBoss(BOSS_ID.BOSS_Lv0, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 100 * Lv ) ;

				
			}
			else if(Lv>=3&&Lv <=5)
			{
			CreateBoss(BOSS_ID.BOSS_Lv1, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 200 * Lv ) ;

			}
				
			else if(Lv>=6&&Lv <=8)
			{	
				CreateBoss(BOSS_ID.BOSS_Lv2, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 300 * Lv );
			}
			
			else if(Lv>=9&&Lv <=10)
			{
				CreateBoss(BOSS_ID.BOSS_Lv3, new Vector2( 1000.0f , 270.0f ), 3.0f, 1, 300 * Lv );
			}
		}
		
		
		/// <summary>
		/// ボス生成
		/// </summary>
		/// <param name='id'>
		/// Identifier.
		/// </param>
		/// <param name='pos'>
		/// Position.
		/// </param>
		/// <param name='speed'>
		/// Speed.
		/// </param>
		/// <param name='power'>
		/// Power.
		/// </param>
		/// <param name='life'>
		/// Life.
		/// </param>
		public void CreateBoss( BOSS_ID id, Vector2 pos, float speed, int power, int life )
		{
			Create( id, OBJECT_ID.BOSS, pos, speed, power, life );
		}
		
		/// <summary>
		/// 生成時間設定
		/// </summary>
		/// <value>
		/// 生成時間
		/// </value>
		public int CreateTime
		{
			set{ m_create_time = value; }
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CBossManager  	m_Instance;
		
		/// <summary>
		/// ボス生成間隔設定
		/// </summary>
		private aqua.CFrameTimer		m_CreateTimer;
		
		/// <summary>
		/// 生成時間
		/// </summary>
		private int						m_create_time;
		
		
		/// <summary>
		/// 仮
		/// </summary>
		
	}
	
	
}

