/*!
 *  @file       enemy_manager.cs
 *  @brief      エネミーマネージャー
 *  @author     Riki Ito, Itiki Amano
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CEnemyManager
	{
		/// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CEnemyManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CEnemyManager( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CEnemyManager( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize( )
		{
			// UIスペース
			Ui_zone = 128.0f;
			
			// カウントダウン
			CountDown = 0;
			
			// エネミー体力
			enemyHP   = 3;
			
			// エネミー体力上昇率初期値
			m_Rizelate = 1.0f;
			
			// 敵生成タイマー
			m_CreateTimer = new aqua.CFrameTimer( 90 );
			
			// 初期化
			m_CreatePattern = CREATE_ID.CREATE_DUMMY;
            
            m_stageLv = CStageLv.GetInstance( ).StageLv;
		}
		
		/// <summary>
		/// 更新
		/// </summary>/
		public void Update( )
		{
			m_stageLv = CStageLv.GetInstance( ).StageLv;
			
			if( m_stageLv >= 1 && m_stageLv <= 2 )
			{
				m_Rizelate = 1;
			}
		
			if( m_stageLv >= 3 && m_stageLv <= 5 )
			{
				m_Rizelate = 2;
			}
			
			if( m_stageLv >= 6 && m_stageLv <= 8 )
			{
				m_Rizelate = 4;
			}
		
			if( m_stageLv >= 9 && m_stageLv <= 10 )
			{
				m_Rizelate = 8;
			}
			
			if( m_stageLv > 10 )
				return;
			
			
			IObject Boss = CGameManager.GetInstance( ).FindObject(OBJECT_ID.BOSS);
			IObject Enemy = CGameManager.GetInstance( ).FindObject(OBJECT_ID.ENEMY);
			ILogo logo = (ILogo)CGameManager.GetInstance( ).FindObject(OBJECT_ID.LOGO);
			
//				if( CountDown != 3 )
//					m_CreateTimer.Update( );
//				
//				if( m_CreateTimer.IsEnd( ) == true )
//				{ 
//					m_CreateTimer = new aqua.CFrameTimer( 180 );
//					CountDown++;
//					
//					//クリエイトパターン(乱数)
//					Random rand = new Random( );
//					m_CreatePattern = (CREATE_ID)rand.Next( (int)CREATE_ID.CREATE_6 ) + 1;
//					
//					
//					if( CountDown == 3 && Enemy == null )
//					{
//						CBossManager.GetInstance( ).CreateBS( );
//						
//						m_CreateTimer = new aqua.CFrameTimer( 30 );
//						
//						CountDown = 0;
//					}
//				}
			
			//デバック用(ボス生成)
			//if( Boss == null )
			//	CBossManager.GetInstance( ).CreateBoss(BOSS_ID.BOSS_Lv1, new Vector2( 1000.0f , 330.0f ), 3.0f, 1, 5000 );
			
			if( Boss == null )
			{
				if( Enemy == null )
				{
					if( logo == null )
					{
						m_CreateTimer.Update( );
					
						if( CountDown == 3 )
						{
							CountDown = 0;
							
							CBossManager.GetInstance( ).CreateBS( );
							
							m_CreateTimer = new aqua.CFrameTimer( 120 );
						}
						
						if( m_CreateTimer.IsEnd( ) )
						{
							m_CreateTimer = new aqua.CFrameTimer( 150 );
							
							// デバック用(敵生成)
							//m_CreatePattern = CREATE_ID.CREATE_1;
							
							//　クリエイトパターン(乱数)
							Random rand = new Random( );
							m_CreatePattern = (CREATE_ID)rand.Next( (int)CREATE_ID.CREATE_5 ) + 1;
							
							CountDown++;
						}
					}
				}
			}
			
			CPlayer1 p1 = (CPlayer1)CGameManager.GetInstance( ).FindObject(OBJECT_ID.PLAYER1);
			CPlayer2 p2 = (CPlayer2)CGameManager.GetInstance( ).FindObject(OBJECT_ID.PLAYER2);
			
			if( p1 != null )
			{
				if( p1.P1State == CPlayer1.STATE.GAMEOVER )
					return;
			}
			
			if( p2 != null )
			{
				if( p2.P2State == CPlayer2.STATE.GAMEOVER )
					return;
			}
			
			switch( m_CreatePattern )
			{
				case CREATE_ID.CREATE_1: Create_Pattern1( ); break;
				case CREATE_ID.CREATE_2: Create_Pattern2( ); break;
				case CREATE_ID.CREATE_3: Create_Pattern3( ); break;
				case CREATE_ID.CREATE_4: Create_Pattern4( ); break;
				case CREATE_ID.CREATE_5: Create_Pattern5( ); break;
				case CREATE_ID.CREATE_6: Create_Pattern6( ); break;
			}
		}
		
		public void Create_Pattern1( )
		{
			for( int i = 1; i <= 3; ++i )
			{
				CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 700 + (80 - 30) * i, Ui_zone + 32 ),       3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_DOWN , OUTSTAGE_ID.OUTSTAGE_DOWN );
				CreateEnemy( ENEMY_ID.ENEMY_RED,  new Vector2( 800 + (80 - 30) * i, 544.0f / 2.0f + 32 ), 3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_FRONT );
				CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 700 + (80 - 30) * i, 544.0f - 32 ),        3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_UP   , OUTSTAGE_ID.OUTSTAGE_UP );
			}
			m_CreatePattern = CREATE_ID.CREATE_DUMMY;
		}
		
		public void Create_Pattern2( )
		{
				for( int i = -1; i <= 0; ++i)
				{
					CreateEnemy( ENEMY_ID.ENEMY_RED, new Vector2( 900, ((544 + Ui_zone) / 2) + (80 + 20) * i ), 3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_BACK );
					CreateEnemy( ENEMY_ID.ENEMY_RED, new Vector2( 700, ((544 + Ui_zone) / 2) + (80 + 20) * i ), 3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_BACK );
				}
				
				for( int i = -1; i <= 1; ++i)
				{
					CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 800, ((544 + Ui_zone) / 2) + 80 * i - 32 ), 3.0f, 1, enemyHP , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_BACK );
				}
			
				m_CreatePattern = CREATE_ID.CREATE_DUMMY;
		}
		
		public void Create_Pattern3( )
		{
				for( int i = 1; i <= 2; ++i  )
				{
					CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 800 + 80 * i,((544 + Ui_zone) / 2 + 32) - 100 ),        3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_UP );
					CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 800 + 80 * i,((544 + Ui_zone) / 2 - 32) + 100 ),        3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_DOWN );
					CreateEnemy( ENEMY_ID.ENEMY_RED , new Vector2( 800 + 120,   ((544 + Ui_zone) / 2 + 32) - 80 * i + 20 ),3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_UP );
					CreateEnemy( ENEMY_ID.ENEMY_RED , new Vector2( 800 + 120,   ((544 + Ui_zone) / 2 - 32) + 80 * i - 20 ),3.0f, 1, (int)(enemyHP * m_Rizelate) , INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_DOWN );
				}
			
				CreateEnemy( ENEMY_ID.ENEMY_BLUE_LV2, new Vector2( 800 , 544 / 2 + 32 ), 3.0f, 1, (int)((enemyHP + 4) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_CICLE );
			
				m_CreatePattern = CREATE_ID.CREATE_DUMMY;
		}
		
		public void Create_Pattern4( )
		{
				for( int i = -1; i <= 1; i++ )
				{
					for( int k = -1; k <= 0; ++k )
					{
						CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 700 + 100 * k, ((544 + Ui_zone) / 2 + 64 ) - 80 * i), 10.0f, 1, (int)(enemyHP * m_Rizelate), INSTAGE_ID.INSTAGE_CICLE, OUTSTAGE_ID.OUTSTAGE_CICLE );
					}
				}
			
				CreateEnemy( ENEMY_ID.ENEMY_RED_LV2, new Vector2( 630, (544 + Ui_zone) / 2 + 50 ) , 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_CICLE, OUTSTAGE_ID.OUTSTAGE_CICLE);
			
				m_CreatePattern = CREATE_ID.CREATE_DUMMY;
		}
		
		public void Create_Pattern5( )
		{
				CreateEnemy( ENEMY_ID.ENEMY_RED_LV2,  new Vector2( 800.0f, ( 544 + Ui_zone - 50 )  / 2 - 64  ), 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_FRONT);
				CreateEnemy( ENEMY_ID.ENEMY_RED_LV2,  new Vector2( 700.0f, ( 544 + Ui_zone - 50 )  / 2 + 128 ), 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_FRONT);
				CreateEnemy( ENEMY_ID.ENEMY_BLUE_LV2, new Vector2( 800.0f, ( 544 + Ui_zone - 50 )  / 2 + 64  ), 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_FRONT);
				CreateEnemy( ENEMY_ID.ENEMY_BLUE_LV2, new Vector2( 700.0f, ( 544 + Ui_zone - 50 )  / 2 - 128 ), 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_FRONT);
			
				m_CreatePattern = CREATE_ID.CREATE_DUMMY;
		}
		
		public void Create_Pattern6( )
		{
				for( int i = -1; i <= 1; i++ )
				{
					CreateEnemy( ENEMY_ID.ENEMY_BLUE, new Vector2( 700 + 100 * i, Ui_zone + 100 - 50 * i), 10.0f, 1, (int)(enemyHP * m_Rizelate), INSTAGE_ID.INSTAGE_DOWN, OUTSTAGE_ID.OUTSTAGE_BACK );
				}
			
				for( int i = -1; i <= 1; i++ )
				{
					CreateEnemy( ENEMY_ID.ENEMY_RED, new Vector2( 700 + 100 * i, 544 - 150 + 50 * i), 10.0f, 1, (int)(enemyHP * m_Rizelate), INSTAGE_ID.INSTAGE_UP, OUTSTAGE_ID.OUTSTAGE_BACK );
				}
			
			CreateEnemy( ENEMY_ID.ENEMY_BLUE_LV2, new Vector2( 700, ( 544 + Ui_zone) / 2 - 40 ), 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_BACK );
			
			CreateEnemy( ENEMY_ID.ENEMY_RED_LV2, new Vector2( 1000, ( 544 + Ui_zone) / 2 - 40 ), 10.0f, 1, (int)( (4 + enemyHP) * m_Rizelate), INSTAGE_ID.INSTAGE_FRONT, OUTSTAGE_ID.OUTSTAGE_BACK );

			
			m_CreatePattern = CREATE_ID.CREATE_DUMMY;
		}
		
		
		/// <summary>
		/// 生成
		/// </summary>
		public void Create( ENEMY_ID enemy_id,　OBJECT_ID object_id, Vector2 pos, float speed, int power, int life, INSTAGE_ID instage_id, OUTSTAGE_ID outstage_id )
		{
			IEnemy enemy = null;
			
			switch (enemy_id)
			{
				case ENEMY_ID.ENEMY_RED:			enemy = new CEnemyRed(     pos, speed, power, life, instage_id, outstage_id );	break;
				case ENEMY_ID.ENEMY_BLUE:			enemy = new CEnemyBlue(    pos, speed, power, life, instage_id, outstage_id );	break;
				case ENEMY_ID.ENEMY_RED_LV2: 		enemy = new CEnemyRed_Lv2( pos, speed, power, life, instage_id, outstage_id );	break;
				case ENEMY_ID.ENEMY_BLUE_LV2: 		enemy = new CEnemyRed_Lv2( pos, speed, power, life, instage_id, outstage_id );	break;
			}
			
			if(enemy == null)
				return;
			
			// 初期化処理
			enemy.Initialize();
			
			// オブジェクトID設定
			enemy.ObjectID = object_id;
		}
		
		/// <summary>
		/// 敵生成
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
		private void CreateEnemy( ENEMY_ID id, Vector2 pos, float speed, int power, int life, INSTAGE_ID instage_id, OUTSTAGE_ID outstage_id )
		{
			Create( id, OBJECT_ID.ENEMY, pos, speed, power, life, instage_id, outstage_id );
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CEnemyManager  	m_Instance;
		
		/// <summary>
		/// 敵生成間隔設定
		/// </summary>
		protected aqua.CFrameTimer		m_CreateTimer;
		
		/// <summary>
		/// 行番号リセットタイマー
		/// </summary>
		protected aqua.CFrameTimer		m_AgainTimer;
		
		/// <summary>
		/// 生成パターンID
		/// </summary>
		private CREATE_ID				m_CreatePattern;
		
		/// <summary>
		/// 上昇率
		/// </summary>
		private float					m_Rizelate;
		
		/// <summary>
		/// ボス生成までのカウント
		/// </summary>
		private int 					CountDown;
		
		/// <summary>
		/// カウント数取得
		/// </summary>
		public int GetCountDown
		{
			get { return CountDown; }
			set { CountDown = value; }
		}
		
		/// <summary>
		/// 敵基礎体力
		/// </summary>
		private int						enemyHP;
		
		/// <summary>
		/// UI表示スペース
		/// </summary>
		private float Ui_zone;
		
		private int m_stageLv;
	}
}

